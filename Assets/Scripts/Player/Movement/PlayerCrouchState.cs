using Platform2D.CharacterAnimation;
using Platform2D.CharacterController;
using Platform2D.Vector;
using System.Collections;
using UnityEngine;

namespace Platform2D.HierarchicalStateMachine
{
    /// <summary>
    /// PlayerCrouchState - Là một Crouch State của Player được kế thừa từ BaseState, được dùng để xử lý Logic và Animation thuộc Crouch.
    /// Tác giả: Nguyễn Ngọc Phú, Ngày tạo: 07/05/2025.
    /// </summary>
    public class PlayerCrouchState : BaseState<PlayerCore, PlayerStateFactory>
    {
        #region --- Overrides ---

        /// <summary>
        /// Khởi tạo PlayerCrouchState.
        /// </summary>
        /// <param name="stateController">Biến truyền vào mang kiểu dữ liệu PlayerCore.</param>
        /// <param name="stateFactory">Biến truyền vào mang kiểu dữ liệu PlayerStateFactory.</param>
        public PlayerCrouchState(PlayerCore stateController, PlayerStateFactory stateFactory) : base(stateController, stateFactory) { }

        /// <summary>
        /// Cài đặt mặc định cho Crouch State.
        /// </summary>
        public override void EnterState() 
        {
            _stateController.States.AllowedSwitch = false;

            _colOffsetDefault = _stateController.Col2D.offset;
            _colSizeDefault = _stateController.Col2D.size;

            _stateController.Col2D.direction = CapsuleDirection2D.Horizontal;
            _stateController.Col2D.size = _colSizeTarget;
            _stateController.Col2D.offset = _colOffsetTarget;

            _subState = _stateFactory.Run();
            _subState.EnterState();
        }

        /// <summary>
        /// Cập nhật Crouch State.
        /// </summary>
        public override void UpdateState() 
        {
            CrouchHandle();

            if (!_stateController.States.IsCeiling)
            {
                CheckSwitchState();
            }
        }

        /// <summary>
        /// Thoát Crouch State.
        /// </summary>
        public override void ExitState() 
        {
            _stateController.States.AllowedSwitch = true;

            _stateController.Col2D.direction = CapsuleDirection2D.Vertical;

            _stateController.Col2D.size = _colSizeDefault;
            _stateController.Col2D.offset = _colOffsetDefault;

            _subState = null;

            _stateController.States.IsCrouch = false;
        }

        /// <summary>
        /// Kiểm tra chuyển đổi State.
        /// </summary>
        public override void CheckSwitchState() 
        {
            if (!_stateController.States.UnholdCrouch) return;

            if (_stateController.States.IsJumping)
            {
                SwitchState(_stateFactory.Jump());
                return;
            }

            if (_stateController.States.OnMove != Vector2.zero)
                SwitchState(_stateFactory.Run());
            else
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
        /// Xử lý logic khi Player đang trong Crouch State.
        /// </summary>
        private void CrouchHandle()
        {
            if (_stateController.States.IsCrouch)
            {
                _subState.UpdateState();
                SwitchSubState();

                if (_stateController.States.UnholdCrouch)
                    _stateController.States.IsCrouch = false;
            }
        }

        /// <summary>
        /// Xử lý chuyển đổi giữa các SubState.
        /// </summary>
        private void SwitchSubState()
        {
            if (_stateController.States.OnMove != Vector2.zero)
                _subState = _stateFactory.Run();
            else if(_stateController.States.OnMove == Vector2.zero)
            {
                _subState = _stateFactory.Idle();
                Debug.Log("hi");
            }

            _subState.EnterState();
        }

        #endregion

        #region --- Fields ---

        private BaseState<PlayerCore, PlayerStateFactory> _subState;

        private Vector2 _colOffsetDefault;
        private Vector2 _colSizeDefault;

        private Vector2 _colOffsetTarget = new Vector2(0.03f, -0.59f);
        private Vector2 _colSizeTarget = new Vector2(1f, 0.9f);

        #endregion

    }
}