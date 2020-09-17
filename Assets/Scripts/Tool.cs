using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tool : MonoBehaviour
{
    [SerializeField] private ContactFilter2D contactFilter = default;
    private PolygonCollider2D _collider;
    private List<Collider2D> _overlapResults;

    void Start()
    {
        _overlapResults = new List<Collider2D>();
        _collider = GetComponentInChildren<PolygonCollider2D>();
    }

    public GameObject GetOrgan()
    {
        _overlapResults.Clear();
        _collider.OverlapCollider(contactFilter, _overlapResults);
        if (_overlapResults.Count > 0)
        {
            return (
                from
                    item in _overlapResults
                orderby
                    item.gameObject.GetComponentInChildren<SpriteRenderer>()?.sortingOrder descending
                select
                    item
            ).ToArray().First().transform.gameObject;
        }
        return null;
    }

}
