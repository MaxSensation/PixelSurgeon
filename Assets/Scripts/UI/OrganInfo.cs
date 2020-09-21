using System.Linq;
using Human;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class OrganInfo : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TextMeshProUGUI header, desc, function;
    private Animator _animator;

    public void OnPointerClick(PointerEventData eventData) => _animator.SetTrigger("Clicked");
    
    private void Start()
    {
        _animator = GetComponent<Animator>();
        OrganManager.OnScenarioGenerated += list => OnOrganPickupEvent(list.First().gameObject);  
        PlayerControls.OnOrganPickupEvent += OnOrganPickupEvent;
    }

    private void OnOrganPickupEvent(GameObject organGO)
    {
        var organ = organGO.GetComponent<Organ>();
        header.text = organ.GetOrganName();
        desc.text = organ.GetOrganDesc();
        function.text = organ.GetOrganFunc();
    }
}
