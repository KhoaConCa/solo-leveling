using Platform2D.CharacterAnimation;
using Platform2D.CharacterController;
using Platform2D.Vector;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.TextCore.Text;


namespace Platform2D.HierarchicalStateMachine
{
    /// <summary>
    /// PlayerRunState - Là một Run State của Player được kế thừa từ BaseState, được dùng để xử lý Logic và Animation thuộc Run.
    /// Tác giả: Nguyễn Ngọc Phú, Ngày tạo: 07/05/2025.
    /// </summary>
    public class PlayerRunState : BaseState<PlayerCore, PlayerStateFactory>
    {
        #region --- Overrides ---

        /// <summary>
        /// Khởi tạo PlayerRunState.
        /// </summary>
        /// <param name="stateController">Biến truyền vào mang kiểu dữ liệu PlayerCore.</param>
        /// <param name="stateFactory">Biến truyền vào mang kiểu dữ liệu PlayerStateFactory.</param>
        public PlayerRunState(PlayerCore stateController, PlayerStateFactory stateFactory) : base(stateController, stateFactory) { }

        /// <summary>
        /// Cài đặt mặc định cho Run State.
        /// </summary>
        public override void EnterState()
        {
            _stateController.States.IsMoving = true;
        }

        /// <summary>
        /// Cập nhật Run State.
        /// </summary>
        public override void UpdateState() 
        {
            CheckSwitchState();

            if (_stateController.States.OnMove != Vector2.zero)
                RunHandle();
        }

        /// <summary>
        /// Thoát Run State.
        /// </summary>
        public override void ExitState() 
        {
            
        }

        /// <summary>
        /// Kiểm tra chuyển đổi State.
        /// </summary>
        public override void CheckSwitchState() 
        {
            if (_stateController.States.OnMove == Vector2.zero || _stateController.States.IsWall)
                SwitchState(_stateFactory.Idle());
        }

        /// <summary>
        /// Chuyển đổi State.
        /// </summary>
        /// <param name="newState">Biến mang kiểu dữ liệu là BaseState.</param>
        public override void SwitchState(BaseState<PlayerCore, PlayerStateFactory> newState)
        {
            ExitState();

            _stateController.CurrentState = newState;
            _stateController.CurrentState.EnterState();
        }

        #endregion

        #region --- Methods ---

        /// <summary>
        /// Xử lý logic khi Player đang trong Run State.
        /// </summary>
        private void RunHandle()
        {
            float dirX = _stateController.States.OnMove.x < 0 ? (float)AXIS_1D.NEGATIVE : (float)AXIS_1D.POSITIVE;
            if (_stateController.transform.localScale.x != dirX)
                FlipDirection(dirX);
            float speed = _stateController.Stats.CurrentMovementSpeed * dirX;
            _stateController.Rg2D.velocity = new Vector2(speed, _stateController.Rg2D.velocity.y);
        }

        private void FlipDirection(float dirX)
        {
            float dirY = _stateController.transform.localScale.y;
            _stateController.transform.localScale = new Vector2(dirX, dirY);
        }

        #endregion

    }
}