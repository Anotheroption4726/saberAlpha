using System.Collections;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    private CharacterPhysicStateEnum physicState = CharacterPhysicStateEnum.Stateless;
    private CharacterPhysicsManagerScript physicsManager;

    //  Movement variables
    private float runGroundSpeed = 40;
    private float runSlideImpulse = 2500;
    private float jumpImpulse = 1200;
    private float slowAirSpeed = 10;
    private float forwardJumpSpeed = 250;
    private float forwardJumpSlideSpeed = 1500;
    private float forwardJumpAirDrag = 0.97f;   //0.997f
    private float crawlSpeed = 10;
    private float wallJumpImpulse = 800;
    private float wallJumpSpeed = 1500;

    //  Timers
    private float slideTime = 0.13f;
    private bool canRunSlide = false;
    private float runSlideStartTime = 0.75f;
    private float runSlideTime = 0.25f;
    private bool hasWallJumped = false;
    private float wallJumpTimer = 0.25f;

    //  Components
    private Rigidbody2D rigidBody;
    [SerializeField] private CharacterCollideCheckScript groundChecker;
    [SerializeField] private CharacterCollideCheckScript rightWallChecker;
    [SerializeField] private CharacterCollideCheckScript leftWallChecker;

    //  Animations
    private CharacterAnimStateEnum animState = CharacterAnimStateEnum.Idle;
    private bool isFacingRight = true;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Animator animator;
    private string[] animationNamesTable = new string[]{"Idle", "Run", "Slide", "Jump", "Jump_forward", "Fall_normal", "Fall_forward", "Crawl_idle", "Crawl_move", "Wallslide", "Run_slide" };

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        physicsManager = GetComponent<CharacterPhysicsManagerScript>();
    }


    private void FixedUpdate()
    {
        if (!Game.GetGamePaused())
        {
            //  Run
            if (physicState == CharacterPhysicStateEnum.RunRight)
            {
                rigidBody.velocity = new Vector2(runGroundSpeed, rigidBody.velocity.y);
                physicState = CharacterPhysicStateEnum.Stateless;
            }

            else if (physicState == CharacterPhysicStateEnum.RunLeft)
            {
                rigidBody.velocity = new Vector2(-runGroundSpeed, rigidBody.velocity.y);
                physicState = CharacterPhysicStateEnum.Stateless;
            }

            else if (physicState == CharacterPhysicStateEnum.RunSlideRight)
            {
                rigidBody.AddForce(Vector2.right * runSlideImpulse);
                physicState = CharacterPhysicStateEnum.Stateless;
            }

            else if (physicState == CharacterPhysicStateEnum.RunSlideLeft)
            {
                rigidBody.AddForce(-Vector2.right * runSlideImpulse);
                physicState = CharacterPhysicStateEnum.Stateless;
            }


            // Jump
            else if (physicState == CharacterPhysicStateEnum.IdleJump)
            {
                rigidBody.AddForce(Vector2.up * jumpImpulse);
                physicState = CharacterPhysicStateEnum.Stateless;
            }

            else if (physicState == CharacterPhysicStateEnum.IdleJumpRight)
            {
                rigidBody.velocity = new Vector2(slowAirSpeed, rigidBody.velocity.y);
                physicState = CharacterPhysicStateEnum.Stateless;
            }

            else if (physicState == CharacterPhysicStateEnum.IdleJumpLeft)
            {
                rigidBody.velocity = new Vector2(-slowAirSpeed, rigidBody.velocity.y);
                physicState = CharacterPhysicStateEnum.Stateless;
            }


            //  Forward Jump
            else if (physicState == CharacterPhysicStateEnum.ForwardJumpRight)
            {
                rigidBody.AddForce(Vector2.up * jumpImpulse);
                rigidBody.AddForce(Vector2.right * forwardJumpSpeed);
                physicState = CharacterPhysicStateEnum.Stateless;
            }

            else if (physicState == CharacterPhysicStateEnum.ForwardJumpLeft)
            {
                rigidBody.AddForce(Vector2.up * jumpImpulse);
                rigidBody.AddForce(-Vector2.right * forwardJumpSpeed);
                physicState = CharacterPhysicStateEnum.Stateless;
            }

            else if (physicState == CharacterPhysicStateEnum.SwitchDirection)
            {
                rigidBody.velocity = new Vector2(-rigidBody.velocity.x, rigidBody.velocity.y);
                physicState = CharacterPhysicStateEnum.Stateless;
            }

            else if (physicState == CharacterPhysicStateEnum.AirDrag)
            {
                rigidBody.velocity = new Vector2(rigidBody.velocity.x * forwardJumpAirDrag, rigidBody.velocity.y);
                physicState = CharacterPhysicStateEnum.Stateless;
            }

            else if (physicState == CharacterPhysicStateEnum.ForwardJumpLandingRight)
            {
                rigidBody.AddForce(Vector2.right * forwardJumpSlideSpeed);
                physicState = CharacterPhysicStateEnum.Stateless;
            }

            else if (physicState == CharacterPhysicStateEnum.ForwardJumpLandingLeft)
            {
                rigidBody.AddForce(-Vector2.right * forwardJumpSlideSpeed);
                physicState = CharacterPhysicStateEnum.Stateless;
            }


            //  Crawl move
            else if (physicState == CharacterPhysicStateEnum.CrawlMoveRight)
            {
                rigidBody.velocity = new Vector2(crawlSpeed, rigidBody.velocity.y);
                physicState = CharacterPhysicStateEnum.Stateless;
            }

            else if (physicState == CharacterPhysicStateEnum.CrawlMoveLeft)
            {
                rigidBody.velocity = new Vector2(-crawlSpeed, rigidBody.velocity.y);
                physicState = CharacterPhysicStateEnum.Stateless;
            }


            //  Wall Jump
            else if (physicState == CharacterPhysicStateEnum.WallJumpRight)
            {
                rigidBody.AddForce(Vector2.up * wallJumpImpulse);
                rigidBody.AddForce(Vector2.right * wallJumpSpeed);
                physicState = CharacterPhysicStateEnum.Stateless;
            }

            else if (physicState == CharacterPhysicStateEnum.WallJumpLeft)
            {
                rigidBody.AddForce(Vector2.up * wallJumpImpulse);
                rigidBody.AddForce(-Vector2.right * wallJumpSpeed);
                physicState = CharacterPhysicStateEnum.Stateless;
            }

            else if (physicState == CharacterPhysicStateEnum.SetGravityLow)
            {
                rigidBody.gravityScale = 0.125f;
                physicState = CharacterPhysicStateEnum.Stateless;
            }

            else if (physicState == CharacterPhysicStateEnum.SetGravityNormal)
            {
                rigidBody.gravityScale = 1;
                physicState = CharacterPhysicStateEnum.Stateless;
            }


            //  Misc
            else if (physicState == CharacterPhysicStateEnum.ResetX)
            {
                rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
                physicState = CharacterPhysicStateEnum.Stateless;
            }

            else if (physicState == CharacterPhysicStateEnum.ResetY)
            {
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0);
                physicState = CharacterPhysicStateEnum.Stateless;
            }

            else if (physicState == CharacterPhysicStateEnum.Reset)
            {
                rigidBody.velocity = new Vector2(0, 0);
                physicState = CharacterPhysicStateEnum.Stateless;
            }

            else if (physicState == CharacterPhysicStateEnum.Stateless)
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
            if (animState.Equals(CharacterAnimStateEnum.Idle))
            {
                //  Run
                if (Input.GetAxisRaw("Keyboard_Horizontal") < 0 || Input.GetAxisRaw("Keyboard_Horizontal") > 0 || Input.GetAxisRaw("Gamepad_Horizontal") < 0 || Input.GetAxisRaw("Gamepad_Horizontal") > 0)
                {
                    SetAnimation("Run", CharacterAnimStateEnum.Run);
                }

                //  Jump
                else if ((Input.GetButtonDown("Keyboard_Jump") || Input.GetButtonDown("Gamepad_Jump")) && groundChecker.GetIsColliding())
                {
                    SetAnimation("Jump", CharacterAnimStateEnum.Jump);
                    //physicState = CharacterPhysicStateEnum.IdleJump;
                    physicsManager.AddForceTrigger(Vector2.up * jumpImpulse);

                }

                //  Crawl
                else if (Input.GetAxisRaw("Keyboard_Vertical") < 0 || Input.GetAxisRaw("Gamepad_Vertical") > 0)
                {
                    SetAnimation("Crawl_idle", CharacterAnimStateEnum.Crawl_idle);
                }

                //  Fall
                if (!groundChecker.GetIsColliding())
                {
                    SetAnimation("Fall_normal", CharacterAnimStateEnum.Fall_normal);
                }
            }


            //
            // Run Actions & Events
            //
            else if (animState.Equals(CharacterAnimStateEnum.Run))
            {
                //  CanRunSlide Timer
                if (!canRunSlide)
                {
                    StartCoroutine("CanRunSlide");
                }

                //  Run Right
                if (Input.GetAxisRaw("Keyboard_Horizontal") > 0 || Input.GetAxisRaw("Gamepad_Horizontal") > 0)
                {
                    FaceRight();
                    //physicState = CharacterPhysicStateEnum.RunRight;
                    physicsManager.ChangeVelocityHorizontal(runGroundSpeed);

                    //  Jump Forward
                    if ((Input.GetButtonDown("Keyboard_Jump") || Input.GetButtonDown("Gamepad_Jump")) && groundChecker.GetIsColliding())
                    {
                        SetAnimation("Jump_forward", CharacterAnimStateEnum.Jump_forward);
                        physicState = CharacterPhysicStateEnum.ForwardJumpRight;
                    }

                    // Run Slide
                    else if (canRunSlide && (Input.GetAxisRaw("Keyboard_Vertical") < 0 || Input.GetAxisRaw("Gamepad_Vertical") > 0))
                    {
                        SetAnimation("Run_slide", CharacterAnimStateEnum.Run_slide);
                        StartCoroutine("StopRunSlide");
                        physicState = CharacterPhysicStateEnum.RunSlideRight;
                    }
                }

                //  Run Left
                else if (Input.GetAxisRaw("Keyboard_Horizontal") < 0 || Input.GetAxisRaw("Gamepad_Horizontal") < 0)
                {
                    FaceLeft();
                    //physicState = CharacterPhysicStateEnum.RunLeft;
                    physicsManager.ChangeVelocityHorizontal(-runGroundSpeed);

                    //  Jump Forward
                    if ((Input.GetButtonDown("Keyboard_Jump") || Input.GetButtonDown("Gamepad_Jump")) && groundChecker.GetIsColliding())
                    {
                        SetAnimation("Jump_forward", CharacterAnimStateEnum.Jump_forward);
                        physicState = CharacterPhysicStateEnum.ForwardJumpLeft;
                    }

                    //  Run Slide
                    else if (canRunSlide && (Input.GetAxisRaw("Keyboard_Vertical") < 0 || Input.GetAxisRaw("Gamepad_Vertical") > 0))
                    {
                        SetAnimation("Run_slide", CharacterAnimStateEnum.Run_slide);
                        StartCoroutine("StopRunSlide");
                        physicState = CharacterPhysicStateEnum.RunSlideLeft;
                    }
                }

                //  Stop Slide
                else if (!Input.anyKey && Input.GetAxisRaw("Gamepad_Horizontal") == 0)
                {
                    SetAnimation("Slide", CharacterAnimStateEnum.Slide);
                    StartCoroutine("StopSlide");
                }

                //  Fall
                if (!groundChecker.GetIsColliding())
                {
                    SetAnimation("Fall_forward", CharacterAnimStateEnum.Fall_forward);
                }
            }


            //
            //  Slide actions & Events
            //
            else if (animState.Equals(CharacterAnimStateEnum.Slide))
            {
                //  Fall
                if (!groundChecker.GetIsColliding())
                {
                    StopCoroutine("StopSlide");
                    SetAnimation("Fall_normal", CharacterAnimStateEnum.Fall_normal);
                }
            }


            //
            // Idle Jump actions & Events
            //
            else if (animState.Equals(CharacterAnimStateEnum.Jump))
            {
                IdleJumpMovement();

                //  Fall Animation
                if (rigidBody.velocity.y < 0)
                {
                    SetAnimation("Fall_normal", CharacterAnimStateEnum.Fall_normal);
                }

                //  Wallslide
                else if ((rightWallChecker.GetIsColliding() && (Input.GetAxisRaw("Keyboard_Horizontal") > 0 || Input.GetAxisRaw("Gamepad_Horizontal") > 0)) || (leftWallChecker.GetIsColliding() && (Input.GetAxisRaw("Keyboard_Horizontal") < 0 || Input.GetAxisRaw("Gamepad_Horizontal") < 0)))
                {
                    SetAnimation("Wallslide", CharacterAnimStateEnum.Wallslide);
                }
            }


            //
            // Idle Fall actions & Events
            //
            else if (animState.Equals(CharacterAnimStateEnum.Fall_normal))
            {
                IdleJumpMovement();

                //  Wallslide
                if ((rightWallChecker.GetIsColliding() && (Input.GetAxisRaw("Keyboard_Horizontal") > 0 || Input.GetAxisRaw("Gamepad_Horizontal") > 0)) || (leftWallChecker.GetIsColliding() && (Input.GetAxisRaw("Keyboard_Horizontal") < 0 || Input.GetAxisRaw("Gamepad_Horizontal") < 0)))
                {
                    SetAnimation("Wallslide", CharacterAnimStateEnum.Wallslide);
                }

                //  Touch Ground
                if (groundChecker.GetIsColliding())
                {
                    SetAnimation("Idle", CharacterAnimStateEnum.Idle);
                }
            }


            //
            //  Forward Jump actions & Events
            //
            else if (animState.Equals(CharacterAnimStateEnum.Jump_forward))
            {
                SwitchDirection();
                AirDrag();

                //  Fall Animation
                if (rigidBody.velocity.y < 0)
                {
                    SetAnimation("Fall_forward", CharacterAnimStateEnum.Fall_forward);
                }

                //  Wallslide
                else if ((isFacingRight && rightWallChecker.GetIsColliding()) || (!isFacingRight && leftWallChecker.GetIsColliding()))
                {
                    SetAnimation("Wallslide", CharacterAnimStateEnum.Wallslide);
                }
            }


            //
            // Forward Fall actions & Events
            //
            else if (animState.Equals(CharacterAnimStateEnum.Fall_forward))
            {
                SwitchDirection();
                AirDrag();

                //  Wallslide
                if ((isFacingRight && rightWallChecker.GetIsColliding()) || (!isFacingRight && leftWallChecker.GetIsColliding()))
                {
                    SetAnimation("Wallslide", CharacterAnimStateEnum.Wallslide);
                }

                //  Touch Ground
                if (groundChecker.GetIsColliding())
                {
                    if (!Input.anyKey && Input.GetAxisRaw("Horizontal") == 0)
                    {
                        if (isFacingRight)
                        {
                            physicState = CharacterPhysicStateEnum.ForwardJumpLandingRight;
                        }

                        else
                        {
                            physicState = CharacterPhysicStateEnum.ForwardJumpLandingLeft;
                        }

                        SetAnimation("Slide", CharacterAnimStateEnum.Slide);
                        StartCoroutine("StopSlide");
                    }

                    else if (Input.GetAxisRaw("Keyboard_Horizontal") > 0 || Input.GetAxisRaw("Keyboard_Horizontal") < 0 || Input.GetAxisRaw("Gamepad_Horizontal") > 0 || Input.GetAxisRaw("Gamepad_Horizontal") < 0)
                    {
                        SetAnimation("Run", CharacterAnimStateEnum.Run);
                    }
                }
            }


            //
            //  Crawl idle actions & Events
            //
            if (animState.Equals(CharacterAnimStateEnum.Crawl_idle))
            {
                //  Move
                if (Input.GetAxisRaw("Keyboard_Horizontal") < 0 || Input.GetAxisRaw("Keyboard_Horizontal") > 0 || Input.GetAxisRaw("Gamepad_Horizontal") < 0 || Input.GetAxisRaw("Gamepad_Horizontal") > 0)
                {
                    SetAnimation("Crawl_move", CharacterAnimStateEnum.Crawl_move);
                }

                //  Stand Up
                else if (Input.GetAxisRaw("Keyboard_Vertical") >= 0 && Input.GetAxisRaw("Gamepad_Vertical") <= 0)
                {
                    SetAnimation("Idle", CharacterAnimStateEnum.Idle);
                }

                //  Fall
                if (!groundChecker.GetIsColliding())
                {
                    SetAnimation("Fall_normal", CharacterAnimStateEnum.Fall_normal);
                }
            }


            //
            //  Crawl moving actions & Events
            //
            if (animState.Equals(CharacterAnimStateEnum.Crawl_move))
            {
                //  Move Crawl Right
                if ((Input.GetAxisRaw("Keyboard_Vertical") < 0 && Input.GetAxisRaw("Keyboard_Horizontal") > 0) || (Input.GetAxisRaw("Gamepad_Vertical") > 0 && Input.GetAxisRaw("Gamepad_Horizontal") > 0))
                {
                    FaceRight();
                    physicState = CharacterPhysicStateEnum.CrawlMoveRight;
                }

                //  Move Crawl Left
                else if ((Input.GetAxisRaw("Keyboard_Vertical") < 0 && Input.GetAxisRaw("Keyboard_Horizontal") < 0) || (Input.GetAxisRaw("Gamepad_Vertical") > 0 && Input.GetAxisRaw("Gamepad_Horizontal") < 0))
                {
                    FaceLeft();
                    physicState = CharacterPhysicStateEnum.CrawlMoveLeft;
                }

                //  Stop
                else if (Input.GetAxisRaw("Keyboard_Horizontal") == 0 && Input.GetAxisRaw("Gamepad_Horizontal") == 0)
                {
                    SetAnimation("Crawl_idle", CharacterAnimStateEnum.Crawl_idle);
                }

                //  Stand Up
                else if ((Input.GetAxisRaw("Keyboard_Vertical") >= 0 && (Input.GetAxisRaw("Keyboard_Horizontal") < 0 || Input.GetAxisRaw("Keyboard_Horizontal") > 0)) || (Input.GetAxisRaw("Gamepad_Vertical") <= 0 && (Input.GetAxisRaw("Gamepad_Horizontal") < 0 || Input.GetAxisRaw("Gamepad_Horizontal") > 0)))
                {
                    SetAnimation("Run", CharacterAnimStateEnum.Run);
                }

                //  Fall
                if (!groundChecker.GetIsColliding())
                {
                    SetAnimation("Fall_normal", CharacterAnimStateEnum.Fall_normal);
                }
            }


            //
            //  Wallslide actions & Events
            //
            if (animState.Equals(CharacterAnimStateEnum.Wallslide))
            {
                //
                if (Input.GetAxisRaw("Gamepad_Horizontal") == 0 && Input.GetAxisRaw("Keyboard_Horizontal") == 0)
                {
                    //physicState = CharaPhysicStateEnum.SetGravityNormal;
                    rigidBody.gravityScale = 1;
                }

                //
                else if ((Input.GetAxisRaw("Gamepad_Horizontal") > 0 && isFacingRight) || (Input.GetAxisRaw("Keyboard_Horizontal") > 0 && isFacingRight) || (Input.GetAxisRaw("Gamepad_Horizontal") < 0 && !isFacingRight) || (Input.GetAxisRaw("Keyboard_Horizontal") < 0 && !isFacingRight))
                {
                    //physicState = CharaPhysicStateEnum.SetGravityLow;
                    rigidBody.gravityScale = 0.125f;
                }

                //  Jump Left
                if (!hasWallJumped && isFacingRight && (Input.GetButtonDown("Keyboard_Jump") || Input.GetButtonDown("Gamepad_Jump")) && (Input.GetAxisRaw("Keyboard_Horizontal") <= 0 || Input.GetAxisRaw("Gamepad_Horizontal") <= 0))
                {
                    //physicState = CharaPhysicStateEnum.SetGravityNormal;
                    rigidBody.gravityScale = 1;
                    rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0);
                    //physicState = CharaPhysicStateEnum.ResetY;
                    FaceLeft();
                    physicState = CharacterPhysicStateEnum.WallJumpLeft;
                    SetAnimation("Jump_forward", CharacterAnimStateEnum.Jump_forward);
                    StartCoroutine("WallJumpTimer");
                }

                //  Jump Right
                else if (!hasWallJumped && !isFacingRight && (Input.GetButtonDown("Keyboard_Jump") || Input.GetButtonDown("Gamepad_Jump")) && (Input.GetAxisRaw("Keyboard_Horizontal") >= 0 || Input.GetAxisRaw("Gamepad_Horizontal") >= 0))
                {
                    //physicState = CharaPhysicStateEnum.SetGravityNormal;
                    rigidBody.gravityScale = 1;
                    rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0);
                    //physicState = CharaPhysicStateEnum.ResetY;
                    FaceRight();
                    physicState = CharacterPhysicStateEnum.WallJumpRight;
                    SetAnimation("Jump_forward", CharacterAnimStateEnum.Jump_forward);
                    StartCoroutine("WallJumpTimer");
                }

                //  Wall slide over
                else if ((isFacingRight && !rightWallChecker.GetIsColliding()) || (!isFacingRight && !leftWallChecker.GetIsColliding()))
                {
                    //physicState = CharaPhysicStateEnum.SetGravityNormal;
                    rigidBody.gravityScale = 1;
                    SetAnimation("Fall_normal", CharacterAnimStateEnum.Fall_normal);
                }

                //  Switch Direction Left
                if (isFacingRight && (Input.GetAxisRaw("Keyboard_Horizontal") < 0 || Input.GetAxisRaw("Gamepad_Horizontal") < 0))
                {
                    //physicState = CharaPhysicStateEnum.SetGravityNormal;
                    rigidBody.gravityScale = 1;
                    FaceLeft();
                    SetAnimation("Fall_normal", CharacterAnimStateEnum.Fall_normal);
                }

                //  Switch Direction Right
                else if (!isFacingRight && (Input.GetAxisRaw("Keyboard_Horizontal") > 0 || Input.GetAxisRaw("Gamepad_Horizontal") > 0))
                {
                    //physicState = CharaPhysicStateEnum.SetGravityNormal;
                    rigidBody.gravityScale = 1;
                    FaceRight();
                    SetAnimation("Fall_normal", CharacterAnimStateEnum.Fall_normal);
                }

                //  Touch Ground
                if (groundChecker.GetIsColliding())
                {
                    //physicState = CharaPhysicStateEnum.SetGravityNormal;
                    rigidBody.gravityScale = 1;
                    SetAnimation("Idle", CharacterAnimStateEnum.Idle);
                }
            }
        }
    }


    //  Timer functions
    private IEnumerator StopSlide()
    {
        yield return new WaitForSeconds(slideTime);
        SetAnimation("Idle", CharacterAnimStateEnum.Idle);
    }

    private IEnumerator CanRunSlide()
    {
        yield return new WaitForSeconds(runSlideStartTime);
        canRunSlide = true;
    }

    private IEnumerator StopRunSlide()
    {
        yield return new WaitForSeconds(runSlideTime);
        SetAnimation("Crawl_move", CharacterAnimStateEnum.Crawl_move);
        canRunSlide = false;
    }

    private IEnumerator WallJumpTimer()
    {
        hasWallJumped = true;
        yield return new WaitForSeconds(wallJumpTimer);
        hasWallJumped = false;
    }


    //  Animation functions
    private void SetAnimation(string arg_animationName, CharacterAnimStateEnum arg_charaAnimStateEnum)
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


    //  Movement functions
    private void IdleJumpMovement()
    {
        //  Idle Jump move Right
        if (Input.GetAxisRaw("Keyboard_Horizontal") > 0 || Input.GetAxisRaw("Gamepad_Horizontal") > 0)
        {
            FaceRight();
            physicState = CharacterPhysicStateEnum.IdleJumpRight;
        }

        //  Idle Jump move Left
        else if (Input.GetAxisRaw("Keyboard_Horizontal") < 0 || Input.GetAxisRaw("Gamepad_Horizontal") < 0)
        {
            FaceLeft();
            physicState = CharacterPhysicStateEnum.IdleJumpLeft;
        }
    }

    private void SwitchDirection()
    {
        //  Switch direction to Right
        if (!isFacingRight && (Input.GetAxisRaw("Keyboard_Horizontal") > 0 || Input.GetAxisRaw("Gamepad_Horizontal") > 0))
        {
            physicState = CharacterPhysicStateEnum.SwitchDirection;
            FaceRight();
        }

        //  Switch direction to Left
        else if (isFacingRight && (Input.GetAxisRaw("Keyboard_Horizontal") < 0 || Input.GetAxisRaw("Gamepad_Horizontal") < 0))
        {
            physicState = CharacterPhysicStateEnum.SwitchDirection;
            FaceLeft();
        }
    }

    private void AirDrag()
    {
        if (!Input.anyKey && Input.GetAxisRaw("Gamepad_Horizontal") == 0)
        {
            physicState = CharacterPhysicStateEnum.AirDrag;
        }
    }
}
