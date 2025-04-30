using Platform2D.CharacterController;
using Platform2D.CharacterInterface;
using Platform2D.CharacterStates;
using Platform2D.CharacterStats;
using Unity.VisualScripting;
using UnityEngine;

namespace Platform2D.CharacterAnimation
{
    /// <summary>
    /// PlayerAnimationController - Được tạo ra nhằm mục đích quản lý và chuyển đổi giữa các animation với nhau
    /// Tác giả: Nguyễn Ngọc Phú, Ngày tạo: 29/04/2025
    /// </summary>
    [System.Serializable]
    public class PlayerAnimationController : IMoveable
    {
        #region --- Constructor ---

        public PlayerAnimationController(
            PlayerController playerController,
            Animator animator
        ) {
            _playerController = playerController; ;
            _animator = animator;
        }

        #endregion

        #region --- Overrides ---

        /// <summary>
        /// Thực hiện animation chạy khi nhân vật di chuyển
        /// </summary>
        public void OnMove()
        {
            var isMovingOnGround = (Mathf.Abs(_playerController.PlayerStates.IsMoving) > 0.01f) && _playerController.PlayerStates.IsGrounded;
            _animator.SetBool(PlayerAnimationParameters.IsMoving, isMovingOnGround);
        }

        public void OnJump()
        {

        }

        #endregion

        #region --- Fields ---

        private readonly PlayerController _playerController;

        private readonly Animator _animator;

        #endregion
    }
}
