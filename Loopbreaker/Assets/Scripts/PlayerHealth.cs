using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100;
    public float currentHealth;
    
    //public Image healthBarFill; // Drag HealthBarFill here in Inspector
    public Slider HealthSlider;
    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateHealthUI()
    {
        if (HealthSlider != null)
        {
            HealthSlider.value = (float)currentHealth / maxHealth;
        }
    }

    void Die()
    {
        Debug.Log("Player Died");
        // Add death logic here (disable movement, respawn, etc.)
        Destroy(gameObject);
    }
}