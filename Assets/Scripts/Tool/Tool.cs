using System.Collections.Generic;
using System.Linq;
using Human;
using UnityEngine;

namespace Tool
{
    public class Tool : MonoBehaviour
    {
        [SerializeField] private ContactFilter2D contactFilter;
        private AudioSource _audioSource;
        private PolygonCollider2D _collider;
        private List<Collider2D> _overlapResults;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _overlapResults = new List<Collider2D>();
            _collider = transform.GetChild(1).GetComponent<PolygonCollider2D>();
        }

        private void Start()
        {
            BodyPart.OnToolUsedEvent += ToolUsed;
        }

        private void OnDestroy()
        {
            BodyPart.OnToolUsedEvent = null;
        }

        private void ToolUsed(BodyPart bodyPart, string usedTool)
        {
            if (usedTool == gameObject.name)
                _audioSource.Play();
        }

        public GameObject GetBodyPart()
        {
            _overlapResults.Clear();
            _collider.OverlapCollider(contactFilter, _overlapResults);
            if (_overlapResults.Count > 0)
                return (
                    from
                        item in _overlapResults
                    orderby
                        item.transform.GetChild(0).GetComponent<SpriteRenderer>()?.sortingOrder descending
                    select
                        item.transform.gameObject
                ).ToArray().First();
            return null;
        }
    }
}