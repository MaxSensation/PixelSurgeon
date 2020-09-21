using Human;
using UnityEngine;

namespace ItemPhysics
{
    public class MoveObjectWithTables : MonoBehaviour
    {
        private BodyPart _bodyPart;
        private bool _isOrgan;
        private Rigidbody2D _rigidbody2D;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _bodyPart = GetComponent<BodyPart>();
            if (_bodyPart != null) _isOrgan = true;
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (_isOrgan)
            {
                if (_bodyPart.IsAttached() && other.CompareTag("PixelMan"))
                    _rigidbody2D.velocity = other.GetComponent<Rigidbody2D>().velocity;
                if (_bodyPart.IsAttached()) return;
            }

            if (other.CompareTag("PixelMan") || other.CompareTag("Table"))
                _rigidbody2D.velocity = other.GetComponent<Rigidbody2D>().velocity;
        }
    }
}