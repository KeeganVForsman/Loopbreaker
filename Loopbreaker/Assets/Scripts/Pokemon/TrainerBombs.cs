using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainerBombs : MonoBehaviour
{
    public float explosionRadius = 2f;
    public float explosionDamage = 30f;
    public float checkInterval = 0.2f;
    public float explosionDelay = 0.2f;
    public LayerMask playerLayer;
    [Header("Option for prefab for explosion")]
    public GameObject explosionEffectPrefab;

    private bool hasExploded = false;

    public void Start()
    {
        InvokeRepeating(nameof(checkForPlayerProx), 0f, checkInterval);
    }
    public void checkForPlayerProx()
    {
        if (hasExploded) return;

        Collider[] players = Physics.OverlapSphere(transform.position, explosionRadius, playerLayer);
        if (players.Length>0)
        {
            Invoke(nameof(Explode), explosionDelay);
            hasExploded = true;
        }
        
    }

    public void Explode()
    {
        if (explosionEffectPrefab)
        {
            GameObject explosion = Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
            Destroy(explosion, 2f);
        }

        Collider[] players = Physics.OverlapSphere(transform.position, explosionRadius, playerLayer);
        foreach (Collider player in players)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(explosionDamage);
                Debug.Log("Player took explosion damage");
            }
        }

        Destroy(gameObject);
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
