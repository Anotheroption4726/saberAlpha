using System.Collections;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    //  Movement
    private int speed = 25;
    private float slideTime = 0.13f;
    private float jump = 8f;
    private Rigidbody2D rigidBody;
    [SerializeField] private GroundCheckerScript groundChecker;

    //  Animations
    private CharaAnimStateEnum animState = CharaAnimStateEnum.Idle;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Animator animator;
    private string[] animationNamesTable = new string[]{"Idle", "Run", "Slide", "Jump"};

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetAxisRaw("Horizontal") > 0)
        {
            SetAnimation("Run", CharaAnimStateEnum.Run);
            sprite.flipX = false;
            rigidBody.velocity = new Vector2(speed, rigidBody.velocity.y);
        }

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetAxisRaw("Horizontal") < 0)
        {
            SetAnimation("Run", CharaAnimStateEnum.Run);
            sprite.flipX = true;
            rigidBody.velocity = new Vector2(-speed, rigidBody.velocity.y);
        }

        if (!Input.anyKey && Input.GetAxisRaw("Horizontal") == 0 && animState.Equals(CharaAnimStateEnum.Run))
        {
            SetAnimation("Slide", CharaAnimStateEnum.Slide);
            StartCoroutine("StopSlide");
        }

        if (Input.GetKey(KeyCode.Space) && groundChecker.GetIsGrounded())
        {
            SetAnimation("Jump", CharaAnimStateEnum.Jump);
            //rigidBody.AddForce(new Vector2(0f, jumpValue), ForceMode2D.Impulse);
            //rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpValue);
            rigidBody.velocity = Vector2.up * jump;
        }

        /*
        if (groundChecker.GetIsGrounded())
        {
            SetAnimation("Idle", CharaAnimStateEnum.Idle);
        }
        */
    }

    private IEnumerator StopSlide()
    {
        yield return new WaitForSeconds(slideTime);
        SetAnimation("Idle", CharaAnimStateEnum.Idle);
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
