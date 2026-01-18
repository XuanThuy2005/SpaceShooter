using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 8f;

    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }
}
