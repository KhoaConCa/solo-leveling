using Platform2D.CharacterStates;
using Platform2D.CharacterStats;
using Platform2D.HierarchicalStateMachine;
using Platform2D.Utilities;
using Unity.VisualScripting;
using UnityEngine;

namespace Platform2D.CharacterController
{
    /// <summary>
    /// PlayerCore - Được dùng làm trung tâm của bộ điều khiển của Player.
    /// Tác giả: Nguyễn Ngọc Phú, Ngày tạo: 07/05/2025.
    /// </summary>
    public class PlayerCore : MonoBehaviour
    {
        #region --- Unity Methods ---

        private void Awake()
        {
            _animator.runtimeAnimatorController = _stats.BaseStats.animator;
            _spriteRenderer.sprite = _stats.BaseStats.sprite;

            StateFactory = new PlayerStateFactory(this);
            CurrentState = StateFactory.Idle();
            CurrentState.EnterState();
        }

        private void FixedUpdate()
        {
            GroundChecker();
            WallChecker();
            CurrentState.UpdateState();
        }

        #endregion

        #region --- Methods ---

        private void GroundChecker()
        {
            _states.OnGround = _col2D.Cast(Vector2.down, _contactFilter, _groundHits, GROUND_DISTANCE) > 0;
        }
        
        private void WallChecker()
        {
            Vector2 dir = _states.Direction.x < 0 ? Vector2.left : Vector2.right;
            _states.IsWall = _col2D.Cast(dir, _contactFilter, _wallHits, WALL_DISTANCE) > 0;
            Debug.Log(_states.IsWall);
        }


        #endregion

        #region --- Properties ---

        public Rigidbody2D Rg2D => _rg2D;
        public Transform BasePos => _basePos;
        public Animator Animator => _animator;

        public PlayerStatesAlter States => _states;
        public PlayerStats Stats => _stats;
        public BaseState<PlayerCore, PlayerStateFactory> CurrentState { get; set; }
        public PlayerStateFactory StateFactory { get; set; }

        #endregion

        #region --- Fields ---

        [Header("Filter Layer")]
        [SerializeField] private ContactFilter2D _contactFilter;

        [Header("Unity Components")]
        [SerializeField] private Rigidbody2D _rg2D;
        [SerializeField] private CapsuleCollider2D _col2D;
        [SerializeField] private Transform _basePos;

        [Header("States & Stats")]
        [SerializeField] private PlayerStatesAlter _states;
        [SerializeField] private PlayerStats _stats;

        [Header("Sprite & Animation")]
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Animator _animator;

        private readonly RaycastHit2D[] _groundHits = new RaycastHit2D[5];
        private readonly RaycastHit2D[] _wallHits = new RaycastHit2D[5];

        private const float GROUND_DISTANCE = 0.05f;
        private const float WALL_DISTANCE = 0.2f;

        #endregion
    }
}
