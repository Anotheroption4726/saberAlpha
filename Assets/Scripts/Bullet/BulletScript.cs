using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] private BulletAnimManagerScript animManager;
    private Bullet bullet = new Bullet();


    //  Start
    void Start()
    {
        animManager.SetAnimation(BulletAnimStateEnum.Bullet_Appear);
    }


    //  Update
    private void Update()
    {
        if (!bullet.GetAnimState().Equals(BulletAnimStateEnum.Bullet_Explode))
        {
            transform.Translate(new Vector3(1, 0, 0) * bullet.GetBulletMovementSpeed() * Time.deltaTime);
        }
    }


    //  OnCollision
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("collision");
        animManager.SetAnimation(BulletAnimStateEnum.Bullet_Explode);
    }


    //
    public Bullet GetBullet()
    {
        return bullet;
    }
}
