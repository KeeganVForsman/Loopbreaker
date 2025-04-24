
using UnityEngine;
public class Trainer : EnemyBase
{
    public float runAwayDistance = 3f;
    public float fixedZZ;
    public float fleeDistanceStop = 4f;
    public bool isFleeing;
    private Rigidbody2D rb;
    public float followDistance;
    public Transform followPokemon;
    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        //fixedZZ = transform.position.z;
    }
    protected override void Update()
    {
        if (!player)
        {
            return;
        }
        Vector2 enemy2D = new Vector2(transform.position.x,  transform.position.y);
        Vector2 player2D = new Vector2(player.position.x,  player.position.y);

        float distance = Vector2.Distance(enemy2D, player2D);

        if (!isFleeing && distance< runAwayDistance)
        {
            isFleeing = true;
        }
        else if(isFleeing && distance>fleeDistanceStop)
        {
            isFleeing = false;
        }

        if (isFleeing)
        {
            RunFromPlayer(enemy2D, player2D);
        }
        else if (followPokemon)
        {
            followChaser(enemy2D, followPokemon.position);
        }

    }

    private void RunFromPlayer(Vector2 enemy2D, Vector2 player2D)
    {
        Vector2 direction = (enemy2D - player2D).normalized;
        Vector3 movement = new Vector3(direction.x, direction.y, 0f) * moveSpeed * Time.deltaTime;
        Vector2 newPosition = enemy2D + direction * moveSpeed * Time.deltaTime;
        transform.position += movement;
        rb.MovePosition(newPosition);
        if (direction != Vector2.zero)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
        }
    }
    private void followChaser(Vector2 selfPos, Vector2 targetPos)
    {
        Vector2 direction = (targetPos - selfPos).normalized;
        Vector2 newPos = selfPos + direction * moveSpeed * Time.deltaTime;
        rb.MovePosition(newPos);

    }
}
