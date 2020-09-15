using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] private LayerMask mask;
    private PlayerActionsScript _controls;
    private bool _isHolding;
    private GameObject _heldObj;
    private float _startPosX, _startPosY;
    private Camera _camera;
    private Vector2 _mousePosition;

    private void Awake()
    {
        _controls = new PlayerActionsScript();
        _camera = Camera.main;

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
        _controls.Player.Click.started += _ => Click();
        _controls.Player.Click.performed += _ => _isHolding = false;
    }

    private void Click()
    {
        _mousePosition = _controls.Player.Mouseposition.ReadValue<Vector2>();
        _mousePosition = _camera.ScreenToWorldPoint(_mousePosition);
        var hit = Physics2D.Raycast(_mousePosition, Vector2.zero, 0f, mask);
        if (hit.collider != null)
        {
            _isHolding = true;
            _heldObj = hit.collider.gameObject;
            _startPosX = _mousePosition.x - _heldObj.transform.position.x;
            _startPosY = _mousePosition.y - _heldObj.transform.position.y;

        }
    }

    private void Update()
    {
        if (_isHolding)
        {
            _mousePosition = _controls.Player.Mouseposition.ReadValue<Vector2>();
            _mousePosition = _camera.ScreenToWorldPoint(_mousePosition);
            _heldObj.transform.position = new Vector3(_mousePosition.x - _startPosX, _mousePosition.y - _startPosY, 0);
        }
    }
}
