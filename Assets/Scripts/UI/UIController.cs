using Platform2D.CharacterController;
using Platform2D.GlobalChecker;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platform2D.CanvasController
{
    /// <summary>
    /// UIController - Được tạo ra để quản lý giữa giao diện người dùng và các chức năng thao tác của game.
    /// Tác giả: Nguyễn Ngọc Phú, Ngày tạo: 28/04/2025
    /// </summary>
    public class UIController : MonoBehaviour
    {
        #region --- Unity Methods ---

        private void Awake()
        {
            _bossHealthBar.SetActive(false);
        }

        #endregion

        #region --- Methods ---

        public void ShowBossHealthBar()
        {
            if(!_bossHealthBar.activeSelf)
                _bossHealthBar.SetActive(true);
        }

        #endregion

        #region --- Fields ---

        [SerializeField] private GameObject _bossHealthBar;

        #endregion

    }
}
