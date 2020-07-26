using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestArenaManagerScript : MonoBehaviour
{
    private float timeScale = 1.0f;
    private float gravityScale = 4.75f;

    void Awake()
    {
        Time.timeScale = timeScale;
        Physics2D.gravity = new Vector2(Physics2D.gravity.x, Physics2D.gravity.y * gravityScale);   //  Attention cette ligne a un bug après un simple 'SceneManager.LoadScene("TestArenaScene")'
    }

    private void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("TestArenaScene");
        }
        */

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!Game.GetGamePaused())
            {
                Time.timeScale = 0.0f;
                Game.SetGamePaused(true);
            }
            else
            {
                Time.timeScale = timeScale;
                Game.SetGamePaused(false);
            }
            
        }
    }
}
