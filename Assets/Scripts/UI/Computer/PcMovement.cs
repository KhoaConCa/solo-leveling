using Platform2D.CharacterInterfaceIS;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PcMovement : MonoBehaviour, IMoveable
{


    #region --- Overrides ---

    private void OnMove(InputAction.CallbackContext context)
    {
        Debug.Log($"OnMove: {context.ReadValue<float>()}");
    }

    void IMoveable.OnMove(InputAction.CallbackContext context)
    {
        Debug.Log($"OnMove: {context.ReadValue<float>()}");
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        Debug.Log($"OnJump: {context.ReadValue<float>()}");
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        Debug.Log($"OnCrouch: {context.ReadValue<float>()}");
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        Debug.Log($"OnDash: {context.ReadValue<float>()}");
    }

    #endregion

    #region --- Unity Methods ---

    private void Awake()
    {
        var player = GameObject.FindGameObjectWithTag(_tagPlayer);

        _playerInputAction = new PlayerInputAction();
        _playerInputAction.Enable();

        _playerInputAction.Player.Move.performed += OnMove;
        _playerInputAction.Player.Move.canceled += OnMove;
        _playerInputAction.Player.Jump.started += OnJump;
        _playerInputAction.Player.Crouch.started += OnCrouch;
        _playerInputAction.Player.Dash.started += OnDash;
    }

    #endregion

    #region --- Fields ---

    [SerializeField] private PlayerInputAction _playerInputAction;

    [SerializeField] private string _tagPlayer = "MainPlayer";

    #endregion

}
