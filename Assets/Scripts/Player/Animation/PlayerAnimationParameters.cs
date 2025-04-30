using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platform2D.CharacterAnimation
{
    /// <summary>
    /// PlayerAnimationParameters - Được tạo ra nhằm lưu trữ tên các tham số được được đặt trong animation.
    /// Tác giả: Nguyễn Ngọc Phú, Ngày tạo: 29/04/2025
    /// </summary>
    public static class PlayerAnimationParameters
    {

        #region --- Fields ---

        #region -- Bool Parameters --
        public readonly static string IsGrounded = "isGrounded";
        public readonly static string IsMoving = "isMoving";
        public readonly static string IsCrouching = "isCrouching";
        #endregion

        #region -- Float Parameters --
        public readonly static string YVelocity = "yVelocity";
        #endregion

        #region -- Trigger Parameters --
        public readonly static string Jump = "jump";
        #endregion

        #endregion

    }
}
