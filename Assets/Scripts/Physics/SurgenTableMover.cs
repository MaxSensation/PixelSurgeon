using System;
using UnityEngine;

public class SurgenTableMover : MonoBehaviour
{
    [SerializeField] private float force = default;
    [SerializeField] private MoveDirection moveDirection = default;
    [SerializeField] private int delay;
    private Rigidbody2D _rigidbody2D;
    private Vector2 _directionVector;
    private float _time;
    private bool _used;
    private enum MoveDirection
    {
        Up,
        Down,
        Left,
        Right
    }
    
    void Start()
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
}
