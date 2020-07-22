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

    private void FixedUpdate()
    {
        //rigidbody.AddRelativeForce(Vector3.forward * 10);
    }

    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            SetAnimation("Run");
            rigidbody.velocity = new Vector3(10, 0, 0);
        }

        if (Input.GetKeyUp("space"))
        {
            SetAnimation("Idle");
            rigidbody.velocity = new Vector3(0, 0, 0);
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
