using System;
using System.Linq;
using Player;
using UnityEngine;

namespace Human
{
    [ExecuteInEditMode]
    public class BodyPart : MonoBehaviour
    {
        public static Action<BodyPart, string> OnToolUsedEvent;
        [SerializeField] private string partName;
        [TextArea] [SerializeField] private string description;
        [SerializeField] private bool isAttached;
        [SerializeField] private ToolAttach toolToAttach;
        [SerializeField] private ToolDetach toolToDetach;
        [SerializeField] private int bloodLostPerSecond;
        [SerializeField] internal bool isBad;
        [SerializeField] private Color badColor;
        [SerializeField] private Vector2 wantedPosition;
        private PolygonCollider2D _col;
        private GameObject _pixelMan;

        private void Awake()
        {
            _pixelMan = GameObject.FindWithTag("PixelMan");
            _col = GetComponent<PolygonCollider2D>();
        }

        private void Start()
        {
            switch (toolToAttach)
            {
                case ToolAttach.Sewingkit:
                    Controls.OnSewnEvent += AttachOrgan;
                    break;
                case ToolAttach.None:
                    Controls.OnDropBodyPartEvent += AttachOrgan;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            switch (toolToDetach)
            {
                case ToolDetach.Scalpel:
                    Controls.OnCutEvent += DetachOrgan;
                    break;
                case ToolDetach.Saw:
                    Controls.OnSawEvent += DetachOrgan;
                    break;
                case ToolDetach.None:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OnDestroy()
        {
            Controls.OnSewnEvent = null;
            Controls.OnDropBodyPartEvent = null;
            Controls.OnCutEvent = null;
            Controls.OnSawEvent = null;
        }

        private void OnDrawGizmosSelected()
        {
            var points = _col.points.Select(pos =>
                pos + new Vector2(_pixelMan.transform.TransformPoint(wantedPosition).x,
                    _pixelMan.transform.TransformPoint(wantedPosition).y)).ToArray();
            Gizmos.color = Color.yellow;
            for (var i = 1; i < points.Length; i++)
            {
                Gizmos.DrawLine(points[i - 1], points[i]);
                if (i == points.Length - 1)
                    Gizmos.DrawLine(points[0], points[points.Length - 1]);
            }
        }

        public void SetAsBadOrgan()
        {
            GetComponentInChildren<SpriteRenderer>().color = badColor;
            isBad = true;
        }


        public void SetCorrectPosition()
        {
            _pixelMan = GameObject.FindWithTag("PixelMan");
            var t = transform;
            wantedPosition = t.parent == _pixelMan.transform
                ? t.localPosition
                : _pixelMan.transform.InverseTransformPoint(t.position);
        }

        public float GetGoalDistance()
        {
            return Vector2.Distance(transform.position, _pixelMan.transform.TransformPoint(wantedPosition));
        }

        public int GetBloodLostAmount()
        {
            return bloodLostPerSecond;
        }

        public bool IsAttached()
        {
            return isAttached;
        }

        private void AttachOrgan(GameObject o)
        {
            if (o != gameObject) return;
            if (!(100f - 100 * Mathf.Clamp01(GetGoalDistance() - 0.1f) > 80)) return;
            if (isAttached) return;
            isAttached = true;
            OnToolUsedEvent?.Invoke(this, toolToAttach.ToString());
        }

        private void DetachOrgan(GameObject o)
        {
            if (o != gameObject) return;
            if (!isAttached) return;
            isAttached = false;
            OnToolUsedEvent?.Invoke(this, toolToDetach.ToString());
        }

        public string GetPartName()
        {
            return partName;
        }

        public string GetOrganDesc()
        {
            return description;
        }

        private enum ToolDetach
        {
            None,
            Scalpel,
            Saw
        }

        private enum ToolAttach
        {
            None,
            Sewingkit
        }
    }
}