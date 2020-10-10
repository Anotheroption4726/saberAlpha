using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private int directionInt = 1;
    private Bullet bullet = new Bullet();
    private BulletAnimStateEnum animState = BulletAnimStateEnum.Bullet_Appear;
    [SerializeField] private Animator animator;

    // Start
    void Start()
    {
        SetAnimation(BulletAnimStateEnum.Bullet_Appear);
        StartCoroutine(EndAnimationCoroutine(bullet.GetBulletAppearAnimationTime(), BulletAnimStateEnum.Bullet_Travel));
    }

    public int GetDirectionInt()
    {
        return directionInt;
    }

    public void SetDirectionInt(int arg_directionInt)
    {
        directionInt = arg_directionInt;
    }

    private void Update()
    {
        transform.Translate(directionInt * transform.right * bullet.GetBulletMovementSpeed() * Time.deltaTime);
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
