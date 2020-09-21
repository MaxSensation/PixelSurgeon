﻿using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip organPickupSound, toolPickupSound, organDropSound, toolDropSound; 
    private AudioSource _audioSource;


    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        PlayerControls.OnToolPickupEvent += OnToolPickupEvent;
        PlayerControls.OnOrganPickupEvent += OnOrganPickupEvent;
        PlayerControls.OnDropOrganEvent += OnDropOrganEvent;
        PlayerControls.OnDropToolEvent += OnDropToolEvent;
    }

    private void OnDestroy()
    {
        PlayerControls.OnToolPickupEvent = null;
        PlayerControls.OnOrganPickupEvent = null;
        PlayerControls.OnDropOrganEvent = null;
        PlayerControls.OnDropToolEvent = null;
    }

    private void OnToolPickupEvent(GameObject obj)
    {
        _audioSource.clip = toolPickupSound;
        _audioSource.Play();
    }
    private void OnOrganPickupEvent(GameObject obj)
    {
        _audioSource.clip = organPickupSound;
        _audioSource.Play();
    }
    private void OnDropOrganEvent(GameObject obj)
    {
        _audioSource.clip = organDropSound;
        _audioSource.Play();
    }
    private void OnDropToolEvent(GameObject obj)
    {
        _audioSource.clip = toolDropSound;
        _audioSource.Play();
    }
}
