using UnityEngine.UI;
using UnityEngine;
using System.Collections;
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
    public float maxHealth = 100;
    public float currentHealth;
    public bool isThrowing = false;
    public Animator animator;
    public Transform player;
    //public Image healthBarFill; // Drag HealthBarFill here in Inspector
    public Slider HealthSlider;
    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody>();
        //fixedZZ = transform.position.z;
        currentHealth = maxHealth;
        HealthSlider.value = currentHealth / maxHealth;
        animator = GetComponent<Animator>();
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
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
        if (isThrowing && Time.time - throwTime >= throwcoolDown)
        {
            ThrowBombsAtPlayer();
            throwTime = Time.time;
        }
        if (animator != null)
        {
            Vector2 flatMoveDirection = new Vector2(moveDirection.x, moveDirection.z);
            float speed = flatMoveDirection.magnitude;
            animator.SetFloat("Speed", speed);
        }
    }
    private void FixedUpdate()
    {
        if (moveDirection !=Vector3.zero)
        {
            rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
            float angle = Mathf.Atan2(moveDirection.z, moveDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, -angle + 90f, 0f);

            animator.SetFloat("Speed", moveDirection.magnitude);
        }
        else
        {
            animator.SetFloat("Speed", 0f);
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
        StartCoroutine(BounceEffect());
    }
    public void ThrowBombsAtPlayer()
    {
        GameObject bomb = Instantiate(bombPrefab, throwPoint.position, throwPoint.rotation);
        Rigidbody bombRB = bomb.GetComponent<Rigidbody>();
        if (bombRB != null)
        {
            Vector3 directionToPlayer = (player.position - throwPoint.position).normalized;

            float spreadAngle = Random.Range(-15f, 15f);
            Quaternion spreadRotation = Quaternion.Euler(0, spreadAngle, 0);
            Vector3 randomDirection = spreadRotation * directionToPlayer;
            bombRB.velocity = randomDirection * throwForce;
        }
    }
    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (HealthSlider != null)
        {
            HealthSlider.value = currentHealth / maxHealth;
        }
        Debug.Log(gameObject.name + "Took " + amount + "Damage. Current health" + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
            
        }
    }
    public void Die()
    {
        Debug.Log(gameObject.name + "is dead");
        FindObjectOfType<SceneBossManager>()?.RegisterBossDefeat();
        gameObject.SetActive(false);
        //Destroy(gameObject);

    }

    public void SwitchToThrowing()
    {
        isThrowing = true;
        Debug.Log(gameObject.name + " is now throwing bombs");
    }

    private IEnumerator BounceEffect(float bounceHeight = 0.5f, float bounceDuration = 0.2f )
    {
        Vector3 originalPosition = transform.position;
        Vector3 targetPosition = originalPosition + new Vector3(0, bounceHeight, 0);

        float elapsedTime = 0f;
        while (elapsedTime <bounceDuration/2f)
        {
            transform.position = Vector3.Lerp(originalPosition, targetPosition, (elapsedTime/(bounceDuration/2f)));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        elapsedTime = 0f;
        while (elapsedTime < bounceDuration/2f)
        {
            transform.position = Vector3.Lerp(targetPosition, originalPosition, (elapsedTime / (bounceDuration / 2f)));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = originalPosition;
    }
}
