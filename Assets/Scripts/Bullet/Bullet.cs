public class Bullet
{
    private int bulletMovementSpeed;
    private float bulletAppearAnimationTime;

    public Bullet(int arg_bulletMovementSpeed = 10, float arg_bulletAppearAnimationTime = 0.5f)
    {
        bulletMovementSpeed = arg_bulletMovementSpeed;
        bulletAppearAnimationTime = arg_bulletAppearAnimationTime;
    }

    public float GetBulletMovementSpeed()
    {
        return bulletMovementSpeed;
    }

    public float GetBulletAppearAnimationTime()
    {
        return bulletAppearAnimationTime;
    }
}
