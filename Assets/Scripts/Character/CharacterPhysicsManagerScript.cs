using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPhysicsManagerScript : MonoBehaviour
{
    private Rigidbody2D rigidBody;

    private bool isTriggerHorizontalVelocity = false;
    private bool isTriggerVerticalVelocity = false;
    private bool isTriggerAddForce = false;

    private float xVelocityValue;
    private float yVelocityValue;
    private Vector2 addForceVector;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (isTriggerHorizontalVelocity)
        {
            rigidBody.velocity = new Vector2(xVelocityValue, rigidBody.velocity.y);
            isTriggerHorizontalVelocity = false;
        }

        if (isTriggerVerticalVelocity)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, yVelocityValue);
            isTriggerVerticalVelocity = false;
        }

        if (isTriggerAddForce)
        {
            rigidBody.AddForce(addForceVector);
            isTriggerAddForce = false;
        }
    }

    public void ChangeVelocityHorizontal(float arg_XValue)
    {
        xVelocityValue = arg_XValue;
        isTriggerHorizontalVelocity = true;
    }

    public void ChangeVelocityVertical(float arg_YValue)
    {
        yVelocityValue = arg_YValue;
        isTriggerVerticalVelocity = true;
    }

    public void AddForceTrigger(Vector2 arg_addForceVector)
    {
        addForceVector = arg_addForceVector;
        isTriggerAddForce = true;
    }
}
