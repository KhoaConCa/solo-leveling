using Platform2D.CharacterAnimation;
using Platform2D.CharacterController;
using Platform2D.Vector;
using System.Collections;
using UnityEngine;

namespace Platform2D.HierarchicalStateMachine
{
    /// <summary>
    /// PlayerDashState - Là một Dash State của Player được kế thừa từ BaseState, được dùng để xử lý Logic và Animation thuộc Dash.
    /// Tác giả: Nguyễn Ngọc Phú, Ngày tạo: 09/05/2025.
    /// </summary>
    public class PlayerDashState : BaseState<PlayerCore, PlayerStateFactory>
    {
        #region --- Overrides ---

        /// <summary>
        /// Khởi tạo PlayerDashState.
        /// </summary>
        /// <param name="stateController">Biến truyền vào mang kiểu dữ liệu PlayerCore.</param>
        /// <param name="stateFactory">Biến truyền vào mang kiểu dữ liệu PlayerStateFactory.</param>
        public PlayerDashState(PlayerCore stateController, PlayerStateFactory stateFactory) : base(stateController, stateFactory) { }

        /// <summary>
        /// Cài đặt mặc định cho Dash State.
        /// </summary>
        public override void EnterState()
        {
        }

        /// <summary>
        /// Cập nhật Dash State.
        /// </summary>
        public override void UpdateState()
        {
            if (_stateController.States.IsDashing)
                _stateController.StartCoroutine(DashHandle());

            if(!_stateController.States.CanDashing && !_stateController.States.IsDashing)
                CheckSwitchState();
        }

        /// <summary>
        /// Thoát Dash State.
        /// </summary>
        public override void ExitState()
        {
            
        }

        /// <summary>
        /// Kiểm tra chuyển đổi State.
        /// </summary>
        public override void CheckSwitchState()
        {
            if (_stateController.States.OnGround && !_stateController.States.IsPenetrable)
            {
                if (_stateController.States.OnMove == Vector2.zero || Mathf.Abs(_stateController.States.OnMove.y) > 0.7f)
                    SwitchState(_stateFactory.Idle());
                else
                    SwitchState(_stateFactory.Run());
            }
            else if (!_stateController.States.OnGround)
                SwitchState(_stateFactory.Fall());
            else if (_stateController.States.IsJumping && _stateController.States.CanJump)
                SwitchState(_stateFactory.Jump());
        }

        /// <summary>
        /// Chuyển đổi State.
        /// </summary>
        /// <param name="newState">Biến mang kiểu dữ liệu là BaseState.</param>
        public override void SwitchState(BaseState<PlayerCore, PlayerStateFactory> newState)
        {
            base.SwitchState(newState);
        }

        #endregion

        #region --- Methods ---

        private IEnumerator DashHandle()
        {
            var speed = _stateController.Stats.DashSpeed * _stateController.transform.localScale.x;
            _stateController.Rg2D.velocity = new Vector2(speed, _stateController.Rg2D.velocity.y);

            yield return new WaitForSeconds(_stateController.Stats.BaseStats.dashDuration);

            _stateController.States.CanDashing = false;
            _stateController.States.IsDashing = false;
        }
        #endregion
    }
}