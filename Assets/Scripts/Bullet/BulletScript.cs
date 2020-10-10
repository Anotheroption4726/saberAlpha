using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private Bullet bullet = new Bullet();
    private BulletAnimStateEnum animState = BulletAnimStateEnum.Bullet_Appear;
    [SerializeField] private Animator animator;
    private Rigidbody2D rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Start
    void Start()
    {
        SetAnimation(BulletAnimStateEnum.Bullet_Appear);
        StartCoroutine(EndAnimationCoroutine(bullet.GetBulletAppearAnimationTime(), BulletAnimStateEnum.Bullet_Travel));

        rigidbody.AddForce(new Vector2(bullet.GetBulletMovementSpeed(), 0));
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
