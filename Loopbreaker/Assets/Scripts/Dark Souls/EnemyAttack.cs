using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public GameObject hitboxPrefab;
    public Transform hitboxSpawnPoint;
    public float hitboxLifetime = 0.3f;
    public float attackCooldown = 1.5f;

    private float cooldownTimer = 0f;
    private bool canAttack => cooldownTimer <= 0f;

    void Update()
    {
        if (cooldownTimer > 0f)
            cooldownTimer -= Time.deltaTime;
    }

    public void TryAttack()
    {
        if (!canAttack) return;

        SpawnHitbox();
        cooldownTimer = attackCooldown;
    }

    void SpawnHitbox()
    {
        if (hitboxPrefab && hitboxSpawnPoint)
        {
            GameObject hb = Instantiate(hitboxPrefab, hitboxSpawnPoint.position, hitboxSpawnPoint.rotation);
            Destroy(hb, hitboxLifetime);
        }
    }
}