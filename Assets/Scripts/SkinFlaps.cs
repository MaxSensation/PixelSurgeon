using UnityEngine;

public class SkinFlaps : MonoBehaviour
{
        [SerializeField] private GameObject openFlaps = default, closedFlaps = default;
        private void Start()
        {
                //PlayerControls.OnCutEvent += CheckKnifeEvent;
                //PlayerControls.OnSewnEvent += CheckSewnEvent;
        }

        private void CheckKnifeEvent(GameObject o)
        {
                if (o == closedFlaps)
                        OpenFlaps();
        }

        private void CheckSewnEvent(GameObject o)
        {
                if (o == openFlaps)
                        CloseFlaps();
        }

        private void OpenFlaps()
        {
                closedFlaps.SetActive(false);
                openFlaps.SetActive(true);
        }

        private void CloseFlaps()
        {
                
                closedFlaps.SetActive(true);
                openFlaps.SetActive(false);
        }
}
