using Platform2D.CanvasController;
using Platform2D.CharacterInterface;
using Platform2D.CharacterStates;
using Platform2D.CharacterStats;
using Platform2D.EnemyType;
using Platform2D.HierarchicalStateMachine;
using Platform2D.UIElement;
using Platform2D.Utilities;
using System.Collections.Generic;
using UnityEngine;

namespace Platform2D.CharacterController
{
    /// <summary>
    /// EnemyController - Được dùng làm trung tâm của bộ điều khiển của Enemy.
    /// Tác giả: Nguyễn Ngọc Phú, Ngày tạo: 06/05/2025.
    /// </summary>
    public class BossController : MonoBehaviour, IStateController<BaseState<BossController, BossStateFactory>>
    {
        #region --- Overrides ---

        public BaseState<BossController, BossStateFactory> CurrentState { get; set; }

        #endregion

        #region --- Unity Methods ---

        private void Awake()
        {
            _col2D.enabled = false;

            _animator.runtimeAnimatorController = _stats.BaseStats.animator;
            _spriteRenderer.sprite = _stats.BaseStats.sprite;

            _states.Direction = gameObject.transform.localScale.x;
            _states.AnchorPosSpawn = this.gameObject.transform.position;

            _stats.SetStats();

            CurrentState = _bossStateFactory.Detect();
            CurrentState.EnterState();
        }

        private void FixedUpdate()
        {
            GroundChecker();

            CurrentState.UpdateState();

            Debug.Log(CurrentState);
        }

        #endregion

        #region --- Methods ---

        private void GroundChecker()
        {
            if (_actionChecker.TargetPlayer == null) return;

            _states.OnGround = _col2D.Cast(Vector2.down, _contactFilter, _groundHits, GROUND_DISTANCE) > 0;

            if (!_states.OnGround && _states.IsDrop)
            {
                _rg2D.gravityScale += 1.4f;
            }

            if (_states.OnGround)
            {
                _rg2D.gravityScale = 0;
                _states.IsDrop = false;
            }
        }

        public void ResetStats()
        {
            CurrentState = _bossStateFactory.Detect();
            CurrentState.EnterState();

            _states.CanAttack = false;

            this.gameObject.transform.position = _states.AnchorPosSpawn;

            _stats.SetStats();
        }

        #endregion

        #region --- Properties ---

        public Rigidbody2D Rg2D => _rg2D;
        public Collider2D Col2D => _col2D;
        public Transform trans2D => gameObject.transform;
        public Animator Animator => _animator;
        public SpriteRenderer SpriteRenderer => _spriteRenderer;
        public ENEMY_TYPE EnemyType => _enemyType;
        public List<ENEMY_ATTACK_TYPE> AttackType => _attackType;

        public BossAreaManager AreaManager => _areaManager;

        public BossActionChecker ActionChecker => _actionChecker;
        public UIController UICtrl => _uiCtrl;
        public CustomHealthBar HealthBar => _healthBar;

        public BossStates States => _states;
        public BossBaseStats Stats => _stats;

        #endregion

        #region --- Fields ---

        [Header("Filter Layer")]
        [SerializeField] private ContactFilter2D _contactFilter;

        [Header("Enemy's Type")]
        [SerializeField] private ENEMY_TYPE _enemyType;
        [SerializeField] private List<ENEMY_ATTACK_TYPE> _attackType;

        [Header("Unity Components")]
        [SerializeField] private Rigidbody2D _rg2D;
        [SerializeField] private Collider2D _col2D;
        [SerializeField] private Transform _groundDetection;

        [Header("Custom Components")]
        [SerializeField] private BossStateFactory _bossStateFactory;
        [SerializeField] private BossAreaManager _areaManager;
        [SerializeField] private BossActionChecker _actionChecker;
        [SerializeField] private UIController _uiCtrl;
        [SerializeField] private CustomHealthBar _healthBar;

        [Header("States & Stats")]
        [SerializeField] private BossStates _states;
        [SerializeField] private BossBaseStats _stats;

        [Header("Sprite & Animation")]
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Animator _animator;

        private readonly RaycastHit2D[] _groundHits = new RaycastHit2D[1];

        private const float GROUND_DISTANCE = 0.05f;

        #endregion
    }
}
