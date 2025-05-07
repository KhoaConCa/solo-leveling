using Platform2D.CharacterAnimation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platform2D.CharacterStates
{
    /// <summary>
    /// PlayerStatesAlter - Được tạo ra để lưu trữ trạng thái của nhân vật.
    /// Tác giả: Nguyễn Ngọc Phú, Ngày tạo: 28/04/2025
    /// </summary>
    public class PlayerStatesAlter : MonoBehaviour
    {

        #region --- Properties ---

        public Animator PlayerAnimator => _animator;

        public Vector2 Direction => this.transform.localScale;
        public Vector2 OnMove { 
            get { 
                return _onMove; 
            }
            set {
                if (Mathf.Abs(value.y) < 0.8f)
                    _onMove = value;
                else _onMove = Vector2.zero;
            }
        }

        public bool IsMoving { 
            get {
                return _isMoving;
            }
            set {
                _isMoving = value;
                _animator.SetBool(AnimationStrings.IsMoving, value);
            }
        }

        public bool OnGround
        {
            get
            {
                return _onGround;
            }
            set
            {
                _onGround = value;
                _animator.SetBool(AnimationStrings.IsGrounded, value);
            }
        }

        public bool IsWall { get; set; }

        #endregion

        #region --- Fields ---

        [SerializeField] private Animator _animator;

        [SerializeField] private Vector2 _onMove;

        [SerializeField] private bool _isMoving;
        [SerializeField] private bool _onGround;

        #endregion

    }
}
