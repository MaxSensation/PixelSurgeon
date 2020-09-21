using System;
using UnityEngine;

public class SkinFlaps : MonoBehaviour
{
        [SerializeField] private GameObject openFlaps = default, closedFlaps = default;
        [SerializeField] private AudioClip openSound, closeSound;
        private AudioSource _audioSource;
        public static Action OnOpenFlapEvent, OnCloseFlapEvent;
        private void Awake()
        {
                _audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
                PlayerControls.OnCutEvent += CheckKnifeEvent;
                PlayerControls.OnSewnEvent += CheckSewnEvent;
        }

        private void OnDestroy()
        {
                PlayerControls.OnCutEvent = null;
                PlayerControls.OnSewnEvent = null;
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
                _audioSource.clip = openSound;
                _audioSource.Play();
                OnOpenFlapEvent?.Invoke();
        }

        private void CloseFlaps()
        {
                closedFlaps.SetActive(true);
                openFlaps.SetActive(false);
                _audioSource.clip = closeSound;
                _audioSource.Play();
                OnCloseFlapEvent?.Invoke();
        }
}
