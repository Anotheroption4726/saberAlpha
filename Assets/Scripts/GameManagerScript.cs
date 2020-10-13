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
        Time.timeScale = gameParameters.GetTimeScale();
        Physics2D.gravity = new Vector2(0, -gameParameters.GetGravityScale());
        inGameCharacter.GetComponent<CharacterScript>().SetCharacter(new Character());
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
                Time.timeScale = 1.0f;
                Game.SetGamePaused(false);
            }
            
        }
    }
}
