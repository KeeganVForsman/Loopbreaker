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
}