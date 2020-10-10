using System.Collections;
using UnityEngine;

public class CharacterAnimManagerScript : MonoBehaviour
{
    [SerializeField] CharacterScript characterScript;
    [SerializeField] private GameObject bullet;

    void OnTheGround()
    {
        characterScript.SetAnimation(CharacterAnimStateEnum.Chara_Ontheground);
        StartCoroutine(Ontheground());
    }

    void OnTheGroundStandUp()
    {
        characterScript.SetAnimation(CharacterAnimStateEnum.Chara_Idle);
    }

    void Shoot_SpawnBullet()
    {
        var loc_bullet = Instantiate(bullet, characterScript.GetBulletSpawnPoint_horizontal().position, Quaternion.LookRotation(characterScript.GetDirectionInt() * transform.forward));
        loc_bullet.GetComponent<BulletScript>().SetDirectionInt(characterScript.GetDirectionInt());
    }

    // Coroutines
    public IEnumerator Ontheground()
    {
        yield return new WaitForSeconds(characterScript.GetCharacter().GetOnTheGroundDuration());
        characterScript.SetAnimation(CharacterAnimStateEnum.Chara_Ontheground_standup);
    }
}
