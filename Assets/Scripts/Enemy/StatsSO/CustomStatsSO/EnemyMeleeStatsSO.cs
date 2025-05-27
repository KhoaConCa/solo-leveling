using Platform2D.CharacterInterface;
using Platform2D.CharacterStats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platform2D.CharacterStats
{
    /// <summary>
    /// EnemyMeleeStatsSO - Lữu trữ các dữ liệu cách tấn công của Enemy.
    /// Tác giả: Nguyễn Ngọc Phú, Ngày tạo: 27/05/2025.
    /// </summary>
    [CreateAssetMenu(fileName = "StatsSO", menuName = "Character/AttackStats/EnemyMeleeAttack", order = 1)]
    public class EnemyMeleeStatsSO : ScriptableObject
    {
        #region --- Fields ---

        public float attackPullForce;

        #endregion
    }
}
