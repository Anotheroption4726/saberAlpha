using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            print("space key was pressed");
        }

        if (Input.GetKeyUp("space"))
        {
            print("Space key was released");
        }
    }
}
