using System.Collections;
using UnityEngine;

public class CharacterAnimManagerScript : MonoBehaviour
{
    [SerializeField] CharacterScript characterScript;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject bullet;


    //  Change Animation and state
    public void SetAnimation(CharacterAnimStateEnum arg_charaAnimStateEnum)
    {
        if (characterScript.GetCharacter().GetAnimState() == arg_charaAnimStateEnum)
        {
            return;
        }

        animator.Play(arg_charaAnimStateEnum.ToString());
        characterScript.GetCharacter().SetAnimState(arg_charaAnimStateEnum);
    }


    // Animation Frames triggers
    private void OnTheGround()
    {
        SetAnimation(CharacterAnimStateEnum.Chara_Ontheground);
        StartCoroutine(EndAnimationCoroutine(characterScript.GetCharacter().GetOnTheGroundDuration(), CharacterAnimStateEnum.Chara_Ontheground_standup));
    }

    private void OnTheGroundStandUp()
    {
        SetAnimation(CharacterAnimStateEnum.Chara_Idle);
    }

    private void Shoot_SpawnBullet()
    {
        var loc_bullet = Instantiate(bullet, characterScript.GetBulletSpawnPoint_horizontal().position, Quaternion.LookRotation(characterScript.GetCharacter().GetDirectionInt() * transform.forward));
        loc_bullet.GetComponent<BulletScript>().SetDirectionInt(characterScript.GetCharacter().GetDirectionInt());
    }


    // Coroutines
    public IEnumerator EndAnimationCoroutine(float arg_time, CharacterAnimStateEnum arg_animState)
    {
        if (
                    characterScript.GetCharacter().GetAnimState().Equals(CharacterAnimStateEnum.Chara_Melee_jump) ||
                    characterScript.GetCharacter().GetAnimState().Equals(CharacterAnimStateEnum.Chara_Melee_jump_up) ||
                    characterScript.GetCharacter().GetAnimState().Equals(CharacterAnimStateEnum.Chara_Melee_jump_up_diagonal) ||
                    characterScript.GetCharacter().GetAnimState().Equals(CharacterAnimStateEnum.Chara_Melee_jump_down) ||
                    characterScript.GetCharacter().GetAnimState().Equals(CharacterAnimStateEnum.Chara_Melee_jump_down_diagonal) ||
                    characterScript.GetCharacter().GetAnimState().Equals(CharacterAnimStateEnum.Chara_Shoot_jump) ||
                    characterScript.GetCharacter().GetAnimState().Equals(CharacterAnimStateEnum.Chara_Shoot_jump_up) ||
                    characterScript.GetCharacter().GetAnimState().Equals(CharacterAnimStateEnum.Chara_Shoot_jump_up_diagonal) ||
                    characterScript.GetCharacter().GetAnimState().Equals(CharacterAnimStateEnum.Chara_Shoot_jump_down) ||
                    characterScript.GetCharacter().GetAnimState().Equals(CharacterAnimStateEnum.Chara_Shoot_jump_down_diagonal)
               )
        {
            //  Touch Ground
            if (characterScript.GetGroundChecker().GetIsColliding())
            {
                yield break;
            }
        }
        yield return new WaitForSeconds(arg_time);
        SetAnimation(arg_animState);
    }

    public IEnumerator CanGroundSlide()
    {
        yield return new WaitForSeconds(characterScript.GetCharacter().GetGroundSlideStartTime());
        characterScript.GetCharacter().SetTrigger_groundSlide_canGroundSlide(true);
    }

    public IEnumerator StopGroundSlide()
    {
        yield return new WaitForSeconds(characterScript.GetCharacter().GetGroundSlideDuration());
        SetAnimation(CharacterAnimStateEnum.Chara_Crawl_move);
        characterScript.GetCharacter().SetTrigger_groundSlide_canGroundSlide(false);
    }

    public IEnumerator WallJumpTimer()
    {
        characterScript.GetCharacter().SetTrigger_wallJump_hasWallJumped(true);
        yield return new WaitForSeconds(characterScript.GetCharacter().GetWallJumpRestrainDuration());
        characterScript.GetCharacter().SetTrigger_wallJump_hasWallJumped(false);
    }

    public IEnumerator StopMeleeRun()
    {
        yield return new WaitForSeconds(characterScript.GetCharacter().GetMeleeRunStopTime());
        SetAnimation(CharacterAnimStateEnum.Chara_Slide);
        StartCoroutine(EndAnimationCoroutine(characterScript.GetCharacter().GetRunStopSlideTime(), CharacterAnimStateEnum.Chara_Idle));
    }
}
