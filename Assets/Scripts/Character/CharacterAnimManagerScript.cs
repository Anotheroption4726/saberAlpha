using System.Collections;
using UnityEngine;

public class CharacterAnimManagerScript : MonoBehaviour
{
    [SerializeField] GameObject characterGameObject;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject bullet;

    CharacterScript characterScript;
    private CharacterAnimStateEnum animStateAfterJumpAttack;


    //  Awake function
    private void Awake()
    {
        characterScript = characterGameObject.GetComponent<CharacterScript>();
    }


    // Setters
    public void SetAnimStateAfterJumpAttack(CharacterAnimStateEnum arg_animStateAfterJumpAttack)
    {
        animStateAfterJumpAttack = arg_animStateAfterJumpAttack;
    }


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


    // OntheGround animation frame triggers
    private void OnTheGround()
    {
        SetAnimation(CharacterAnimStateEnum.Chara_Ontheground);
        StartCoroutine(EndAnimationCoroutine(characterScript.GetCharacter().GetOnTheGroundDuration(), CharacterAnimStateEnum.Chara_Ontheground_standup));
    }

    private void OnTheGroundStandUp()
    {
        SetAnimation(CharacterAnimStateEnum.Chara_Idle);
    }


    // Attack animation frame triggers
    private void attackIdleEnd()
    {
        SetAnimation(CharacterAnimStateEnum.Chara_Idle);
    }

    private void attackJumpEnd()
    {
        SetAnimation(animStateAfterJumpAttack);
    }

    private void meleeRunEnd()
    {
        SetAnimation(CharacterAnimStateEnum.Chara_Slide);
        StartCoroutine(EndAnimationCoroutine(characterScript.GetCharacter().GetRunStopSlideTime(), CharacterAnimStateEnum.Chara_Idle));
    }

    private void Shoot_SpawnBullet()
    {
        var loc_bullet = Instantiate(bullet, characterScript.GetBulletSpawnPoint_horizontal().position, characterScript.GetBulletSpawnPoint_horizontal().rotation);
        loc_bullet.GetComponent<BulletScript>().SetCharaShootAnimState(characterScript.GetCharacter().GetAnimState());
    }

    private void Shoot_SpawnBulletIdleUp()
    {
        var loc_bullet = Instantiate(bullet, characterScript.GetBulletSpawnPoint_idle_up().position, characterScript.GetBulletSpawnPoint_idle_up().rotation);
        loc_bullet.GetComponent<BulletScript>().SetCharaShootAnimState(characterScript.GetCharacter().GetAnimState());
    }

    private void Shoot_SpawnBulletJumpUp()
    {
        var loc_bullet = Instantiate(bullet, characterScript.GetBulletSpawnPoint_jump_up().position, characterScript.GetBulletSpawnPoint_jump_up().rotation);
        loc_bullet.GetComponent<BulletScript>().SetCharaShootAnimState(characterScript.GetCharacter().GetAnimState());
    }

    private void Shoot_SpawnBulletIdleUpDiagonal()
    {
        var loc_bullet = Instantiate(bullet, characterScript.GetBulletSpawnPoint_jump_up_diagonal().position, characterScript.GetBulletSpawnPoint_jump_up_diagonal().rotation);
        loc_bullet.GetComponent<BulletScript>().SetCharaShootAnimState(characterScript.GetCharacter().GetAnimState());
    }

    private void Shoot_SpawnBulletJumpUpDiagonal()
    {
        
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

    public IEnumerator StopGroundSlide()
    {
        yield return new WaitForSeconds(characterScript.GetCharacter().GetGroundSlideDuration());
        SetAnimation(CharacterAnimStateEnum.Chara_Crawl_move);
        characterScript.GetCharacter().SetTrigger_groundSlide_canGroundSlide(false);
    }
}
