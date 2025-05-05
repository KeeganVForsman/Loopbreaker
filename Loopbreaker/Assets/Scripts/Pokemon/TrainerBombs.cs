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

    }
}
