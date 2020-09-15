using UnityEngine;

public class PixelMan : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Organ"))
        {
            Debug.Log("Organ entered body");
            other.transform.parent = transform;
        }
    }
}
