using UnityEngine;
using System.Collections;

public class SwordSwing : MonoBehaviour
{
    public Transform pivotPoint;
    public float swingAngle = 90f;
    public float swingDuration = 0.2f;
    public bool destroyAfterSwing = false;

    private float swingSpeed;
    private float currentAngle = 0f;
    private bool isSwinging = false;
    private Quaternion startingRotation;

    private Collider weaponCollider;
    private int currentComboStep = 1;

    // For third-hit forward lunge visual
    public float lungeDistance = 1f;
    public float lungeDuration = 0.1f;

    private Vector3 originalLocalPosition;
    private Quaternion originalLocalRotation;

    void Awake()
    {
        weaponCollider = GetComponent<Collider>();
        if (weaponCollider != null)
        {
            weaponCollider.enabled = false;
        }
    }

    void Start()
    {
        
        
            originalLocalPosition = transform.localPosition;
            originalLocalRotation = transform.localRotation;
        


        swingSpeed = swingAngle / swingDuration;
        startingRotation = pivotPoint.localRotation;
    }

    void Update()
    {
        if (isSwinging)
        {
            float deltaAngle = swingSpeed * Time.deltaTime;
            currentAngle += deltaAngle;

            pivotPoint.Rotate(0f, 0f, deltaAngle, Space.Self);

            if (currentAngle >= swingAngle)
            {
                EndSwing();
            }
        }
    }

    public void StartSwing(int comboStep)
    {
        if (!isSwinging)
        {
            currentComboStep = comboStep;
            currentAngle = 0f;
            isSwinging = true;

            // Reset rotation first
            pivotPoint.localRotation = startingRotation;

            switch (comboStep)
            {
                case 1:
                    pivotPoint.localPosition = Vector3.zero; // Right side (default)
                    break;

                case 2:
                    pivotPoint.localPosition = new Vector3(-1f, 0f, 0f); // Mirror to left
                    pivotPoint.localRotation = startingRotation * Quaternion.Euler(0f, 0f, -30f);
                    break;

                case 3:
                    pivotPoint.localPosition = Vector3.zero;
                    StartCoroutine(PerformLungeMotion()); // Visible forward motion
                    break;
            }

            if (weaponCollider != null)
                StartCoroutine(EnableColliderTemporarily());
        }
    }

    private void EndSwing()
    {
        isSwinging = false;
        pivotPoint.localRotation = startingRotation;

        if (destroyAfterSwing)
        {
            Destroy(gameObject);
        }
    }

    private System.Collections.IEnumerator EnableColliderTemporarily()
    {
        weaponCollider.enabled = true;
        yield return new WaitForSeconds(swingDuration);
        weaponCollider.enabled = false;
    }

    public void ResetWeapon()
    {
        transform.localPosition = originalLocalPosition;
        transform.localRotation = originalLocalRotation;
    }

    private IEnumerator PerformLungeMotion()
    {
        Vector3 start = transform.localPosition;
        Vector3 lungeTarget = start + transform.forward * lungeDistance;

        float elapsed = 0f;

        while (elapsed < lungeDuration)
        {
            transform.localPosition = Vector3.Lerp(start, lungeTarget, elapsed / lungeDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = start;
    }
}