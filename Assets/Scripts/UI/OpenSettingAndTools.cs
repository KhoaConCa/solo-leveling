using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Platform2D.UI
{
    /// <summary>
    /// OpenSettingAndTools - Quản lý mở cài đặt và công cụ trong giao diện người dùng.
    /// Tác giả: Dương Nhật Khoa, Ngày tạo: 26/05/2025.
    /// </summary>

    public class OpenSettingAndTools : MonoBehaviour
    {
        #region --- Unity Methods ---
        void Awake()
        {
            _buttons = new Dictionary<Button, GameObject>
            {
                { _openTools, _toolsPannel },
                { _openSettings, _settingPannel }
            };

            foreach (var button in _buttons.Keys)
            {
                Button btn = button;
                Debug.Log($"Button: {btn.name} - Pannel: {_buttons[btn].name}");
                btn.onClick.AddListener(() => OnClickButton(btn));
            }
        }

        #endregion

        #region --- Methods ---

        /// <summary>
        ///  Xử lý sự kiện khi người dùng nhấn vào một nút trong vùng cài đặt.
        /// </summary>
        /// <param name="btn">Nút được nhấn</param>
        public void OnClickButton(Button btn)
        {
            if (btn == null)
                return;
            else if (btn == _openTools)
            {
                _mainNavigation.PauseGame();

                _buttons[btn].SetActive(true);
                this.gameObject.SetActive(false);
                _goBackToHome.gameObject.SetActive(true);

                _mainNavigation.OnClickButton(_inventoryButton);
            }
            else if (btn == _openSettings)
            {
                _mainNavigation.PauseGame();

                _settingPannel.SetActive(true);
            }
        }

        #endregion

        #region --- Fields ---

        [Header("Pannel")]
        [SerializeField] private GameObject _settingPannel;
        [SerializeField] private GameObject _toolsPannel;

        [Header("Button")]
        [SerializeField] private Button _openSettings;
        [SerializeField] private Button _openTools;
        [SerializeField] private Button _goBackToHome;
        [SerializeField] private Button _inventoryButton;
        [SerializeField] private UIMainNavigation _mainNavigation;

        private Dictionary<Button, GameObject> _buttons = new Dictionary<Button, GameObject>();

        #endregion
    }
}

