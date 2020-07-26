using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestArenaManagerScript : MonoBehaviour
{
    private float timeScale = 1;
    private float gravityScale = 4.75f;
    private bool gamePaused = false;

    void Awake()
    {
        Time.timeScale = timeScale;
        Physics2D.gravity = new Vector2(Physics2D.gravity.x, Physics2D.gravity.y * gravityScale);   //  Attention cette ligne a un bug après un simple 'SceneManager.LoadScene("TestArenaScene")'
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("TestArenaScene");
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !gamePaused)
        {
            Time.timeScale = 0;
            gamePaused = true;
        }

        
        if (Input.GetKeyDown(KeyCode.Escape) && gamePaused)
        {
            Time.timeScale = 1;
            gamePaused = false;
        }
    }
}
