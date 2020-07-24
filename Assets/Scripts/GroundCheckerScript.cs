using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheckerScript : MonoBehaviour
{
    [SerializeField] private CharacterScript characterScript;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("True");
    }
}
