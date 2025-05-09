using UnityEngine;

public class SmoughEnemy : EnemyBase
{
    public GameObject hitboxPrefab;
    public Transform hitboxSpawnPoint;
    public float hitboxLifetime = 0.5f;
    public float hitstopDuration = 0.1f;
    public Collider2D HitBox;
    public Transform ornstein;

    protected override void Start()
    {
        base.Start();
        moveSpeed = 1.3f;
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
        FlashEffect flash = GetComponent<FlashEffect>();
        if (flash) flash.FlashWhite();

        base.Attack();

        if (hitboxPrefab && hitboxSpawnPoint)
        {
            GameObject hb = Instantiate(hitboxPrefab, hitboxSpawnPoint.position, hitboxSpawnPoint.rotation);
            Destroy(hb, hitboxLifetime);
            //HitBox = hb.GetComponent<Collider2D>();
            // Trigger hitstop
            
            if (HitstopManager.Instance != null)
            {
                HitstopManager.Instance.TriggerHitstop(hitstopDuration);
            }
        }
    }
    public void OnHitboxTrigger(Collider other, float damageAmount)
    {
        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
        Debug.Log("Checking PlayerHealth on " + other.name + ": " + (playerHealth != null));
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damageAmount);
            Debug.Log("Player taking damage :" + damage);
        }
    }
}