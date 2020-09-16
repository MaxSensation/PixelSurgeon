using UnityEngine;

public class SurgenTableMover : MonoBehaviour
{
    [SerializeField] private float force = default;
    [SerializeField] private MoveDirection moveDirection = default;
    private Rigidbody2D _rigidbody2D;

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
        var directionVector = Vector2.zero;
        switch (moveDirection)
        {
            case MoveDirection.Up:
                directionVector = Vector2.up;
                break;
            case MoveDirection.Down:
                directionVector = Vector2.down;
                break;
            case MoveDirection.Left:
                directionVector = Vector2.left;
                break;
            case MoveDirection.Right:
                directionVector = Vector2.right;
                break;
        }
        PushTableToGameView(directionVector);
    }

    private void PushTableToGameView(Vector2 direction)
    {
        _rigidbody2D.AddForce(direction * force * 1000 * Time.deltaTime);
    }
}
