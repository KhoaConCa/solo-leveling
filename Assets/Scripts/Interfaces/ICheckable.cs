using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platform2D.CharacterInterface
{
    /// <summary>
    /// IMoveable - Interface với nhiệm vụ tạo thực hiện kiểm tra tương tác giữa nhân vật với object.
    /// Tác giả: Nguyễn Ngọc Phú, Ngày tạo: 28/04/2025
    /// </summary>
    internal interface ICheckable
    {

        #region --- Methods ---

        public void IsGrounded();

        public void IsOnWall(float direction);

        #endregion

    }
}
