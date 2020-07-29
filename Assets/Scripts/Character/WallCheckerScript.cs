using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCheckerScript : MonoBehaviour
{
    private bool isTouchingWall;
    [SerializeField] private LayerMask groundLayerMask;

    public bool GetIsTouchingWall()
    {
        return isTouchingWall;
    }

    private void OnTriggerStay2D(Collider2D arg_collider)
    {
        if (!isTouchingWall && arg_collider != null & ((1 << arg_collider.gameObject.layer) & groundLayerMask) != 0)
        {
            isTouchingWall = true;
        }
    }

    private void OnTriggerExit2D(Collider2D arg_collider)
    {
        if (isTouchingWall && arg_collider != null & ((1 << arg_collider.gameObject.layer) & groundLayerMask) != 0)
        {
            isTouchingWall = false;
        }
    }
}
