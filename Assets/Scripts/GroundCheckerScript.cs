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

    private void OnTriggerStay2D(Collider2D arg_collider)
    {   
        if (!isGrounded && arg_collider != null & ((1 << arg_collider.gameObject.layer) & groundLayerMask) != 0)
        {
            isGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D arg_collider)
    {
        if (isGrounded && arg_collider != null & ((1 << arg_collider.gameObject.layer) & groundLayerMask) != 0)
        {
            isGrounded = false;
        }
    }
}
