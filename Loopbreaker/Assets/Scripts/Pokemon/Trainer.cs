
using UnityEngine;
public class Trainer : EnemyBase
{
    public float runAwayDistance = 3f;

    protected override void Update()
    {
        if (!player)
        {
            return;
        }
        Vector3 flatPosition = new Vector3(transform.position.x, 0f, transform.position.z);
        Vector3 flatPlayerPosition = new Vector3(player.position.x, 0f, player.position.z);

        float distance = Vector3.Distance(flatPosition, flatPlayerPosition);

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
        Vector3 direction = (transform.position - player.position);
        direction.y = 0f;
        direction = direction.normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
        if (direction!= Vector3.zero)
        {
            Quaternion lookrotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookrotation, 0.2f);
        }
    }
}
