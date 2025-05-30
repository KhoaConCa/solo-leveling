using Platform2D.CharacterAnimation;
using Platform2D.CharacterController;
using Platform2D.Utilities;
using System.Collections;
using UnityEngine;

namespace Platform2D.HierarchicalStateMachine
{
    /// <summary>
    /// BossDetectState - Là một Detect State của Boss được kế thừa từ BaseState, được dùng để xử lý Logic và Animation thuộc Detect.
    /// Tác giả: Nguyễn Ngọc Phú, Ngày tạo: 28/05/2025.
    /// </summary>
    public class BossDetectState : BaseState<BossController, BossStateFactory>
    {
        #region --- Overrides ---

        /// <summary>
        /// Khởi tạo BossDetectState.
        /// </summary>
        /// <param name="stateController">Biến truyền vào mang kiểu dữ liệu BossController.</param>
        /// <param name="stateFactory">Biến truyền vào mang kiểu dữ liệu BossStateFactory.</param>
        public BossDetectState(BossController stateController, BossStateFactory stateFactory) : base(stateController, stateFactory) { }

        /// <summary>
        /// Cài đặt mặc định cho Detect State.
        /// </summary>
        public override void EnterState() 
        {
            _stateController.States.Invulnerable = true;
            _stateController.States.IsDetecting = false;

            _stateController.Rg2D.velocity = Vector2.zero;
            _stateController.Col2D.enabled = false;

            if (_stateController.transform.localScale.x < 0)
                _stateController.transform.localScale = new Vector2(1, 1);

            _stateController.Animator.SetTrigger("wait");
            _stateController.UICtrl.ShowBossHealthBar(false);
        }

        /// <summary>
        /// Cập nhật Detect State.
        /// </summary>
        public override void UpdateState() 
        {
            DetectHandle();

            CheckSwitchState();
        }

        /// <summary>
        /// Thoát Detect State.
        /// </summary>
        public override void ExitState()
        {
            _stateController.States.IsDetecting = false;
        }

        /// <summary>
        /// Kiểm tra chuyển đổi State.
        /// </summary>
        public override void CheckSwitchState() 
        {
            if (_stateController.States.IsDead)
            {
                SwitchState(_stateFactory.Dead());
                return;
            }

            if (_stateController.States.IsDetecting)
                SwitchState(_stateFactory.Appearance());
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
        private void DetectHandle()
        {
            Vector2 boxSize = new Vector2(_stateController.Stats.BaseStats.detectedRange * 2.5f, _stateController.Stats.BaseStats.detectedRange);
            Collider2D foundTarget = Physics2D.OverlapBox(_stateController.Col2D.bounds.center, boxSize, 0f, LayerMask.GetMask("Player"));
            
            if (foundTarget == null) return;

            if (!foundTarget.gameObject.CompareTag(TagLayerName.Player)) return;
            Debug.Log(foundTarget.gameObject.name);

            float range = (foundTarget.transform.position - _stateController.transform.position).magnitude;

            if(range >= _stateController.Stats.BaseStats.detectedRange * 2.5f)
            {
                _stateController.ActionChecker.TargetPlayer = null;
                _stateController.ActionChecker.Player = null;
                return;
            }

            _stateController.ActionChecker.TargetPlayer = foundTarget.gameObject;
            _stateController.ActionChecker.Player = foundTarget.gameObject.GetComponentInParent<PlayerActionChecker>();
            _stateController.States.IsDetecting = true;
        }

        #endregion
    }
}