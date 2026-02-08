using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int damage = 10;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Health h = col.gameObject.GetComponent<Health>();

            if (h != null)
                h.TakeDamage(damage);
        }
    }
}
