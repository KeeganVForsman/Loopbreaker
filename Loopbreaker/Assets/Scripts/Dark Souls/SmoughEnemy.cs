using UnityEngine;

public class SmoughEnemy : EnemyBase
{
    public GameObject hitboxPrefab;
    public Transform hitboxSpawnPoint;
    public float hitboxLifetime = 0.5f;
    public float hitstopDuration = 0.1f;

    public Transform ornstein;

    protected override void Start()
    {
        base.Start();
        moveSpeed = 1f;
        damage = 25f;
        attackCooldown = 3f;
    }

    protected override void Update()
    {
        base.Update(); // keep this to retain base functionality
        AvoidOtherBoss(ornstein);
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