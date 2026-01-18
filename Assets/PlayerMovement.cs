using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePointLeft;
    public Transform firePointRight;

    void Update()
    {
        // Player đuổi chuột
        var worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPoint.z = 0;
        transform.position = worldPoint;

        // Bắn khi click chuột
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(bulletPrefab, firePointLeft.position, Quaternion.identity);
            Instantiate(bulletPrefab, firePointRight.position, Quaternion.identity);
        }
    }
}
