using System.Security.Cryptography;
using UnityEngine;

public class SwordSwing : MonoBehaviour
{
    public Transform pivotPoint; // Where the sword rotates around (usually the player hand)
    public float swingAngle = 90f;
    public float swingDuration = 0.2f;
    public bool destroyAfterSwing = false; // Optional, if you want to keep sword

    private float swingSpeed;
    private float currentAngle = 0f;
    private bool isSwinging = false;
    private Quaternion startingRotation;

    private Collider weaponCollider;


    void Awake()
     {
        weaponCollider = GetComponent<Collider>();
        if (weaponCollider != null)
        {
            weaponCollider.enabled = false; // Disable by default
        }
     }

    private void Start()
    {
        swingSpeed = swingAngle / swingDuration;
        startingRotation = transform.localRotation;
    }

    private void Update()
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

    public void StartSwing()
    {
        if (!isSwinging)
        {
            currentAngle = 0f;
            isSwinging = true;

            if (weaponCollider != null)
            {
                StartCoroutine(EnableColliderTemporarily());
            }
        }
    }

    private void EndSwing()
    {
        isSwinging = false;

        // Optional: Reset rotation
        pivotPoint.localRotation = startingRotation;

        if (destroyAfterSwing)
        {
            Destroy(gameObject);
        }
    }


    private System.Collections.IEnumerator EnableColliderTemporarily()
    {
        weaponCollider.enabled = true;
        yield return new WaitForSeconds(0.2f); // Match hitboxLifetime or animation timing
        weaponCollider.enabled = false;
    }
}