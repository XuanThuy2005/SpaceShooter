using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 8f;
    Vector2 direction;

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("Player"))
    {
        Health h = other.GetComponent<Health>();
        if (h != null)
            h.TakeDamage(20);

        Destroy(gameObject);
    }
}
}
