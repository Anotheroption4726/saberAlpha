using System.Collections;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    private int directionInt = 1;

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
    private float wallSlideGravity = 0.125f;

    //  Components
    private CharacterPhysicsManagerScript physicsManager;
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
        physicsManager = GetComponent<CharacterPhysicsManagerScript>();
    }


    private void Update()
    {
        if (!Game.GetGamePaused())
        {
            //
            if (directionInt > 0)
            {
                //sprite.flipX = false;
            }
            else
            {
                //sprite.flipX = true;
            }

            //
            // Idle Actions & Events
            //
            if (animState.Equals(CharacterAnimStateEnum.Idle))
            {
                //  Run
                if (ReturnHorizontalInput() > 0 || ReturnHorizontalInput() < 0)
                {
                    SetAnimation("Run", CharacterAnimStateEnum.Run);
                }

                //  Jump
                else if ((Input.GetButtonDown("Keyboard_Jump") || Input.GetButtonDown("Gamepad_Jump")) && groundChecker.GetIsColliding())
                {
                    SetAnimation("Jump", CharacterAnimStateEnum.Jump);
                    physicsManager.AddForceMethod(Vector2.up * jumpImpulse);

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

                //  Run
                if (ReturnHorizontalInput() > 0 || ReturnHorizontalInput() < 0)
                {
                    int loc_directionInt = ReturnHorizontalInput();
                    directionInt = loc_directionInt;

                    ////////////////////////////////////////////////////////////////////////////
                    if (directionInt == 1)
                    {
                        FaceRight();
                    }
                    else
                    {
                        FaceLeft();
                    }
                    ////////////////////////////////////////////////////////////////////////////

                    physicsManager.ChangeVelocityHorizontal(loc_directionInt * runGroundSpeed);

                    //  Jump Forward
                    if ((Input.GetButtonDown("Keyboard_Jump") || Input.GetButtonDown("Gamepad_Jump")) && groundChecker.GetIsColliding())
                    {
                        SetAnimation("Jump_forward", CharacterAnimStateEnum.Jump_forward);
                        physicsManager.AddForceMethod(new Vector2(loc_directionInt * forwardJumpSpeed, jumpImpulse));
                    }

                    // Run Slide
                    else if (canRunSlide && (Input.GetAxisRaw("Keyboard_Vertical") < 0 || Input.GetAxisRaw("Gamepad_Vertical") > 0))
                    {
                        SetAnimation("Run_slide", CharacterAnimStateEnum.Run_slide);
                        StartCoroutine("StopRunSlide");
                        physicsManager.AddForceMethod(new Vector2(loc_directionInt * runSlideImpulse, 0));
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
                if (physicsManager.GetRigidbody().velocity.y < 0)
                {
                    SetAnimation("Fall_normal", CharacterAnimStateEnum.Fall_normal);
                }

                //  Wallslide
                else if ((rightWallChecker.GetIsColliding() && ReturnHorizontalInput() > 0) || (leftWallChecker.GetIsColliding() && ReturnHorizontalInput() < 0))
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
                if ((rightWallChecker.GetIsColliding() && ReturnHorizontalInput() > 0) || (leftWallChecker.GetIsColliding() && ReturnHorizontalInput() < 0))
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
                if (physicsManager.GetRigidbody().velocity.y < 0)
                {
                    SetAnimation("Fall_forward", CharacterAnimStateEnum.Fall_forward);
                }

                //  Wallslide
                else if ((directionInt > 0 && rightWallChecker.GetIsColliding()) || (directionInt < 0 && leftWallChecker.GetIsColliding()))
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
                if ((directionInt > 0 && rightWallChecker.GetIsColliding()) || (directionInt < 0 && leftWallChecker.GetIsColliding()))
                {
                    SetAnimation("Wallslide", CharacterAnimStateEnum.Wallslide);
                }

                //  Touch Ground
                if (groundChecker.GetIsColliding())
                {
                    if (!Input.anyKey && ReturnHorizontalInput() == 0)
                    {
                        physicsManager.AddForceMethod(new Vector2(directionInt * forwardJumpSlideSpeed, 0));
                        SetAnimation("Slide", CharacterAnimStateEnum.Slide);
                        StartCoroutine("StopSlide");
                    }

                    else if (ReturnHorizontalInput() > 0 || ReturnHorizontalInput() < 0)
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
                if (ReturnHorizontalInput() > 0 || ReturnHorizontalInput() < 0)
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
                    physicsManager.ChangeVelocityHorizontal(crawlSpeed);
                }

                //  Move Crawl Left
                else if ((Input.GetAxisRaw("Keyboard_Vertical") < 0 && Input.GetAxisRaw("Keyboard_Horizontal") < 0) || (Input.GetAxisRaw("Gamepad_Vertical") > 0 && Input.GetAxisRaw("Gamepad_Horizontal") < 0))
                {
                    FaceLeft();
                    physicsManager.ChangeVelocityHorizontal(-crawlSpeed);
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
                    //NOT IN FIXEDUPDATE
                    physicsManager.SetRigidBodyGravity(1);
                }

                //
                else if ((Input.GetAxisRaw("Gamepad_Horizontal") > 0 && isFacingRight) || (Input.GetAxisRaw("Keyboard_Horizontal") > 0 && isFacingRight) || (Input.GetAxisRaw("Gamepad_Horizontal") < 0 && !isFacingRight) || (Input.GetAxisRaw("Keyboard_Horizontal") < 0 && !isFacingRight))
                {
                    //NOT IN FIXEDUPDATE
                    physicsManager.SetRigidBodyGravity(wallSlideGravity);
                }

                //  Jump Left
                if (!hasWallJumped && isFacingRight && (Input.GetButtonDown("Keyboard_Jump") || Input.GetButtonDown("Gamepad_Jump")) && (Input.GetAxisRaw("Keyboard_Horizontal") <= 0 || Input.GetAxisRaw("Gamepad_Horizontal") <= 0))
                {
                    //NOT IN FIXED UPDATE
                    physicsManager.SetRigidBodyGravity(1);
                    physicsManager.SetRigidBodyVelocity(new Vector2(physicsManager.GetRigidbody().velocity.x, 0));
                    physicsManager.AddForceMethod(new Vector2(-wallJumpSpeed, wallJumpImpulse));

                    FaceLeft();
                    SetAnimation("Jump_forward", CharacterAnimStateEnum.Jump_forward);
                    StartCoroutine("WallJumpTimer");
                }

                //  Jump Right
                else if (!hasWallJumped && !isFacingRight && (Input.GetButtonDown("Keyboard_Jump") || Input.GetButtonDown("Gamepad_Jump")) && (Input.GetAxisRaw("Keyboard_Horizontal") >= 0 || Input.GetAxisRaw("Gamepad_Horizontal") >= 0))
                {
                    //NOT IN FIXED UPDATE
                    physicsManager.SetRigidBodyGravity(1);
                    physicsManager.SetRigidBodyVelocity(new Vector2(physicsManager.GetRigidbody().velocity.x, 0));
                    physicsManager.AddForceMethod(new Vector2(wallJumpSpeed, wallJumpImpulse));

                    FaceRight();
                    SetAnimation("Jump_forward", CharacterAnimStateEnum.Jump_forward);
                    StartCoroutine("WallJumpTimer");
                }

                //  Wall slide over
                else if ((isFacingRight && !rightWallChecker.GetIsColliding()) || (!isFacingRight && !leftWallChecker.GetIsColliding()))
                {
                    //NOT IN FIXED UPDATE
                    physicsManager.SetRigidBodyGravity(1);
                    SetAnimation("Fall_normal", CharacterAnimStateEnum.Fall_normal);
                }

                //  Switch Direction Left
                if (isFacingRight && (Input.GetAxisRaw("Keyboard_Horizontal") < 0 || Input.GetAxisRaw("Gamepad_Horizontal") < 0))
                {
                    //NOT IN FIXED UPDATE
                    physicsManager.SetRigidBodyGravity(1);
                    FaceLeft();
                    SetAnimation("Fall_normal", CharacterAnimStateEnum.Fall_normal);
                }

                //  Switch Direction Right
                else if (!isFacingRight && (Input.GetAxisRaw("Keyboard_Horizontal") > 0 || Input.GetAxisRaw("Gamepad_Horizontal") > 0))
                {
                    //NOT IN FIXED UPDATE
                    physicsManager.SetRigidBodyGravity(1);
                    FaceRight();
                    SetAnimation("Fall_normal", CharacterAnimStateEnum.Fall_normal);
                }

                //  Touch Ground
                if (groundChecker.GetIsColliding())
                {
                    //NOT IN FIXED UPDATE
                    physicsManager.SetRigidBodyGravity(1);
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
    private int ReturnHorizontalInput()
    {
        int loc_horizonatlInputValue;

        if (Input.GetAxisRaw("Keyboard_Horizontal") < 0 || Input.GetAxisRaw("Gamepad_Horizontal") < 0)
        {
            loc_horizonatlInputValue = -1;
        }
        else if (Input.GetAxisRaw("Keyboard_Horizontal") > 0 || Input.GetAxisRaw("Gamepad_Horizontal") > 0)
        {
            loc_horizonatlInputValue = 1;
        }
        else
        {
            loc_horizonatlInputValue = 0;
        }

        return loc_horizonatlInputValue;
    }

    private void IdleJumpMovement()
    {
        //  Idle Jump move Right
        if (Input.GetAxisRaw("Keyboard_Horizontal") > 0 || Input.GetAxisRaw("Gamepad_Horizontal") > 0)
        {
            FaceRight();
            physicsManager.ChangeVelocityHorizontal(slowAirSpeed);
        }

        //  Idle Jump move Left
        else if (Input.GetAxisRaw("Keyboard_Horizontal") < 0 || Input.GetAxisRaw("Gamepad_Horizontal") < 0)
        {
            FaceLeft();
            physicsManager.ChangeVelocityHorizontal(-slowAirSpeed);
        }
    }

    private void SwitchDirection()
    {
        //  Switch direction to Right
        if (!isFacingRight && (Input.GetAxisRaw("Keyboard_Horizontal") > 0 || Input.GetAxisRaw("Gamepad_Horizontal") > 0))
        {
            physicsManager.SwitchHorizontalDirection();
            FaceRight();
        }

        //  Switch direction to Left
        else if (isFacingRight && (Input.GetAxisRaw("Keyboard_Horizontal") < 0 || Input.GetAxisRaw("Gamepad_Horizontal") < 0))
        {
            physicsManager.SwitchHorizontalDirection();
            FaceLeft();
        }
    }

    private void AirDrag()
    {
        if (!Input.anyKey && Input.GetAxisRaw("Gamepad_Horizontal") == 0)
        {
            physicsManager.HorizontalDrag(forwardJumpAirDrag);
        }
    }
}
