public class Bullet
{
    private BulletAnimStateEnum animState;
    private int bulletMovementSpeed;

    public Bullet(int arg_bulletMovementSpeed = 75)
    {
        bulletMovementSpeed = arg_bulletMovementSpeed;
    }

    public float GetBulletMovementSpeed()
    {
        return bulletMovementSpeed;
    }

    public BulletAnimStateEnum GetAnimState()
    {
        return animState;
    }

    public void SetAnimState(BulletAnimStateEnum arg_animState)
    {
        animState = arg_animState;
    }
}
