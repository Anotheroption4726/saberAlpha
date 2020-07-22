using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private string[] animationNamesTable = new string[]{"Idle", "Run"};

    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            SetAnimation("Run");
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
