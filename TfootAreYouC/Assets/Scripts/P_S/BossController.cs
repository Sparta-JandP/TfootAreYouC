using UnityEngine;

public class BossController : MonoBehaviour
{
    public int maxHealth = 100;
    private int curHealth;

    private void Start()
    {
        curHealth = maxHealth;
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.CompareTag("Enemy")) // 다른 Collider2D가 "Enemy" 태그를 가진 오브젝트인 경우
        {
            // 적과 충돌했을 때 킹의 체력 감소
            int damage = 20; // 감소시킬 체력 양

            // curHealth를 0 미만으로 가지지 않도록 Mathf.Max 함수를 사용하여 제한
            curHealth = Mathf.Max(curHealth - damage, 0);

            
        }
    }
}