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
    public class PlayerAnimationController : IMoveable, IAction
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
            var isMovingOnGround = (Mathf.Abs(_playerController.PlayerStates.Horizontal) > 0.01f) && _playerController.PlayerStates.IsGrounded;
            _animator.SetBool(AnimationStrings.IsMoving, isMovingOnGround);
        }

        /// <summary>
        /// Thực hiện animation nhảy khi nhân vật nhảy.
        /// </summary>
        public void OnJump()
        {
            if(_playerController.PlayerStates.IsJumping)
                _animator.SetTrigger(AnimationStrings.JumpTrigger);
        }

        public void OnFall(float yVel)
        {
            _animator.SetFloat(AnimationStrings.YVelocity, yVel);
        }

        /// <summary>
        /// Kích hoạt điều của 'IsGround' của các animation.
        /// </summary>
        public void OnGrounded()
        {
            _animator.SetBool(AnimationStrings.IsGrounded, _playerController.PlayerStates.IsGrounded);
        }

        /// <summary>
        /// Thực hiện animation cúi người khi nhân vật cúi.
        /// </summary>
        public void OnCrouch()
        {
            _animator.SetBool(AnimationStrings.IsCrouching, _playerController.PlayerStates.IsCrouching);
        }

        /// <summary>
        /// Thực hiện animation lướt khi nhân vật lướt.
        /// </summary>
        public void OnDash()
        {
            _animator.SetBool(AnimationStrings.Dash, _playerController.PlayerStates.IsDashing);
        }

        public void OnAttack()
        {
            _animator.SetTrigger(AnimationStrings.AttackTrigger);
        }

        #endregion

        #region --- Properties ---

        public bool CanMove => _animator.GetBool(AnimationStrings.CanMove);

        #endregion

        #region --- Fields ---

        private readonly PlayerController _playerController;

        private readonly Animator _animator;

        #endregion
    }
}
