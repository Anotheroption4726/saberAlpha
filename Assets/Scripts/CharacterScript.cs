using System.Collections;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    //  Movement
    private int speed = 25;
    private float slideTime = 0.13f;
    private Rigidbody2D physics;
    private CharaAnimStateEnum animState = CharaAnimStateEnum.Idle;

    //  Animations
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Animator animator;
    private string[] animationNamesTable = new string[]{"Idle", "Run", "Slide", "Jump"};

    private void Awake()
    {
        physics = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetAxisRaw("Horizontal") > 0)
        {
            SetAnimation("Run", CharaAnimStateEnum.Run);
            sprite.flipX = false;
            physics.velocity = new Vector3(speed, 0, 0);
        }

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetAxisRaw("Horizontal") < 0)
        {
            SetAnimation("Run", CharaAnimStateEnum.Run);
            sprite.flipX = true;
            physics.velocity = new Vector3(-speed, 0, 0);
        }

        if (animState.Equals(CharaAnimStateEnum.Run))
        {
            if (!Input.anyKey && Input.GetAxisRaw("Horizontal") == 0)
            {
                SetAnimation("Slide", CharaAnimStateEnum.Slide);
                StartCoroutine("StopSlide");
            }
        }

        if (Input.GetKey(KeyCode.Space))
        {
            SetAnimation("Jump", CharaAnimStateEnum.Jump);
        }
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

    private IEnumerator StopSlide()
    {
        yield return new WaitForSeconds(slideTime);
        SetAnimation("Idle", CharaAnimStateEnum.Idle);
    }
}
