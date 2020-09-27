using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    [SerializeField] private Image pauseIcon;
    [SerializeField] GameObject inGameCharacter;
    [SerializeField] PhysicsMaterial2D characterPhysicMaterial;
    [SerializeField] PhysicsMaterial2D platformPhysicMaterial;

    private GameParametersScript gameParameters;

    void Awake()
    {
        gameParameters = GetComponent<GameParametersScript>();
        Game.SetGameType(gameParameters.GetGameType());
        pauseIcon.enabled = false;

        if (Game.GetGameType() == GameTypeEnum.NormalGame)
        {
            //platformPhysicMaterial.friction = Game.GetDefaultPlatformFriction();
            //characterPhysicMaterial.friction = Game.GetDefaultCharacterFriction();
            inGameCharacter.GetComponent<CharacterScript>().SetCharacter(new Character());
        }

        if (Game.GetGameType() == GameTypeEnum.TestGame)
        {
            Time.timeScale = gameParameters.GetTimeScale();
            Physics2D.gravity = new Vector2(0, -gameParameters.GetGravityScale());
            platformPhysicMaterial.friction = gameParameters.GetPlatformFriction();
            characterPhysicMaterial.friction = gameParameters.GetCharacterFriction();

            inGameCharacter.GetComponent<CharacterScript>().SetCharacter(
                                                                            new Character(
                                                                                            gameParameters.GetRunMovementSpeed(),
                                                                                            gameParameters.GetRunStopSlideTime(),
                                                                                            gameParameters.GetIdleJumpVerticalForce(),
                                                                                            gameParameters.GetIdleJumpMovementSpeed(),
                                                                                            gameParameters.GetForwardJumpHorizontalForce(),
                                                                                            gameParameters.GetForwardJumpStopSlideForce(),
                                                                                            gameParameters.GetForwardJumpHorizontalAirDrag(),
                                                                                            gameParameters.GetFallMaxSpeedVelocityValue(),
                                                                                            gameParameters.GetOnTheGroundDuration(),
                                                                                            gameParameters.GetOnTheGroundStandUpTime(),
                                                                                            gameParameters.GetCrawlMovementSpeed(),
                                                                                            gameParameters.GetRunSlideHorizontalForce(),
                                                                                            gameParameters.GetRunSlideStartTime(),
                                                                                            gameParameters.GetRunSlideDuration(),
                                                                                            gameParameters.GetWallSlideHoldGravity(),
                                                                                            gameParameters.GetWallJumpVerticalForce(),
                                                                                            gameParameters.GetWallJumpHorizontalForce(),
                                                                                            gameParameters.GetWallJumpRestrainDuration()
                                                                                         )
                                                                        );
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) || Input.GetButtonDown("Gamepad_Select"))
        {
            Game.SetGamePaused(false);
            SceneManager.LoadScene("TestScene");
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
                Time.timeScale = gameParameters.GetTimeScale();
                Game.SetGamePaused(false);
            }
            
        }
    }
}
