public class SmoughEnemy : EnemyBase
{
    protected override void Start()
    {
        base.Start();
        moveSpeed = 1f;
        damage = 25f;
        attackCooldown = 3f;
    }

    protected override void Attack()
    {
        base.Attack();
        // Add heavy attack animation/effects here
    }
}