using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ComboAttack : MonoBehaviour
{
    public GameObject hitboxPrefab;
    public Transform hitboxSpawnPoint;
    public float hitboxLifetime = 0.2f;
    public float comboResetTime = 0.8f;
    public int maxCombo = 3;

    public float lungeDistance = 5f;
    public float lungeDuration = 0.1f;

    public float moveLockDuration = 0.15f;
    public float thirdAttackLungeForce = 4f;

    private int currentCombo = 0;
    private float lastAttackTime = 0f;
    private bool canAttack = true;

    [Header("Aim Assist Settings")]
    public float aimAssistRange = 3f;
    public float aimAssistAngle = 45f;
    public LayerMask enemyLayer;

    [Header("Sword Swing Settings")]
    public SwordSwing swordSwing; // 


    private Rigidbody rb;
    private PlayerMovement playerMovement;



    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerMovement = GetComponent<PlayerMovement>();

        if (swordSwing == null)
        {
            Debug.LogWarning("SwordSwing not assigned on ComboAttack! Assign it in the inspector.");
        }
    }

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
            StartCoroutine(TriggerAttack(currentCombo));
        }
    }

    System.Collections.IEnumerator TriggerAttack(int comboStep)
    {
        canAttack = false;

        // Lock player input
        if (playerMovement != null)
        {
            playerMovement.enabled = false;
            rb.velocity = Vector3.zero; // Stop movement immediately
        }

        ApplyAimAssist();

        Debug.Log("Attack Step: " + comboStep);

        // Start sword swing!
        if (swordSwing != null)
        {
            swordSwing.StartSwing(comboStep);
        }

        // Spawn a visible hitbox
        if (hitboxPrefab && hitboxSpawnPoint)
        {
            GameObject hb = Instantiate(hitboxPrefab, hitboxSpawnPoint.position, hitboxSpawnPoint.rotation);
            Destroy(hb, hitboxLifetime);
        }

        // Third hit gets a lunge
        if (comboStep == 3)
        {
            Vector3 lungeDirection = transform.up;
            rb.velocity = lungeDirection * lungeDistance;

            yield return new WaitForSeconds(lungeDuration);
            rb.velocity = Vector3.zero; // Stop after lunge
        }
        else
        {
            yield return new WaitForSeconds(moveLockDuration);
        }

        if (playerMovement != null)
        {
            rb.velocity = Vector3.zero;
            playerMovement.enabled = true;
        }

        // Reset sword transform
        if (swordSwing != null)
        {
            swordSwing.ResetWeapon();
        }

        canAttack = true;
    }

    void ApplyAimAssist()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, aimAssistRange, enemyLayer);

        Transform bestTarget = null;
        float closestAngle = aimAssistAngle;

        Debug.Log("Found enemies: " + hits.Length);

        foreach (Collider hit in hits)
        {
            Vector3 toTarget = (hit.transform.position - transform.position).normalized;
            float angle = Vector3.Angle(transform.up, toTarget); // Assuming 'up' is forward

            if (angle < closestAngle)
            {
                closestAngle = angle;
                bestTarget = hit.transform;
            }
        }

        if (bestTarget)
        {
            Vector3 direction = (bestTarget.position - transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, direction);
            transform.rotation = targetRotation;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, aimAssistRange);

        // Visualize cone
        Vector3 forward = transform.up;
        Vector3 leftLimit = Quaternion.Euler(0f, 0f, -aimAssistAngle) * forward;
        Vector3 rightLimit = Quaternion.Euler(0f, 0f, aimAssistAngle) * forward;

        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position, transform.position + leftLimit * aimAssistRange);
        Gizmos.DrawLine(transform.position, transform.position + rightLimit * aimAssistRange);
    }
}