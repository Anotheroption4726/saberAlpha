using System.Collections;
using UnityEngine;

public class CharacterAnimManagerScript : MonoBehaviour
{
    [SerializeField] CharacterScript characterScript;
    [SerializeField] private GameObject bullet;

    void Shoot_SpawnBullet()
    {
        var loc_bullet = Instantiate(bullet, characterScript.GetBulletSpawnPoint_horizontal().position, Quaternion.LookRotation(characterScript.GetDirectionInt() * transform.forward));
        loc_bullet.GetComponent<BulletScript>().SetDirectionInt(characterScript.GetDirectionInt());
    }
}
