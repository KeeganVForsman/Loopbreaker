public class OrnsteinEnemy : EnemyBase
{
    protected override void Start()
    {
        base.Start();
        moveSpeed = 3f;
        damage = 8f;
        attackCooldown = 1f;
    }

    protected override void Attack()
    {
        base.Attack();
        // Add quick dash/stab animation/effects here
    }
}