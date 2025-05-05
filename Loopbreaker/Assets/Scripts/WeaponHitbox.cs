using UnityEngine;

public class WeaponHitbox : MonoBehaviour
{
    public int damage = 20;
    Charizard charizard;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }

            Charizard chariHealth = other.GetComponent<Charizard>();
            if (chariHealth != null)
            {
                chariHealth.TakeDamage(damage);
            }
        }
        
    }
    
}