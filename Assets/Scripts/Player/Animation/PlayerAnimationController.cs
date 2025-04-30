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

        /// <summary>
        /// Thực hiện animation nhảy khi nhân vật nhảy.
        /// </summary>
        public void OnJump()
        {
            if(_playerController.PlayerStates.IsJumping > 0.01f && _playerController.PlayerStates.IsGrounded)
                _animator.SetTrigger(PlayerAnimationParameters.Jump);

            _animator.SetFloat(PlayerAnimationParameters.YVelocity, _playerController.Rg2D.velocity.y);
        }

        /// <summary>
        /// Kích hoạt điều của 'IsGround' của các animation.
        /// </summary>
        public void OnGrounded()
        {
            _animator.SetBool(PlayerAnimationParameters.IsGrounded, _playerController.PlayerStates.IsGrounded);
        }

        /// <summary>
        /// Thực hiện animation cúi người khi nhân vật cúi.
        /// </summary>
        public void OnCrouch()
        {
            _animator.SetBool(PlayerAnimationParameters.IsCrouching, _playerController.PlayerStates.IsCrouching);
        }

        /// <summary>
        /// Thực hiện animation lướt khi nhân vật lướt.
        /// </summary>
        public void OnDash()
        {
            _animator.SetBool(PlayerAnimationParameters.Dash, _playerController.PlayerStates.IsDashing);
        }

        #endregion

        #region --- Fields ---

        private readonly PlayerController _playerController;

        private readonly Animator _animator;

        #endregion
    }
}
