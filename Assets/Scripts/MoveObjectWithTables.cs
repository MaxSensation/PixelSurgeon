using UnityEngine;

public class MoveObjectWithTables : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Table"))
        {
            _rigidbody2D.velocity = other.GetComponent<Rigidbody2D>().velocity;
        }
    }
}
