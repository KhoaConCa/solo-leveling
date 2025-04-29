using Platform2D.CharacterInterface;
using Platform2D.GlobalChecker;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Platform2D.CharacterController
{
    /// <summary>
    /// PlayerMovementController - Được tạo ra để quản lý và xử lý các chức năng liên quan đến di chuyển của nhân vật.
    /// Tác giả: Nguyễn Ngọc Phú, Ngày tạo: 28/04/2025
    /// </summary>
    public class PlayerMovementController : MonoBehaviour, IMoveable, ICheckable
    {

        #region --- Overrides ---

        /// <summary>
        /// Thực hiện thay đổi vị trí chiều ngang của nhân vật.
        /// </summary>
        public void OnMove()
        {
            float velY = _playerController.Rg2D.velocity.y;
            float velX = _playerController.PlayerStates.IsMoving;

            float movementSpeed = _playerController.PlayerStats.MovementSpeed;

            _playerController.Rg2D.velocity = new Vector2(velX * movementSpeed, velY);
        }

        /// <summary>
        /// Thực hiện thao tác nhảy cho nhân vật
        /// </summary>
        public void OnJump()
        {
            float velY = _playerController.PlayerStates.IsJumping;
            float velX = _playerController.Rg2D.velocity.x;

            float jumpSpeed = _playerController.PlayerStats.JumpSpeed;
            
            _playerController.Rg2D.velocity = new Vector2(velX, velY * jumpSpeed);
        }

        /// <summary>
        /// Thực hiện kiểm tra xem nhân vật có đang chạm vào mặt đất không
        /// </summary>
        public void IsGrounded()
        {
            _playerController.PlayerStates.IsGrounded = _playerController.GroundChecker.Cast(Vector2.down, _contactFilter, _hit2Ds, GROUND_DISTANCE) > 0;
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
            IsGrounded();

            // Thực hiện nhảy khi 'IsGround' là true.
            if (_playerController.PlayerStates.IsGrounded)
                OnJump();

            // Khi 'IsGround' là false và 'IsJumping' > 0, reset hướng nhảy.
            if (_playerController.PlayerStates.IsJumping > 0 && !_playerController.PlayerStates.IsGrounded)
                _playerController.PlayerStates.IsJumping = 0;
        }

        #endregion

        #region --- Fields ---

        private PlatformChecker _platformChecker = new PlatformChecker();

        [SerializeField] private PlayerController _playerController;

        [SerializeField] private InputAction _inputAction;
        [SerializeField] private PlayerInputAction _playerAction;

        private ContactFilter2D _contactFilter;
        private RaycastHit2D[] _hit2Ds = new RaycastHit2D[5];

        private float direction;

        private const float GROUND_DISTANCE = 0.05f;

        #endregion

    }
}
