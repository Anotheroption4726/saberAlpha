using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    //  Components
    [SerializeField] private Image pauseIcon;
    [SerializeField] GameObject inGameCharacter;

    //  Gamae Parameters
    [SerializeField] private GameTypeEnum gameType;
    [SerializeField] private float timeScale = 1;

    //
    //  Character test Parameters 
    //

    //  Run Parameters
    [SerializeField] private float fixed_run_movementSpeed;
    [SerializeField] private float timer_run_stopSlideTime;

    //  Idle Jump Parameters
    [SerializeField] private float impulse_idleJump_verticalForce;
    [SerializeField] private float fixed_idleJump_movementSpeed;

    // Forward Jump Parameters
    [SerializeField] private float impulse_forwardJump_horizontalForce;
    [SerializeField] private float impulse_forwardJump_stopSlideForce;
    [SerializeField] private float ratio_forwardJump_horizontalAirDrag;

    //  Fall MaxSpeed Parameters
    [SerializeField] private float threshold_fallMaxSpeed_velocityValue;

    //  On the Ground Parameters
    [SerializeField] private float timer_onTheGround_duration;
    [SerializeField] private float timer_onTheGround_StandUpTime;

    //  Crawl Parameters
    [SerializeField] private float fixed_crawl_movementSpeed;

    //  Run Slide Parameters
    [SerializeField] private float impulse_runSlide_horizontalForce;
    [SerializeField] private float timer_runSlide_startTime;
    [SerializeField] private float timer_runSlide_duration;

    //  WallSlide Parameters
    [SerializeField] private float ratio_wallSlide_holdGravity;

    //  WallJump Parameters
    [SerializeField] private float impulse_wallJump_verticalForce;
    [SerializeField] private float impulse_wallJump_horizontalForce;
    [SerializeField] private float timer_wallJump_restrainDuration;


    void Awake()
    {
        Game.SetGameType(gameType);

        if (Game.GetGameType() == GameTypeEnum.NormalGame)
        {
            inGameCharacter.GetComponent<CharacterScript>().SetCharacter(new Character());
        }

        if (Game.GetGameType() == GameTypeEnum.TestGame)
        {
            inGameCharacter.GetComponent<CharacterScript>().SetCharacter(
                                                                            new Character(
                                                                                            fixed_run_movementSpeed,
                                                                                            timer_run_stopSlideTime,
                                                                                            impulse_idleJump_verticalForce,
                                                                                            fixed_idleJump_movementSpeed,
                                                                                            impulse_forwardJump_horizontalForce,
                                                                                            impulse_forwardJump_stopSlideForce,
                                                                                            ratio_forwardJump_horizontalAirDrag,
                                                                                            threshold_fallMaxSpeed_velocityValue,
                                                                                            timer_onTheGround_duration,
                                                                                            timer_onTheGround_StandUpTime,
                                                                                            fixed_crawl_movementSpeed,
                                                                                            impulse_runSlide_horizontalForce,
                                                                                            timer_runSlide_startTime,
                                                                                            timer_runSlide_duration,
                                                                                            ratio_wallSlide_holdGravity,
                                                                                            impulse_wallJump_verticalForce,
                                                                                            impulse_wallJump_horizontalForce,
                                                                                            timer_wallJump_restrainDuration
                                                                                         )
                                                                        );
        }

        Time.timeScale = timeScale;
        pauseIcon.enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) || Input.GetButtonDown("Gamepad_Select"))
        {
            Game.SetGamePaused(false);
            SceneManager.LoadScene("TestArenaScene");
        }

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Gamepad_Start"))
        {
            if (!Game.GetGamePaused())
            {
                pauseIcon.enabled = true;
                Time.timeScale = 0.0f;
                Game.SetGamePaused(true);
            }
            else
            {
                pauseIcon.enabled = false;
                Time.timeScale = timeScale;
                Game.SetGamePaused(false);
            }
            
        }
    }
}
