using Platform2D.CameraSystem;
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
            _stateController.States.IsTouchOneWay = false;
        }

        /// <summary>
        /// Cập nhật Run State.
        /// </summary>
        public override void UpdateState() 
        {
            if (_stateController.States.OnMove != Vector2.zero)
                RunHandle();

            CheckSwitchState();
        }

        /// <summary>
        /// Thoát Run State.
        /// </summary>
        public override void ExitState() 
        {
            _stateController.States.IsMoving = false;
        }

        /// <summary>
        /// Kiểm tra chuyển đổi State.
        /// </summary>
        public override void CheckSwitchState() 
        {
            if (!_stateController.States.AllowedSwitch) return;

            if (_stateController.States.IsAttacking)
            {
                SwitchState(_stateFactory.GroundAttack());
                return;
            }

            if(_stateController.States.IsDashing && _stateController.States.CanDashing)
            {
                SwitchState(_stateFactory.Dash());
                return;
            }

            if (!_stateController.States.OnGround || _stateController.States.IsPenetrable)
            {
                SwitchState(_stateFactory.Fall());
                return;
            }

            if (_stateController.States.IsCrouch)
            {
                SwitchState(_stateFactory.Crouch());
                return;
            }

            if (_stateController.States.IsJumping)
            {
                SwitchState(_stateFactory.Jump());
                return;
            }

            if (_stateController.States.OnMove == Vector2.zero || Mathf.Abs(_stateController.States.OnMove.y) > 0.7f)
                SwitchState(_stateFactory.Idle());
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

        /// <summary>
        /// Xử lý logic khi Player đang trong Run State.
        /// </summary>
        private void RunHandle()
        {
            float dirX = _stateController.States.OnMove.x < 0 ? (float)AXIS_1D.NEGATIVE : (float)AXIS_1D.POSITIVE;
            if (_stateController.transform.localScale.x != dirX)
                FlipDirection(dirX);

            if (_stateController.States.IsWall || Mathf.Abs(_stateController.States.OnMove.y) > 0.7f)
                dirX = 0f;

            float speed = _stateController.Stats.CurrentMovementSpeed * dirX;
            _stateController.Rg2D.velocity = new Vector2(speed, _stateController.Rg2D.velocity.y);
        }

        private void FlipDirection(float dirX)
        {
            float dirY = _stateController.transform.localScale.y;
            _stateController.transform.localScale = new Vector2(dirX, dirY);
            _stateController.CameraFollower.TurnCalling();
        }

        #endregion
    }
}