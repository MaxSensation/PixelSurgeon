using Human;
using UnityEngine;

public class MoveObjectWithTables : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private bool _isOrgan;
    private Organ _organ;
    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _organ = GetComponent<Organ>();
        if (_organ != null) _isOrgan = true;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (_isOrgan){
            if (_organ.IsAttached() && other.CompareTag("PixelMan"))
                _rigidbody2D.velocity = other.GetComponent<Rigidbody2D>().velocity;
            if (_organ.IsAttached()) return;
        }
        if (other.CompareTag("PixelMan") || other.CompareTag("Table"))
            _rigidbody2D.velocity = other.GetComponent<Rigidbody2D>().velocity;
    }
}
