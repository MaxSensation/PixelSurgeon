using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] private LayerMask mask = default;
    private PlayerActionsScript _controls;
    private bool _isHolding;
    private GameObject _heldObj;
    private float _startPosX, _startPosY = 0;
    private Camera _camera;
    private Vector2 _mousePos;

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
        _controls.Player.Click.performed += _ =>
        {
            if (_heldObj != null)
            {
                _isHolding = false;
                _heldObj.transform.localScale = new Vector3(1f, 1f, 1f);
            }
        };
    }

    private void Click()
    {
        _mousePos = _controls.Player.Mouseposition.ReadValue<Vector2>();
        _mousePos = _camera.ScreenToWorldPoint(_mousePos);
        RaycastHit2D[] hits = Physics2D.RaycastAll(_mousePos, Vector2.zero, 0f, mask);

        if (hits.Length > 0)
        {
            _heldObj = hits.OrderByDescending(item => item.transform.GetComponentInChildren<SpriteRenderer>()?.sortingOrder).First().transform.gameObject;

            if (_heldObj.layer == 9) //layer 9 == Organs
            {
                _startPosX = _mousePos.x - _heldObj.transform.position.x;
                _startPosY = _mousePos.y - _heldObj.transform.position.y;
            }
            else if (_heldObj.layer == 8) //layer 8 == Tools
            {
                _startPosX = 0;
                _startPosY = 0;
            }
            _heldObj.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
            _isHolding = true;
        }

    }

    private void Update()
    {
        if (_isHolding)
        {
            HoldObj();
        }
    }

    private void HoldObj()
    {
        _mousePos = _controls.Player.Mouseposition.ReadValue<Vector2>();
        _mousePos = _camera.ScreenToWorldPoint(_mousePos);
        _heldObj.transform.position = new Vector3(_mousePos.x - _startPosX, _mousePos.y - _startPosY, 0);
    }

}
