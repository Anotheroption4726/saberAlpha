using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    [SerializeField] private Animator animator;
    private string[] animationNamesTable = new string[]{"Idle", "Run"};

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            SetAnimation("Run");
            //rigidbody.AddForce(Vector2.right * 100);
            rigidbody.velocity = new Vector3(10, 0, 0);
        }

        if (Input.GetKeyUp("space"))
        {
            SetAnimation("Idle");
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
}
