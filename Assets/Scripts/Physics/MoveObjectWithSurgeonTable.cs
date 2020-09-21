using UnityEngine;

public class MoveObjectWithSurgeonTable : MonoBehaviour
{
    [SerializeField] private GameObject table = default;
    private Rigidbody2D _rb, _tablerb;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _tablerb = table.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _rb.velocity = _tablerb.velocity;
    }
}
