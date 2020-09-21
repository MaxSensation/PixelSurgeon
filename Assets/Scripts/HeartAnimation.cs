using System;
using Human;
using UnityEngine;

public class HeartAnimation : MonoBehaviour
{
    private Animator _animator;
    private AudioSource _audioSource;
    private bool _isOrgan;
    private Organ _thisOrgan;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _thisOrgan = transform.parent.GetComponent<Organ>();
        if (_thisOrgan == null) return;
        _isOrgan = true;
        _audioSource = GetComponent<AudioSource>();
        if (_thisOrgan.IsAttached())
            _animator.SetTrigger("Connect");
    }

    private void Start()
    {
        Organ.OnOrganModifiedEvent += OnOrganModifiedEvent;
    }

    private void OnDestroy()
    {
        Organ.OnOrganModifiedEvent = null;
    }

    private void OnOrganModifiedEvent(Organ organ, string toolUsed)
    {
        if (organ.GetOrganName() != "Heart") return;
        switch (toolUsed)
        {
            case "Sewingkit":
                if (_isOrgan && organ == _thisOrgan)
                {
                    _animator.SetTrigger("Connect");
                } else if (!_isOrgan)
                    _animator.SetTrigger("Connect");
                break;
            case "Scalpel":
                if (_isOrgan && organ == _thisOrgan)
                {
                    _animator.SetTrigger("Disconnect");
                } else if (!_isOrgan)
                    _animator.SetTrigger("Disconnect");
                break;
        }
    }

    public void PlayHeartBeat()
    {
        _audioSource.Play();
    }
}
