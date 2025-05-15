using Platform2D.CharacterAnimation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platform2D.CharacterStates
{
    /// <summary>
    /// EnemyStates - Được dùng để lưu trạng thái của Enemy.
    /// Tác giả: Nguyễn Ngọc Phú, Ngày tạo: 06/05/2025.
    /// </summary>
    public class EnemyStates : MonoBehaviour
    {
        #region --- Properties ---

        public Vector2 KnockBackDirection { get; set; }
        public float Direction {  get; set; }

        public float AnchorPosX { get; set; }

        public bool FirstFlipDirection { get; set; } = false;

        public bool IsMoving
        {
            get { return _isMoving; }
            set
            {
                _isMoving = value;
                _animator.SetBool(AnimationStrings.IsMoving, value);
            }
        }

        public bool CanMove => _animator.GetBool(AnimationStrings.CanMove);

        public bool Invulnerable { get; set; } = false;
        public bool IsHitting { 
            get { return _isHitting; }
            set {
                if (value)
                    _animator.SetTrigger(AnimationStrings.HitTrigger);
                _isHitting = value;
            }
        }

        public bool IsDead
        {
            get { return _isDead; }
            set
            {
                if (value)
                    _animator.SetTrigger(AnimationStrings.DeadTrigger);
                _isDead = value;
            }
        }

        public bool IsDetecting { get; set; } = false;
        public bool IsChasing { get; set; } = false;
        public bool IsReturn { get; set; } = false;

        public float RangeToPlayer { get; set; } = -1;

        public bool OnGround { get; set; } = false;
        public bool OnWall { get; set; } = false;

        #endregion

        #region --- Fields ---

        [Header("State Parameters")]
        [SerializeField] private bool _isMoving = false;
        [SerializeField] private bool _isHitting = false;
        [SerializeField] private bool _isDead = false;
        [SerializeField] private bool _canDisale = false;

        [Header("Animator")]
        [SerializeField] private Animator _animator;

        #endregion
    }
}
