using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxCollisionHandler : MonoBehaviour
{
    private Charizard enemyRef;
    
    private float DamageAmount;

    public void Setup( Charizard charri, float damage)
    {
        enemyRef = charri;
        
        DamageAmount = damage;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        enemyRef.OnHitboxTrigger(other, DamageAmount);
    }
}
