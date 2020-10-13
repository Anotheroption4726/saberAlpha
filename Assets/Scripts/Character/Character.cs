public class Character
{
    // States and Triggers
    private CharacterAnimStateEnum animState = CharacterAnimStateEnum.Chara_Idle;
    private int directionInt = 1;
    private bool trigger_groundSlide_canGroundSlide = false;
    private bool trigger_wallJump_hasWallJumped = false;

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

    //  Crawl Parameters
    private float fixed_crawl_movementSpeed;

    //  Ground Slide Parameters
    private float impulse_groundSlide_horizontalForce;
    private float timer_groundSlide_canStartTime;
    private float timer_groundSlide_duration;

    //  WallSlide Parameters
    private float ratio_wallSlide_holdGravity;

    //  WallJump Parameters
    private float impulse_wallJump_verticalForce;
    private float impulse_wallJump_horizontalForce;
    private float timer_wallJump_restrainDuration;


    //  Constructor
    public Character
    (
        float arg_run_movementSpeed = 40,
        float arg_run_stopSlideTime = 0.13f,
        float arg_idleJump_verticalForce = 1200,
        float arg_idleJump_movementSpeed = 10,
        float arg_forwardJump_horizontalForce = 250,
        float arg_forwardJump_stopSlideForce = 1500,
        float arg_forwardJump_horizontalAirDrag = 0.97f, //0.97f
        float arg_fallMaxSpeed_velocityValue = 60,
        float arg_onTheGround_duration = 2,
        float arg_crawl_movementSpeed = 10,
        float arg_groundSlide_horizontalForce = 2000,
        float arg_groundSlide_canStartTime = 0.75f,
        float arg_groundSlide_duration = 0.5f,
        float arg_wallSlide_holdGravity = 0.125f,
        float arg_wallJump_verticalForce = 1500,
        float arg_wallJump_horizontalForce = 1500,
        float arg_wallJump_restrainDuration = 0.25f
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
        fixed_crawl_movementSpeed = arg_crawl_movementSpeed;
        impulse_groundSlide_horizontalForce = arg_groundSlide_horizontalForce;
        timer_groundSlide_canStartTime = arg_groundSlide_canStartTime;
        timer_groundSlide_duration = arg_groundSlide_duration;
        ratio_wallSlide_holdGravity = arg_wallSlide_holdGravity;
        impulse_wallJump_verticalForce = arg_wallJump_verticalForce;
        impulse_wallJump_horizontalForce = arg_wallJump_horizontalForce;
        timer_wallJump_restrainDuration = arg_wallJump_restrainDuration;
    }


    //  Getters
    public CharacterAnimStateEnum GetAnimState()
    {
        return animState;
    }

    public int GetDirectionInt()
    {
        return directionInt;
    }

    public bool GetTrigger_groundSlide_canGroundSlide()
    {
        return trigger_groundSlide_canGroundSlide;
    }

    public bool GetTrigger_wallJump_hasWallJumped()
    {
        return trigger_wallJump_hasWallJumped;
    }

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

    public float GetCrawlMovementSpeed()
    {
        return fixed_crawl_movementSpeed;
    }

    public float GetGroundSlideHorizontalForce()
    {
        return impulse_groundSlide_horizontalForce;
    }

    public float GetGroundSlideCanStartTime()
    {
        return timer_groundSlide_canStartTime;
    }

    public float GetGroundSlideDuration()
    {
        return timer_groundSlide_duration;
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


    // Setters
    public void SetAnimState(CharacterAnimStateEnum arg_animState)
    {
        animState = arg_animState;
    }

    public void SetDirectionInt(int arg_directionInt)
    {
        directionInt = arg_directionInt;
    }

    public void SetTrigger_groundSlide_canGroundSlide(bool arg_trigger_groundSlide_canGroundSlide)
    {
        trigger_groundSlide_canGroundSlide = arg_trigger_groundSlide_canGroundSlide;
    }

    public void SetTrigger_wallJump_hasWallJumped(bool arg_trigger_wallJump_hasWallJumped)
    {
        trigger_wallJump_hasWallJumped = arg_trigger_wallJump_hasWallJumped;
    }
}
