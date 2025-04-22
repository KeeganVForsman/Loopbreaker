using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float attackRange = 1.5f;
    public float damage = 10f;
    public float attackCooldown = 2f;

    protected Transform player;
    protected float lastAttackTime;

    protected virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    protected virtual void Update()
    {
        if (!player) return;

        float distance = Vector3.Distance(transform.position, player.position);
        if (distance > attackRange)
        {
            MoveTowardsPlayer();
        }
        else if (Time.time - lastAttackTime >= attackCooldown)
        {
            Attack();
            lastAttackTime = Time.time;
        }
    }

    protected virtual void MoveTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
        transform.up = direction; // face player
    }

    protected virtual void Attack()
    {
        Debug.Log(name + " attacks for " + damage + " damage!");
        // Add actual damage code here
    }
}