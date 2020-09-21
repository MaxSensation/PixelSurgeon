using UnityEngine;

namespace Player
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioClip bodyPartPickupSound, toolPickupSound, bodyPartDropSound, toolDropSound;
        private AudioSource _audioSource;


        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            Controls.OnToolPickupEvent += OnToolPickupEvent;
            Controls.OnBodyPartPickupEvent += OnBodyPartPickupEvent;
            Controls.OnDropToolEvent += OnDropToolEvent;
            Controls.OnDropBodyPartEvent += OnDropBodyPartEvent;
        }

        private void OnDestroy()
        {
            Controls.OnToolPickupEvent = null;
            Controls.OnBodyPartPickupEvent = null;
            Controls.OnDropBodyPartEvent = null;
            Controls.OnDropToolEvent = null;
        }

        private void OnToolPickupEvent(GameObject obj)
        {
            _audioSource.clip = toolPickupSound;
            _audioSource.Play();
        }

        private void OnBodyPartPickupEvent(GameObject obj)
        {
            _audioSource.clip = bodyPartPickupSound;
            _audioSource.Play();
        }

        private void OnDropBodyPartEvent(GameObject obj)
        {
            _audioSource.clip = bodyPartDropSound;
            _audioSource.Play();
        }

        private void OnDropToolEvent(GameObject obj)
        {
            _audioSource.clip = toolDropSound;
            _audioSource.Play();
        }
    }
}