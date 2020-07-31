using System.Collections;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    private CharaPhysicStateEnum physicState = CharaPhysicStateEnum.Stateless;

    //  Movement variables
    private float runGroundSpeed = 40;
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
    private bool hasWallJumped = false;
    private float wallJumpTimer = 0.1f;

    //  Components
    private Rigidbody2D rigidBody;
    [SerializeField] private PlatformCollideCheckScript groundChecker;
    [SerializeField] private PlatformCollideCheckScript rightWallChecker;
    [SerializeField] private PlatformCollideCheckScript leftWallChecker;

    //  Animations
    private CharaAnimStateEnum animState = CharaAnimStateEnum.Idle;
    private bool isFacingRight = true;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Animator animator;
    private string[] animationNamesTable = new string[]{"Idle", "Run", "Slide", "Jump", "Jump_forward", "Fall_normal", "Fall_forward", "Crawl_idle", "Crawl_move", "Wallslide" };

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
                rigidBody.AddForce(Vector2.up * jumpImpulse);
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
                rigidBody.AddForce(Vector2.up * jumpImpulse);
                rigidBody.AddForce(Vector2.right * forwardJumpSpeed);
                physicState = CharaPhysicStateEnum.Stateless;
            }

            if (physicState == CharaPhysicStateEnum.ForwardJumpLeft)
            {
                rigidBody.AddForce(Vector2.up * jumpImpulse);
                rigidBody.AddForce(-Vector2.right * forwardJumpSpeed);
                physicState = CharaPhysicStateEnum.Stateless;
            }

            if (physicState == CharaPhysicStateEnum.SwitchDirection)
            {
                rigidBody.velocity = new Vector2(-rigidBody.velocity.x, rigidBody.velocity.y);
                physicState = CharaPhysicStateEnum.Stateless;
            }

            if (physicState == CharaPhysicStateEnum.AirDrag)
            {
                rigidBody.velocity = new Vector2(rigidBody.velocity.x * forwardJumpAirDrag, rigidBody.velocity.y);
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


            //  Crawl move
            if (physicState == CharaPhysicStateEnum.CrawlMoveRight)
            {
                rigidBody.velocity = new Vector2(crawlSpeed, rigidBody.velocity.y);
                physicState = CharaPhysicStateEnum.Stateless;
            }

            if (physicState == CharaPhysicStateEnum.CrawlMoveLeft)
            {
                rigidBody.velocity = new Vector2(-crawlSpeed, rigidBody.velocity.y);
                physicState = CharaPhysicStateEnum.Stateless;
            }


            //  Wall Jump
            if (physicState == CharaPhysicStateEnum.WallJumpRight)
            {
                rigidBody.AddForce(Vector2.up * wallJumpImpulse);
                rigidBody.AddForce(Vector2.right * wallJumpSpeed);
                physicState = CharaPhysicStateEnum.Stateless;
            }

            if (physicState == CharaPhysicStateEnum.WallJumpLeft)
            {
                rigidBody.AddForce(Vector2.up * wallJumpImpulse);
                rigidBody.AddForce(-Vector2.right * wallJumpSpeed);
                physicState = CharaPhysicStateEnum.Stateless;
            }


            //  Misc
            if (physicState == CharaPhysicStateEnum.ResetX)
            {
                rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
                physicState = CharaPhysicStateEnum.Stateless;
            }

            if (physicState == CharaPhysicStateEnum.ResetY)
            {
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0);
                physicState = CharaPhysicStateEnum.Stateless;
            }

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
                if ((Input.GetButtonDown("Keyboard_Jump") || Input.GetButtonDown("Gamepad_Jump")) && groundChecker.GetIsColliding())
                {
                    SetAnimation("Jump", CharaAnimStateEnum.Jump);
                    physicState = CharaPhysicStateEnum.IdleJump;
                }

                //  Crawl
                if (Input.GetAxisRaw("Keyboard_Vertical") < 0 || Input.GetAxisRaw("Gamepad_Vertical") > 0)
                {
                    SetAnimation("Crawl_idle", CharaAnimStateEnum.Crawl_idle);
                }

                //  Fall
                if (!groundChecker.GetIsColliding())
                {
                    SetAnimation("Fall_normal", CharaAnimStateEnum.Fall_normal);
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
                    if ((Input.GetButtonDown("Keyboard_Jump") || Input.GetButtonDown("Gamepad_Jump")) && groundChecker.GetIsColliding())
                    {
                        SetAnimation("Jump_forward", CharaAnimStateEnum.Jump_forward);
                        physicState = CharaPhysicStateEnum.ForwardJumpRight;
                    }
                }

                //  Run Left
                if (Input.GetAxisRaw("Keyboard_Horizontal") < 0 || Input.GetAxisRaw("Gamepad_Horizontal") < 0)
                {
                    FaceLeft();
                    physicState = CharaPhysicStateEnum.RunLeft;

                    //  Jump Forward
                    if ((Input.GetButtonDown("Keyboard_Jump") || Input.GetButtonDown("Gamepad_Jump")) && groundChecker.GetIsColliding())
                    {
                        SetAnimation("Jump_forward", CharaAnimStateEnum.Jump_forward);
                        physicState = CharaPhysicStateEnum.ForwardJumpLeft;
                    }
                }

                //  Slide
                if (!Input.anyKey && Input.GetAxisRaw("Gamepad_Horizontal") == 0)
                {
                    SetAnimation("Slide", CharaAnimStateEnum.Slide);
                    StartCoroutine("StopSlide");
                }

                //  Fall
                if (!groundChecker.GetIsColliding())
                {
                    SetAnimation("Fall_forward", CharaAnimStateEnum.Fall_forward);
                }
            }


            //
            //  Slide actions & Events
            //
            if (animState.Equals(CharaAnimStateEnum.Slide))
            {
                //  Fall
                if (!groundChecker.GetIsColliding())
                {
                    SetAnimation("Fall_forward", CharaAnimStateEnum.Fall_forward);
                }
            }


            //
            // Idle Jump actions & Events
            //
            if (animState.Equals(CharaAnimStateEnum.Jump))
            {
                IdleJumpMovement();

                //  Fall Animation
                if (rigidBody.velocity.y < 0)
                {
                    SetAnimation("Fall_normal", CharaAnimStateEnum.Fall_normal);
                }

                //  Wallslide
                if ((rightWallChecker.GetIsColliding() && (Input.GetAxisRaw("Keyboard_Horizontal") > 0 || Input.GetAxisRaw("Gamepad_Horizontal") > 0)) || (leftWallChecker.GetIsColliding() && (Input.GetAxisRaw("Keyboard_Horizontal") < 0 || Input.GetAxisRaw("Gamepad_Horizontal") < 0)))
                {
                    SetAnimation("Wallslide", CharaAnimStateEnum.Wallslide);
                }
            }


            //
            // Idle Fall actions & Events
            //
            if (animState.Equals(CharaAnimStateEnum.Fall_normal))
            {
                IdleJumpMovement();

                //  Wallslide
                if ((rightWallChecker.GetIsColliding() && (Input.GetAxisRaw("Keyboard_Horizontal") > 0 || Input.GetAxisRaw("Gamepad_Horizontal") > 0)) || (leftWallChecker.GetIsColliding() && (Input.GetAxisRaw("Keyboard_Horizontal") < 0 || Input.GetAxisRaw("Gamepad_Horizontal") < 0)))
                {
                    SetAnimation("Wallslide", CharaAnimStateEnum.Wallslide);
                }

                //  Touch Ground
                if (groundChecker.GetIsColliding())
                {
                    SetAnimation("Idle", CharaAnimStateEnum.Idle);
                }
            }


            //
            //  Forward Jump actions & Events
            //
            if (animState.Equals(CharaAnimStateEnum.Jump_forward))
            {
                SwitchDirection();
                AirDrag();

                //  Fall Animation
                if (rigidBody.velocity.y < 0)
                {
                    SetAnimation("Fall_forward", CharaAnimStateEnum.Fall_forward);
                }

                //  Wallslide
                if ((isFacingRight && rightWallChecker.GetIsColliding()) || (!isFacingRight && leftWallChecker.GetIsColliding()))
                {
                    SetAnimation("Wallslide", CharaAnimStateEnum.Wallslide);
                }
            }


            //
            // Forward Fall actions & Events
            //
            if (animState.Equals(CharaAnimStateEnum.Fall_forward))
            {
                SwitchDirection();
                AirDrag();

                //  Wallslide
                if ((isFacingRight && rightWallChecker.GetIsColliding()) || (!isFacingRight && leftWallChecker.GetIsColliding()))
                {
                    SetAnimation("Wallslide", CharaAnimStateEnum.Wallslide);
                }

                //  Touch Ground
                if (groundChecker.GetIsColliding())
                {
                    if (!Input.anyKey || Input.GetAxisRaw("Horizontal") == 0)
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


            //
            //  Crawl idle actions & Events
            //
            if (animState.Equals(CharaAnimStateEnum.Crawl_idle))
            {
                //  Move
                if (Input.GetAxisRaw("Keyboard_Horizontal") < 0 || Input.GetAxisRaw("Keyboard_Horizontal") > 0 || Input.GetAxisRaw("Gamepad_Horizontal") < 0 || Input.GetAxisRaw("Gamepad_Horizontal") > 0)
                {
                    SetAnimation("Crawl_move", CharaAnimStateEnum.Crawl_move);
                }

                //  Stand Up
                if (Input.GetAxisRaw("Keyboard_Vertical") >= 0 && Input.GetAxisRaw("Gamepad_Vertical") <= 0)
                {
                    SetAnimation("Idle", CharaAnimStateEnum.Idle);
                }

                //  Fall
                if (!groundChecker.GetIsColliding())
                {
                    SetAnimation("Fall_normal", CharaAnimStateEnum.Fall_normal);
                }
            }


            //
            //  Crawl moving actions & Events
            //
            if (animState.Equals(CharaAnimStateEnum.Crawl_move))
            {
                //  Move Crawl Right
                if ((Input.GetAxisRaw("Keyboard_Vertical") < 0 && Input.GetAxisRaw("Keyboard_Horizontal") > 0) || (Input.GetAxisRaw("Gamepad_Vertical") > 0 && Input.GetAxisRaw("Gamepad_Horizontal") > 0))
                {
                    FaceRight();
                    physicState = CharaPhysicStateEnum.CrawlMoveRight;
                }

                //  Move Crawl Left
                if ((Input.GetAxisRaw("Keyboard_Vertical") < 0 && Input.GetAxisRaw("Keyboard_Horizontal") < 0) || (Input.GetAxisRaw("Gamepad_Vertical") > 0 && Input.GetAxisRaw("Gamepad_Horizontal") < 0))
                {
                    FaceLeft();
                    physicState = CharaPhysicStateEnum.CrawlMoveLeft;
                }

                //  Stop
                if (Input.GetAxisRaw("Keyboard_Horizontal") == 0 && Input.GetAxisRaw("Gamepad_Horizontal") == 0)
                {
                    SetAnimation("Crawl_idle", CharaAnimStateEnum.Crawl_idle);
                }

                //  Stand Up
                if ((Input.GetAxisRaw("Keyboard_Vertical") >= 0 && (Input.GetAxisRaw("Keyboard_Horizontal") < 0 || Input.GetAxisRaw("Keyboard_Horizontal") > 0)) || (Input.GetAxisRaw("Gamepad_Vertical") <= 0 && (Input.GetAxisRaw("Gamepad_Horizontal") < 0 || Input.GetAxisRaw("Gamepad_Horizontal") > 0)))
                {
                    SetAnimation("Run", CharaAnimStateEnum.Run);
                }

                //  Fall
                if (!groundChecker.GetIsColliding())
                {
                    SetAnimation("Fall_normal", CharaAnimStateEnum.Fall_normal);
                }
            }


            //
            //  Wallslide actions & Events
            //
            if (animState.Equals(CharaAnimStateEnum.Wallslide))
            {
                //bool loc_hasJumped = false;

                //  Jump Left
                if (!hasWallJumped && isFacingRight && (Input.GetButtonDown("Keyboard_Jump") || Input.GetButtonDown("Gamepad_Jump")) && (Input.GetAxisRaw("Keyboard_Horizontal") <= 0 || Input.GetAxisRaw("Gamepad_Horizontal") <= 0))
                {
                    rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0);
                    //physicState = CharaPhysicStateEnum.ResetY;
                    FaceLeft();
                    physicState = CharaPhysicStateEnum.WallJumpLeft;
                    SetAnimation("Jump_forward", CharaAnimStateEnum.Jump_forward);
                    //loc_hasJumped = true;
                    StartCoroutine("WallJumpTimer");
                }

                //  Jump Right
                if (!hasWallJumped && !isFacingRight && (Input.GetButtonDown("Keyboard_Jump") || Input.GetButtonDown("Gamepad_Jump")) && (Input.GetAxisRaw("Keyboard_Horizontal") >= 0 || Input.GetAxisRaw("Gamepad_Horizontal") >= 0))
                {
                    rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0);
                    //physicState = CharaPhysicStateEnum.ResetY;
                    FaceRight();
                    physicState = CharaPhysicStateEnum.WallJumpRight;
                    SetAnimation("Jump_forward", CharaAnimStateEnum.Jump_forward);
                    //loc_hasJumped = true;
                    StartCoroutine("WallJumpTimer");
                }

                //  Switch Direction Left
                if (isFacingRight && (Input.GetAxisRaw("Keyboard_Horizontal") < 0 || Input.GetAxisRaw("Gamepad_Horizontal") < 0))
                {
                    FaceLeft();
                    SetAnimation("Fall_normal", CharaAnimStateEnum.Fall_normal);
                }

                //  Switch Direction Right
                if (!isFacingRight && (Input.GetAxisRaw("Keyboard_Horizontal") > 0 || Input.GetAxisRaw("Gamepad_Horizontal") > 0))
                {
                    FaceRight();
                    SetAnimation("Fall_normal", CharaAnimStateEnum.Fall_normal);
                }

                //  Touch Ground
                if (groundChecker.GetIsColliding())
                {
                    SetAnimation("Idle", CharaAnimStateEnum.Idle);
                }
            }
        }
    }


    //  Timer functions
    private IEnumerator StopSlide()
    {
        yield return new WaitForSeconds(slideTime);
        SetAnimation("Idle", CharaAnimStateEnum.Idle);
    }

    private IEnumerator WallJumpTimer()
    {
        hasWallJumped = true;
        yield return new WaitForSeconds(wallJumpTimer);
        hasWallJumped = false;
    }


    //  Animation functions
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


    //  Movement functions
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
            physicState = CharaPhysicStateEnum.SwitchDirection;
            FaceRight();
        }

        //  Switch direction to Left
        if (isFacingRight && (Input.GetAxisRaw("Keyboard_Horizontal") < 0 || Input.GetAxisRaw("Gamepad_Horizontal") < 0))
        {
            physicState = CharaPhysicStateEnum.SwitchDirection;
            FaceLeft();
        }
    }

    private void AirDrag()
    {
        if (!Input.anyKey && Input.GetAxisRaw("Gamepad_Horizontal") == 0)
        {
            physicState = CharaPhysicStateEnum.AirDrag;
        }
    }
}
