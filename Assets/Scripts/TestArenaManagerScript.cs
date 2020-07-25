using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestArenaManagerScript : MonoBehaviour
{
    [SerializeField] private float timeScale;
    [SerializeField] private float gravityScale;    //  4.75f

    void Awake()
    {
        Time.timeScale = timeScale;
        Physics2D.gravity = new Vector2(Physics2D.gravity.x, Physics2D.gravity.y * gravityScale);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene("TestArenaScene");
        }
    }
}
