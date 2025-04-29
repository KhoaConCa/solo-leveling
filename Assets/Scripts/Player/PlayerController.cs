using Platform2D.CharacterStates;
using Platform2D.CharacterStats;
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
            Rg2D = gameObject.GetComponent<Rigidbody2D>();

            PlayerStates = gameObject.GetComponentInChildren<PlayerStates>();
            PlayerStats = gameObject.GetComponentInChildren<PlayerStats>();
        }

        #endregion

        #region --- Properties ---

        public Rigidbody2D Rg2D { get; set; }

        public PlayerStates PlayerStates { get; private set; }
        public PlayerStats PlayerStats { get; private set; }

        #endregion

    }
}