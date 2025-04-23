
using UnityEngine;
public class Trainer : EnemyBase
{
    public float runAwayDistance = 3f;
    public float fixedZZ;
    protected override void Start()
    {
        base.Start();
        //fixedZZ = transform.position.z;
    }
    protected override void Update()
    {
        if (!player)
        {
            return;
        }
        Vector2 flatPosition = new Vector2(transform.position.x,  transform.position.z);
        Vector2 flatPlayerPosition = new Vector2(player.position.x,  player.position.z);

        float distance = Vector2.Distance(flatPosition, flatPlayerPosition);

        if (distance<runAwayDistance)
        {
            RunFromPlayer();
        }
        else
        {
            //commands the dragon enemy
        }

    }

    private void RunFromPlayer()
    {
        Vector2 direction = (new Vector2(transform.position.x,transform.position.y)- new Vector2(player.position.x,player.position.y)).normalized;
        Vector3 movement = new Vector3(direction.x, direction.y, 0f) * moveSpeed * Time.deltaTime;

        transform.position = new Vector3(transform.position.x, transform.position.y, fixedZZ);
    }
}
