
using UnityEngine;
public class Trainer : EnemyBase
{

    public GameObject hitboxPrefab;
    public Transform hitboxSpawnPoint;
    public float hitboxLifetime = 0.5f;
    public float hitstopDuration = 0.1f;
    public float runAwayDistance = 3f;
    public float fixedZZ;
    public float fleeDistanceStop = 4f;
    public bool isFleeing;
    private Rigidbody rb;
    public float followDistance;
    public Transform followPokemon;
    public Vector3 moveDirection;
    public GameObject bombPrefab;
    public Transform throwPoint;
    public float throwcoolDown = 2f;
    public float throwForce = 10f;
    public float throwTime = 0f;

    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody>();
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
            followChaser(transform.position, followPokemon.position);
        }
        else
        {
            moveDirection = Vector3.zero;
        }
        if (isFleeing && Time.time - throwTime >= throwcoolDown)
        {
            ThrowBombs();
            throwTime = Time.time;
        }

    }
    private void FixedUpdate()
    {
        if (moveDirection !=Vector3.zero)
        {
            rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
            float angle = Mathf.Atan2(moveDirection.z, moveDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, -angle + 90f, 0f);
        }
    }

    private void RunFromPlayer(Vector2 enemy2D, Vector2 player2D)
    {
        Vector2 direction = (enemy2D - player2D).normalized;
        moveDirection = new Vector3(direction.x, 0f, direction.y);
    }
    private void followChaser(Vector2 selfPos, Vector2 targetPos)
    {
        Vector2 direction = (targetPos - selfPos).normalized;
        
        moveDirection = direction.normalized;

    }

    public void ThrowBombs()
    {
        if (bombPrefab == null || throwPoint == null || player == null) 
        {
            return;
        }
        GameObject bomb = Instantiate(bombPrefab, throwPoint.position, throwPoint.rotation);

        Rigidbody bombRB = bomb.GetComponent<Rigidbody>();
        if (bombRB != null) 
        {
            Vector3 directionToPlayer = (player.position - throwPoint.position).normalized;
            bombRB.velocity = directionToPlayer * throwForce;
        }
    }
}
