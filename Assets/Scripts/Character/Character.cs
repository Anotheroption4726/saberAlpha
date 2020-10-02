public class Character
{
    //  Run Parameters
    private float fixed_run_movementSpeed;
    private float timer_run_stopSlideTime;

    //  Idle Jump Parameters
    private float impulse_idleJump_verticalForce;
    private float fixed_idleJump_movementSpeed;

    // Forward Jump Parameters
    private float impulse_forwardJump_horizontalForce;
    private float impulse_forwardJump_stopSlideForce;
    private float ratio_forwardJump_horizontalAirDrag;

    //  Fall MaxSpeed Parameters
    private float threshold_fallMaxSpeed_velocityValue;

    //  On the Ground Parameters
    private float timer_onTheGround_duration;
    private float timer_onTheGround_StandUpTime;
    private bool trigger_onTheGround_isOntheGround = false;

    //  Crawl Parameters
    private float fixed_crawl_movementSpeed;

    //  Run Slide Parameters
    private float impulse_runSlide_horizontalForce;
    private float timer_runSlide_startTime;
    private float timer_runSlide_duration;
    private bool trigger_runSlide_canRunSlide = false;

    //  WallSlide Parameters
    private float ratio_wallSlide_holdGravity;

    //  WallJump Parameters
    private float impulse_wallJump_verticalForce;
    private float impulse_wallJump_horizontalForce;
    private float timer_wallJump_restrainDuration;
    private bool trigger_wallJump_hasWallJumped = false;

    // Melee Parameters
    private float meleeIdleStopTime;
    private float meleeIdleUpStopTime;
    private float meleeRunStopTime;
    private float meleeJumpStopTime;
    private float meleeJumpForwardStopTime;


    //  Constructor
    public Character
    (
        float arg_run_movementSpeed = 40,
        float arg_run_stopSlideTime = 0.13f,
        float arg_idleJump_verticalForce = 1200,
        float arg_idleJump_movementSpeed = 10,
        float arg_forwardJump_horizontalForce = 250,
        float arg_forwardJump_stopSlideForce = 1500,
        float arg_forwardJump_horizontalAirDrag = 0.97f,    //0.997f
        float arg_fallMaxSpeed_velocityValue = 60,
        float arg_onTheGround_duration = 2,
        float arg_onTheGround_StandUpTime = 0.5f,
        float arg_crawl_movementSpeed = 10,
        float arg_runSlide_horizontalForce = 2500,
        float arg_runSlide_startTime = 0.75f,
        float arg_runSlide_duration = 0.25f,
        float arg_wallSlide_holdGravity = 0.125f,
        float arg_wallJump_verticalForce = 800,
        float arg_wallJump_horizontalForce = 1500,
        float arg_wallJump_restrainDuration = 0.25f,
        float arg_meleeIdleStopTime = 0.35f,
        float arg_meleeIdleUpStopTime = 0.35f,
        float arg_meleeRunStopTime = 0.25f,
        float arg_meleeJumpStopTime = 0.45f,
        float arg_meleeJumpForwardStopTime = 0.5f
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
        fixed_crawl_movementSpeed = arg_crawl_movementSpeed;
        impulse_runSlide_horizontalForce = arg_runSlide_horizontalForce;
        timer_runSlide_startTime = arg_runSlide_startTime;
        timer_runSlide_duration = arg_runSlide_duration;
        ratio_wallSlide_holdGravity = arg_wallSlide_holdGravity;
        impulse_wallJump_verticalForce = arg_wallJump_verticalForce;
        impulse_wallJump_horizontalForce = arg_wallJump_horizontalForce;
        timer_wallJump_restrainDuration = arg_wallJump_restrainDuration;
        meleeIdleStopTime = arg_meleeIdleStopTime;
        meleeIdleUpStopTime = arg_meleeIdleUpStopTime;
        meleeRunStopTime = arg_meleeRunStopTime;
        meleeJumpStopTime = arg_meleeJumpStopTime;
        meleeJumpForwardStopTime = arg_meleeJumpForwardStopTime;
    }


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

    public float GetMeleeIdleStopTime()
    {
        return meleeIdleStopTime;
    }

    public float GetMeleeIdleUpStopTime()
    {
        return meleeIdleUpStopTime;
    }

    public float GetMeleeRunStopTime()
    {
        return meleeRunStopTime;
    }

    public float GetMeleeJumpStopTime()
    {
        return meleeJumpStopTime;
    }

    public float GetMeleeJumpForwardStopTime()
    {
        return meleeJumpForwardStopTime;
    }


    //  Setters
    public void SetOnTheGroundIsOntheGround(bool arg_isOntheGround)
    {
        trigger_onTheGround_isOntheGround = arg_isOntheGround;
    }

    public void SetRunSlideCanRunSlide(bool arg_canRunSlide)
    {
        trigger_runSlide_canRunSlide = arg_canRunSlide;
    }

    public void SetWallJumpHasWallJumped(bool arg_hasWallJumped)
    {
        trigger_wallJump_hasWallJumped = arg_hasWallJumped;
    }
}
