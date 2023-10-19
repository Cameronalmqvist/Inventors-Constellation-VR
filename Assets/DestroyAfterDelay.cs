using UnityEngine;

public class DestroyAfterDelay : MonoBehaviour
{
    public float delay = 12.0f;

    void Start()
    {
        // Call the DestroyObject function after the specified delay
        Invoke("DestroyObject", delay);
    }

    void DestroyObject()
    {
        // Destroy the GameObject this script is attached to
        Destroy(gameObject);
    }
}
