using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RangedAttack : MonoBehaviour, IEffect
{
    public GameObject bullet;
    public Transform shooterOrigin;
    public LayerMask shootMask;

    private GameObject target;
    private bool canShoot;
    private float range;

    public event Action OnApplyingEffect;

    private void Awake()
    {
        range = 7;  //TODO: TileMap 끝 위치값 가져와서 오브젝트와의 거리 계산하기
    }

    public void ApplyEffect(int power, float rate)
    {
        Invoke("ResetCooldown", rate);
        StartCoroutine(ShootingRoutine(power, rate));
    }

    IEnumerator ShootingRoutine(int power, float rate)
    {
        while (true)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, range, shootMask);

            if (hit.collider && canShoot)
            {
                target = hit.collider.gameObject;
                OnApplyingEffect?.Invoke();
                yield return new WaitForSeconds(0.9f);
                Shoot(power);
                yield return new WaitForSeconds(rate);  // 쿨다운 시간만큼 기다린 후 다시 시작
            }
            else
            {
                yield return null;  // 다음 프레임까지 기다림
            }
        }
    }

    void ResetCooldown()
    {
        canShoot = true;
    }

    void Shoot(int power)
    {
        GameObject myBullet = Instantiate(bullet, shooterOrigin.position, Quaternion.identity); 
        myBullet.GetComponent<ProjectileController>().damage = power; // 퀸의 effect(attack) power를 bullet에 반영하기
        StageManager.instance.StageObjects.Add(myBullet);
    }
}
