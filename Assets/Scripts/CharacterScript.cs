using System.Collections;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    private CharaPhysicStateEnum physicState = CharaPhysicStateEnum.Stateless;

    //  Movement variables
    private int runGroundSpeed = 40;
    private float jumpImpulse_addForce = 1200;
    private int slowAirSpeed = 10;
    private float forwardJumpSpeed_addForce = 250;
    private float forwardJumpSlideSpeed = 1500;
    private float forwardJumpAirDrag = 0.997f;

    //  Timers
    private float slideTime = 0.13f;
    private float fallNormalTimer = 0.5f;
    private float fallForwardTimer = 0.75f;

    //  Components
    private Rigidbody2D rigidBody;
    [SerializeField] private GroundCheckerScript groundChecker;

    //  Animations
    private CharaAnimStateEnum animState = CharaAnimStateEnum.Idle;
    private bool isFacingRight = true;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Animator animator;
    private string[] animationNamesTable = new string[]{"Idle", "Run", "Slide", "Jump", "Jump_forward", "Fall_normal", "Fall_forward"};

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }


    private void FixedUpdate()
    {
        if (!Game.GetGamePaused())
        {
            //  Run
            if (physicState == CharaPhysicStateEnum.RunRight)
            {
                rigidBody.velocity = new Vector2(runGroundSpeed, rigidBody.velocity.y);
                physicState = CharaPhysicStateEnum.Stateless;
            }

            if (physicState == CharaPhysicStateEnum.RunLeft)
            {
                rigidBody.velocity = new Vector2(-runGroundSpeed, rigidBody.velocity.y);
                physicState = CharaPhysicStateEnum.Stateless;
            }


            // Jump
            if (physicState == CharaPhysicStateEnum.IdleJump)
            {
                rigidBody.AddForce(Vector2.up * jumpImpulse_addForce);
                physicState = CharaPhysicStateEnum.Stateless;
            }

            if (physicState == CharaPhysicStateEnum.IdleJumpRight)
            {
                rigidBody.velocity = new Vector2(slowAirSpeed, rigidBody.velocity.y);
                physicState = CharaPhysicStateEnum.Stateless;
            }

            if (physicState == CharaPhysicStateEnum.IdleJumpLeft)
            {
                rigidBody.velocity = new Vector2(-slowAirSpeed, rigidBody.velocity.y);
                physicState = CharaPhysicStateEnum.Stateless;
            }


            //  Forward Jump
            if (physicState == CharaPhysicStateEnum.ForwardJumpRight)
            {
                rigidBody.AddForce(Vector2.up * jumpImpulse_addForce);
                rigidBody.AddForce(Vector2.right * forwardJumpSpeed_addForce);
                physicState = CharaPhysicStateEnum.Stateless;
            }

            if (physicState == CharaPhysicStateEnum.ForwardJumpLeft)
            {
                rigidBody.AddForce(Vector2.up * jumpImpulse_addForce);
                rigidBody.AddForce(-Vector2.right * forwardJumpSpeed_addForce);
                physicState = CharaPhysicStateEnum.Stateless;
            }

            if (physicState == CharaPhysicStateEnum.ForwardJumpLandingRight)
            {
                rigidBody.AddForce(Vector2.right * forwardJumpSlideSpeed);
                physicState = CharaPhysicStateEnum.Stateless;
            }

            if (physicState == CharaPhysicStateEnum.ForwardJumpLandingLeft)
            {
                rigidBody.AddForce(-Vector2.right * forwardJumpSlideSpeed);
                physicState = CharaPhysicStateEnum.Stateless;
            }


            //  Misc
            if (physicState == CharaPhysicStateEnum.Reset)
            {
                rigidBody.velocity = new Vector2(0, 0);
                physicState = CharaPhysicStateEnum.Stateless;
            }

            if (physicState == CharaPhysicStateEnum.Stateless)
            {
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, rigidBody.velocity.y);
            }
        }
    }


    private void Update()
    {
        if (!Game.GetGamePaused())
        {
            //
            // Idle Actions & Events
            //
            if (animState.Equals(CharaAnimStateEnum.Idle))
            {
                //  Run
                if (Input.GetAxisRaw("Keyboard_Horizontal") < 0 || Input.GetAxisRaw("Keyboard_Horizontal") > 0 || Input.GetAxisRaw("Gamepad_Horizontal") < 0 || Input.GetAxisRaw("Gamepad_Horizontal") > 0)
                {
                    SetAnimation("Run", CharaAnimStateEnum.Run);
                }

                //  Jump
                if ((Input.GetButtonDown("Keyboard_Jump") || Input.GetButtonDown("Gamepad_Jump")) && groundChecker.GetIsGrounded())
                {
                    SetAnimation("Jump", CharaAnimStateEnum.Jump);
                    StartCoroutine("FallNormal");
                    physicState = CharaPhysicStateEnum.IdleJump;
                }
            }


            //
            // Run Actions & Events
            //
            if (animState.Equals(CharaAnimStateEnum.Run))
            {
                //  Run Right
                if (Input.GetAxisRaw("Keyboard_Horizontal") > 0 || Input.GetAxisRaw("Gamepad_Horizontal") > 0)
                {
                    FaceRight();
                    physicState = CharaPhysicStateEnum.RunRight;

                    //  Jump Forward
                    if ((Input.GetButtonDown("Keyboard_Jump") || Input.GetButtonDown("Gamepad_Jump")) && groundChecker.GetIsGrounded())
                    {
                        SetAnimation("Jump_forward", CharaAnimStateEnum.Jump_forward);
                        StartCoroutine("FallForward");
                        physicState = CharaPhysicStateEnum.ForwardJumpRight;
                    }
                }

                //  Run Left
                if (Input.GetAxisRaw("Keyboard_Horizontal") < 0 || Input.GetAxisRaw("Gamepad_Horizontal") < 0)
                {
                    FaceLeft();
                    physicState = CharaPhysicStateEnum.RunLeft;

                    //  Jump Forward
                    if ((Input.GetButtonDown("Keyboard_Jump") || Input.GetButtonDown("Gamepad_Jump")) && groundChecker.GetIsGrounded())
                    {
                        SetAnimation("Jump_forward", CharaAnimStateEnum.Jump_forward);
                        StartCoroutine("FallForward");
                        physicState = CharaPhysicStateEnum.ForwardJumpLeft;
                    }
                }

                //  Slide
                if (!Input.anyKey && Input.GetAxisRaw("Gamepad_Horizontal") == 0)
                {
                    SetAnimation("Slide", CharaAnimStateEnum.Slide);
                    StartCoroutine("StopSlide");
                }
            }


            //
            // Idle Jump actions & Events
            //
            if (animState.Equals(CharaAnimStateEnum.Jump))
            {
                IdleJumpMovement();

                /*
                //  Air move Right
                if (Input.GetAxisRaw("Keyboard_Horizontal") > 0 || Input.GetAxisRaw("Gamepad_Horizontal") > 0)
                {
                    FaceRight();
                    physicState = CharaPhysicStateEnum.IdleJumpRight;
                }

                //  Air move Left
                if (Input.GetAxisRaw("Keyboard_Horizontal") < 0 || Input.GetAxisRaw("Gamepad_Horizontal") < 0)
                {
                    FaceLeft();
                    physicState = CharaPhysicStateEnum.IdleJumpLeft;
                }
                */
            }


            //
            //  Forward Jump actions & Events
            //
            if (animState.Equals(CharaAnimStateEnum.Jump_forward))
            {
                SwitchDirection();

                /*
                //  Switch direction to Right
                if (!isFacingRight && (Input.GetAxisRaw("Keyboard_Horizontal") > 0 || Input.GetAxisRaw("Gamepad_Horizontal") > 0))
                {
                    rigidBody.velocity = new Vector2(-rigidBody.velocity.x, rigidBody.velocity.y);
                    FaceRight();
                }

                //  Switch direction to Left
                if (isFacingRight && (Input.GetAxisRaw("Keyboard_Horizontal") < 0 || Input.GetAxisRaw("Gamepad_Horizontal") < 0))
                {
                    rigidBody.velocity = new Vector2(-rigidBody.velocity.x, rigidBody.velocity.y);
                    FaceLeft();
                }
                */

                // Air drag
                if (!Input.anyKey && Input.GetAxisRaw("Gamepad_Horizontal") == 0)
                {
                    rigidBody.velocity = new Vector2(rigidBody.velocity.x * forwardJumpAirDrag, rigidBody.velocity.y);
                }
            }


            //
            // Idle Fall actions & Events
            //
            if (animState.Equals(CharaAnimStateEnum.Fall_normal))
            {
                IdleJumpMovement();

                /*
                //  Fall Right
                if (Input.GetAxisRaw("Keyboard_Horizontal") > 0 || Input.GetAxisRaw("Gamepad_Horizontal") > 0)
                {
                    FaceRight();
                    physicState = CharaPhysicStateEnum.IdleJumpRight;
                }

                //  Fall Left
                if (Input.GetAxisRaw("Keyboard_Horizontal") < 0 || Input.GetAxisRaw("Gamepad_Horizontal") < 0)
                {
                    FaceLeft();
                    physicState = CharaPhysicStateEnum.IdleJumpLeft;
                }
                */

                //  Touch Ground
                if (groundChecker.GetIsGrounded())
                {
                    SetAnimation("Idle", CharaAnimStateEnum.Idle);
                }
            }


            //
            // Forward Fall actions & Events
            //
            if (animState.Equals(CharaAnimStateEnum.Fall_forward))
            {
                SwitchDirection();

                /*
                //  Switch direction to Right
                if (!isFacingRight && (Input.GetAxisRaw("Keyboard_Horizontal") > 0 || Input.GetAxisRaw("Gamepad_Horizontal") > 0))
                {
                    rigidBody.velocity = new Vector2(-rigidBody.velocity.x, rigidBody.velocity.y);
                    FaceRight();
                }

                //  Switch direction to Left
                if (isFacingRight && (Input.GetAxisRaw("Keyboard_Horizontal") < 0 || Input.GetAxisRaw("Gamepad_Horizontal") < 0))
                {
                    rigidBody.velocity = new Vector2(-rigidBody.velocity.x, rigidBody.velocity.y);
                    FaceLeft();
                }
                */

                //  Touch Ground
                if (groundChecker.GetIsGrounded())
                {
                    if (!Input.anyKey && Input.GetAxisRaw("Horizontal") == 0)
                    {
                        if (isFacingRight)
                        {
                            physicState = CharaPhysicStateEnum.ForwardJumpLandingRight;
                        }

                        if (!isFacingRight)
                        {
                            physicState = CharaPhysicStateEnum.ForwardJumpLandingLeft;
                        }

                        SetAnimation("Slide", CharaAnimStateEnum.Slide);
                        StartCoroutine("StopSlide");
                    }

                    if (Input.GetAxisRaw("Keyboard_Horizontal") > 0 || Input.GetAxisRaw("Keyboard_Horizontal") < 0 || Input.GetAxisRaw("Gamepad_Horizontal") > 0 || Input.GetAxisRaw("Gamepad_Horizontal") < 0)
                    {
                        SetAnimation("Run", CharaAnimStateEnum.Run);
                    }
                }
            }
        }
    }


    private IEnumerator StopSlide()
    {
        yield return new WaitForSeconds(slideTime);
        SetAnimation("Idle", CharaAnimStateEnum.Idle);
    }

    private IEnumerator FallNormal()
    {
        yield return new WaitForSeconds(fallNormalTimer);
        SetAnimation("Fall_normal", CharaAnimStateEnum.Fall_normal);
    }

    private IEnumerator FallForward()
    {
        yield return new WaitForSeconds(fallForwardTimer);
        SetAnimation("Fall_forward", CharaAnimStateEnum.Fall_forward);
    }

    private void SetAnimation(string arg_animationName, CharaAnimStateEnum arg_charaAnimStateEnum)
    {
        foreach (string lp_animation in animationNamesTable)
        {
            if (arg_animationName != lp_animation)
            {
                animator.SetBool(lp_animation, false);
            }
        }

        animator.SetBool(arg_animationName, true);
        animState = arg_charaAnimStateEnum;
    }

    private void FaceRight()
    {
        sprite.flipX = false;
        isFacingRight = true;
    }

    private void FaceLeft()
    {
        sprite.flipX = true;
        isFacingRight = false;
    }

    private void IdleJumpMovement()
    {
        //  Idle Jump move Right
        if (Input.GetAxisRaw("Keyboard_Horizontal") > 0 || Input.GetAxisRaw("Gamepad_Horizontal") > 0)
        {
            FaceRight();
            physicState = CharaPhysicStateEnum.IdleJumpRight;
        }

        //  Idle Jump move Left
        if (Input.GetAxisRaw("Keyboard_Horizontal") < 0 || Input.GetAxisRaw("Gamepad_Horizontal") < 0)
        {
            FaceLeft();
            physicState = CharaPhysicStateEnum.IdleJumpLeft;
        }
    }

    private void SwitchDirection()
    {
        //  Switch direction to Right
        if (!isFacingRight && (Input.GetAxisRaw("Keyboard_Horizontal") > 0 || Input.GetAxisRaw("Gamepad_Horizontal") > 0))
        {
            rigidBody.velocity = new Vector2(-rigidBody.velocity.x, rigidBody.velocity.y);
            FaceRight();
        }

        //  Switch direction to Left
        if (isFacingRight && (Input.GetAxisRaw("Keyboard_Horizontal") < 0 || Input.GetAxisRaw("Gamepad_Horizontal") < 0))
        {
            rigidBody.velocity = new Vector2(-rigidBody.velocity.x, rigidBody.velocity.y);
            FaceLeft();
        }
    }
}
