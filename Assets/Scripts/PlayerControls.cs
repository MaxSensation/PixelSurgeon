using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    private PlayerActionsScript controls;
    [SerializeField] private LayerMask mask;
    private RaycastHit2D hit; //debug temp
    private Ray ray; //också debug temp

    private void Awake()
    {
        controls = new PlayerActionsScript();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void Start()
    {
        controls.Player.Drag.performed += _ => Drag();
    }

    private void Drag()
    {
        Vector2 mousePosition = controls.Player.Mouseposition.ReadValue<Vector2>();
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        ray = Camera.main.ScreenPointToRay(mousePosition);
        Debug.Log(mousePosition);
        hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(mousePosition), Vector2.zero);
        if (hit.collider != null)
            Debug.Log("Target Position: " + hit.collider.gameObject.transform.position);
    }

    private void OnDrawGizmos()
    {
        Vector2 mousePosition = controls.Player.Mouseposition.ReadValue<Vector2>();
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        mousePosition.y = -mousePosition.y;
        Gizmos.color = Color.red;
        Gizmos.DrawLine(mousePosition, new Vector3(mousePosition.x, mousePosition.y, 2f));
        if (hit.collider)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(mousePosition, hit.point.normalized);
            Debug.Log("hit");
        }
    }
}
