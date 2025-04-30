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

        public void Awake()
        {
            /*if(!platformChecker.IsMobilePlatform())
            {
                foreach (var item in mobileObject)
                {
                    item.SetActive(false);
                }
                return;
            }*/
            _playerController = GameObject.FindWithTag(_tagMainPlayer).GetComponent<PlayerController>();
        }

        #endregion

        #region --- Properties ---

        public PlayerController PlayerController { get { return _playerController; } }

        #endregion

        #region --- Fields ---

        [SerializeField] private PlayerController _playerController;

        [SerializeField] private PlatformChecker _platformChecker = new PlatformChecker();

        [SerializeField] private List<GameObject> _mobileObject = new List<GameObject>();

        [SerializeField] private string _tagMainPlayer;

        #endregion

    }
}
