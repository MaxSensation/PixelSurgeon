using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    private PlayerActionsScript _controls;
    [SerializeField] private LayerMask mask;
    
    private void Awake()
    {
        _controls = new PlayerActionsScript();
    }

    private void OnEnable()
    {
        _controls.Enable();
    }

    private void OnDisable()
    {
        _controls.Disable();
    }

    private void Start()
    {
        _controls.Player.Click.performed += _ => Click();
    }

    private void Click()
    {
        Vector2 mousePosition = _controls.Player.Mouseposition.ReadValue<Vector2>();
        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(mousePosition);
        var hit = Physics2D.Raycast(worldPoint, Vector2.zero);
        if (hit.collider)
        {
            Debug.Log(hit.collider.name);
        }
    }
}
