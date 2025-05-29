using Platform2D.CharacterAnimation;
using Platform2D.CharacterController;
using Platform2D.Utilities;
using System.Collections;
using UnityEngine;

namespace Platform2D.HierarchicalStateMachine
{
    /// <summary>
    /// BossAppearanceState - Là một Detect State của Boss được kế thừa từ BaseState, được dùng để xử lý Logic và Animation thuộc Detect.
    /// Tác giả: Nguyễn Ngọc Phú, Ngày tạo: 28/05/2025.
    /// </summary>
    public class BossAppearanceState : BaseState<BossController, BossStateFactory>
    {
        #region --- Overrides ---

        /// <summary>
        /// Khởi tạo BossAppearanceState.
        /// </summary>
        /// <param name="stateController">Biến truyền vào mang kiểu dữ liệu BossController.</param>
        /// <param name="stateFactory">Biến truyền vào mang kiểu dữ liệu BossStateFactory.</param>
        public BossAppearanceState(BossController stateController, BossStateFactory stateFactory) : base(stateController, stateFactory) { }

        /// <summary>
        /// Cài đặt mặc định cho Detect State.
        /// </summary>
        public override void EnterState() 
        {
            _stateController.States.Invulnerable = true;

            _stateController.UICtrl.ShowBossHealthBar();
            _stateController.HealthBar.SetMaxHealth(_stateController.Stats.BaseStats.healthPoint);
            _stateController.Stats.CurrentHealthPoint = 0;
            _stateController.HealthBar.ChangeHealth(0);
            _stateController.HealthBar.ChangeText(_stateController.Stats.BaseStats.name);

            TimeHeal();

            _isDone = false;
        }

        /// <summary>
        /// Cập nhật Detect State.
        /// </summary>
        public override void UpdateState() 
        {
            if(!_isDone)
                AppearanceHandle();

            CheckSwitchState();
        }

        /// <summary>
        /// Thoát Detect State.
        /// </summary>
        public override void ExitState()
        {
            _stateController.States.Invulnerable = false;
            _stateController.Col2D.enabled = true;
            _stateController.Rg2D.gravityScale = 1f;
        }

        /// <summary>
        /// Kiểm tra chuyển đổi State.
        /// </summary>
        public override void CheckSwitchState() 
        {
            if(_stateController.States.OnGround)
            {
                SwitchState(_stateFactory.Attack());
            }
        }

        /// <summary>
        /// Chuyển đổi State.
        /// </summary>
        /// <param name="newState">Biến mang kiểu dữ liệu là BaseState.</param>
        public override void SwitchState(BaseState<BossController, BossStateFactory> newState)
        {
            base.SwitchState(newState);
        }

        #endregion

        #region --- Methods ---

        /// <summary>
        /// Xử lý logic khi Boss đang trong Detect State.
        /// </summary>
        private void AppearanceHandle()
        {
            if (_stateController.Stats.CurrentHealthPoint >= _stateController.Stats.BaseStats.healthPoint && _stateController.States.IsFinishing)
            {
                _stateController.States.IsDrop = true;
                _isDone = true;
                return;
            }

            if (_stateController.Stats.CurrentHealthPoint < _stateController.Stats.BaseStats.healthPoint)
            {
                _stateController.Stats.CurrentHealthPoint += _stateController.Stats.BaseStats.healthPoint / _timeHeal / 50;

                if (_stateController.Stats.CurrentHealthPoint >= _stateController.Stats.BaseStats.healthPoint)
                {
                    _stateController.Stats.CurrentHealthPoint = _stateController.Stats.BaseStats.healthPoint;
                    _stateController.Col2D.enabled = true;
                    _stateController.States.Invulnerable = false;
                }

                _stateController.HealthBar.ChangeHealth(_stateController.Stats.CurrentHealthPoint);
            }

            if(_stateController.Col2D.bounds.center.y < _stateController.States.AnchorPosCenter.transform.position.y)
            {
                var speed = _stateController.Stats.BaseStats.movementSpeed * Vector2.up.y;
                _stateController.Rg2D.velocity = new Vector2(_stateController.Rg2D.velocity.x, speed);
            }
            else
                _stateController.Rg2D.velocity = Vector2.zero;
        }

        private void TimeHeal()
        {
            Vector2 pointCenter = _stateController.States.AnchorPosCenter.transform.position - _stateController.Col2D.bounds.center;
            var length = pointCenter.magnitude;
            var vel = _stateController.Stats.BaseStats.movementSpeed * Vector2.up.y;

            _timeHeal = length / vel;
        }

        #endregion

        #region --- Fields ---

        private float _timeHeal;
        private bool _isDone;

        #endregion
    }
}