using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public float speed = 0.8f;
    public float damage;

    private void Update()
    {
        transform.position += new Vector3(speed * Time.fixedDeltaTime, 0, 0);
        // Todo: 타일맵 넘어가면 Destroy
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            if (other.TryGetComponent<HealthSystem>(out HealthSystem healthSystem))
            {
                healthSystem.ChangeHealth(-damage);
                Destroy(gameObject);
            }
        }
        
    }
}
