using Platform2D.CharacterChecker;
using Platform2D.CharacterStates;
using Platform2D.CharacterStats;
using Platform2D.HierarchicalStateMachine;
using Platform2D.Utilities;
using Unity.VisualScripting;
using UnityEngine;

namespace Platform2D.CharacterController
{
    /// <summary>
    /// EnemyController - Được dùng làm trung tâm của bộ điều khiển của Enemy.
    /// Tác giả: Nguyễn Ngọc Phú, Ngày tạo: 06/05/2025.
    /// </summary>
    public class EnemyController : MonoBehaviour
    {
        #region --- Unity Methods ---

        private void Awake()
        {
            _animator.runtimeAnimatorController = _stats.BaseStats.animator;
            _spriteRenderer.sprite = _stats.BaseStats.sprite;

            _states.Direction = gameObject.transform.localScale.x;
            _states.AnchorPosX = gameObject.transform.position.x;

            EnemyStateFactory = new EnemyStateFactory(this);
            CurrentState = EnemyStateFactory.Idle();
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
            int groundLayer = LayerMask.GetMask(TagLayerName.StaticLevel, TagLayerName.Penatrable);
            RaycastHit2D groundHit = Physics2D.Raycast(_groundDetection.position, Vector2.down, GROUND_DISTANCE, groundLayer);

            if (groundHit.collider == null)
            {
                _states.OnGround = false;
                return;
            }

            if(groundHit.collider.CompareTag(TagLayerName.Ground) || groundHit.collider.CompareTag(TagLayerName.OneWay))
                _states.OnGround = true;
        }

        private void WallChecker()
        {
            Vector2 direction = gameObject.transform.localScale.x < 0 ? Vector2.left : Vector2.right;
            _states.OnWall = _col2D.Cast(direction, _contactFilter, wallHits, WALL_DISTANCE) > 0;
        }


        #endregion

        #region --- Properties ---

        public Rigidbody2D Rg2D => _rg2D;
        public Transform trans2D => gameObject.transform;
        public Animator Animator => _animator;

        public EnemyStates States => _states;
        public EnemyStats Stats => _stats;
        public BaseState<EnemyController, EnemyStateFactory> CurrentState { get; set; }
        public EnemyStateFactory EnemyStateFactory { get; set; }

        #endregion

        #region --- Fields ---

        [Header("Filter Layer")]
        [SerializeField] private ContactFilter2D _contactFilter;

        [Header("Unity Components")]
        [SerializeField] private Rigidbody2D _rg2D;
        [SerializeField] private CapsuleCollider2D _col2D;
        [SerializeField] private Transform _groundDetection;

        [Header("States & Stats")]
        [SerializeField] private EnemyStates _states;
        [SerializeField] private EnemyStats _stats;

        [Header("Sprite & Animation")]
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Animator _animator;

        private readonly RaycastHit2D[] wallHits = new RaycastHit2D[5];

        private const float GROUND_DISTANCE = 1f;
        private const float WALL_DISTANCE = 0.2f;

        #endregion
    }
}
