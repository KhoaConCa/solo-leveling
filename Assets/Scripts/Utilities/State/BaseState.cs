using Platform2D.CharacterController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platform2D.HierarchicalStateMachine
{
    /// <summary>
    /// BaseState - Được tạo là một Base State trong Hierarchical State Machine.
    /// Tác giả: Nguyễn Ngọc Phú, Ngày tạo: 06/05/2025.
    /// </summary>
    /// <typeparam name="T">Truyền vào là một Controller của đối tượng.</typeparam>
    /// <typeparam name="U">Truyền vào là một State Factory của đối tượng.</typeparam>
    public abstract class BaseState<T, U> where T : IStateController<BaseState<T, U>>
    {
        #region --- Methods ---

        /// <summary>
        /// Khởi tạo BaseState.
        /// </summary>
        /// <param name="stateController">Biến mang kiểu dữ liệu là một Controller của đối tượng.</param>
        /// <param name="stateFactory">Biến mang kiểu dữ liệu là một State Factory của đối tượng.</param>
        protected BaseState(T stateController, U stateFactory)
        {
            _stateController = stateController;
            _stateFactory = stateFactory;
        }

        /// <summary>
        /// Cài đặt mặc định cho State.
        /// </summary>
        public abstract void EnterState();

        /// <summary>
        /// Cập nhật State.
        /// </summary>
        public abstract void UpdateState();

        /// <summary>
        /// Thoát State.
        /// </summary>
        public abstract void ExitState();

        /// <summary>
        /// Kiểm tra điều kiện chuyển đổi State.
        /// </summary>
        public abstract void CheckSwitchState();

        /// <summary>
        /// Phương thức đổi State.
        /// </summary>
        /// <param name="newState">Biến truyền vào là một State muốn đổi.</param>
        public virtual void SwitchState(BaseState<T, U> newState) 
        {
            ExitState();

            _stateController.CurrentState = newState;
            _stateController.CurrentState.EnterState();
        }

        #endregion

        #region --- Fields ---

        protected T _stateController;
        protected U _stateFactory;

        #endregion
    }
}
