using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platform2D.GlobalInterface
{
    /// <summary>
    /// IDamageable - Là một Interface khai báo các hàm tính sát thương.
    /// Tác giả: Nguyễn Ngọc Phú, Ngày tạo: 09/05/2025.
    /// </summary>
    public interface IDamageable
    {
        #region --- Methods ---

        public void ReceiveDamage(float damage, Vector2 knockBack);

        #endregion
    }
}
