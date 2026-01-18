using UnityEngine;

public class ShowLog : MonoBehaviour
{
    void Start()
    {
        Debug.Log("Hello World!");
    }

    void Update()
    {
        Debug.Log("Update called! " + Time.frameCount);
    }
}
