using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrnsteinHitboxHandler : MonoBehaviour
{
    public float damageAmount = 10f;
    private void OnTriggerEnter(Collider other)
    {
        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damageAmount);

        }
    }
}
