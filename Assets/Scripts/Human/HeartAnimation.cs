using UnityEngine;

namespace Human
{
    public class HeartAnimation : MonoBehaviour
    {
        private Animator _animator;
        private AudioSource _audioSource;
        private bool _isBodyPart;
        private BodyPart _thisBodyPart;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _thisBodyPart = transform.parent.GetComponent<BodyPart>();
            if (_thisBodyPart == null) return;
            _isBodyPart = true;
            _audioSource = GetComponent<AudioSource>();
            if (_thisBodyPart.IsAttached())
                _animator.SetTrigger("Connect");
        }

        private void Start()
        {
            BodyPart.OnToolUsedEvent += OnOrganModifiedEvent;
        }

        private void OnDestroy()
        {
            BodyPart.OnToolUsedEvent = null;
        }

        private void OnOrganModifiedEvent(BodyPart bodyPart, string toolUsed)
        {
            if (bodyPart.GetPartName() != "Heart") return;
            switch (toolUsed)
            {
                case "Sewingkit":
                    if (_isBodyPart && bodyPart == _thisBodyPart)
                        _animator.SetTrigger("Connect");
                    else if (!_isBodyPart)
                        _animator.SetTrigger("Connect");
                    break;
                case "Scalpel":
                    if (_isBodyPart && bodyPart == _thisBodyPart)
                        _animator.SetTrigger("Disconnect");
                    else if (!_isBodyPart)
                        _animator.SetTrigger("Disconnect");
                    break;
            }
        }

        public void PlayHeartBeat()
        {
            _audioSource.Play();
        }
    }
}