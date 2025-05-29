using Platform2D.BossAttackType;
using Platform2D.CharacterController;
using UnityEngine;

namespace Platform2D.HierarchicalStateMachine
{
    /// <summary>
    /// BossStateFactory - Được dùng để khởi tạo các States thuộc Boss.
    /// Tác giả: Nguyễn Ngọc Phú, Ngày tạo: 06/05/2025
    /// </summary>
    public class BossStateFactory : MonoBehaviour
    {
        #region --- Methods ---

        /// <summary>
        /// Khởi tạo State Detect của Boss
        /// </summary>
        /// <returns>Trả về State Detect của Boss.</returns>
        public BaseState<BossController, BossStateFactory> Detect() => new BossDetectState(_controller, this);

        /// <summary>
        /// Khởi tạo State Appearance của Boss
        /// </summary>
        /// <returns>Trả về State Appearance của Boss.</returns>
        public BaseState<BossController, BossStateFactory> Appearance() => new BossAppearanceState(_controller, this);

        /// <summary>
        /// Khởi tạo State Attack của Boss
        /// </summary>
        /// <returns>Trả về State Attack của Boss.</returns>
        public BaseState<BossController, BossStateFactory> Attack() => new BossAttackState(_controller, this);

        /// <summary>
        /// Khởi tạo State Injure của Boss
        /// </summary>
        /// <returns>Trả về State Injure của Boss.</returns>
        public BaseState<BossController, BossStateFactory> Injure() => new BossInjureState(_controller, this);

        #endregion

        #region --- Fields ---

        [SerializeField] private BossController _controller;

        #endregion
    }
}
