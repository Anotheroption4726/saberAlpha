﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private CharacterAnimStateEnum charaShootAnimState;
    private int directionInt = 1;
    private Bullet bullet = new Bullet();
    private BulletAnimStateEnum animState = BulletAnimStateEnum.Bullet_Appear;


    //  Start
    void Start()
    {
        SetAnimation(BulletAnimStateEnum.Bullet_Appear);
        StartCoroutine(EndAnimationCoroutine(bullet.GetBulletAppearAnimationTime(), BulletAnimStateEnum.Bullet_Travel));
    }


    //  Getters
    public CharacterAnimStateEnum GetCharaShootAnimState()
    {
        return charaShootAnimState;
    }

    public int GetDirectionInt()
    {
        return directionInt;
    }


    //  Setters
    public void SetCharaShootAnimState(CharacterAnimStateEnum arg_charaShootAnimState)
    {
        charaShootAnimState = arg_charaShootAnimState;
    }

    public void SetDirectionInt(int arg_directionInt)
    {
        directionInt = arg_directionInt;
    }


    //  Update
    private void Update()
    {
        transform.Translate(directionInt * new Vector3(1, 0, 0) * bullet.GetBulletMovementSpeed() * Time.deltaTime);

        /*
        if (charaShootAnimState == CharacterAnimStateEnum.Chara_Shoot_idle || charaShootAnimState == CharacterAnimStateEnum.Chara_Shoot_jump)
        {
            //transform.Translate(directionInt * transform.right * bullet.GetBulletMovementSpeed() * Time.deltaTime);
            //transform.Translate(directionInt * transform.right * bullet.GetBulletMovementSpeed() * Time.deltaTime);
        }
        else if (charaShootAnimState == CharacterAnimStateEnum.Chara_Shoot_idle_up)
        {
            //transform.Translate(-transform.up * bullet.GetBulletMovementSpeed() * Time.deltaTime);
        }
        */
    }


    //  Coroutines
    IEnumerator EndAnimationCoroutine(float arg_time, BulletAnimStateEnum arg_animState)
    {
        yield return new WaitForSeconds(arg_time);
        SetAnimation(arg_animState);
    }

    //  Animation functions
    private void SetAnimation(BulletAnimStateEnum arg_bulletAnimStateEnum)
    {
        if (animState == arg_bulletAnimStateEnum)
        {
            return;
        }

        animator.Play(arg_bulletAnimStateEnum.ToString());
        animState = arg_bulletAnimStateEnum;
    }
}
