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

        public bool IsHitting { 
            get { return _isHitting; }
            set {
                if (value)
                    _animator.SetTrigger(AnimationStrings.HitTrigger);
                _isHitting = value;
            }
        }

        public bool OnGround { get; set; } = false;
        public bool OnWall { get; set; } = false;

        #endregion

        #region --- Fields ---

        [Header("State Parameters")]
        [SerializeField] private bool _isMoving = false;
        [SerializeField] private bool _isHitting = false;

        [Header("Animator")]
        [SerializeField] private Animator _animator;

        #endregion
    }
}
