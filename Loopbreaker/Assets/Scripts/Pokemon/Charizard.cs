using UnityEngine;
using UnityEngine.UI;
public class Charizard : EnemyBase
{
    public GameObject hitboxPrefab;
    public Transform hitboxSpawnPoint;
    public float hitboxLifetime = 0.5f;
    public float hitstopDuration = 0.1f;
    public Collider hitBox;
    public float maxHealth = 100;
    public float currentHealth;

    //public Image healthBarFill; // Drag HealthBarFill here in Inspector
    public Slider HealthSlider;
    protected override void Start()
    {
        base.Start();
        moveSpeed = 2f;
        damage = 15f;
        attackCooldown = 3f;

        currentHealth = maxHealth;
        HealthSlider.value = currentHealth / maxHealth;
    }

    protected override void Attack()
    {
        base.Attack();
        GameObject hb = Instantiate(hitboxPrefab, hitboxSpawnPoint.position, hitboxSpawnPoint.rotation);
        hitBox = hb.GetComponent<Collider>();
        Destroy(hb, hitboxLifetime);
        if (hitBox != null)
        {
            HitBoxCollisionHandler handler = hb.AddComponent<HitBoxCollisionHandler>();
            handler.Setup(this, damage);
        }
        // Trigger hitstop
        if (HitstopManager.Instance != null)
        {
            HitstopManager.Instance.TriggerHitstop(hitstopDuration);
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

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (HealthSlider != null)
        {
            HealthSlider.value = currentHealth / maxHealth;
        }
        Debug.Log(gameObject.name + "Took " + amount + "Damage. Current health" + currentHealth);

        if (currentHealth<=0)
        {
            Die();
            
        }
    }
    public void Die()
    {
        Debug.Log(gameObject.name + "is dead");
        FindObjectOfType<SceneBossManager>()?.RegisterBossDefeat();
        gameObject.SetActive(false);
        //Destroy(gameObject);

    }

}
