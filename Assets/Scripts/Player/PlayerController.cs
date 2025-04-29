using Platform2D.CharacterStates;
using Platform2D.CharacterStats;
using Platform2D.Vector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platform2D.CharacterController
{
    /// <summary>
    /// PlayerController - Đóng vai trò trung tâm nhằm quản lý và lưu trữ các thông tin quan trọng.
    /// Tác giả: Nguyễn Ngọc Phú, Ngày tạo: 28/04/2025
    /// </summary>
    public class PlayerController : MonoBehaviour
    {

        #region --- Unity Methods ---

        public void Awake()
        {
            _isFacingRight = true;

            _rg2D = gameObject.GetComponent<Rigidbody2D>();
            _groundChecker = GameObject.FindGameObjectWithTag(GROUND_CHECKER).GetComponent<CapsuleCollider2D>();

            _playerStats = gameObject.GetComponentInChildren<PlayerStats>();
            _playerStates = gameObject.GetComponentInChildren<PlayerStates>();
        }

        public void Update()
        {
            FlipPlayerObject();
        }

        #endregion

        #region --- Methods ---

        /// <summary>
        /// Thực hiện đổi chiều của nhân vật.
        /// </summary>
        public void FlipPlayerObject()
        {
            if (_playerStates == null) return;

            if (_playerStates.IsMoving > 0 && !IsFacingRight)
                IsFacingRight = true;
            else if (_playerStates.IsMoving < 0 && IsFacingRight)
                IsFacingRight = false;
        }

        #endregion

        #region --- Properties ---

        public Rigidbody2D Rg2D { get { return _rg2D; } }
        public CapsuleCollider2D GroundChecker { get { return _groundChecker; } }

        public PlayerStats PlayerStats { get { return _playerStats; } }
        public PlayerStates PlayerStates { get { return _playerStates; } }

        public bool IsFacingRight
        {
            get { return _isFacingRight; }
            private set
            {
                if(_isFacingRight != value)
                    this.gameObject.transform.localScale *= new Vector2((float)AXIS_1D.NEGATIVE, (float)AXIS_1D.POSITIVE);

                _isFacingRight = value;
            }
        }

        #endregion

        #region --- Fields ---

        [SerializeField] private Rigidbody2D _rg2D;
        [SerializeField] private CapsuleCollider2D _groundChecker;

        [SerializeField] private PlayerStats _playerStats;
        [SerializeField] private PlayerStates _playerStates;

        [SerializeField] private bool _isFacingRight;

        [SerializeField] private const string GROUND_CHECKER = "GroundChecker";

        #endregion

    }
}