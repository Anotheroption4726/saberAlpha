﻿using System.Collections;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    private Character character;

    //  Components
    private CharacterPhysicsManagerScript physicsManager;
    [SerializeField] private CharacterCollideCheckScript groundChecker;
    [SerializeField] private CharacterCollideCheckScript rightWallChecker;
    [SerializeField] private CharacterCollideCheckScript leftWallChecker;

    //  Animations
    private CharacterAnimStateEnum animState = CharacterAnimStateEnum.Chara_Idle;
    private int directionInt = 1;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Animator animator;

    //  Getters and Setters
    public Character GetCharacter()
    {
        return character;
    }

    public void SetCharacter(Character arg_character)
    {
        character = arg_character;
    }


    //  Awake Function
    private void Awake()
    {
        physicsManager = GetComponent<CharacterPhysicsManagerScript>();
    }


    //  Update Function
    private void Update()
    {
        if (!Game.GetGamePaused())
        {
            //
            // Idle Actions & Events
            //
            if (animState.Equals(CharacterAnimStateEnum.Chara_Idle))
            {
                //  Crawl
                if (ReturnVerticalInput() < 0)
                {
                    SetAnimation(CharacterAnimStateEnum.Chara_Crawl_idle);
                }

                //  Fall
                if (!groundChecker.GetIsColliding())
                {
                    SetAnimation(CharacterAnimStateEnum.Chara_Fall_normal);
                }

                // Melee
                if (Input.GetButtonDown("Gamepad_Melee") && ReturnVerticalInput() > 0 && ReturnHorizontalInput() != 0)
                {
                    SetAnimation(CharacterAnimStateEnum.Chara_Melee_idle_up_diagonal);
                    StartCoroutine(EndAnimationCoroutine(character.GetMeleeIdleUpDiagonalStopTime(), CharacterAnimStateEnum.Chara_Idle));
                }
                else if (Input.GetButtonDown("Gamepad_Melee") && ReturnVerticalInput() > 0)
                {
                    SetAnimation(CharacterAnimStateEnum.Chara_Melee_idle_up);
                    StartCoroutine(EndAnimationCoroutine(character.GetMeleeIdleUpStopTime(), CharacterAnimStateEnum.Chara_Idle));
                }
                else if (Input.GetButtonDown("Gamepad_Melee"))
                {
                    SetAnimation(CharacterAnimStateEnum.Chara_Melee_idle);
                    StartCoroutine(EndAnimationCoroutine(character.GetMeleeIdleStopTime(), CharacterAnimStateEnum.Chara_Idle));
                }

                // Shoot
                else if (Input.GetButtonDown("Gamepad_Shoot") && ReturnVerticalInput() > 0 && ReturnHorizontalInput() != 0)
                {
                    SetAnimation(CharacterAnimStateEnum.Chara_Shoot_idle_up_diagonal);
                    StartCoroutine(EndAnimationCoroutine(character.GetShootIdleUpDiagonalStopTime(), CharacterAnimStateEnum.Chara_Idle));
                }
                else if (Input.GetButtonDown("Gamepad_Shoot") && ReturnVerticalInput() > 0)
                {
                    SetAnimation(CharacterAnimStateEnum.Chara_Shoot_idle_up);
                    StartCoroutine(EndAnimationCoroutine(character.GetShootIdleUpStopTime(), CharacterAnimStateEnum.Chara_Idle));
                }
                else if (Input.GetButtonDown("Gamepad_Shoot"))
                {
                    SetAnimation(CharacterAnimStateEnum.Chara_Shoot_idle);
                    StartCoroutine(EndAnimationCoroutine(character.GetShootIdleStopTime(), CharacterAnimStateEnum.Chara_Idle));
                }

                //  Run
                else if (ReturnHorizontalInput() != 0)
                {
                    SetAnimation(CharacterAnimStateEnum.Chara_Run);
                }

                //  Jump
                else if ((Input.GetButtonDown("Keyboard_Jump") || Input.GetButtonDown("Gamepad_Jump")) && groundChecker.GetIsColliding())
                {
                    SetAnimation(CharacterAnimStateEnum.Chara_Jump);
                    physicsManager.AddForceMethod(Vector2.up * character.GetIdleJumpVerticalForce());
                }
            }


            //
            // Run Actions & Events
            //
            else if (animState.Equals(CharacterAnimStateEnum.Chara_Run))
            {
                //  CanGroundSlide Timer
                if (!character.GetGroundSlideCanGroundSlide())
                {
                    StartCoroutine("CanGroundSlide");
                }

                //  Run
                if (ReturnHorizontalInput() != 0)
                {
                    int loc_directionInt = ReturnHorizontalInput();
                    SetDirection(loc_directionInt);
                    physicsManager.ChangeVelocityHorizontal(loc_directionInt * character.GetRunMovementSpeed());

                    //  Jump Forward
                    if ((Input.GetButtonDown("Keyboard_Jump") || Input.GetButtonDown("Gamepad_Jump")) && groundChecker.GetIsColliding())
                    {
                        //  Wallslide
                        if ((directionInt > 0 && rightWallChecker.GetIsColliding()) || (directionInt < 0 && leftWallChecker.GetIsColliding()))
                        {
                            //NOT IN FIXEDUPDATE
                            physicsManager.SetRigidBodyVelocity(new Vector2(physicsManager.GetRigidbody().velocity.x, 0));

                            physicsManager.SetRigidBodyMaterial(physicsManager.GetColliderMaterialTable()[0]);
                            SetAnimation(CharacterAnimStateEnum.Chara_Wallslide);
                        }

                        // Forward Jump
                        else
                        {
                            SetAnimation(CharacterAnimStateEnum.Chara_Jump_forward);
                            physicsManager.AddForceMethod(new Vector2(loc_directionInt * character.GetForwardJumpHorizontalForce(), character.GetIdleJumpVerticalForce()));
                        }
                    }

                    // Run Slide
                    else if (character.GetGroundSlideCanGroundSlide() && ReturnVerticalInput() < 0)
                    {
                        SetAnimation(CharacterAnimStateEnum.Chara_Run_slide);
                        StartCoroutine("StopGroundSlide");
                        physicsManager.AddForceMethod(new Vector2(loc_directionInt * character.GetGroundSlideHorizontalForce(), 0));
                    }
                }

                //  Stop Slide
                else if (!Input.anyKey && ReturnHorizontalInput() == 0)
                {
                    SetAnimation(CharacterAnimStateEnum.Chara_Slide);
                    StartCoroutine(EndAnimationCoroutine(character.GetRunStopSlideTime(), CharacterAnimStateEnum.Chara_Idle));
                }

                //  Fall
                if (!groundChecker.GetIsColliding())
                {
                    SetAnimation(CharacterAnimStateEnum.Chara_Fall_forward);
                }

                // Melee Run
                if (Input.GetButtonDown("Gamepad_Melee"))
                {
                    SetAnimation(CharacterAnimStateEnum.Chara_Melee_run);
                    StartCoroutine("StopMeleeRun");
                }
            }


            //
            //  Slide actions & Events
            //
            else if (animState.Equals(CharacterAnimStateEnum.Chara_Slide))
            {
                //  Fall
                if (!groundChecker.GetIsColliding())
                {
                    StopCoroutine("EndAnimationCoroutine");
                    SetAnimation(CharacterAnimStateEnum.Chara_Fall_normal);
                }
            }


            //
            // Idle Jump actions & Events
            //
            else if (animState.Equals(CharacterAnimStateEnum.Chara_Jump))
            {
                physicsManager.SetRigidBodyMaterial(physicsManager.GetColliderMaterialTable()[1]);
                IdleJumpMovement();

                //  Fall Animation
                if (physicsManager.GetRigidbody().velocity.y < 0)
                {
                    SetAnimation(CharacterAnimStateEnum.Chara_Fall_normal);
                }

                //  Wallslide
                else if ((rightWallChecker.GetIsColliding() && ReturnHorizontalInput() > 0) || (leftWallChecker.GetIsColliding() && ReturnHorizontalInput() < 0))
                {
                    physicsManager.SetRigidBodyMaterial(physicsManager.GetColliderMaterialTable()[0]);
                    SetAnimation(CharacterAnimStateEnum.Chara_Wallslide);
                }

                // Melee Jump
                if (Input.GetButtonDown("Gamepad_Melee") && ReturnVerticalInput() > 0 && ReturnHorizontalInput() != 0)
                {
                    SetAnimation(CharacterAnimStateEnum.Chara_Melee_jump_up_diagonal);
                    StartCoroutine(EndAnimationCoroutine(character.GetMeleeJumpUpDiagonalStopTime(), CharacterAnimStateEnum.Chara_Fall_normal));
                }
                else if (Input.GetButtonDown("Gamepad_Melee") && ReturnVerticalInput() < 0 && ReturnHorizontalInput() != 0)
                {
                    SetAnimation(CharacterAnimStateEnum.Chara_Melee_jump_down_diagonal);
                    StartCoroutine(EndAnimationCoroutine(character.GetMeleeJumpDownDiagonalStopTime(), CharacterAnimStateEnum.Chara_Fall_normal));
                }
                else if (Input.GetButtonDown("Gamepad_Melee") && ReturnVerticalInput() > 0)
                {
                    SetAnimation(CharacterAnimStateEnum.Chara_Melee_jump_up);
                    StartCoroutine(EndAnimationCoroutine(character.GetMeleeJumpUpStopTime(), CharacterAnimStateEnum.Chara_Fall_normal));
                }
                else if (Input.GetButtonDown("Gamepad_Melee") && ReturnVerticalInput() < 0)
                {
                    SetAnimation(CharacterAnimStateEnum.Chara_Melee_jump_down);
                    StartCoroutine(EndAnimationCoroutine(character.GetMeleeJumpDownStopTime(), CharacterAnimStateEnum.Chara_Fall_normal));
                }
                else if (Input.GetButtonDown("Gamepad_Melee"))
                {
                    SetAnimation(CharacterAnimStateEnum.Chara_Melee_jump);
                    StartCoroutine(EndAnimationCoroutine(character.GetMeleeJumpStopTime(), CharacterAnimStateEnum.Chara_Fall_normal));
                }

                // Shoot Jump
                if (Input.GetButtonDown("Gamepad_Shoot") && ReturnVerticalInput() > 0 && ReturnHorizontalInput() != 0)
                {
                    SetAnimation(CharacterAnimStateEnum.Chara_Shoot_jump_up_diagonal);
                    StartCoroutine(EndAnimationCoroutine(character.GetShootJumpUpDiagonalStopTime(), CharacterAnimStateEnum.Chara_Fall_normal));
                }
                else if (Input.GetButtonDown("Gamepad_Shoot") && ReturnVerticalInput() < 0 && ReturnHorizontalInput() != 0)
                {
                    SetAnimation(CharacterAnimStateEnum.Chara_Shoot_jump_down_diagonal);
                    StartCoroutine(EndAnimationCoroutine(character.GetShootJumpDownDiagonalStopTime(), CharacterAnimStateEnum.Chara_Fall_normal));
                }
                else if (Input.GetButtonDown("Gamepad_Shoot") && ReturnVerticalInput() > 0)
                {
                    SetAnimation(CharacterAnimStateEnum.Chara_Shoot_jump_up);
                    StartCoroutine(EndAnimationCoroutine(character.GetShootJumpUpStopTime(), CharacterAnimStateEnum.Chara_Fall_normal));
                }
                else if (Input.GetButtonDown("Gamepad_Shoot") && ReturnVerticalInput() < 0)
                {
                    SetAnimation(CharacterAnimStateEnum.Chara_Shoot_jump_down);
                    StartCoroutine(EndAnimationCoroutine(character.GetShootJumpDownStopTime(), CharacterAnimStateEnum.Chara_Fall_normal));
                }
                else if (Input.GetButtonDown("Gamepad_Shoot"))
                {
                    SetAnimation(CharacterAnimStateEnum.Chara_Shoot_jump);
                    StartCoroutine(EndAnimationCoroutine(character.GetShootJumpStopTime(), CharacterAnimStateEnum.Chara_Fall_normal));
                }
            }


            //
            // Idle Fall actions & Events
            //
            else if (animState.Equals(CharacterAnimStateEnum.Chara_Fall_normal))
            {
                physicsManager.SetRigidBodyMaterial(physicsManager.GetColliderMaterialTable()[1]);
                IdleJumpMovement();

                //  Wallslide
                if ((rightWallChecker.GetIsColliding() && ReturnHorizontalInput() > 0) || (leftWallChecker.GetIsColliding() && ReturnHorizontalInput() < 0))
                {
                    physicsManager.SetRigidBodyMaterial(physicsManager.GetColliderMaterialTable()[0]);
                    SetAnimation(CharacterAnimStateEnum.Chara_Wallslide);
                }

                //  Touch Ground
                if (groundChecker.GetIsColliding())
                {
                    physicsManager.SetRigidBodyMaterial(physicsManager.GetColliderMaterialTable()[0]);
                    SetAnimation(CharacterAnimStateEnum.Chara_Idle);
                }

                //  Maximum Speed
                if (physicsManager.GetRigidbody().velocity.y < -character.GetFallMaxSpeedVelocityValue())
                {
                    SetAnimation(CharacterAnimStateEnum.Chara_Fall_maxspeed);
                }
            }


            //
            //  Fall maxspeed Action & Events
            //
            else if (animState.Equals(CharacterAnimStateEnum.Chara_Fall_maxspeed))
            {
                IdleJumpMovement();

                //  Touch Ground
                if (groundChecker.GetIsColliding() && !character.GetOnTheGroundIsOntheGround())
                {
                    physicsManager.SetRigidBodyMaterial(physicsManager.GetColliderMaterialTable()[0]);
                    SetAnimation(CharacterAnimStateEnum.Chara_Ontheground);
                    StartCoroutine("Ontheground");
                    character.SetOnTheGroundIsOntheGround(false);
                }
            }


            //
            //  Forward Jump actions & Events
            //
            else if (animState.Equals(CharacterAnimStateEnum.Chara_Jump_forward))
            {
                physicsManager.SetRigidBodyMaterial(physicsManager.GetColliderMaterialTable()[1]);
                SwitchDirectionForwardJump();
                AirDrag();

                //  Wallslide
                if ((directionInt > 0 && rightWallChecker.GetIsColliding()) || (directionInt < 0 && leftWallChecker.GetIsColliding()))
                {
                    //NOT IN FIXEDUPDATE
                    physicsManager.SetRigidBodyVelocity(new Vector2(physicsManager.GetRigidbody().velocity.x, 0));

                    physicsManager.SetRigidBodyMaterial(physicsManager.GetColliderMaterialTable()[0]);
                    SetAnimation(CharacterAnimStateEnum.Chara_Wallslide);
                }

                //  Fall Animation
                else if (physicsManager.GetRigidbody().velocity.y < 0)
                {
                    SetAnimation(CharacterAnimStateEnum.Chara_Fall_forward);
                }
            }


            //
            // Forward Fall actions & Events
            //
            else if (animState.Equals(CharacterAnimStateEnum.Chara_Fall_forward))
            {
                SwitchDirectionForwardJump();
                AirDrag();

                //  Wallslide
                if ((directionInt > 0 && rightWallChecker.GetIsColliding()) || (directionInt < 0 && leftWallChecker.GetIsColliding()))
                {
                    physicsManager.SetRigidBodyMaterial(physicsManager.GetColliderMaterialTable()[0]);
                    SetAnimation(CharacterAnimStateEnum.Chara_Wallslide);
                }

                //  Touch Ground
                if (groundChecker.GetIsColliding())
                {
                    if (!Input.anyKey && ReturnHorizontalInput() == 0)
                    {
                        //physicsManager.AddForceMethod(new Vector2(directionInt * character.GetForwardJumpStopSlideForce(), 0));
                        SetAnimation(CharacterAnimStateEnum.Chara_Slide);
                        StartCoroutine(EndAnimationCoroutine(character.GetRunStopSlideTime(), CharacterAnimStateEnum.Chara_Idle));
                    }

                    else if (ReturnHorizontalInput() > 0 || ReturnHorizontalInput() < 0)
                    {
                        SetAnimation(CharacterAnimStateEnum.Chara_Run);
                    }

                    physicsManager.SetRigidBodyMaterial(physicsManager.GetColliderMaterialTable()[0]);
                }

                //  Maximum Speed
                if (physicsManager.GetRigidbody().velocity.y < -character.GetFallMaxSpeedVelocityValue())
                {
                    SetAnimation(CharacterAnimStateEnum.Chara_Fall_maxspeed);
                }
            }


            //
            //  Crawl idle actions & Events
            //
            if (animState.Equals(CharacterAnimStateEnum.Chara_Crawl_idle))
            {
                //  Move
                if (ReturnHorizontalInput() > 0 || ReturnHorizontalInput() < 0)
                {
                    SetAnimation(CharacterAnimStateEnum.Chara_Crawl_move);
                }

                //  Stand Up
                else if (ReturnVerticalInput() >= 0)
                {
                    SetAnimation(CharacterAnimStateEnum.Chara_Idle);
                }

                //  Fall
                if (!groundChecker.GetIsColliding())
                {
                    SetAnimation(CharacterAnimStateEnum.Chara_Fall_normal);
                }
            }


            //
            //  Crawl moving actions & Events
            //
            if (animState.Equals(CharacterAnimStateEnum.Chara_Crawl_move))
            {
                //  Move Crawl
                if (ReturnVerticalInput() < 0 && ReturnHorizontalInput() != 0)
                {
                    int loc_directionInt = ReturnHorizontalInput();
                    SetDirection(loc_directionInt);
                    physicsManager.ChangeVelocityHorizontal(loc_directionInt * character.GetCrawlMovementSpeed());
                }

                //  Stop
                else if (ReturnHorizontalInput() == 0)
                {
                    SetAnimation(CharacterAnimStateEnum.Chara_Crawl_idle);
                }

                //  Stand Up and Run
                else if (ReturnVerticalInput() >= 0 && ReturnHorizontalInput() != 0)
                {
                    SetAnimation(CharacterAnimStateEnum.Chara_Run);
                }

                //  Fall
                if (!groundChecker.GetIsColliding())
                {
                    SetAnimation(CharacterAnimStateEnum.Chara_Fall_normal);
                }
            }


            //
            //  Wallslide actions & Events
            //
            if (animState.Equals(CharacterAnimStateEnum.Chara_Wallslide))
            {
                //  Fall resistance
                if (ReturnHorizontalInput() == 0)
                {
                    //NOT IN FIXEDUPDATE
                    physicsManager.SetRigidBodyGravity(1);
                }
                else if ((ReturnHorizontalInput() > 0 && directionInt > 0) || (ReturnHorizontalInput() < 0 && directionInt < 0))
                {
                    //NOT IN FIXEDUPDATE
                    physicsManager.SetRigidBodyGravity(character.GetWallSlideHoldGravity());
                }

                //  Jump Left
                if (!character.GetWallJumpHasWallJumped() && (Input.GetButtonDown("Keyboard_Jump") || Input.GetButtonDown("Gamepad_Jump")) && ((ReturnHorizontalInput() <= 0 && directionInt > 0) || (ReturnHorizontalInput() >= 0 && directionInt < 0)))
                {
                    SetDirection(-directionInt);

                    //NOT IN FIXED UPDATE
                    physicsManager.SetRigidBodyGravity(1);
                    physicsManager.SetRigidBodyVelocity(new Vector2(physicsManager.GetRigidbody().velocity.x, 0));
                    physicsManager.AddForceMethod(new Vector2(directionInt * character.GetWallJumpHorizontalForce(), character.GetWallJumpVerticalForce()));
                    SetAnimation(CharacterAnimStateEnum.Chara_Jump_forward);
                    StartCoroutine("WallJumpTimer");
                }

                //  No more wall
                else if ((directionInt > 0 && !rightWallChecker.GetIsColliding()) || (directionInt < 0 && !leftWallChecker.GetIsColliding()))
                {
                    //NOT IN FIXED UPDATE
                    physicsManager.SetRigidBodyGravity(1);
                    SetAnimation(CharacterAnimStateEnum.Chara_Fall_normal);
                }

                //  Switch Direction
                if ((directionInt > 0 && ReturnHorizontalInput() < 0) || (directionInt < 0 && ReturnHorizontalInput() > 0))
                {
                    //NOT IN FIXED UPDATE
                    physicsManager.SetRigidBodyGravity(1);

                    int loc_directionInt = ReturnHorizontalInput();
                    SetDirection(loc_directionInt);
                    SetAnimation(CharacterAnimStateEnum.Chara_Fall_normal);
                }

                //  Touch Ground
                if (groundChecker.GetIsColliding())
                {
                    //NOT IN FIXED UPDATE
                    physicsManager.SetRigidBodyGravity(1);
                    SetAnimation(CharacterAnimStateEnum.Chara_Idle);
                }
            }
        }
    }


    //  Timer functions
    IEnumerator EndAnimationCoroutine(float arg_time, CharacterAnimStateEnum arg_animState)
    {
        yield return new WaitForSeconds(arg_time);
        SetAnimation(arg_animState);
    }

    private IEnumerator CanGroundSlide()
    {
        yield return new WaitForSeconds(character.GetGroundSlideStartTime());
        character.SetGroundSlideCanGroundSlide(true);
    }

    private IEnumerator StopGroundSlide()
    {
        yield return new WaitForSeconds(character.GetGroundSlideDuration());
        SetAnimation(CharacterAnimStateEnum.Chara_Crawl_move);
        character.SetGroundSlideCanGroundSlide(false);
    }

    private IEnumerator WallJumpTimer()
    {
        character.SetWallJumpHasWallJumped(true);
        yield return new WaitForSeconds(character.GetWallJumpRestrainDuration());
        character.SetWallJumpHasWallJumped(false);
    }

    private IEnumerator Ontheground()
    {
        yield return new WaitForSeconds(character.GetOnTheGroundDuration());
        SetAnimation(CharacterAnimStateEnum.Chara_Ontheground_standup);
        StartCoroutine("OnthegroundStandup");
    }

    private IEnumerator OnthegroundStandup()
    {
        yield return new WaitForSeconds(character.GetOnTheGroundStandUpTime());
        SetAnimation(CharacterAnimStateEnum.Chara_Idle);
        character.SetOnTheGroundIsOntheGround(false);
    }

    private IEnumerator StopMeleeRun()
    {
        yield return new WaitForSeconds(character.GetMeleeRunStopTime());
        //SetAnimation(CharacterAnimStateEnum.Idle);
        SetAnimation(CharacterAnimStateEnum.Chara_Slide);
        StartCoroutine(EndAnimationCoroutine(character.GetRunStopSlideTime(), CharacterAnimStateEnum.Chara_Idle));
    }


    //  Animation functions
    private void SetAnimation(CharacterAnimStateEnum arg_charaAnimStateEnum)
    {
        if (animState == arg_charaAnimStateEnum)
        {
            return;
        }

        animator.Play(arg_charaAnimStateEnum.ToString());
        animState = arg_charaAnimStateEnum;
    }

    private void SetDirection(int arg_direction)
    {
        if (arg_direction > 0)
        {
            sprite.flipX = false;
        }
        else
        {
            sprite.flipX = true;
        }

        directionInt = arg_direction;
    }


    //  Player Input
    private int ReturnHorizontalInput()
    {
        int loc_horizonalInputValue;

        if (Input.GetAxisRaw("Keyboard_Horizontal") < 0 || Input.GetAxisRaw("Gamepad_Horizontal") < 0)
        {
            loc_horizonalInputValue = -1;
        }
        else if (Input.GetAxisRaw("Keyboard_Horizontal") > 0 || Input.GetAxisRaw("Gamepad_Horizontal") > 0)
        {
            loc_horizonalInputValue = 1;
        }
        else
        {
            loc_horizonalInputValue = 0;
        }

        return loc_horizonalInputValue;
    }

    private int ReturnVerticalInput()
    {
        int loc_verticalInputValue;

        if (Input.GetAxisRaw("Keyboard_Vertical") < 0 || Input.GetAxisRaw("Gamepad_Vertical") > 0)
        {
            loc_verticalInputValue = -1;
        }
        else if (Input.GetAxisRaw("Keyboard_Vertical") > 0 || Input.GetAxisRaw("Gamepad_Vertical") < 0)
        {
            loc_verticalInputValue = 1;
        }
        else
        {
            loc_verticalInputValue = 0;
        }

        return loc_verticalInputValue;
    }


    //  Physics & Movement
    private void IdleJumpMovement()
    {
        if (ReturnHorizontalInput() != 0)
        {
            int loc_directionInt = ReturnHorizontalInput();
            SetDirection(loc_directionInt);
            physicsManager.ChangeVelocityHorizontal(loc_directionInt * character.GetIdleJumpMovementSpeed());
        }
    }

    private void SwitchDirectionForwardJump()
    {
        if (ReturnHorizontalInput() != 0 && ReturnHorizontalInput() != directionInt)
        {
            SetDirection(-directionInt);
            physicsManager.SwitchHorizontalDirection();
        }
    }

    private void AirDrag()
    {
        if (!Input.anyKey && ReturnHorizontalInput() == 0)
        {
            physicsManager.HorizontalDrag(character.GetForwardJumpHorizontalAirDrag());
        }
    }
}
