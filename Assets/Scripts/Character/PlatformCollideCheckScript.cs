using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCollideCheckScript : MonoBehaviour
{
    private bool isColliding;
    [SerializeField] private LayerMask groundLayerMask;

    public bool GetIsColliding()
    {
        return isColliding;
    }

    private void OnTriggerStay2D(Collider2D arg_collider)
    {   
        if (!isColliding && arg_collider != null & ((1 << arg_collider.gameObject.layer) & groundLayerMask) != 0)
        {
            isColliding = true;
        }
    }

    private void OnTriggerExit2D(Collider2D arg_collider)
    {
        if (isColliding && arg_collider != null & ((1 << arg_collider.gameObject.layer) & groundLayerMask) != 0)
        {
            isColliding = false;
        }
    }
}
