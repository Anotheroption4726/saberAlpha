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
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private CharacterAnimManagerScript animManager;
    [SerializeField] private Transform bulletSpawnPoint_horizontal;
    [SerializeField] private Transform bulletSpawnPoint_idle_up;
    [SerializeField] private Transform bulletSpawnPoint_jump_up;
    [SerializeField] private Transform bulletSpawnPoint_idle_up_diagonal;
    [SerializeField] private Transform bulletSpawnPoint_jump_up_diagonal;
    private float bulletSpawnPointPosition_horizontal;
    private float bulletSpawnPointPosition_idle_up;
    private float bulletSpawnPointPosition_jump_up;
    private float bulletSpawnPointPosition_idle_up_diagonal;
    private float bulletSpawnPointPosition_jump_up_diagonal;


    //  Getters and Setters
    public Character GetCharacter()
    {
        return character;
    }

    public CharacterCollideCheckScript GetGroundChecker()
    {
        return groundChecker;
    }

    public Transform GetBulletSpawnPoint_horizontal()
    {
        return bulletSpawnPoint_horizontal;
    }

    public Transform GetBulletSpawnPoint_idle_up()
    {
        return bulletSpawnPoint_idle_up;
    }

    public Transform GetBulletSpawnPoint_jump_up()
    {
        return bulletSpawnPoint_jump_up;
    }

    public Transform GetBulletSpawnPoint_idle_up_diagonal()
    {
        return bulletSpawnPoint_idle_up_diagonal;
    }

    public Transform GetBulletSpawnPoint_jump_up_diagonal()
    {
        return bulletSpawnPoint_jump_up_diagonal;
    }

    public void SetCharacter(Character arg_character)
    {
        character = arg_character;
    }


    //  Awake Function
    private void Awake()
    {
        bulletSpawnPointPosition_horizontal = bulletSpawnPoint_horizontal.localPosition.x;
        bulletSpawnPointPosition_idle_up = bulletSpawnPoint_idle_up.localPosition.x;
        bulletSpawnPointPosition_jump_up = bulletSpawnPoint_jump_up.localPosition.x;
        bulletSpawnPointPosition_idle_up_diagonal = bulletSpawnPoint_idle_up_diagonal.localPosition.x;
        bulletSpawnPointPosition_jump_up_diagonal = bulletSpawnPoint_jump_up_diagonal.localPosition.x;
    }


    //  Update Function
    private void Update()
    {
        if (!Game.GetGamePaused())
        {
            //
            // Idle Actions & Events
            //
            if (character.GetAnimState().Equals(CharacterAnimStateEnum.Chara_Idle))
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
                }
                else if (Input.GetButtonDown("Gamepad_Melee") && ReturnVerticalInput() > 0)
                {
                    animManager.SetAnimation(CharacterAnimStateEnum.Chara_Melee_idle_up);
                }
                else if (Input.GetButtonDown("Gamepad_Melee"))
                {
                    animManager.SetAnimation(CharacterAnimStateEnum.Chara_Melee_idle);
                }

                // Shoot
                else if (Input.GetButtonDown("Gamepad_Shoot") && ReturnVerticalInput() > 0 && ReturnHorizontalInput() != 0)
                {
                    animManager.SetAnimation(CharacterAnimStateEnum.Chara_Shoot_idle_up_diagonal);
                }
                else if (Input.GetButtonDown("Gamepad_Shoot") && ReturnVerticalInput() > 0)
                {
                    animManager.SetAnimation(CharacterAnimStateEnum.Chara_Shoot_idle_up);
                }
                else if (Input.GetButtonDown("Gamepad_Shoot"))
                {
                    animManager.SetAnimation(CharacterAnimStateEnum.Chara_Shoot_idle);
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
            else if (character.GetAnimState().Equals(CharacterAnimStateEnum.Chara_Run))
            {
                //  CanGroundSlide Timer
                if (!character.GetTrigger_groundSlide_canGroundSlide())
                {
                    StartCoroutine(CanGroundSlide());
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
                        if ((character.GetDirectionInt() > 0 && rightWallChecker.GetIsColliding()) || (character.GetDirectionInt() < 0 && leftWallChecker.GetIsColliding()))
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
                    else if (character.GetTrigger_groundSlide_canGroundSlide() && ReturnVerticalInput() < 0)
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
                }
            }


            //
            //  Slide actions & Events
            //
            else if (character.GetAnimState().Equals(CharacterAnimStateEnum.Chara_Slide))
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
            else if (character.GetAnimState().Equals(CharacterAnimStateEnum.Chara_Jump))
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
            else if (character.GetAnimState().Equals(CharacterAnimStateEnum.Chara_Fall_normal))
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
            else if (character.GetAnimState().Equals(CharacterAnimStateEnum.Chara_Fall_maxspeed))
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
            else if (character.GetAnimState().Equals(CharacterAnimStateEnum.Chara_Jump_forward))
            {
                physicsManager.SetRigidBodyMaterial(physicsManager.GetColliderMaterialTable()[1]);
                SwitchDirectionForwardJump();
                AirDrag();

                //  Wallslide
                if ((character.GetDirectionInt() > 0 && rightWallChecker.GetIsColliding()) || (character.GetDirectionInt() < 0 && leftWallChecker.GetIsColliding()))
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
            else if (character.GetAnimState().Equals(CharacterAnimStateEnum.Chara_Fall_forward))
            {
                SwitchDirectionForwardJump();
                AirDrag();

                //  Wallslide
                if ((character.GetDirectionInt() > 0 && rightWallChecker.GetIsColliding()) || (character.GetDirectionInt() < 0 && leftWallChecker.GetIsColliding()))
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
                    character.GetAnimState().Equals(CharacterAnimStateEnum.Chara_Melee_jump) ||
                    character.GetAnimState().Equals(CharacterAnimStateEnum.Chara_Melee_jump_up) ||
                    character.GetAnimState().Equals(CharacterAnimStateEnum.Chara_Melee_jump_up_diagonal) ||
                    character.GetAnimState().Equals(CharacterAnimStateEnum.Chara_Melee_jump_down) ||
                    character.GetAnimState().Equals(CharacterAnimStateEnum.Chara_Melee_jump_down_diagonal) ||
                    character.GetAnimState().Equals(CharacterAnimStateEnum.Chara_Shoot_jump) ||
                    character.GetAnimState().Equals(CharacterAnimStateEnum.Chara_Shoot_jump_up) ||
                    character.GetAnimState().Equals(CharacterAnimStateEnum.Chara_Shoot_jump_up_diagonal) ||
                    character.GetAnimState().Equals(CharacterAnimStateEnum.Chara_Shoot_jump_down) ||
                    character.GetAnimState().Equals(CharacterAnimStateEnum.Chara_Shoot_jump_down_diagonal)
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
            if (character.GetAnimState().Equals(CharacterAnimStateEnum.Chara_Crawl_idle))
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
            if (character.GetAnimState().Equals(CharacterAnimStateEnum.Chara_Crawl_move))
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
            if (character.GetAnimState().Equals(CharacterAnimStateEnum.Chara_Wallslide))
            {
                //  Fall resistance
                if (ReturnHorizontalInput() == 0)
                {
                    //NOT IN FIXEDUPDATE
                    physicsManager.SetRigidBodyGravity(1);
                }
                else if ((ReturnHorizontalInput() > 0 && character.GetDirectionInt() > 0) || (ReturnHorizontalInput() < 0 && character.GetDirectionInt() < 0))
                {
                    //NOT IN FIXEDUPDATE
                    physicsManager.SetRigidBodyGravity(character.GetWallSlideHoldGravity());
                }

                //  Jump
                if (!character.GetTrigger_wallJump_hasWallJumped() && (Input.GetButtonDown("Keyboard_Jump") || Input.GetButtonDown("Gamepad_Jump")) && ((ReturnHorizontalInput() <= 0 && character.GetDirectionInt() > 0) || (ReturnHorizontalInput() >= 0 && character.GetDirectionInt() < 0)))
                {
                    SetDirection(-character.GetDirectionInt());

                    //NOT IN FIXED UPDATE
                    physicsManager.SetRigidBodyGravity(1);
                    physicsManager.SetRigidBodyVelocity(new Vector2(physicsManager.GetRigidbody().velocity.x, 0));
                    physicsManager.AddForceMethod(new Vector2(character.GetDirectionInt() * character.GetWallJumpHorizontalForce(), character.GetWallJumpVerticalForce()));
                    animManager.SetAnimation(CharacterAnimStateEnum.Chara_Jump_forward);
                    StartCoroutine(WallJumpTimer());
                }

                //  No more wall
                else if ((character.GetDirectionInt() > 0 && !rightWallChecker.GetIsColliding()) || (character.GetDirectionInt() < 0 && !leftWallChecker.GetIsColliding()))
                {
                    //NOT IN FIXED UPDATE
                    physicsManager.SetRigidBodyGravity(1);
                    animManager.SetAnimation(CharacterAnimStateEnum.Chara_Fall_normal);
                }

                //  Switch Direction
                if ((character.GetDirectionInt() > 0 && ReturnHorizontalInput() < 0) || (character.GetDirectionInt() < 0 && ReturnHorizontalInput() > 0))
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
            bulletSpawnPoint_horizontal.localRotation = Quaternion.LookRotation(new Vector3(0, 0, 1));
            bulletSpawnPoint_idle_up_diagonal.localRotation = Quaternion.LookRotation(new Vector3(0, 0, 1), new Vector3(-1, 1, 0));
            bulletSpawnPoint_jump_up_diagonal.localRotation = Quaternion.LookRotation(new Vector3(0, 0, 1), new Vector3(-1, 1, 0));
        }
        else
        {
            sprite.flipX = true;
            bulletSpawnPoint_horizontal.localRotation = Quaternion.LookRotation(new Vector3(0, 0, -1));
            bulletSpawnPoint_idle_up_diagonal.localRotation = Quaternion.LookRotation(new Vector3(0, 0, -1), new Vector3(1, 1, 0));
            bulletSpawnPoint_jump_up_diagonal.localRotation = Quaternion.LookRotation(new Vector3(0, 0, -1), new Vector3(1, 1, 0));
        }

        character.SetDirectionInt(arg_direction);
        bulletSpawnPoint_horizontal.localPosition = new Vector3(character.GetDirectionInt() * bulletSpawnPointPosition_horizontal, bulletSpawnPoint_horizontal.localPosition.y, bulletSpawnPoint_horizontal.localPosition.z);
        bulletSpawnPoint_idle_up.localPosition = new Vector3(character.GetDirectionInt() * bulletSpawnPointPosition_idle_up, bulletSpawnPoint_idle_up.localPosition.y, bulletSpawnPoint_idle_up.localPosition.z);
        bulletSpawnPoint_jump_up.localPosition = new Vector3(character.GetDirectionInt() * bulletSpawnPointPosition_jump_up, bulletSpawnPoint_jump_up.localPosition.y, bulletSpawnPoint_jump_up.localPosition.z);
        bulletSpawnPoint_idle_up_diagonal.localPosition = new Vector3(character.GetDirectionInt() * bulletSpawnPointPosition_idle_up_diagonal, bulletSpawnPoint_idle_up_diagonal.localPosition.y, bulletSpawnPoint_idle_up_diagonal.localPosition.z);
        bulletSpawnPoint_jump_up_diagonal.localPosition = new Vector3(character.GetDirectionInt() * bulletSpawnPointPosition_jump_up_diagonal, bulletSpawnPoint_jump_up_diagonal.localPosition.y, bulletSpawnPoint_jump_up_diagonal.localPosition.z);
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


    //  Coroutines
    public IEnumerator CanGroundSlide()
    {
        yield return new WaitForSeconds(character.GetGroundSlideCanStartTime());
        character.SetTrigger_groundSlide_canGroundSlide(true);
    }

    public IEnumerator WallJumpTimer()
    {
        character.SetTrigger_wallJump_hasWallJumped(true);
        yield return new WaitForSeconds(character.GetWallJumpRestrainDuration());
        character.SetTrigger_wallJump_hasWallJumped(false);
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
        if (ReturnHorizontalInput() != 0 && ReturnHorizontalInput() != character.GetDirectionInt())
        {
            SetDirection(-character.GetDirectionInt());
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
            animManager.SetAnimStateAfterJumpAttack(arg_animState);
        }
        else if (Input.GetButtonDown("Gamepad_Melee") && ReturnVerticalInput() < 0 && ReturnHorizontalInput() != 0)
        {
            animManager.SetAnimation(CharacterAnimStateEnum.Chara_Melee_jump_down_diagonal);
            animManager.SetAnimStateAfterJumpAttack(arg_animState);
        }
        else if (Input.GetButtonDown("Gamepad_Melee") && ReturnVerticalInput() > 0)
        {
            animManager.SetAnimation(CharacterAnimStateEnum.Chara_Melee_jump_up);
            animManager.SetAnimStateAfterJumpAttack(arg_animState);
        }
        else if (Input.GetButtonDown("Gamepad_Melee") && ReturnVerticalInput() < 0)
        {
            animManager.SetAnimation(CharacterAnimStateEnum.Chara_Melee_jump_down);
            animManager.SetAnimStateAfterJumpAttack(arg_animState);
        }
        else if (Input.GetButtonDown("Gamepad_Melee"))
        {
            animManager.SetAnimation(CharacterAnimStateEnum.Chara_Melee_jump);
            animManager.SetAnimStateAfterJumpAttack(arg_animState);
        }

        // Shoot Jump
        if (Input.GetButtonDown("Gamepad_Shoot") && ReturnVerticalInput() > 0 && ReturnHorizontalInput() != 0)
        {
            animManager.SetAnimation(CharacterAnimStateEnum.Chara_Shoot_jump_up_diagonal);
            animManager.SetAnimStateAfterJumpAttack(arg_animState);
        }
        else if (Input.GetButtonDown("Gamepad_Shoot") && ReturnVerticalInput() < 0 && ReturnHorizontalInput() != 0)
        {
            animManager.SetAnimation(CharacterAnimStateEnum.Chara_Shoot_jump_down_diagonal);
            animManager.SetAnimStateAfterJumpAttack(arg_animState);
        }
        else if (Input.GetButtonDown("Gamepad_Shoot") && ReturnVerticalInput() > 0)
        {
            animManager.SetAnimation(CharacterAnimStateEnum.Chara_Shoot_jump_up);
            animManager.SetAnimStateAfterJumpAttack(arg_animState);
        }
        else if (Input.GetButtonDown("Gamepad_Shoot") && ReturnVerticalInput() < 0)
        {
            animManager.SetAnimation(CharacterAnimStateEnum.Chara_Shoot_jump_down);
            animManager.SetAnimStateAfterJumpAttack(arg_animState);
        }
        else if (Input.GetButtonDown("Gamepad_Shoot"))
        {
            animManager.SetAnimation(CharacterAnimStateEnum.Chara_Shoot_jump);
            animManager.SetAnimStateAfterJumpAttack(arg_animState);
        }
    }
}
