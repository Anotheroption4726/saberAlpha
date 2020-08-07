using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameParametersScript : MonoBehaviour
{
    //  Game Parameters
    [SerializeField] private GameTypeEnum gameType;
    [SerializeField] private float timeScale;
    [SerializeField] private float gravityScale;
    [SerializeField] private float platformFriction;
    [SerializeField] private float characterFriction;

    //  Character Run Parameters
    [SerializeField] private float fixed_run_movementSpeed;
    [SerializeField] private float timer_run_stopSlideTime;

    //  Character Idle Jump Parameters
    [SerializeField] private float impulse_idleJump_verticalForce;
    [SerializeField] private float fixed_idleJump_movementSpeed;

    //  Character Forward Jump Parameters
    [SerializeField] private float impulse_forwardJump_horizontalForce;
    [SerializeField] private float impulse_forwardJump_stopSlideForce;
    [SerializeField] private float ratio_forwardJump_horizontalAirDrag;

    //  Character Fall MaxSpeed Parameters
    [SerializeField] private float threshold_fallMaxSpeed_velocityValue;

    //  Character On the Ground Parameters
    [SerializeField] private float timer_onTheGround_duration;
    [SerializeField] private float timer_onTheGround_StandUpTime;

    //  Character Crawl Parameters
    [SerializeField] private float fixed_crawl_movementSpeed;

    //  Character Run Slide Parameters
    [SerializeField] private float impulse_runSlide_horizontalForce;
    [SerializeField] private float timer_runSlide_startTime;
    [SerializeField] private float timer_runSlide_duration;

    //  Character WallSlide Parameters
    [SerializeField] private float ratio_wallSlide_holdGravity;

    //  Character WallJump Parameters
    [SerializeField] private float impulse_wallJump_verticalForce;
    [SerializeField] private float impulse_wallJump_horizontalForce;
    [SerializeField] private float timer_wallJump_restrainDuration;


    //  Getters
    public GameTypeEnum GetGameType()
    {
        return gameType;
    }

    public float GetTimeScale()
    {
        return timeScale;
    }

    public float GetGravityScale()
    {
        return gravityScale;
    }

    public float GetPlatformFriction()
    {
        return platformFriction;
    }

    public float GetCharacterFriction()
    {
        return characterFriction;
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

    public float GetOnTheGroundStandUpTime()
    {
        return timer_onTheGround_StandUpTime;
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
}
