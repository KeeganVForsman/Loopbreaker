using UnityEngine;

public class ComboAttack : MonoBehaviour
{
    public GameObject hitboxPrefab;
    public Transform hitboxSpawnPoint;
    public float hitboxLifetime = 0.2f;
    public float comboResetTime = 0.8f;
    public int maxCombo = 3;

    private int currentCombo = 0;
    private float lastAttackTime = 0f;
    private bool canAttack = true;

    void Update()
    {
        if (Time.time - lastAttackTime > comboResetTime)
        {
            currentCombo = 0;
        }
    }

    public void OnAttack()
    {
        if (!canAttack) return;

        if (Time.time - lastAttackTime <= comboResetTime || currentCombo == 0)
        {
            currentCombo++;
            if (currentCombo > maxCombo)
            {
                currentCombo = 1;
            }

            lastAttackTime = Time.time;
            TriggerAttack(currentCombo);
        }
    }

    void TriggerAttack(int comboStep)
    {
        canAttack = false;

        Debug.Log("Attack Step: " + comboStep);

        // Spawn a visible hitbox
        if (hitboxPrefab && hitboxSpawnPoint)
        {
            GameObject hb = Instantiate(hitboxPrefab, hitboxSpawnPoint.position, hitboxSpawnPoint.rotation);
            Destroy(hb, hitboxLifetime);
        }

        Invoke(nameof(ResetAttack), 0.2f);
    }

    void ResetAttack()
    {
        canAttack = true;
    }
}