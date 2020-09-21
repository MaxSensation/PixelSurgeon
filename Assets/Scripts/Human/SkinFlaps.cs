﻿using System;
using Player;
using UnityEngine;

namespace Human
{
    public class SkinFlaps : MonoBehaviour
    {
        public static Action OnOpenFlapEvent, OnCloseFlapEvent;
        [SerializeField] private GameObject openFlaps, closedFlaps;
        [SerializeField] private AudioClip openSound, closeSound;
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            Controls.OnCutEvent += CheckKnifeEvent;
            Controls.OnSewnEvent += CheckSewnEvent;
        }

        private void OnDestroy()
        {
            Controls.OnCutEvent = null;
            Controls.OnSewnEvent = null;
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
}