using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestArenaManagerScript : MonoBehaviour
{
    [SerializeField] private float timeScale;

    void Awake()
    {
        Time.timeScale = timeScale;
    }
}
