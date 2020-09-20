using System.Linq;
using Human;
using TMPro;
using UnityEngine;

public class OrganInfo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI header, desc, function;

    private void Start()
    {
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
