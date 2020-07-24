using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheckerScript : MonoBehaviour
{
    private bool isGrounded;
    [SerializeField] private LayerMask groundLayerMask;

    public bool GetIsGrounded()
    {
        return isGrounded;
    }

    private void OnTriggerStay2D(Collider2D collider)
    {   
        if (collider != null)
        {
            if (((1 << collider.gameObject.layer) & groundLayerMask) != 0)
            {
                isGrounded = true;
                Debug.Log(isGrounded);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isGrounded = false;
        Debug.Log(isGrounded);
    }
}
