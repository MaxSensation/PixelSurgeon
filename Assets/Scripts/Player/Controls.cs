using System;
using System.Linq;
using Human;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class Controls : MonoBehaviour
    {
        public static Action<GameObject> OnCutEvent,
            OnSawEvent,
            OnSewnEvent,
            OnBodyPartPickupEvent,
            OnToolPickupEvent,
            OnDropBodyPartEvent,
            OnDropToolEvent;

        [SerializeField] private LayerMask mask;
        private Camera _camera;
        private PlayerActionsScript _controls;
        private GameObject _heldObj;
        private SpriteRenderer _heldObjSpriteRen;
        private bool _isHolding;
        private Vector2 _mousePos;
        private int _oldSortOrder;
        private float _startPosX, _startPosY;

        private void Awake()
        {
            _controls = new PlayerActionsScript();
            _camera = Camera.main;
        }

        private void Start()
        {
            _controls.Player.Scroll.performed += Scroll;
            _controls.Player.LeftClick.started += _ => LeftClick();
            _controls.Player.LeftClick.performed += _ => LeftClickRelease();
            _controls.Player.RightClick.performed += _ => RightClick();
        }

        private void Update()
        {
            if (_isHolding) HoldObj();
        }

        private void OnEnable()
        {
            _controls.Enable();
        }

        private void OnDisable()
        {
            _controls.Disable();
        }

        private void Scroll(InputAction.CallbackContext ctx)
        {
            if (_isHolding && _heldObj.layer == 8) Rotate(ctx);
        }

        private void LeftClickRelease()
        {
            {
                if (_heldObj == null) return;
                _isHolding = false;
                _heldObj.transform.localScale = new Vector3(1f, 1f, 1f);
                _heldObjSpriteRen.sortingOrder = _oldSortOrder;
                switch (_heldObj.layer)
                {
                    case 8:
                        OnDropToolEvent?.Invoke(_heldObj);
                        break;
                    case 9:
                        OnDropBodyPartEvent?.Invoke(_heldObj);
                        break;
                }

                _heldObj = null;
            }
        }

        private void RightClick()
        {
            if (_heldObj == null) return;
            switch (_heldObj.name)
            {
                case "Saw":
                    OnSawEvent?.Invoke(_heldObj.GetComponent<Tool.Tool>().GetBodyPart());
                    break;
                case "Scalpel":
                    OnCutEvent?.Invoke(_heldObj.GetComponent<Tool.Tool>().GetBodyPart());
                    break;
                case "Sewingkit":
                    OnSewnEvent?.Invoke(_heldObj.GetComponent<Tool.Tool>().GetBodyPart());
                    break;
            }
        }

        private void LeftClick()
        {
            _mousePos = _controls.Player.Mouseposition.ReadValue<Vector2>();
            _mousePos = _camera.ScreenToWorldPoint(_mousePos);
            var hits = Physics2D.RaycastAll(_mousePos, Vector2.zero, 0f, mask);
            var gameobjects = hits.Where(hit => hit.collider.gameObject.activeSelf)
                .Select(hit => hit.collider.gameObject).ToArray();
            if (gameobjects.Length <= 0) return;
            _heldObj = (
                from
                    item in gameobjects
                orderby
                    item.transform.GetChild(0).GetComponent<SpriteRenderer>()?.sortingLayerID,
                    item.transform.GetChild(0).GetComponent<SpriteRenderer>()?.sortingOrder descending
                where
                    mask == (mask | (1 << item.transform.gameObject.layer))
                select
                    item.transform.gameObject
            ).ToArray().First();

            if (_heldObj.layer == 9 && _heldObj.GetComponent<BodyPart>().IsAttached())
            {
                _heldObj = null;
                return;
            }

            _heldObjSpriteRen = _heldObj.transform.GetChild(0).GetComponent<SpriteRenderer>();
            _oldSortOrder = _heldObjSpriteRen.sortingOrder;
            _heldObjSpriteRen.sortingOrder = 10;

            switch (_heldObj.layer)
            {
                //layer 8 == Tools
                case 8:
                    _startPosX = 0;
                    _startPosY = 0;
                    OnToolPickupEvent?.Invoke(_heldObj);
                    break;
                //layer 9 == Organs
                case 9:
                    _startPosX = _mousePos.x - _heldObj.transform.position.x;
                    _startPosY = _mousePos.y - _heldObj.transform.position.y;
                    OnBodyPartPickupEvent?.Invoke(_heldObj);
                    break;
            }

            _heldObj.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
            _isHolding = true;
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
}