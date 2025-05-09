using UnityEngine;

public class OrnsteinEnemy : EnemyBase
{
    public GameObject hitboxPrefab;
    public Transform hitboxSpawnPoint;
    public float hitboxLifetime = 0.5f;
    public float hitstopDuration = 0.1f;

    public Transform smough;

    protected override void Start()
    {
        base.Start();
        moveSpeed = 3.4f;
        damage = 25f;
        attackCooldown = 3f;
    }

    protected override void Update()
    {
        base.Update(); // keep this to retain base functionality
        AvoidOtherBoss(smough);
    }

    protected override void Attack()
    {

        FlashEffect flash = GetComponent<FlashEffect>();
        if (flash) flash.FlashWhite();

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