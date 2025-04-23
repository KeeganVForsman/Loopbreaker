using UnityEngine;

public class OrnsteinEnemy : EnemyBase
{
    public GameObject hitboxPrefab;
    public Transform hitboxSpawnPoint;
    public float hitboxLifetime = 0.5f;
    public float hitstopDuration = 0.1f;

    protected override void Start()
    {
        base.Start();
        moveSpeed = 4f;
        damage = 25f;
        attackCooldown = 3f;
    }

    protected override void Attack()
    {
        base.Attack();

        if (hitboxPrefab && hitboxSpawnPoint)
        {
            GameObject hb = Instantiate(hitboxPrefab, hitboxSpawnPoint.position, hitboxSpawnPoint.rotation);
            Destroy(hb, hitboxLifetime);

            // Trigger hitstop
            if (HitstopManager.Instance != null)
            {
                HitstopManager.Instance.TriggerHitstop(hitstopDuration);
            }
        }
    }
}