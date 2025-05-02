using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Platform2D.CharacterInterface
{
    /// <summary>
    /// IMoveable - Interface với nhiệm vụ tạo các hàm di chuyển cho nhân vật.
    /// Tác giả: Nguyễn Ngọc Phú, Ngày tạo: 28/04/2025
    /// </summary>
    internal interface IMoveable
    {

        #region --- Methods ---

        /// <summary>
        /// Thực hiện di chuyển nhân vật khi người chơi thao tác.
        /// </summary>
        public void OnMove();

        /// <summary>
        /// Thực hiện nhảy cho nhân vật khi người chơi thao tác.
        /// </summary>
        public void OnJump();

        /// <summary>
        /// Thực hiện cúi người cho nhân vật khi người chơi thao tác.
        /// </summary>
        public void OnCrouch();

        /// <summary>
        /// Thực hiện lướt cho nhân vật khi người chơi thao tác.
        /// </summary>
        public void OnDash();

        #endregion

    }
}

namespace Platform2D.CharacterInterfaceIS
{
    /// <summary>
    /// IMoveable - Interface với nhiệm vụ tạo các hàm di chuyển cho nhân vật với Input System.
    /// Tác giả: Nguyễn Ngọc Phú, Ngày tạo: 01/05/2025
    /// </summary>
    internal interface IMoveable
    {

        #region --- Methods ---

        /// <summary>
        /// Thực hiện di chuyển nhân vật khi người chơi thao tác.
        /// </summary>
        public void OnMove(InputAction.CallbackContext context);

        /// <summary>
        /// Thực hiện nhảy cho nhân vật khi người chơi thao tác.
        /// </summary>
        public void OnJump(InputAction.CallbackContext context);

        /// <summary>
        /// Thực hiện cúi người cho nhân vật khi người chơi thao tác.
        /// </summary>
        public void OnCrouch(InputAction.CallbackContext context);

        /// <summary>
        /// Thực hiện lướt cho nhân vật khi người chơi thao tác.
        /// </summary>
        public void OnDash(InputAction.CallbackContext context);

        #endregion

    }
}
