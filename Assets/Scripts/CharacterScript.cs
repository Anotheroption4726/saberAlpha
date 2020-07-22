using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    //  Movement
    public int speed = 25;
    private Rigidbody2D physics;

    //  Animations
    [SerializeField] private Animator animator;
    private string[] animationNamesTable = new string[]{"Idle", "Run"};

    private void Awake()
    {
        physics = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            SetAnimation("Run");
            physics.velocity = new Vector3(speed, 0, 0);
        }

        if (Input.GetKeyUp("space"))
        {
            SetAnimation("Idle");
            physics.velocity = new Vector3(0, 0, 0);
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
