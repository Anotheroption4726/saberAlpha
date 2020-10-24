using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAnimManagerScript : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private BulletScript bulletScript;
    [SerializeField] private Animator animator;


    //  Bullet init animation trigger
    private void BulletAppearEnd()
    {
        SetAnimation(BulletAnimStateEnum.Bullet_Travel);
    }

    private void BulletDestroyEnd()
    {
        Destroy(bullet);
    }


    //  Animation functions
    public void SetAnimation(BulletAnimStateEnum arg_bulletAnimStateEnum)
    {
        if (bulletScript.GetBullet().GetAnimState() == arg_bulletAnimStateEnum)
        {
            return;
        }

        animator.Play(arg_bulletAnimStateEnum.ToString());
        bulletScript.GetBullet().SetAnimState(arg_bulletAnimStateEnum);
    }
}
