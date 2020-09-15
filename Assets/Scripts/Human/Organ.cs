using System;
using System.Linq;
using UnityEngine;

namespace Human
{
    public class Organ : MonoBehaviour
    {
        [SerializeField] private string name;
        [TextArea] [SerializeField] private string description, bodyFunction;
        
        private Vector2 _wantedPosition;
        private GameObject _pixelMan;
        private void Start()
        {
            _pixelMan = FindObjectOfType<PixelMan>().gameObject;
        }

        public void SetCorrectPosition()
        {
            _pixelMan = FindObjectOfType<PixelMan>().gameObject;
            if (transform.parent == _pixelMan.transform)
            {
                Debug.Log("In Body");
                _wantedPosition = transform.localPosition;
            }
            else
            {
                Debug.Log("Out of Body");
                _wantedPosition = _pixelMan.transform.InverseTransformPoint(transform.position);
            }
        }

        private void OnDrawGizmosSelected()
        {
            var col = GetComponent<PolygonCollider2D>();
            var points = col.points.Select(pos => pos + new Vector2(_pixelMan.transform.TransformPoint(_wantedPosition).x,_pixelMan.transform.TransformPoint(_wantedPosition).y)).ToArray();
            Gizmos.color = Color.yellow;
            for (var i = 1; i < points.Length; i++)
            {
                Gizmos.DrawLine(points[i-1], points[i]);
                if (i == points.Length - 1)
                    Gizmos.DrawLine(points[0], points[points.Length-1]);
            }
        }
    }
}
