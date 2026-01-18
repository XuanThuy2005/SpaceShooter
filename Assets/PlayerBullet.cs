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
            if (explosionPrefab != null)
            {
                Instantiate(
                    explosionPrefab,
                    other.transform.position,
                    Quaternion.identity
                );
            }

            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
