using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 200;
    private int currentHealth;

    public BossHealthBar bossHealthBar; // Drag the boss's UI script in the inspector

    void Start()
    {
        currentHealth = maxHealth;
        if (bossHealthBar != null)
        {
            bossHealthBar.SetHealth(currentHealth, maxHealth);
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (bossHealthBar != null)
        {
            bossHealthBar.TakeDamage(amount);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log(gameObject.name + " died.");
        // Optional: Disable enemy, play animation, etc.
        gameObject.SetActive(false);

        FindObjectOfType<SceneBossManager>()?.RegisterBossDefeat();
    }
}