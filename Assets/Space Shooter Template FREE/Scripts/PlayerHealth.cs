using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : Health
{
    protected override void Die()
    {
        Debug.Log("Player Dead");

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
