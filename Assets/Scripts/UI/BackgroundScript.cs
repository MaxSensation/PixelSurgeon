using UnityEngine;

namespace UI
{
    public class BackgroundScript : MonoBehaviour
    {
        private SpriteRenderer spriteRenderer;
        
        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Update() => UpdateScale();

        private void UpdateScale()
        {
            var worldScreenHeight = Camera.main.orthographicSize * 2.0;
            var sprite = spriteRenderer.sprite;
            transform.localScale = new Vector2(
                    (float) (worldScreenHeight / Screen.height * Screen.width / sprite.bounds.size.x),
                    (float) (worldScreenHeight / sprite.bounds.size.y));
        }
    }
}