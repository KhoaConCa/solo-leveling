using Platform2D.CameraSystem;
using Platform2D.CharacterStates;
using Platform2D.CharacterStats;
using Platform2D.HierarchicalStateMachine;
using Platform2D.Utilities;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.VersionControl.Asset;

namespace Platform2D.CharacterController
{
    /// <summary>
    /// PlayerCore - Được dùng làm trung tâm của bộ điều khiển của Player.
    /// Tác giả: Nguyễn Ngọc Phú, Ngày tạo: 07/05/2025.
    /// </summary>
    public class PlayerCore : MonoBehaviour, IStateController<BaseState<PlayerCore, PlayerStateFactory>>
    {
        #region --- Overrides ---

        public BaseState<PlayerCore, PlayerStateFactory> CurrentState { get; set; }

        #endregion

        #region --- Unity Methods ---

        private void Awake()
        {
            _animator.runtimeAnimatorController = _stats.BaseStats.animator;
            _spriteRenderer.sprite = _stats.BaseStats.sprite;

            _cameraFollower = _cameraFollowerGO.gameObject.GetComponent<CameraFollower>();

            _coolDown = new Utilities.Timer();

            StateFactory = new PlayerStateFactory(this);
            CurrentState = StateFactory.Idle();
            CurrentState.EnterState();
        }

        private void FixedUpdate()
        {
            if (_states.IsPenetrable && _movementChecker.IsOneWay)
                _movementChecker.TryStartDisable();

            Debug.Log(CurrentState);

            ResetDashingCooldown();

            GroundChecker();
            WallChecker();
            CeilingChecker();

            CurrentState.UpdateState();
        }

        #endregion

        #region --- Methods ---

        /// <summary>
        /// Kiểm tra hiện tại Player có đứng trên Ground.
        /// </summary>
        private void GroundChecker()
        {
            _states.OnGround = _col2D.Cast(Vector2.down, _contactFilter, _groundHits, GROUND_DISTANCE) > 0;
        }
        
        /// <summary>
        /// Kiểm tra hiện tại Player có chạm Wall.
        /// </summary>
        private void WallChecker()
        {
            Vector2 dir = _states.Direction.x < 0 ? Vector2.left : Vector2.right;
            _states.IsWall = _col2D.Cast(dir, _contactFilter, _wallHits, WALL_DISTANCE) > 0;
        }

        /// <summary>
        /// Kiểm tra hiện tại Player có chạm Ceiling.
        /// </summary>
        private void CeilingChecker()
        {
            _states.IsCeiling = _col2D.Cast(Vector2.up, _contactFilter, _ceilingHits, CEILING_DISTANCE) > 0;
        }

        private void ResetDashingCooldown()
        {
            if (!_states.CanDashing)
            {
                bool countDown;
                _coolDown.FixedTimeCountdown(out countDown, _stats.BaseStats.dashCoolDown);

                if (countDown)
                    _states.CanDashing = countDown;

                Debug.Log($"Can Dashing: {_states.CanDashing}");
            }
            else if (_states.CanDashing && _states.IsDashing)
            {
                _coolDown.StartCountdown();
            }

        }

        #endregion

        #region --- Properties ---

        public Rigidbody2D Rg2D => _rg2D;
        public CapsuleCollider2D Col2D => _col2D;
        public Transform BasePos => _basePos;
        public Animator Animator => _animator;

        public PlayerMovementChecker MovementChecker => _movementChecker;
        public PlayerActionChecker ActionChecker => _actionChecker;

        public CameraFollower CameraFollower => _cameraFollower;

        public PlayerStatesAlter States => _states;
        public PlayerStats Stats => _stats;
        public PlayerStateFactory StateFactory { get; set; }

        #endregion

        #region --- Fields ---

        [Header("Filter Layer")]
        [SerializeField] private ContactFilter2D _contactFilter;

        [Header("Unity Components")]
        [SerializeField] private Rigidbody2D _rg2D;
        [SerializeField] private CapsuleCollider2D _col2D;
        [SerializeField] private Transform _basePos;
        [SerializeField] private CameraFollower _cameraFollowerGO;

        [Header("Custom Components")]
        [SerializeField] private PlayerMovementChecker _movementChecker;
        [SerializeField] private PlayerActionChecker _actionChecker;

        [Header("States & Stats")]
        [SerializeField] private PlayerStatesAlter _states;
        [SerializeField] private PlayerStats _stats;

        [Header("Sprite & Animation")]
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Animator _animator;

        [SerializeField] private CameraFollower _cameraFollower;

        private Utilities.Timer _coolDown;

        private readonly RaycastHit2D[] _groundHits = new RaycastHit2D[1];
        private readonly RaycastHit2D[] _wallHits = new RaycastHit2D[5];
        private readonly RaycastHit2D[] _ceilingHits = new RaycastHit2D[5];

        private const float GROUND_DISTANCE = 0.05f;
        private const float WALL_DISTANCE = 0.2f;
        private const float CEILING_DISTANCE = 0.3f;

        #endregion
    }
}
