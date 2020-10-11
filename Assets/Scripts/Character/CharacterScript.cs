using System.Collections;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    private Character character;

    //  Components
    [SerializeField] private CharacterPhysicsManagerScript physicsManager;
    [SerializeField] private CharacterCollideCheckScript groundChecker;
    [SerializeField] private CharacterCollideCheckScript rightWallChecker;
    [SerializeField] private CharacterCollideCheckScript leftWallChecker;
    [SerializeField] private CharacterAnimManagerScript animManager;
    [SerializeField] private Transform bulletSpawnPoint_horizontal;
    private float bulletSpawnPointPosition_horizontal;

    //  Animations
    private int directionInt = 1;
    [SerializeField] private SpriteRenderer sprite;


    //  Getters and Setters
    public Character GetCharacter()
    {
        return character;
    }

    public CharacterCollideCheckScript GetGroundChecker()
    {
        return groundChecker;
    }

    public int GetDirectionInt()
    {
        return directionInt;
    }

    public Transform GetBulletSpawnPoint_horizontal()
    {
        return bulletSpawnPoint_horizontal;
    }

    public void SetCharacter(Character arg_character)
    {
        character = arg_character;
    }


    //  Awake Function
    private void Awake()
    {
        bulletSpawnPointPosition_horizontal = bulletSpawnPoint_horizontal.localPosition.x;
    }


    //  Update Function
    private void Update()
    {
        if (!Game.GetGamePaused())
        {
            //
            // Idle Actions & Events
            //
            if (animManager.GetAnimState().Equals(CharacterAnimStateEnum.Chara_Idle))
            {
                //  Crawl
                if (ReturnVerticalInput() < 0)
                {
                    animManager.SetAnimation(CharacterAnimStateEnum.Chara_Crawl_idle);
                }

                //  Fall
                if (!groundChecker.GetIsColliding())
                {
                    animManager.SetAnimation(CharacterAnimStateEnum.Chara_Fall_normal);
                }

                // Melee
                if (Input.GetButtonDown("Gamepad_Melee") && ReturnVerticalInput() > 0 && ReturnHorizontalInput() != 0)
                {
                    animManager.SetAnimation(CharacterAnimStateEnum.Chara_Melee_idle_up_diagonal);
                    StartCoroutine(animManager.EndAnimationCoroutine(character.GetMeleeIdleUpDiagonalStopTime(), CharacterAnimStateEnum.Chara_Idle));
                }
                else if (Input.GetButtonDown("Gamepad_Melee") && ReturnVerticalInput() > 0)
                {
                    animManager.SetAnimation(CharacterAnimStateEnum.Chara_Melee_idle_up);
                    StartCoroutine(animManager.EndAnimationCoroutine(character.GetMeleeIdleUpStopTime(), CharacterAnimStateEnum.Chara_Idle));
                }
                else if (Input.GetButtonDown("Gamepad_Melee"))
                {
                    animManager.SetAnimation(CharacterAnimStateEnum.Chara_Melee_idle);
                    StartCoroutine(animManager.EndAnimationCoroutine(character.GetMeleeIdleStopTime(), CharacterAnimStateEnum.Chara_Idle));
                }

                // Shoot
                else if (Input.GetButtonDown("Gamepad_Shoot") && ReturnVerticalInput() > 0 && ReturnHorizontalInput() != 0)
                {
                    animManager.SetAnimation(CharacterAnimStateEnum.Chara_Shoot_idle_up_diagonal);
                    StartCoroutine(animManager.EndAnimationCoroutine(character.GetShootIdleUpDiagonalStopTime(), CharacterAnimStateEnum.Chara_Idle));
                }
                else if (Input.GetButtonDown("Gamepad_Shoot") && ReturnVerticalInput() > 0)
                {
                    animManager.SetAnimation(CharacterAnimStateEnum.Chara_Shoot_idle_up);
                    StartCoroutine(animManager.EndAnimationCoroutine(character.GetShootIdleUpStopTime(), CharacterAnimStateEnum.Chara_Idle));
                }
                else if (Input.GetButtonDown("Gamepad_Shoot"))
                {
                    animManager.SetAnimation(CharacterAnimStateEnum.Chara_Shoot_idle);
                    StartCoroutine(animManager.EndAnimationCoroutine(character.GetShootIdleStopTime(), CharacterAnimStateEnum.Chara_Idle));
                }

                //  Run
                else if (ReturnHorizontalInput() != 0)
                {
                    animManager.SetAnimation(CharacterAnimStateEnum.Chara_Run);
                }

                //  Jump
                else if ((Input.GetButtonDown("Keyboard_Jump") || Input.GetButtonDown("Gamepad_Jump")) && groundChecker.GetIsColliding())
                {
                    animManager.SetAnimation(CharacterAnimStateEnum.Chara_Jump);
                    physicsManager.AddForceMethod(Vector2.up * character.GetIdleJumpVerticalForce());
                }
            }


            //
            // Run Actions & Events
            //
            else if (animManager.GetAnimState().Equals(CharacterAnimStateEnum.Chara_Run))
            {
                //  CanGroundSlide Timer
                if (!animManager.GetTrigger_groundSlide_canGroundSlide())
                {
                    StartCoroutine(animManager.CanGroundSlide());
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
                            animManager.SetAnimation(CharacterAnimStateEnum.Chara_Wallslide);
                        }

                        // Forward Jump
                        else
                        {
                            animManager.SetAnimation(CharacterAnimStateEnum.Chara_Jump_forward);
                            physicsManager.AddForceMethod(new Vector2(loc_directionInt * character.GetForwardJumpHorizontalForce(), character.GetIdleJumpVerticalForce()));
                        }
                    }

                    // Ground Slide
                    else if (animManager.GetTrigger_groundSlide_canGroundSlide() && ReturnVerticalInput() < 0)
                    {
                        animManager.SetAnimation(CharacterAnimStateEnum.Chara_Groundslide);
                        StartCoroutine(animManager.StopGroundSlide());
                        physicsManager.AddForceMethod(new Vector2(loc_directionInt * character.GetGroundSlideHorizontalForce(), 0));
                    }
                }

                //  Stop Slide
                else if (!Input.anyKey && ReturnHorizontalInput() == 0)
                {
                    animManager.SetAnimation(CharacterAnimStateEnum.Chara_Slide);
                    StartCoroutine(animManager.EndAnimationCoroutine(character.GetRunStopSlideTime(), CharacterAnimStateEnum.Chara_Idle));
                }

                //  Fall
                if (!groundChecker.GetIsColliding())
                {
                    animManager.SetAnimation(CharacterAnimStateEnum.Chara_Fall_forward);
                }

                // Melee Run
                if (Input.GetButtonDown("Gamepad_Melee"))
                {
                    animManager.SetAnimation(CharacterAnimStateEnum.Chara_Melee_run);
                    StartCoroutine(animManager.StopMeleeRun());
                }
            }


            //
            //  Slide actions & Events
            //
            else if (animManager.GetAnimState().Equals(CharacterAnimStateEnum.Chara_Slide))
            {
                //  Fall
                if (!groundChecker.GetIsColliding())
                {
                    StopCoroutine("EndAnimationCoroutine");
                    animManager.SetAnimation(CharacterAnimStateEnum.Chara_Fall_normal);
                }
            }


            //
            // Idle Jump actions & Events
            //
            else if (animManager.GetAnimState().Equals(CharacterAnimStateEnum.Chara_Jump))
            {
                physicsManager.SetRigidBodyMaterial(physicsManager.GetColliderMaterialTable()[1]);
                IdleJumpMovement();

                //  Fall Animation
                if (physicsManager.GetRigidbody().velocity.y < 0)
                {
                    animManager.SetAnimation(CharacterAnimStateEnum.Chara_Fall_normal);
                }

                //  Wallslide
                else if ((rightWallChecker.GetIsColliding() && ReturnHorizontalInput() > 0) || (leftWallChecker.GetIsColliding() && ReturnHorizontalInput() < 0))
                {
                    physicsManager.SetRigidBodyMaterial(physicsManager.GetColliderMaterialTable()[0]);
                    animManager.SetAnimation(CharacterAnimStateEnum.Chara_Wallslide);
                }

                JumpMeleeShootActions(CharacterAnimStateEnum.Chara_Fall_normal);
            }


            //
            // Idle Fall actions & Events
            //
            else if (animManager.GetAnimState().Equals(CharacterAnimStateEnum.Chara_Fall_normal))
            {
                physicsManager.SetRigidBodyMaterial(physicsManager.GetColliderMaterialTable()[1]);
                IdleJumpMovement();

                //  Wallslide
                if ((rightWallChecker.GetIsColliding() && ReturnHorizontalInput() > 0) || (leftWallChecker.GetIsColliding() && ReturnHorizontalInput() < 0))
                {
                    physicsManager.SetRigidBodyMaterial(physicsManager.GetColliderMaterialTable()[0]);
                    animManager.SetAnimation(CharacterAnimStateEnum.Chara_Wallslide);
                }

                //  Touch Ground
                if (groundChecker.GetIsColliding())
                {
                    physicsManager.SetRigidBodyMaterial(physicsManager.GetColliderMaterialTable()[0]);
                    animManager.SetAnimation(CharacterAnimStateEnum.Chara_Idle);
                }

                //  Maximum Speed
                if (physicsManager.GetRigidbody().velocity.y < -character.GetFallMaxSpeedVelocityValue())
                {
                    animManager.SetAnimation(CharacterAnimStateEnum.Chara_Fall_maxspeed);
                }

                JumpMeleeShootActions(CharacterAnimStateEnum.Chara_Fall_normal);
            }


            //
            //  Fall maxspeed Action & Events
            //
            else if (animManager.GetAnimState().Equals(CharacterAnimStateEnum.Chara_Fall_maxspeed))
            {
                //IdleJumpMovement();

                //  Touch Ground
                if (groundChecker.GetIsColliding())
                {
                    physicsManager.SetRigidBodyMaterial(physicsManager.GetColliderMaterialTable()[0]);
                    animManager.SetAnimation(CharacterAnimStateEnum.Chara_Ontheground_start);
                }
            }


            //
            //  Forward Jump actions & Events
            //
            else if (animManager.GetAnimState().Equals(CharacterAnimStateEnum.Chara_Jump_forward))
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
                    animManager.SetAnimation(CharacterAnimStateEnum.Chara_Wallslide);
                }

                //  Fall Animation
                else if (physicsManager.GetRigidbody().velocity.y < 0)
                {
                    animManager.SetAnimation(CharacterAnimStateEnum.Chara_Fall_forward);
                }

                JumpMeleeShootActions(CharacterAnimStateEnum.Chara_Fall_forward);
            }


            //
            // Forward Fall actions & Events
            //
            else if (animManager.GetAnimState().Equals(CharacterAnimStateEnum.Chara_Fall_forward))
            {
                SwitchDirectionForwardJump();
                AirDrag();

                //  Wallslide
                if ((directionInt > 0 && rightWallChecker.GetIsColliding()) || (directionInt < 0 && leftWallChecker.GetIsColliding()))
                {
                    physicsManager.SetRigidBodyMaterial(physicsManager.GetColliderMaterialTable()[0]);
                    animManager.SetAnimation(CharacterAnimStateEnum.Chara_Wallslide);
                }

                //  Touch Ground
                if (groundChecker.GetIsColliding())
                {
                    if (!Input.anyKey && ReturnHorizontalInput() == 0)
                    {
                        //physicsManager.AddForceMethod(new Vector2(directionInt * character.GetForwardJumpStopSlideForce(), 0));
                        animManager.SetAnimation(CharacterAnimStateEnum.Chara_Slide);
                        StartCoroutine(animManager.EndAnimationCoroutine(character.GetRunStopSlideTime(), CharacterAnimStateEnum.Chara_Idle));
                    }

                    else if (ReturnHorizontalInput() > 0 || ReturnHorizontalInput() < 0)
                    {
                        animManager.SetAnimation(CharacterAnimStateEnum.Chara_Run);
                    }

                    physicsManager.SetRigidBodyMaterial(physicsManager.GetColliderMaterialTable()[0]);
                }

                //  Maximum Speed
                if (physicsManager.GetRigidbody().velocity.y < -character.GetFallMaxSpeedVelocityValue())
                {
                    animManager.SetAnimation(CharacterAnimStateEnum.Chara_Fall_maxspeed);
                }

                JumpMeleeShootActions(CharacterAnimStateEnum.Chara_Fall_forward);
            }


            //
            //  Melee and Shoot actions & Events
            //
            if (
                    animManager.GetAnimState().Equals(CharacterAnimStateEnum.Chara_Melee_jump) ||
                    animManager.GetAnimState().Equals(CharacterAnimStateEnum.Chara_Melee_jump_up) ||
                    animManager.GetAnimState().Equals(CharacterAnimStateEnum.Chara_Melee_jump_up_diagonal) ||
                    animManager.GetAnimState().Equals(CharacterAnimStateEnum.Chara_Melee_jump_down) ||
                    animManager.GetAnimState().Equals(CharacterAnimStateEnum.Chara_Melee_jump_down_diagonal) ||
                    animManager.GetAnimState().Equals(CharacterAnimStateEnum.Chara_Shoot_jump) ||
                    animManager.GetAnimState().Equals(CharacterAnimStateEnum.Chara_Shoot_jump_up) ||
                    animManager.GetAnimState().Equals(CharacterAnimStateEnum.Chara_Shoot_jump_up_diagonal) ||
                    animManager.GetAnimState().Equals(CharacterAnimStateEnum.Chara_Shoot_jump_down) ||
                    animManager.GetAnimState().Equals(CharacterAnimStateEnum.Chara_Shoot_jump_down_diagonal)
               )
            {
                //  Touch Ground
                if (groundChecker.GetIsColliding())
                {
                    physicsManager.SetRigidBodyMaterial(physicsManager.GetColliderMaterialTable()[0]);
                    animManager.SetAnimation(CharacterAnimStateEnum.Chara_Idle);
                }
            }


            //
            //  Crawl idle actions & Events
            //
            if (animManager.GetAnimState().Equals(CharacterAnimStateEnum.Chara_Crawl_idle))
            {
                //  Move
                if (ReturnHorizontalInput() > 0 || ReturnHorizontalInput() < 0)
                {
                    animManager.SetAnimation(CharacterAnimStateEnum.Chara_Crawl_move);
                }

                //  Stand Up
                else if (ReturnVerticalInput() >= 0)
                {
                    animManager.SetAnimation(CharacterAnimStateEnum.Chara_Idle);
                }

                //  Fall
                if (!groundChecker.GetIsColliding())
                {
                    animManager.SetAnimation(CharacterAnimStateEnum.Chara_Fall_normal);
                }
            }


            //
            //  Crawl moving actions & Events
            //
            if (animManager.GetAnimState().Equals(CharacterAnimStateEnum.Chara_Crawl_move))
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
                    animManager.SetAnimation(CharacterAnimStateEnum.Chara_Crawl_idle);
                }

                //  Stand Up and Run
                else if (ReturnVerticalInput() >= 0 && ReturnHorizontalInput() != 0)
                {
                    animManager.SetAnimation(CharacterAnimStateEnum.Chara_Run);
                }

                //  Fall
                if (!groundChecker.GetIsColliding())
                {
                    animManager.SetAnimation(CharacterAnimStateEnum.Chara_Fall_normal);
                }
            }


            //
            //  Wallslide actions & Events
            //
            if (animManager.GetAnimState().Equals(CharacterAnimStateEnum.Chara_Wallslide))
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
                if (!animManager.GetTrigger_wallJump_hasWallJumped() && (Input.GetButtonDown("Keyboard_Jump") || Input.GetButtonDown("Gamepad_Jump")) && ((ReturnHorizontalInput() <= 0 && directionInt > 0) || (ReturnHorizontalInput() >= 0 && directionInt < 0)))
                {
                    SetDirection(-directionInt);

                    //NOT IN FIXED UPDATE
                    physicsManager.SetRigidBodyGravity(1);
                    physicsManager.SetRigidBodyVelocity(new Vector2(physicsManager.GetRigidbody().velocity.x, 0));
                    physicsManager.AddForceMethod(new Vector2(directionInt * character.GetWallJumpHorizontalForce(), character.GetWallJumpVerticalForce()));
                    animManager.SetAnimation(CharacterAnimStateEnum.Chara_Jump_forward);
                    StartCoroutine(animManager.WallJumpTimer());
                }

                //  No more wall
                else if ((directionInt > 0 && !rightWallChecker.GetIsColliding()) || (directionInt < 0 && !leftWallChecker.GetIsColliding()))
                {
                    //NOT IN FIXED UPDATE
                    physicsManager.SetRigidBodyGravity(1);
                    animManager.SetAnimation(CharacterAnimStateEnum.Chara_Fall_normal);
                }

                //  Switch Direction
                if ((directionInt > 0 && ReturnHorizontalInput() < 0) || (directionInt < 0 && ReturnHorizontalInput() > 0))
                {
                    //NOT IN FIXED UPDATE
                    physicsManager.SetRigidBodyGravity(1);

                    int loc_directionInt = ReturnHorizontalInput();
                    SetDirection(loc_directionInt);
                    animManager.SetAnimation(CharacterAnimStateEnum.Chara_Fall_normal);
                }

                //  Touch Ground
                if (groundChecker.GetIsColliding())
                {
                    //NOT IN FIXED UPDATE
                    physicsManager.SetRigidBodyGravity(1);
                    animManager.SetAnimation(CharacterAnimStateEnum.Chara_Idle);
                }
            }
        }
    }


    //  Player Input

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
        bulletSpawnPoint_horizontal.localPosition = new Vector3(directionInt * bulletSpawnPointPosition_horizontal, bulletSpawnPoint_horizontal.localPosition.y, bulletSpawnPoint_horizontal.localPosition.z);
    }

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


    //  Jump melee & shoot actions
    private void JumpMeleeShootActions(CharacterAnimStateEnum arg_animState)
    {
        // Melee Jump
        if (Input.GetButtonDown("Gamepad_Melee") && ReturnVerticalInput() > 0 && ReturnHorizontalInput() != 0)
        {
            animManager.SetAnimation(CharacterAnimStateEnum.Chara_Melee_jump_up_diagonal);
            StartCoroutine(animManager.EndAnimationCoroutine(character.GetMeleeJumpUpDiagonalStopTime(), arg_animState));
        }
        else if (Input.GetButtonDown("Gamepad_Melee") && ReturnVerticalInput() < 0 && ReturnHorizontalInput() != 0)
        {
            animManager.SetAnimation(CharacterAnimStateEnum.Chara_Melee_jump_down_diagonal);
            StartCoroutine(animManager.EndAnimationCoroutine(character.GetMeleeJumpDownDiagonalStopTime(), arg_animState));
        }
        else if (Input.GetButtonDown("Gamepad_Melee") && ReturnVerticalInput() > 0)
        {
            animManager.SetAnimation(CharacterAnimStateEnum.Chara_Melee_jump_up);
            StartCoroutine(animManager.EndAnimationCoroutine(character.GetMeleeJumpUpStopTime(), arg_animState));
        }
        else if (Input.GetButtonDown("Gamepad_Melee") && ReturnVerticalInput() < 0)
        {
            animManager.SetAnimation(CharacterAnimStateEnum.Chara_Melee_jump_down);
            StartCoroutine(animManager.EndAnimationCoroutine(character.GetMeleeJumpDownStopTime(), arg_animState));
        }
        else if (Input.GetButtonDown("Gamepad_Melee"))
        {
            animManager.SetAnimation(CharacterAnimStateEnum.Chara_Melee_jump);
            StartCoroutine(animManager.EndAnimationCoroutine(character.GetMeleeJumpStopTime(), arg_animState));
        }

        // Shoot Jump
        if (Input.GetButtonDown("Gamepad_Shoot") && ReturnVerticalInput() > 0 && ReturnHorizontalInput() != 0)
        {
            animManager.SetAnimation(CharacterAnimStateEnum.Chara_Shoot_jump_up_diagonal);
            StartCoroutine(animManager.EndAnimationCoroutine(character.GetShootJumpUpDiagonalStopTime(), arg_animState));
        }
        else if (Input.GetButtonDown("Gamepad_Shoot") && ReturnVerticalInput() < 0 && ReturnHorizontalInput() != 0)
        {
            animManager.SetAnimation(CharacterAnimStateEnum.Chara_Shoot_jump_down_diagonal);
            StartCoroutine(animManager.EndAnimationCoroutine(character.GetShootJumpDownDiagonalStopTime(), arg_animState));
        }
        else if (Input.GetButtonDown("Gamepad_Shoot") && ReturnVerticalInput() > 0)
        {
            animManager.SetAnimation(CharacterAnimStateEnum.Chara_Shoot_jump_up);
            StartCoroutine(animManager.EndAnimationCoroutine(character.GetShootJumpUpStopTime(), arg_animState));
        }
        else if (Input.GetButtonDown("Gamepad_Shoot") && ReturnVerticalInput() < 0)
        {
            animManager.SetAnimation(CharacterAnimStateEnum.Chara_Shoot_jump_down);
            StartCoroutine(animManager.EndAnimationCoroutine(character.GetShootJumpDownStopTime(), arg_animState));
        }
        else if (Input.GetButtonDown("Gamepad_Shoot"))
        {
            animManager.SetAnimation(CharacterAnimStateEnum.Chara_Shoot_jump);
            StartCoroutine(animManager.EndAnimationCoroutine(character.GetShootJumpStopTime(), arg_animState));
        }
    }
}
