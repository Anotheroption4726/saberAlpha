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


    //  Constructor
    /*
    public Character
    (
        float arg_run_movementSpeed, 
        float arg_run_stopSlideTime, 
        float arg_idleJump_verticalForce, 
        float arg_idleJump_movementSpeed, 
        float arg_forwardJump_horizontalForce, 
        float arg_forwardJump_stopSlideForce, 
        float arg_forwardJump_horizontalAirDrag, 
        float arg_fallMaxSpeed_velocityValue,
        float arg_onTheGround_duration,
        float arg_onTheGround_StandUpTime,
        bool arg_onTheGround_isOntheGround,
        float arg_crawl_movementSpeed,
        float arg_runSlide_horizontalForce,
        float arg_runSlide_startTime,
        float arg_runSlide_duration,
        bool arg_runSlide_canRunSlide,
        float arg_wallSlide_holdGravity,
        float arg_wallJump_verticalForce,
        float arg_wallJump_horizontalForce,
        float arg_wallJump_restrainDuration,
        bool arg_wallJump_hasWallJumped
    )
    {
        fixed_run_movementSpeed = arg_run_movementSpeed;
        timer_run_stopSlideTime = arg_run_stopSlideTime;
        impulse_idleJump_verticalForce = arg_idleJump_verticalForce;
        fixed_idleJump_movementSpeed = arg_idleJump_movementSpeed;
        impulse_forwardJump_horizontalForce = arg_forwardJump_horizontalForce;
        impulse_forwardJump_stopSlideForce = arg_forwardJump_stopSlideForce;
        ratio_forwardJump_horizontalAirDrag = arg_forwardJump_horizontalAirDrag;
        threshold_fallMaxSpeed_velocityValue = arg_fallMaxSpeed_velocityValue;
        timer_onTheGround_duration = arg_onTheGround_duration;
        timer_onTheGround_StandUpTime = arg_onTheGround_StandUpTime;
        trigger_onTheGround_isOntheGround = arg_onTheGround_isOntheGround;
        fixed_crawl_movementSpeed = arg_crawl_movementSpeed;
        impulse_runSlide_horizontalForce = arg_runSlide_horizontalForce;
        timer_runSlide_startTime = arg_runSlide_startTime;
        timer_runSlide_duration = arg_runSlide_duration;
        trigger_runSlide_canRunSlide = arg_runSlide_canRunSlide;
        ratio_wallSlide_holdGravity = arg_wallSlide_holdGravity;
        impulse_wallJump_verticalForce = arg_wallJump_verticalForce;
        impulse_wallJump_horizontalForce = arg_wallJump_horizontalForce;
        timer_wallJump_restrainDuration = arg_wallJump_restrainDuration;
        trigger_wallJump_hasWallJumped = arg_wallJump_hasWallJumped;
    }
    */


    //  Getters
    public float GetRunMovementSpeed()
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

    public bool GetOnTheGroundIsOntheGround()
    {
        return trigger_onTheGround_isOntheGround;
    }

    public float GetCrawlMovementSpeed()
    {
        return fixed_crawl_movementSpeed;
    }

    public float GetRunSlideHorizontalForce()
    {
        return impulse_runSlide_horizontalForce;
    }

    public float GetRunSlideStartTime()
    {
        return timer_runSlide_startTime;
    }

    public float GetRunSlideDuration()
    {
        return timer_runSlide_duration;
    }

    public bool GetRunSlideCanRunSlide()
    {
        return trigger_runSlide_canRunSlide;
    }

    public float GetWallSlideHoldGravity()
    {
        return ratio_wallSlide_holdGravity;
    }

    public float GetWallJumpVerticalForce()
    {
        return impulse_wallJump_verticalForce;
    }

    public float GetWallJumpHorizontalForce()
    {
        return impulse_wallJump_horizontalForce;
    }

    public float GetWallJumpRestrainDuration()
    {
        return timer_wallJump_restrainDuration;
    }

    public bool GetWallJumpHasWallJumped()
    {
        return trigger_wallJump_hasWallJumped;
    }
}
