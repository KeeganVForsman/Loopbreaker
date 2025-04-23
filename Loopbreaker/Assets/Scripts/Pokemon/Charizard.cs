
public class Charizard : EnemyBase
{
    protected override void Start()
    {
        base.Start();
        moveSpeed = 2f;
        damage = 15f;
        attackCooldown = 3f;
    }

    protected override void Attack()
    {
        base.Attack();
        // Add heavy attack animation/effects here
    }
}
