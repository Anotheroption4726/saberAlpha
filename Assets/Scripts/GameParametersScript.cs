using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameParametersScript : MonoBehaviour
{
    //  Game Parameters
    [SerializeField] private GameTypeEnum gameType;
    [SerializeField] private float timeScale;
    [SerializeField] private float gravityScale;


    //  Getters
    public GameTypeEnum GetGameType()
    {
        return gameType;
    }

    public float GetTimeScale()
    {
        return timeScale;
    }

    public float GetGravityScale()
    {
        return gravityScale;
    }
}
