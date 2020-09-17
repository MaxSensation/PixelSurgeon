using System.Linq;
using UnityEngine;

namespace Human
{
    [ExecuteInEditMode]
    public class Organ : MonoBehaviour
    {
        [SerializeField] private string organName = default;
        [TextArea] [SerializeField] private string description, bodyFunction;
        [SerializeField] private int bloodLostPerSecond = default;
        [SerializeField] internal bool badOrgan = default;
        [SerializeField] private Color badOrganColor = default;
        [SerializeField]private Vector2 wantedPosition;
        private PolygonCollider2D _col;
        private GameObject _pixelMan;
        private void Start()
        {
            _pixelMan = FindObjectOfType<PixelMan>().gameObject;
            _col = GetComponent<PolygonCollider2D>();
            GetComponentInChildren<SpriteRenderer>().color = badOrgan ? badOrganColor : Color.white;
        }

        public string getOrganName()
        {
            return organName;
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
    }
}
