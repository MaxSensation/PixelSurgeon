using System;
using System.Collections.Generic;
using System.Linq;
using Human;
using UnityEngine;

public class Tool : MonoBehaviour
{
    [SerializeField] private ContactFilter2D contactFilter = default;
    private PolygonCollider2D _collider;
    private List<Collider2D> _overlapResults;
    private AudioSource _audioSource;
    
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _overlapResults = new List<Collider2D>();
        _collider = transform.GetChild(1).GetComponent<PolygonCollider2D>();
        Organ.OnOrganModifiedEvent += ToolUsed;
    }

    private void OnDestroy()
    {
        Organ.OnOrganModifiedEvent -= ToolUsed;
    }

    private void ToolUsed(Organ organ, string usedTool)
    {
        if (usedTool == gameObject.name)
            _audioSource.Play();
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
                select
                    item.transform.gameObject
            ).ToArray().First();
        }
        return null;
    }

}
