using System.Collections;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    //  Movement
    private int groundSpeed = 25;
    private int airSpeed = 10;
    private float slideTime = 0.13f;
    private float jumpDistance = 8f;
    private float fallNormalTimer = 0.5f;
    private float fallForwardTimer = 0.75f;
    private Rigidbody2D rigidBody;
    [SerializeField] private GroundCheckerScript groundChecker;

    //  Animations
    private CharaAnimStateEnum animState = CharaAnimStateEnum.Idle;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Animator animator;
    private string[] animationNamesTable = new string[]{"Idle", "Run", "Slide", "Jump", "Jump_forward", "Fall_normal", "Fall_forward"};

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        //
        // Idle Actions & Events
        //
        if (animState.Equals(CharaAnimStateEnum.Idle))
        {
            //  Run
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetAxisRaw("Horizontal") < 0 || Input.GetAxisRaw("Horizontal") > 0)
            {
                SetAnimation("Run", CharaAnimStateEnum.Run);
            }

            //  Jump
            if (Input.GetKey(KeyCode.Space) && groundChecker.GetIsGrounded())
            {
                SetAnimation("Jump", CharaAnimStateEnum.Jump);
                rigidBody.velocity = Vector2.up * jumpDistance;
                StartCoroutine("FallNormal");
            }
        }


        //
        // Run Actions & Events
        //
        if (animState.Equals(CharaAnimStateEnum.Run))
        {
            //  Run Right
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetAxisRaw("Horizontal") > 0)
            {
                sprite.flipX = false;
                rigidBody.velocity = new Vector2(groundSpeed, rigidBody.velocity.y);
            }

            //  Run Left
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetAxisRaw("Horizontal") < 0)
            {
                sprite.flipX = true;
                rigidBody.velocity = new Vector2(-groundSpeed, rigidBody.velocity.y);
            }

            //  Slide
            if (!Input.anyKey && Input.GetAxisRaw("Horizontal") == 0)
            {
                SetAnimation("Slide", CharaAnimStateEnum.Slide);
                StartCoroutine("StopSlide");
            }

            //  Jump Forward
            if (Input.GetKey(KeyCode.Space) && groundChecker.GetIsGrounded())
            {
                SetAnimation("Jump_forward", CharaAnimStateEnum.Jump_forward);
                rigidBody.velocity = Vector2.up * jumpDistance;
                StartCoroutine("FallForward");
            }
        }


        //
        // Slide Actions & Events
        //
        if (animState.Equals(CharaAnimStateEnum.Slide))
        {
            //rigidBody.velocity = new Vector2(groundSpeed, rigidBody.velocity.y);
        }


        //
        // Jump actions & Events
        //
        if (animState.Equals(CharaAnimStateEnum.Jump))
        {
            //  Air move Right
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetAxisRaw("Horizontal") > 0)
            {
                sprite.flipX = false;
                rigidBody.velocity = new Vector2(airSpeed, rigidBody.velocity.y);
            }

            //  Air move Left
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetAxisRaw("Horizontal") < 0)
            {
                sprite.flipX = true;
                rigidBody.velocity = new Vector2(-airSpeed, rigidBody.velocity.y);
            }
        }


        //
        //  Jump forward actions & Events
        //
        if (animState.Equals(CharaAnimStateEnum.Jump_forward))
        {
            //  Jump move Right
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetAxisRaw("Horizontal") > 0)
            {
                sprite.flipX = false;
                rigidBody.velocity = new Vector2(groundSpeed, rigidBody.velocity.y);
            }

            //  Jump move Left
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetAxisRaw("Horizontal") < 0)
            {
                sprite.flipX = true;
                rigidBody.velocity = new Vector2(-groundSpeed, rigidBody.velocity.y);
            }
        }


        //
        // Fall normal actions & Events
        //
        if (animState.Equals(CharaAnimStateEnum.Fall_normal))
        {
            //  Fall Right
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetAxisRaw("Horizontal") > 0)
            {
                sprite.flipX = false;
                rigidBody.velocity = new Vector2(airSpeed, rigidBody.velocity.y);
            }

            //  Fall Left
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetAxisRaw("Horizontal") < 0)
            {
                sprite.flipX = true;
                rigidBody.velocity = new Vector2(-airSpeed, rigidBody.velocity.y);
            }

            //  Touch Ground
            if (groundChecker.GetIsGrounded())
            {
                SetAnimation("Idle", CharaAnimStateEnum.Idle);
            }
        }


        //
        // Fall forward actions & Events
        //
        if (animState.Equals(CharaAnimStateEnum.Fall_forward))
        {
            bool loc_directionChange = false;

            //  Switch Direction
            if (!loc_directionChange)
            {
                if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetAxisRaw("Horizontal") < 0 || Input.GetAxisRaw("Horizontal") > 0)
                {
                    loc_directionChange = true;
                }
            }

            //  Fall Right & Left
            if (loc_directionChange)
            {
                if (Input.GetKey(KeyCode.RightArrow) || Input.GetAxisRaw("Horizontal") > 0)
                {
                    sprite.flipX = false;
                    rigidBody.velocity = new Vector2(airSpeed, rigidBody.velocity.y);
                }

                if (Input.GetKey(KeyCode.LeftArrow) || Input.GetAxisRaw("Horizontal") < 0)
                {
                    sprite.flipX = true;
                    rigidBody.velocity = new Vector2(-airSpeed, rigidBody.velocity.y);
                }
            }

            //  Touch Ground
            if (groundChecker.GetIsGrounded())
            {
                SetAnimation("Slide", CharaAnimStateEnum.Slide);
                StartCoroutine("StopSlide");
            }
        }
    }

    private IEnumerator StopSlide()
    {
        yield return new WaitForSeconds(slideTime);
        SetAnimation("Idle", CharaAnimStateEnum.Idle);
    }

    private IEnumerator FallNormal()
    {
        yield return new WaitForSeconds(fallNormalTimer);
        SetAnimation("Fall_normal", CharaAnimStateEnum.Fall_normal);
    }

    private IEnumerator FallForward()
    {
        yield return new WaitForSeconds(fallForwardTimer);
        SetAnimation("Fall_forward", CharaAnimStateEnum.Fall_forward);
    }

    private void SetAnimation(string arg_animationName, CharaAnimStateEnum arg_charaAnimStateEnum)
    {
        foreach (string lp_animation in animationNamesTable)
        {
            if (arg_animationName != lp_animation)
            {
                animator.SetBool(lp_animation, false);
            }
        }

        animator.SetBool(arg_animationName, true);
        animState = arg_charaAnimStateEnum;
    }
}
