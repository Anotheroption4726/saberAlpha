public class Character
{
    //  Run Parameters
    private float fixed_run_movementSpeed = 40;
    private float timer_run_stopSlideTime = 0.13f;

    //  Idle Jump Parameters
    private float impulse_idleJump_verticalForce = 1200;
    private float fixed_idleJump_movementSpeed = 10;

    // Forward Jump Parameters
    private float impulse_forwardJump_horizontalForce = 250;
    private float impulse_forwardJump_stopSlideForce = 1500;
    private float ratio_forwardJump_horizontalAirDrag = 0.97f;   //0.997f

    //  Fall MaxSpeed Parameters
    private float threshold_fallMaxSpeed_velocityValue = 60;

    //  On the Ground Parameters
    private float timer_onTheGround_duration = 2;
    private float timer_onTheGround_StandUpTime = 0.5f;
    private bool trigger_onTheGround_isOntheGround = false;

    //  Crawl Parameters
    private float fixed_crawl_movementSpeed = 10;

    //  Run Slide Parameters
    private float impulse_runSlide_horizontalForce = 2500;
    private float timer_runSlide_startTime = 0.75f;
    private float timer_runSlide_duration = 0.25f;
    private bool trigger_runSlide_canRunSlide = false;

    //  WallSlide Parameters
    private float ratio_wallSlide_holdGravity = 0.125f;

    //  WallJump Parameters
    private float impulse_wallJump_verticalForce = 800;
    private float impulse_wallJump_horizontalForce = 1500;
    private float timer_wallJump_restrainDuration = 0.25f;
    private bool trigger_wallJump_hasWallJumped = false;

    //  Getters
    public float getRunMovementSpeed()
    {
        return fixed_run_movementSpeed;
    }

    public float GetRunStopSlideTime()
    {
        return timer_run_stopSlideTime;
    }

    public float GetIdleJumpVerticalForce()
    {
        return impulse_idleJump_verticalForce;
    }

    public float GetIdleJumpMovementSpeed()
    {
        return fixed_idleJump_movementSpeed;
    }

    public float GetForwardJumpHorizontalForce()
    {
        return impulse_forwardJump_horizontalForce;
    }

    public float GetForwardJumpStopSlideForce()
    {
        return impulse_forwardJump_stopSlideForce;
    }

    public float GetForwardJumpHorizontalAirDrag()
    {
        return ratio_forwardJump_horizontalAirDrag;
    }

    public float GetFallMaxSpeedVelocityValue()
    {
        return threshold_fallMaxSpeed_velocityValue;
    }

    public float GetOnTheGroundDuration()
    {
        return timer_onTheGround_duration;
    }

    public float GetOnTheGroundStandUpTime()
    {
        return timer_onTheGround_StandUpTime;
    }
}
