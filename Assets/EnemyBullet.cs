using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 4f;

    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if (transform.position.y < -6)
        {
            Destroy(gameObject);
        }
    }
}
