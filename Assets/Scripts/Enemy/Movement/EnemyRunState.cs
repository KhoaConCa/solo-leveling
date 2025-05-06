using Platform2D.CharacterAnimation;
using Platform2D.CharacterController;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.TextCore.Text;


namespace Platform2D.HierarchicalStateMachine
{
    /// <summary>
    /// EnemyRunState - Là một Run State của Enemy được kế thừa từ BaseState, được dùng để xử lý Logic và Animation thuộc Run.
    /// Tác giả: Nguyễn Ngọc Phú, Ngày tạo: 06/05/2025.
    /// </summary>
    public class EnemyRunState : BaseState<EnemyController, EnemyStateFactory>
    {
        #region --- Overrides ---

        /// <summary>
        /// Khởi tạo EnemyRunState.
        /// </summary>
        /// <param name="stateController">Biến truyền vào mang kiểu dữ liệu EnemyController.</param>
        /// <param name="stateFactory">Biến truyền vào mang kiểu dữ liệu EnemyStateFactory.</param>
        public EnemyRunState(EnemyController stateController, EnemyStateFactory stateFactory) : base(stateController, stateFactory) { }

        /// <summary>
        /// Cài đặt mặc định cho Run State.
        /// </summary>
        public override void EnterState()
        {
            AnchorMaxPos = _stateController.States.AnchorPosX + (_stateController.Stats.BaseStats.movementRange * _stateController.States.Direction);
            _currentTime = _countdownTime;
            _stateController.States.IsMoving = true;
        }

        /// <summary>
        /// Cập nhật Run State.
        /// </summary>
        public override void UpdateState() 
        {
            CheckSwitchState();

            RunHandle();
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
            if (!_stateController.States.IsMoving)
                SwitchState(_stateFactory.Idle());
        }

        /// <summary>
        /// Chuyển đổi State.
        /// </summary>
        /// <param name="newState">Biến mang kiểu dữ liệu là BaseState.</param>
        public override void SwitchState(BaseState<EnemyController, EnemyStateFactory> newState)
        {
            ExitState();

            _stateController.CurrentState = newState;
            _stateController.CurrentState.EnterState();
        }

        #endregion

        #region --- Methods ---

        /// <summary>
        /// Xử lý logic khi Enemy đang trong Run State.
        /// </summary>
        private void RunHandle()
        {
            float currentPosX = _stateController.trans2D.position.x;
            float direction = _stateController.States.Direction;
            bool reachAnchorPos = (currentPosX <= AnchorMaxPos && direction < 0) || (currentPosX >= AnchorMaxPos && direction > 0);

            if (_stateController.States.IsMoving)
                _stateController.Rg2D.velocity = new Vector2(
                                _stateController.States.Direction * _stateController.Stats.BaseStats.movementSpeed,
                                _stateController.Rg2D.velocity.y
                            );

            if (_stateController.States.OnWall || reachAnchorPos || !_stateController.States.OnGround)
                _stateController.States.IsMoving = false;
        }

        #endregion

        #region --- Properties ---

        public float AnchorMaxPos { get; set; }

        #endregion

        #region --- Fields ---

        private readonly float _countdownTime = 0.2f;
        private float _currentTime;

        #endregion
    }
}