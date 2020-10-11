﻿using System.Collections;
using UnityEngine;

public class CharacterAnimManagerScript : MonoBehaviour
{
    [SerializeField] CharacterScript characterScript;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject bullet;
    private CharacterAnimStateEnum animState = CharacterAnimStateEnum.Chara_Idle;

    public CharacterAnimStateEnum GetAnimState()
    {
        return animState;
    }

    public void SetAnimState(CharacterAnimStateEnum arg_animState)
    {
        animState = arg_animState;
    }

    //  Animation functions
    public void SetAnimation(CharacterAnimStateEnum arg_charaAnimStateEnum)
    {
        if (animState == arg_charaAnimStateEnum)
        {
            return;
        }

        animator.Play(arg_charaAnimStateEnum.ToString());
        animState = arg_charaAnimStateEnum;
    }

    // Animation Frames triggers
    private void OnTheGround()
    {
        SetAnimation(CharacterAnimStateEnum.Chara_Ontheground);
        StartCoroutine(EndAnimationCoroutine(characterScript.GetCharacter().GetOnTheGroundDuration(), CharacterAnimStateEnum.Chara_Ontheground_standup));
    }

    private void OnTheGroundStandUp()
    {
        SetAnimation(CharacterAnimStateEnum.Chara_Idle);
    }

    private void Shoot_SpawnBullet()
    {
        var loc_bullet = Instantiate(bullet, characterScript.GetBulletSpawnPoint_horizontal().position, Quaternion.LookRotation(characterScript.GetDirectionInt() * transform.forward));
        loc_bullet.GetComponent<BulletScript>().SetDirectionInt(characterScript.GetDirectionInt());
    }

    // Coroutines
    public IEnumerator EndAnimationCoroutine(float arg_time, CharacterAnimStateEnum arg_animState)
    {
        if (
                    animState.Equals(CharacterAnimStateEnum.Chara_Melee_jump) ||
                    animState.Equals(CharacterAnimStateEnum.Chara_Melee_jump_up) ||
                    animState.Equals(CharacterAnimStateEnum.Chara_Melee_jump_up_diagonal) ||
                    animState.Equals(CharacterAnimStateEnum.Chara_Melee_jump_down) ||
                    animState.Equals(CharacterAnimStateEnum.Chara_Melee_jump_down_diagonal) ||
                    animState.Equals(CharacterAnimStateEnum.Chara_Shoot_jump) ||
                    animState.Equals(CharacterAnimStateEnum.Chara_Shoot_jump_up) ||
                    animState.Equals(CharacterAnimStateEnum.Chara_Shoot_jump_up_diagonal) ||
                    animState.Equals(CharacterAnimStateEnum.Chara_Shoot_jump_down) ||
                    animState.Equals(CharacterAnimStateEnum.Chara_Shoot_jump_down_diagonal)
               )
        {
            //  Touch Ground
            if (characterScript.GetGroundChecker().GetIsColliding())
            {
                yield break;
            }
        }
        yield return new WaitForSeconds(arg_time);
        SetAnimation(arg_animState);
    }
}
