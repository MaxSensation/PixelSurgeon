using Human;
using UnityEngine;

public class HeartAnimation : MonoBehaviour
{
    private Animator _animator;
    private AudioSource _audioSource;
    private bool _isOrgan;
    private Organ thisOrgan;
    private void Start()
    {
        _animator = GetComponent<Animator>();
        Organ.OnOrganModifiedEvent += OnOrganModifiedEvent;
        thisOrgan = transform.parent.GetComponent<Organ>();
        if (thisOrgan == null) return;
        _isOrgan = true;
        _audioSource = GetComponent<AudioSource>();
        if (!thisOrgan.IsAttached())
            _animator.SetTrigger("Disconnect");
    }

    private void OnOrganModifiedEvent(Organ organ, string toolUsed)
    {
        if (organ.GetOrganName() != "Heart") return;
        switch (toolUsed)
        {
            case "Sewingkit":
                if (_isOrgan && organ == thisOrgan)
                {
                    _animator.SetTrigger("Connect");
                } else if (!_isOrgan)
                    _animator.SetTrigger("Connect");
                break;
            case "Scalpel":
                if (_isOrgan && organ == thisOrgan)
                {
                    _animator.SetTrigger("Disconnect");
                } else if (!_isOrgan)
                    _animator.SetTrigger("Disconnect");
                break;
        }
    }

    public void PlayHeartBeat()
    {
        if (thisOrgan.IsAttached()) _audioSource.Play();
    }
}
