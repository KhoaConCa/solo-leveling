using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
