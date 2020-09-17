using System.Linq;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] private LayerMask mask = default;
    public static Action cutSkinEvent, sewnSkinEvent;
    private PlayerActionsScript _controls;
    private bool _isHolding;
    private GameObject _heldObj;
    private float _startPosX, _startPosY = 0;
    private Camera _camera;
    private Vector2 _mousePos;
    private SpriteRenderer _heldObjSpriteRen;
    private int _oldSortOrder;

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
        _controls.Player.LeftClick.started += _ => Click();
        _controls.Player.Scroll.performed += ctx => { if (_isHolding && _heldObj.layer == 8) Rotate(ctx); };
        _controls.Player.LeftClick.performed += _ =>
        {
            if (_heldObj != null)
            {
                _isHolding = false;
                _heldObj.transform.localScale = new Vector3(1f, 1f, 1f);
                _heldObjSpriteRen.sortingOrder = _oldSortOrder;
            }
        };
    }

    //TODO: ändra så att du måste kolla IsAttached på organ så att du inte kan ta upp organ som sitter fast. 
    //OM du släpper organ sätt igång ett event kallat; OnDropOrgan, när man cuttar med såg OnSawEvent.
    private void Click()
    {
        _mousePos = _controls.Player.Mouseposition.ReadValue<Vector2>();
        _mousePos = _camera.ScreenToWorldPoint(_mousePos);
        RaycastHit2D[] hits = Physics2D.RaycastAll(_mousePos, Vector2.zero, 0f, mask);

        if (hits.Length <= 0) return;
        _heldObj = (
            from
                item in hits
            orderby
                item.transform.GetComponentInChildren<SpriteRenderer>()?.sortingLayerID,
                item.transform.GetComponentInChildren<SpriteRenderer>()?.sortingOrder descending
            select
                item
        ).ToArray().First().transform.gameObject;
        _heldObjSpriteRen = _heldObj.GetComponentInChildren<SpriteRenderer>();
        _oldSortOrder = _heldObjSpriteRen.sortingOrder;
        _heldObjSpriteRen.sortingOrder = 10;

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

    private void Rotate(InputAction.CallbackContext ctx)
    {
        if (ctx.ReadValue<Vector2>().y > 0)
            _heldObj.transform.Rotate(Vector3.forward, 90f);
        else
            _heldObj.transform.Rotate(Vector3.forward, -90f);
    }
}
