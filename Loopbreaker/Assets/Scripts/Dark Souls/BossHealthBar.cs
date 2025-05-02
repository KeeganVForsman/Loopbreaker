using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    public int maxHealth = 200;
    public int currentHealth;

    public Image healthBarFill;

    void Start()
    {
        if (currentHealth == 0)
        {
            currentHealth = maxHealth;
        }
        UpdateHealthUI();
    }

    public void SetHealth(int current, int max)
    {
        currentHealth = current;
        maxHealth = max;
        UpdateHealthUI();
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateHealthUI()
    {
        if (healthBarFill)
        {
            healthBarFill.fillAmount = (float)currentHealth / maxHealth;
        }
    }

    void Die()
    {
        Debug.Log(gameObject.name + " (UI) says: Boss died.");
        // Optional: play UI fade or hide animation
    }
}