using System.Collections;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    //  Movement
    private int speed = 25;
    private float slideTime = 0.13f;
    private Rigidbody2D physics;
    private bool isStopSliding = false;

    //  Animations
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Animator animator;
    private string[] animationNamesTable = new string[]{"Idle", "Run", "Slide"};

    private void Awake()
    {
        physics = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (!isStopSliding)
        {
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetAxisRaw("Horizontal") > 0)
            {
                sprite.flipX = false;
                SetAnimation("Run");
                physics.velocity = new Vector3(speed, 0, 0);
            }

            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetAxisRaw("Horizontal") < 0)
            {
                sprite.flipX = true;
                SetAnimation("Run");
                physics.velocity = new Vector3(-speed, 0, 0);
            }

            if (!Input.anyKey && Input.GetAxisRaw("Horizontal") == 0)
            {
                SetAnimation("Slide");
                StartCoroutine("StopSlide");
                isStopSliding = true;
            }
        }
    }

    private void SetAnimation(string arg_animationName)
    {
        foreach (string lp_animation in animationNamesTable)
        {
            if (arg_animationName != lp_animation)
            {
                animator.SetBool(lp_animation, false);
            }
        }

        animator.SetBool(arg_animationName, true);
    }

    private IEnumerator StopSlide()
    {
        yield return new WaitForSeconds(slideTime);
        isStopSliding = false;
        SetAnimation("Idle");
    }
}
