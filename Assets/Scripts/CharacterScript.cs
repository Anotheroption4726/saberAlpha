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
    private Rigidbody2D rigidBody;
    [SerializeField] private GroundCheckerScript groundChecker;

    //  Animations
    private CharaAnimStateEnum animState = CharaAnimStateEnum.Idle;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Animator animator;
    private string[] animationNamesTable = new string[]{"Idle", "Run", "Slide", "Jump", "Fall_normal"};

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        //  Move Right
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetAxisRaw("Horizontal") > 0)
        {
            sprite.flipX = false;
            int loc_speed = 0;

            if (groundChecker.GetIsGrounded())
            {
                SetAnimation("Run", CharaAnimStateEnum.Run);
                loc_speed = groundSpeed;
            }

            if (!groundChecker.GetIsGrounded())
            {
                loc_speed = airSpeed;
            }

            rigidBody.velocity = new Vector2(loc_speed, rigidBody.velocity.y);
        }

        //  Move Left
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetAxisRaw("Horizontal") < 0)
        {
            sprite.flipX = true;
            int loc_speed = 0;

            if (groundChecker.GetIsGrounded())
            {
                SetAnimation("Run", CharaAnimStateEnum.Run);
                loc_speed = groundSpeed;
            }

            if (!groundChecker.GetIsGrounded())
            {
                loc_speed = airSpeed;
            }

            rigidBody.velocity = new Vector2(-loc_speed, rigidBody.velocity.y);
        }

        //  Slide after Run
        if (!Input.anyKey && Input.GetAxisRaw("Horizontal") == 0 && animState.Equals(CharaAnimStateEnum.Run))
        {
            SetAnimation("Slide", CharaAnimStateEnum.Slide);
            StartCoroutine("StopSlide");
        }

        //  Jump
        if (Input.GetKey(KeyCode.Space) && groundChecker.GetIsGrounded())
        {
            SetAnimation("Jump", CharaAnimStateEnum.Jump);
            rigidBody.velocity = Vector2.up * jumpDistance;
            StartCoroutine("FallNormal");
        }

        //  Fall Normal
        if (animState.Equals(CharaAnimStateEnum.Fall_normal) && groundChecker.GetIsGrounded())
        {
            SetAnimation("Idle", CharaAnimStateEnum.Idle);
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
