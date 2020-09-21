using System.Linq;
using Human;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class BodyPartInfo : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private TextMeshProUGUI header, desc;
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            BodyPartManager.OnScenarioGeneratedEvent += list => OnOrganPickupEvent(list.First().gameObject);
            Controls.OnBodyPartPickupEvent += OnOrganPickupEvent;
        }

        private void OnDestroy()
        {
            BodyPartManager.OnScenarioGeneratedEvent -= list => OnOrganPickupEvent(list.First().gameObject);
            Controls.OnBodyPartPickupEvent -= OnOrganPickupEvent;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _animator.SetTrigger("Clicked");
        }

        private void OnOrganPickupEvent(GameObject organGO)
        {
            var organ = organGO.GetComponent<BodyPart>();
            header.text = organ.GetPartName();
            desc.text = organ.GetOrganDesc();
        }
    }
}