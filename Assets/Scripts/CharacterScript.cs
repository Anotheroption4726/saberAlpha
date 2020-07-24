using System.Collections;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    //  Movement
    private int speed = 25;
    private float slideTime = 0.13f;
    private Rigidbody2D rigidBody;
    private CharaAnimStateEnum animState = CharaAnimStateEnum.Idle;

    //  Animations
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


        Debug.DrawRay(transform.position, -Vector2.up * 3, Color.red);
        RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, -Vector2.up * 3);
        //RaycastHit2D raycastHit;
        //Physics.Raycast(transform.position, -Vector2.up, 3, LayerMask.NameToLayer("Ground"));

        /*
        if (Physics.Raycast(transform.position, -Vector2.up * 3, Mathf.Infinity, 1 << 8))
        {
            Debug.Log("Touching ground");
        }
        */

        if (raycastHit.collider != null)
        {
            //Debug.Log("Raycast On");

            if (raycastHit.collider.gameObject.name != "Character")
            {
                Debug.Log("Touching ground");
            }
        }

        if (Input.GetKey(KeyCode.Space) && !animState.Equals(CharaAnimStateEnum.Jump))
        {
            SetAnimation("Jump", CharaAnimStateEnum.Jump);
            //rigidBody.AddForce(new Vector2(0f, 15f), ForceMode2D.Impulse);
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, 6);
        }

        NormalizeRigidBodyVelocity();
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

    private void NormalizeRigidBodyVelocity()
    {
        if (rigidBody.velocity.x > 25)
        {
            rigidBody.velocity = new Vector2(25, rigidBody.velocity.y);
        }

        if (rigidBody.velocity.x < -25)
        {
            rigidBody.velocity = new Vector2(-25, rigidBody.velocity.y);
        }

        if (rigidBody.velocity.y > 25)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, 25);
        }

        if (rigidBody.velocity.y < -25)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, -25);
        }
    }
}
