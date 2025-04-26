using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class PlayerMovementController : MonoBehaviour, IMoveable
{

    #region --- Overrides ---

    public void OnMove()
    {
        float velY = _playerController.rigidbody2D.velocity.y;
        float velX = _playerController.playerStates.isMoving;

        float movementSpeed = _playerController.playerStats.movementSpeed;

        _playerController.rigidbody2D.velocity = new Vector2(velX * movementSpeed, velY);
    }

    #endregion

    #region --- Unity Methods ---

    public void Awake()
    {
        _playerController = gameObject.GetComponent<PlayerController>();

        /*if (_platformChecker.IsMobilePlatform())
        {
            _inputAction = gameObject.GetComponent<InputAction>();
            _playerAction = new PlayerInputAction();

            _playerAction.Player.Move.performed += tcx =>
            {
                _playerController.playerStates.isMoving = tcx.ReadValue<float>();
            };
            _playerAction.Player.Move.canceled += tcx =>
            {
                _playerController.playerStates.isMoving = tcx.ReadValue<float>();
            };
        }*/
    }

    public void FixedUpdate()
    {
        OnMove();
    }

    #endregion

    #region --- Fields ---

    private PlatformChecker _platformChecker = new PlatformChecker();

    [SerializeField] private PlayerController _playerController;

    [SerializeField] private InputAction _inputAction;
    [SerializeField] private PlayerInputAction _playerAction;

    private float direction;

    #endregion

}
