using System;
using UnityEngine;

namespace ItemPhysics
{
    public class TablePusher : MonoBehaviour
    {
        [SerializeField] private float force;
        [SerializeField] private MoveDirection moveDirection;
        [SerializeField] private int delay;
        private Vector2 _directionVector;
        private Rigidbody2D _rigidbody2D;
        private float _time;
        private bool _used;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _directionVector = Vector2.zero;
            switch (moveDirection)
            {
                case MoveDirection.Up:
                    _directionVector = Vector2.up;
                    break;
                case MoveDirection.Down:
                    _directionVector = Vector2.down;
                    break;
                case MoveDirection.Left:
                    _directionVector = Vector2.left;
                    break;
                case MoveDirection.Right:
                    _directionVector = Vector2.right;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void Update()
        {
            if (_used)
                return;
            if (_time > delay)
            {
                PushTableToGameView(_directionVector);
                _used = true;
            }

            _time += Time.deltaTime;
        }

        private void PushTableToGameView(Vector2 direction)
        {
            _rigidbody2D.AddForce(direction * force);
        }

        private enum MoveDirection
        {
            Up,
            Down,
            Left,
            Right
        }
    }
}