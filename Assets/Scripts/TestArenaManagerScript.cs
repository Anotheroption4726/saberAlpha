using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TestArenaManagerScript : MonoBehaviour
{
    private float timeScale = 1.0f;
    private float gravityScale = 4.75f; //4.75f
    [SerializeField] private Image pauseIcon;

    void Awake()
    {
        Time.timeScale = timeScale;
        Physics2D.gravity = new Vector2(Physics2D.gravity.x, Physics2D.gravity.y * gravityScale);   //  Attention cette ligne a un bug après un simple 'SceneManager.LoadScene("TestArenaScene")'
        pauseIcon.enabled = false;
    }

    private void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("TestArenaScene");
        }
        */

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
