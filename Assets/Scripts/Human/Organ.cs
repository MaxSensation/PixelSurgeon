using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using UnityEngine;

namespace Human
{
    [ExecuteInEditMode]
    public class Organ : MonoBehaviour
    {
        [SerializeField] private string organName = default;
        [TextArea] [SerializeField] private string description, bodyFunction;
        [SerializeField] private bool isAttached = default;
        [SerializeField] private ToolAttach toolToAttach = default;
        [SerializeField] private ToolDetach toolToDetach = default;
        [SerializeField] private int bloodLostPerSecond = default;
        [SerializeField] internal bool badOrgan = default;
        [SerializeField] private Color badOrganColor = default;
        [SerializeField]private Vector2 wantedPosition = default;
        public static Action<Organ, string> OnOrganModifiedEvent;
        private PolygonCollider2D _col;
        private GameObject _pixelMan;

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
        
        private void Start()
        {
            _pixelMan = FindObjectOfType<PixelMan>().gameObject;
            _col = GetComponent<PolygonCollider2D>();
            switch (toolToAttach)
            {
                case ToolAttach.Sewingkit:
                    PlayerControls.OnSewnEvent += AttachOrgan;
                    break;
                case ToolAttach.None:
                    PlayerControls.OnDropOrganEvent += AttachOrgan;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            switch (toolToDetach)
            {
                case ToolDetach.Scalpel:
                    PlayerControls.OnCutEvent += DetachOrgan;
                    break;
                case ToolDetach.Saw:
                    PlayerControls.OnSawEvent += DetachOrgan;
                    break;
                case ToolDetach.None:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            GetComponentInChildren<SpriteRenderer>().color = badOrgan ? badOrganColor : Color.white;
        }

        private void OnDestroy()
        {
            switch (toolToAttach)
            {
                case ToolAttach.Sewingkit:
                    PlayerControls.OnSewnEvent -= AttachOrgan;
                    break;
                case ToolAttach.None:
                    PlayerControls.OnDropOrganEvent -= AttachOrgan;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            switch (toolToDetach)
            {
                case ToolDetach.Scalpel:
                    PlayerControls.OnCutEvent -= DetachOrgan;
                    break;
                case ToolDetach.Saw:
                    PlayerControls.OnSawEvent -= DetachOrgan;
                    break;
                case ToolDetach.None:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }


        public void SetCorrectPosition()
        {
            _pixelMan = FindObjectOfType<PixelMan>().gameObject;
            if (transform.parent == _pixelMan.transform)
                wantedPosition = transform.localPosition;
            else
                wantedPosition = _pixelMan.transform.InverseTransformPoint(transform.position);
        }
        
        private void OnDrawGizmosSelected()
        {
            var points = _col.points.Select(pos => pos + new Vector2(_pixelMan.transform.TransformPoint(wantedPosition).x,_pixelMan.transform.TransformPoint(wantedPosition).y)).ToArray();
            Gizmos.color = Color.yellow;
            for (var i = 1; i < points.Length; i++)
            {
                Gizmos.DrawLine(points[i-1], points[i]);
                if (i == points.Length - 1)
                    Gizmos.DrawLine(points[0], points[points.Length-1]);
            }
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
            if(isAttached) return; 
            isAttached = true;
            OnOrganModifiedEvent?.Invoke(this, toolToAttach.ToString());
        }
        
        private void DetachOrgan(GameObject o)
        {
            if (o != gameObject) return;
            if(!isAttached) return;
            isAttached = false;
            OnOrganModifiedEvent?.Invoke(this, toolToDetach.ToString());
        }
        
        public string GetOrganName()
        {
            return organName;
        }
        public string GetOrganDesc()
        {
            return description;
        }
        public string GetOrganFunc()
        {
            return bodyFunction;
        }
    }
}
