using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 5f;
    public float lifeTime = 5f;

    private Vector2 moveDir;

    public void SetDirection(Vector2 dir)
    {
        moveDir = dir.normalized;
    }

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.Translate(moveDir * speed * Time.deltaTime);
    }
}
