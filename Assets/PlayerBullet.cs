using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public GameObject explosionPrefab;

    public float speed = 8f;
    public float lifeTime = 4f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Health h = other.GetComponent<Health>();
            if (h != null)
                h.TakeDamage(20);

            Destroy(gameObject);
        }
    }
}
// k can them 