using Platform2D.CharacterAnimation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platform2D.CharacterStates
{
    /// <summary>
    /// BossStates - Được dùng để lưu trạng thái của Boss.
    /// Tác giả: Nguyễn Ngọc Phú, Ngày tạo: 06/05/2025.
    /// </summary>
    public class BossStates : MonoBehaviour
    {
        #region --- Properties ---

        public Vector2 KnockBackDirection { get; set; }
        public float Direction {  get; set; }

        public GameObject AnchorPosCenter => _anchorPosCenter;
        public GameObject AnchorPosLeft => _anchorPosLeft;
        public GameObject AnchorPosRight => _anchorPosRight;


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

        public bool IsDetecting
        {
            get
            {
                return _isDetecting;
            }
            set
            {
                _isDetecting = value;
                if (_isDetecting)
                    _animator.SetTrigger(AnimationStrings.DetectTrigger);
            }
        }

        public bool IsFinishing => _animator.GetBool(AnimationStrings.IsFinish);

        public bool IsChasing { get; set; } = false;
        public bool IsReturn { get; set; } = false;

        public float RangeToPlayer { get; set; } = -1;

        public bool BossCanAttack => _animator.GetBool(AnimationStrings.CanAttack);

        public bool IsAttacking
        {
            get { 
                return _canAttack; 
            }
            set {
                _canAttack = value;
                if (_canAttack)
                    _animator.SetTrigger(AnimationStrings.AttackTrigger);
            } 
        }

        public bool MeleeAttacking
        {
            get
            {
                return _meleeAttack;
            }
            set
            {
                _meleeAttack = value;
                if (_meleeAttack)
                    _animator.SetTrigger("meleeAttack");
            }
        }

        public bool CanAttack { get; set; } = true;

        public bool OnGround { get; set; } = false;
        public bool OnWall { get; set; } = false;

        #endregion

        #region --- Fields ---

        [Header("State Parameters")]
        [SerializeField] private GameObject _anchorPosCenter;
        [SerializeField] private GameObject _anchorPosLeft;
        [SerializeField] private GameObject _anchorPosRight;

        [SerializeField] private bool _isDetecting = false;
        [SerializeField] private bool _isMoving = false;
        [SerializeField] private bool _isHitting = false;
        [SerializeField] private bool _isDead = false;
        [SerializeField] private bool _canDisale = false;
        [SerializeField] private bool _canAttack = false;

        [SerializeField] private bool _meleeAttack = false;

        [Header("Animator")]
        [SerializeField] private Animator _animator;

        #endregion
    }
}
