using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tool : MonoBehaviour
{
    [SerializeField] private ContactFilter2D contactFilter = default;
    [SerializeField] private LayerMask mask = default;
    private PolygonCollider2D _collider;
    private List<Collider2D> _overlapResults;

    private void Start()
    {
        _overlapResults = new List<Collider2D>();
        _collider = transform.GetChild(1).GetComponent<PolygonCollider2D>();
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
                    item.transform.GetChild(0).GetComponent<SpriteRenderer>()?.sortingOrder descending
                where 
                    mask == (mask | (1 << item.transform.gameObject.layer))
                select
                    item
            ).ToArray().First().transform.gameObject;
        }
        return null;
    }

}
