using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameParametersScript : MonoBehaviour
{
    //  Game Parameters
    [SerializeField] private GameTypeEnum gameType;
    [SerializeField] private float timeScale;
    [SerializeField] private float gravityScale;

    //  Character Run Parameters
    [SerializeField] private float runMovementSpeed_fixed;
    [SerializeField] private float runStopSlideTime_timer;

    //  Character Idle Jump Parameters
    [SerializeField] private float idleJumpVerticalForce_impulse;
    [SerializeField] private float idleJumpMovementSpeed_fixed;

    //  Character Forward Jump Parameters
    [SerializeField] private float forwardJumpHorizontalForce_impulse;
    [SerializeField] private float forwardJumpStopSlideForce_impulse;
    [SerializeField] private float forwardJumpHorizontalAirDrag_ratio;

    //  Character Fall MaxSpeed Parameters
    [SerializeField] private float fallMaxSpeedVelocityValue_threshold;

    //  Character On the Ground Parameters
    [SerializeField] private float onTheGroundDuration_timer;
    [SerializeField] private float onTheGroundStandUpTime_timer;

    //  Character Crawl Parameters
    [SerializeField] private float crawlMovementSpeed_fixed;

    //  Character Run Slide Parameters
    [SerializeField] private float runSlideHorizontalForce_impulse;
    [SerializeField] private float runSlideStartTime_timer;
    [SerializeField] private float runSlideDuration_timer;

    //  Character WallSlide Parameters
    [SerializeField] private float wallSlideHoldGravity_ratio;

    //  Character WallJump Parameters
    [SerializeField] private float wallJumpVerticalForce_impulse;
    [SerializeField] private float wallJumpHorizontalForce_impulse;
    [SerializeField] private float wallJumpRestrainDuration_timer;


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

    public float GetRunMovementSpeed()
    {
        return runMovementSpeed_fixed;
    }

    public float GetRunStopSlideTime()
    {
        return runStopSlideTime_timer;
    }

    public float GetIdleJumpVerticalForce()
    {
        return idleJumpVerticalForce_impulse;
    }

    public float GetIdleJumpMovementSpeed()
    {
        return idleJumpMovementSpeed_fixed;
    }

    public float GetForwardJumpHorizontalForce()
    {
        return forwardJumpHorizontalForce_impulse;
    }

    public float GetForwardJumpStopSlideForce()
    {
        return forwardJumpStopSlideForce_impulse;
    }

    public float GetForwardJumpHorizontalAirDrag()
    {
        return forwardJumpHorizontalAirDrag_ratio;
    }

    public float GetFallMaxSpeedVelocityValue()
    {
        return fallMaxSpeedVelocityValue_threshold;
    }

    public float GetOnTheGroundDuration()
    {
        return onTheGroundDuration_timer;
    }

    public float GetOnTheGroundStandUpTime()
    {
        return onTheGroundStandUpTime_timer;
    }

    public float GetCrawlMovementSpeed()
    {
        return crawlMovementSpeed_fixed;
    }

    public float GetRunSlideHorizontalForce()
    {
        return runSlideHorizontalForce_impulse;
    }

    public float GetRunSlideStartTime()
    {
        return runSlideStartTime_timer;
    }

    public float GetRunSlideDuration()
    {
        return runSlideDuration_timer;
    }

    public float GetWallSlideHoldGravity()
    {
        return wallSlideHoldGravity_ratio;
    }

    public float GetWallJumpVerticalForce()
    {
        return wallJumpVerticalForce_impulse;
    }

    public float GetWallJumpHorizontalForce()
    {
        return wallJumpHorizontalForce_impulse;
    }

    public float GetWallJumpRestrainDuration()
    {
        return wallJumpRestrainDuration_timer;
    }
}
