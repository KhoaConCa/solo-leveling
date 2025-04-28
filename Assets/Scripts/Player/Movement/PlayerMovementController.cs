using Platform2D.CharacterInterface;
using Platform2D.GlobalChecker;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

namespace Platform2D.CharacterController
{
    /// <summary>
    /// PlayerMovementController - Được tạo ra để quản lý và xử lý các chức năng liên quan đến di chuyển của nhân vật.
    /// Tác giả: Nguyễn Ngọc Phú, Ngày tạo: 28/04/2025
    /// </summary>
    public class PlayerMovementController : MonoBehaviour, IMoveable
    {

        #region --- Overrides ---

        /// <summary>
        /// Thực hiện thay đổi vị trí chiều ngang của nhân vật.
        /// </summary>
        public void OnMove()
        {
            float velY = _playerController.Rg2D.velocity.y;
            float velX = _playerController.PlayerStates.isMoving;

            float movementSpeed = _playerController.PlayerStats.movementSpeed;

            _playerController.Rg2D.velocity = new Vector2(velX * movementSpeed, velY);
        }

        /// <summary>
        /// Thực hiện thao tác nhảy cho nhân vật
        /// </summary>
        public void OnJump()
        {

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
}
