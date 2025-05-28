using Platform2D.CharacterInterface;
using Platform2D.CharacterStats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platform2D.CharacterStats
{
    /// <summary>
    /// BossMeleeStatsSO - Lữu trữ các dữ liệu cách tấn công của Boss.
    /// Tác giả: Nguyễn Ngọc Phú, Ngày tạo: 28/05/2025.
    /// </summary>
    [CreateAssetMenu(fileName = "StatsSO", menuName = "Character/AttackStats/BossMeleeAttack", order = 1)]
    public class BossMeleeStatsSO : ScriptableObject
    {
        #region --- Fields ---

        public float attackPullForce;

        #endregion
    }
}
