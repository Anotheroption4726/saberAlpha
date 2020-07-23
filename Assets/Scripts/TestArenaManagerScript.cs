using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestArenaManagerScript : MonoBehaviour
{
    [SerializeField] private float timeScale;

    void Awake()
    {
        Time.timeScale = timeScale;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            SceneManager.LoadScene("TestArenaScene");
        }
    }
}
