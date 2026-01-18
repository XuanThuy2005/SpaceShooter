using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // ================= SPAWNER =================
    [Header("=== SPAWNER SETTINGS ===")]
    public bool isSpawner = false;
    public GameObject enemyPrefab;

    public float spawnInterval = 1.5f;
    public float minSpawnInterval = 0.4f;
    public float difficultyIncreaseTime = 10f;

    // ================= WORLD =================
    [Header("=== WORLD LIMITS ===")]
    public float xLimit = 2.5f;
    public float yLimit = 5f;
    public float spawnOffset = 1.5f;

    // ================= MOVEMENT =================
    [Header("=== ENEMY MOVEMENT ===")]
    public float speed = 2f;
    public float changeDirTime = 1f;

    // ================= AVOID PLAYER =================
    [Header("=== AVOID PLAYER ===")]
    public float minDistanceFromPlayer = 2f;

    // ================= SHOOTING =================
    [Header("=== ENEMY SHOOTING ===")]
    public GameObject enemyBulletPrefab;
    public float shootInterval = 0.5f;

    [Header("=== BURST SHOOTING ===")]
    public int burstCount = 5;          // số viên mỗi loạt
    public float burstDelay = 0.12f;    // khoảng cách giữa các viên

    // ================= PRIVATE =================
    private Vector2 moveDir;
    private float dirTimer;
    private float shootTimer;
    private Transform player;

    // ================= UNITY =================
    void Start()
    {
        if (isSpawner)
        {
            StartSpawner();
        }
        else
        {
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
            PickRandomDirection();
        }
    }

    void Update()
    {
        if (isSpawner) return;

        MoveEnemy();
        ChangeDirectionOverTime();
        HandleShooting();
        DestroyIfOutOfBounds();
    }

    // ================= SPAWNER LOGIC =================
    void StartSpawner()
    {
        InvokeRepeating(nameof(SpawnEnemy), 1f, spawnInterval);
        InvokeRepeating(nameof(IncreaseDifficulty),
                        difficultyIncreaseTime,
                        difficultyIncreaseTime);
    }

    void SpawnEnemy()
    {
        Vector3 spawnPos = GetSpawnPositionOutsideScreen();
        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }

    void IncreaseDifficulty()
    {
        spawnInterval = Mathf.Max(minSpawnInterval, spawnInterval - 0.2f);

        CancelInvoke(nameof(SpawnEnemy));
        InvokeRepeating(nameof(SpawnEnemy), 0f, spawnInterval);
    }

    Vector3 GetSpawnPositionOutsideScreen()
    {
        int side = Random.Range(0, 3); // 0: top, 1: left, 2: right

        if (side == 0) // TOP
            return new Vector3(Random.Range(-xLimit, xLimit),
                               yLimit + spawnOffset, 0);

        if (side == 1) // LEFT
            return new Vector3(-xLimit - spawnOffset,
                               Random.Range(-yLimit, yLimit), 0);

        // RIGHT
        return new Vector3(xLimit + spawnOffset,
                           Random.Range(-yLimit, yLimit), 0);
    }

    // ================= ENEMY LOGIC =================
    void MoveEnemy()
    {
        if (player != null)
        {
            float dist = Vector2.Distance(transform.position, player.position);

            // Né Player nếu quá gần
            if (dist < minDistanceFromPlayer)
            {
                moveDir = (transform.position - player.position).normalized;
            }
        }

        transform.Translate(moveDir * speed * Time.deltaTime);
    }

    void ChangeDirectionOverTime()
    {
        dirTimer += Time.deltaTime;

        if (dirTimer >= changeDirTime)
        {
            PickRandomDirection();
            dirTimer = 0f;
        }
    }

    void PickRandomDirection()
    {
        int x = Random.Range(-1, 2);
        int y = Random.Range(-1, 2);

        if (x == 0 && y == 0)
            y = -1;

        moveDir = new Vector2(x, y).normalized;
    }

    // ================= SHOOTING =================
    void HandleShooting()
    {
        if (enemyBulletPrefab == null || player == null) return;

        shootTimer += Time.deltaTime;

        if (shootTimer >= shootInterval)
        {
            shootTimer = 0f;
            StartCoroutine(BurstShoot());
        }
    }


    void ShootAtPlayer()
    {
        if (enemyBulletPrefab == null || player == null) return;

        Vector2 dir = (player.position - transform.position).normalized;

        GameObject bullet = Instantiate(
            enemyBulletPrefab,
            transform.position,
            Quaternion.identity
        );

        EnemyBullet eb = bullet.GetComponent<EnemyBullet>();
        if (eb != null)
        {
            eb.SetDirection(dir);
        }
    }

    // ================= CLEANUP =================
    void DestroyIfOutOfBounds()
    {
        if (transform.position.y > yLimit + spawnOffset ||
            transform.position.y < -yLimit - spawnOffset ||
            transform.position.x > xLimit + spawnOffset ||
            transform.position.x < -xLimit - spawnOffset)
        {
            Destroy(gameObject);
        }
    }
    System.Collections.IEnumerator BurstShoot()
{
    for (int i = 0; i < burstCount; i++)
    {
        ShootAtPlayer();
        yield return new WaitForSeconds(burstDelay);
    }
}

}
