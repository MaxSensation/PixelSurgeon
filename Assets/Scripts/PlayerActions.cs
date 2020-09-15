// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/PlayerActions.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerActionsScript : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerActionsScript()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerActions"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""9e9675df-74a7-4154-ae83-b9fdec823278"",
            ""actions"": [
                {
                    ""name"": ""Drag"",
                    ""type"": ""PassThrough"",
                    ""id"": ""af00327d-ca44-4e0f-b216-3c0b19672261"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Mouseposition"",
                    ""type"": ""PassThrough"",
                    ""id"": ""91ac6de2-a157-4813-8f36-8be02f99f909"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""3ac6ef0b-25a0-4982-8708-8207f9c1908c"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""controls"",
                    ""action"": ""Drag"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b49d4f0c-1974-4805-9c8d-c59bff830fb4"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""controls"",
                    ""action"": ""Mouseposition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""controls"",
            ""bindingGroup"": ""controls"",
            ""devices"": [
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": true,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Drag = m_Player.FindAction("Drag", throwIfNotFound: true);
        m_Player_Mouseposition = m_Player.FindAction("Mouseposition", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Drag;
    private readonly InputAction m_Player_Mouseposition;
    public struct PlayerActions
    {
        private @PlayerActionsScript m_Wrapper;
        public PlayerActions(@PlayerActionsScript wrapper) { m_Wrapper = wrapper; }
        public InputAction @Drag => m_Wrapper.m_Player_Drag;
        public InputAction @Mouseposition => m_Wrapper.m_Player_Mouseposition;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Drag.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDrag;
                @Drag.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDrag;
                @Drag.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDrag;
                @Mouseposition.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMouseposition;
                @Mouseposition.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMouseposition;
                @Mouseposition.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMouseposition;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Drag.started += instance.OnDrag;
                @Drag.performed += instance.OnDrag;
                @Drag.canceled += instance.OnDrag;
                @Mouseposition.started += instance.OnMouseposition;
                @Mouseposition.performed += instance.OnMouseposition;
                @Mouseposition.canceled += instance.OnMouseposition;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    private int m_controlsSchemeIndex = -1;
    public InputControlScheme controlsScheme
    {
        get
        {
            if (m_controlsSchemeIndex == -1) m_controlsSchemeIndex = asset.FindControlSchemeIndex("controls");
            return asset.controlSchemes[m_controlsSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnDrag(InputAction.CallbackContext context);
        void OnMouseposition(InputAction.CallbackContext context);
    }
}
