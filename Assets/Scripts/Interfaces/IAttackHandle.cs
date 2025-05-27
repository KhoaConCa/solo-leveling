using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platform2D.CharacterInterface
{
    /// <summary>
    /// IAttackHandle - Phân loại và thực hiện từng cách thức tấn công cho Enemy.
    /// Tác giả: Nguyễn Ngọc Phú, Ngày tạo: 27/05/2025.
    /// </summary>
    public interface IAttackHandle
    {
        #region --- Methods ---

        /// <summary>
        /// Thực hiện xử lý tấn công.
        /// </summary>
        public void AttackHandle();

        #endregion

        #region --- Properties ---

        public bool IsFinished { get; set; }

        #endregion
    }
}
