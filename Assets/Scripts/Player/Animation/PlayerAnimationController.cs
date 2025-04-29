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
    public class PlayerAnimationController : MonoBehaviour, IMoveable
    {
        #region --- Overrides ---

        /// <summary>
        /// Thực hiện animation chạy khi nhân vật di chuyển
        /// </summary>
        public void OnMove()
        {
            _animator.SetBool(PlayerAnimationParameters.IsMoving, Mathf.Abs(_playerController.PlayerStates.IsMoving) > 0.01f);
        }

        public void OnJump()
        {

        }

        #endregion

        #region --- Unity Methods ---

        public void Awake()
        {
            _playerController = this.gameObject.GetComponentInParent<PlayerController>();
            _animator = this.gameObject.GetComponent<Animator>();
        }

        public void FixedUpdate()
        {
            // Chỉ thực hiện animation chạy khi 'IsGround' là true.
            if(_playerController.PlayerStates.IsGrounded)
                OnMove();
        }

        #endregion

        #region --- Fields ---

        [SerializeField] private PlayerController _playerController;

        [SerializeField] private Animator _animator;

        #endregion
    }
}
