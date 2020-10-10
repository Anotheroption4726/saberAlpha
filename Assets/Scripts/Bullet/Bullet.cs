public class Bullet
{
    private float bulletAppearAnimationTime;

    public Bullet(float arg_bulletAppearAnimationTime = 4f)
    {
        bulletAppearAnimationTime = arg_bulletAppearAnimationTime;
    }

    public float GetBulletAppearAnimationTime()
    {
        return bulletAppearAnimationTime;
    }
}
