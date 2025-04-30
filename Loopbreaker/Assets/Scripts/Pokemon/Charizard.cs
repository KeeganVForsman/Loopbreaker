using UnityEngine;
public class Charizard : EnemyBase
{
    public GameObject hitboxPrefab;
    public Transform hitboxSpawnPoint;
    public float hitboxLifetime = 0.5f;
    public float hitstopDuration = 0.1f;
    protected override void Start()
    {
        base.Start();
        moveSpeed = 2f;
        damage = 15f;
        attackCooldown = 3f;
    }

    protected override void Attack()
    {
        base.Attack();
        GameObject hb = Instantiate(hitboxPrefab, hitboxSpawnPoint.position, hitboxSpawnPoint.rotation);
        Destroy(hb, hitboxLifetime);

        // Trigger hitstop
        if (HitstopManager.Instance != null)
        {
            HitstopManager.Instance.TriggerHitstop(hitstopDuration);
        }
    }
}
