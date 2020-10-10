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
    private float timer_onTheGround_StartTime;
    private float timer_onTheGround_duration;
    private float timer_onTheGround_StandUpTime;

    //  Crawl Parameters
    private float fixed_crawl_movementSpeed;

    //  Ground Slide Parameters
    private float impulse_groundSlide_horizontalForce;
    private float timer_groundSlide_startTime;
    private float timer_groundSlide_duration;

    //  WallSlide Parameters
    private float ratio_wallSlide_holdGravity;

    //  WallJump Parameters
    private float impulse_wallJump_verticalForce;
    private float impulse_wallJump_horizontalForce;
    private float timer_wallJump_restrainDuration;

    // Melee Parameters
    private float meleeIdleStopTime;
    private float meleeIdleUpStopTime;
    private float meleeIdleUpDiagonalStopTime;
    private float meleeRunStopTime;
    private float meleeJumpStopTime;
    private float meleeJumpUpStopTime;
    private float meleeJumpUpDiagonalStopTime;
    private float meleeJumpDownStopTime;
    private float meleeJumpDownDiagonalStopTime;

    // Shoot Parameters
    private float shootIdleStopTime;
    private float shootIdleUpStopTime;
    private float shootIdleUpDiagonalStopTime;
    private float shootJumpStopTime;
    private float shootJumpUpStopTime;
    private float shootJumpUpDiagonalStopTime;
    private float shootJumpDownStopTime;
    private float shootJumpDownDiagonalStopTime;


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
        float arg_onTheGround_StartTime = 0.25f,
        float arg_onTheGround_duration = 2,
        float arg_onTheGround_StandUpTime = 0.5f,
        float arg_crawl_movementSpeed = 10,
        float arg_groundSlide_horizontalForce = 2000,
        float arg_groundSlide_startTime = 0.75f,
        float arg_groundSlide_duration = 0.5f,
        float arg_wallSlide_holdGravity = 0.125f,
        float arg_wallJump_verticalForce = 1500,
        float arg_wallJump_horizontalForce = 1500,
        float arg_wallJump_restrainDuration = 0.25f,
        float arg_meleeIdleStopTime = 0.35f,
        float arg_meleeIdleUpStopTime = 0.35f,
        float arg_meleeIdleUpDiagonalStopTime = 0.4f,
        float arg_meleeRunStopTime = 0.25f, //0.25f
        float arg_meleeJumpStopTime = 0.45f,
        float arg_meleeJumpUpStopTime = 0.5f,
        float arg_meleeJumpUpDiagonalStopTime = 0.5f,
        float arg_meleeJumpDownStopTime = 0.5f,
        float arg_meleeJumpDownDiagonalStopTime = 0.5f,
        float arg_shootIdleStopTime = 0.4f,
        float arg_shootIdleUpStopTime = 0.4f,
        float arg_shootIdleUpDiagonalStopTime = 0.4f,
        float arg_shootJumpStopTime = 0.4f,
        float arg_shootJumpUpStopTime = 0.4f,
        float arg_shootJumpUpDiagonalStopTime = 0.3f,
        float arg_shootJumpDownStopTime = 0.3f,
        float arg_shootJumpDownDiagonalStopTime = 0.3f
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
        timer_onTheGround_StartTime = arg_onTheGround_StartTime;
        timer_onTheGround_duration = arg_onTheGround_duration;
        timer_onTheGround_StandUpTime = arg_onTheGround_StandUpTime;
        fixed_crawl_movementSpeed = arg_crawl_movementSpeed;
        impulse_groundSlide_horizontalForce = arg_groundSlide_horizontalForce;
        timer_groundSlide_startTime = arg_groundSlide_startTime;
        timer_groundSlide_duration = arg_groundSlide_duration;
        ratio_wallSlide_holdGravity = arg_wallSlide_holdGravity;
        impulse_wallJump_verticalForce = arg_wallJump_verticalForce;
        impulse_wallJump_horizontalForce = arg_wallJump_horizontalForce;
        timer_wallJump_restrainDuration = arg_wallJump_restrainDuration;
        meleeIdleStopTime = arg_meleeIdleStopTime;
        meleeIdleUpStopTime = arg_meleeIdleUpStopTime;
        meleeIdleUpDiagonalStopTime = arg_meleeIdleUpDiagonalStopTime;
        meleeRunStopTime = arg_meleeRunStopTime;
        meleeJumpStopTime = arg_meleeJumpStopTime;
        meleeJumpUpStopTime = arg_meleeJumpUpStopTime;
        meleeJumpUpDiagonalStopTime = arg_meleeJumpUpDiagonalStopTime;
        meleeJumpDownStopTime = arg_meleeJumpDownStopTime;
        meleeJumpDownDiagonalStopTime = arg_meleeJumpDownDiagonalStopTime;
        shootIdleStopTime = arg_shootIdleStopTime;
        shootIdleUpStopTime = arg_shootIdleUpStopTime;
        shootIdleUpDiagonalStopTime = arg_shootIdleUpDiagonalStopTime;
        shootJumpStopTime = arg_shootJumpStopTime;
        shootJumpUpStopTime = arg_shootJumpUpStopTime;
        shootJumpUpDiagonalStopTime = arg_shootJumpUpDiagonalStopTime;
        shootJumpDownStopTime = arg_shootJumpDownStopTime;
        shootJumpDownDiagonalStopTime = arg_shootJumpDownDiagonalStopTime;
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

    public float GetOnTheGroundStartTime()
    {
        return timer_onTheGround_StartTime;
    }

    public float GetOnTheGroundDuration()
    {
        return timer_onTheGround_duration;
    }

    public float GetOnTheGroundStandUpTime()
    {
        return timer_onTheGround_StandUpTime;
    }

    public float GetCrawlMovementSpeed()
    {
        return fixed_crawl_movementSpeed;
    }

    public float GetGroundSlideHorizontalForce()
    {
        return impulse_groundSlide_horizontalForce;
    }

    public float GetGroundSlideStartTime()
    {
        return timer_groundSlide_startTime;
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

    public float GetMeleeIdleStopTime()
    {
        return meleeIdleStopTime;
    }

    public float GetMeleeIdleUpStopTime()
    {
        return meleeIdleUpStopTime;
    }

    public float GetMeleeIdleUpDiagonalStopTime()
    {
        return meleeIdleUpDiagonalStopTime;
    }

    public float GetMeleeRunStopTime()
    {
        return meleeRunStopTime;
    }

    public float GetMeleeJumpStopTime()
    {
        return meleeJumpStopTime;
    }

    public float GetMeleeJumpUpStopTime()
    {
        return meleeJumpUpStopTime;
    }

    public float GetMeleeJumpUpDiagonalStopTime()
    {
        return meleeJumpUpDiagonalStopTime;
    }

    public float GetMeleeJumpDownStopTime()
    {
        return meleeJumpDownStopTime;
    }

    public float GetMeleeJumpDownDiagonalStopTime()
    {
        return meleeJumpDownDiagonalStopTime;
    }

    public float GetShootIdleStopTime()
    {
        return shootIdleStopTime;
    }

    public float GetShootIdleUpStopTime()
    {
        return shootIdleUpStopTime;
    }

    public float GetShootIdleUpDiagonalStopTime()
    {
        return shootIdleUpDiagonalStopTime;
    }

    public float GetShootJumpStopTime()
    {
        return shootJumpStopTime;
    }

    public float GetShootJumpUpStopTime()
    {
        return shootJumpUpStopTime;
    }

    public float GetShootJumpUpDiagonalStopTime()
    {
        return shootJumpUpDiagonalStopTime;
    }

    public float GetShootJumpDownStopTime()
    {
        return shootJumpDownStopTime;
    }

    public float GetShootJumpDownDiagonalStopTime()
    {
        return shootJumpDownDiagonalStopTime;
    }
}
