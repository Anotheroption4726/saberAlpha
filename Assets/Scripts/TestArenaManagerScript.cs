using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TestArenaManagerScript : MonoBehaviour
{
    private float timeScale = 1;
    [SerializeField] private Image pauseIcon;
    [SerializeField] GameObject inGameCharacter;

    void Awake()
    {
        inGameCharacter.GetComponent<CharacterScript>().SetCharacter(new Character
                                                                         (
                                                                            
                                                                         )
                                                                    );


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
