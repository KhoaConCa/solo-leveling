using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Platform2D.UI
{
    /// <summary>
    /// UIMainNavigation - Quản lý điều hướng chính trong giao diện người dùng.
    /// Tác giả: Dương Nhật Khoa, Ngày tạo: 24/05/2025.
    /// </summary>

    public class UIMainNavigation : MonoBehaviour
    {
        #region --- Unity Methods ---

        private void Awake()
        {
            _buttonControlUI = new Dictionary<Button, GameObject>
            {
                { _inventoryButton, _inventory },
                { _mapButton, _map },
                { _statusButton, _status },
                { _goBackButton, _menu }
            };
            _mainBarButtons = new List<Button> { _inventoryButton, _mapButton, _statusButton };

            foreach (var button in _mainBarButtons)
            {
                // Clone để tránh closure bug
                Button btn = button;
                btn.onClick.AddListener(() => OnClickButton(btn));
            }

            OnClickButton(_inventoryButton);
        }

        #endregion

        #region --- Methods ---

        public void OnClickButton(Button clickedButton)
        {
            if (clickedButton == _goBackButton)
            {
                _menu.SetActive(false);
                _goBackButton.gameObject.SetActive(false);
                _settingZone.gameObject.SetActive(true);
                return;
            }

            if (_activeButton == clickedButton)
                return;

            if (_activeButton != null)
                SetButtonSprite(_activeButton, _defaultSprite);

            SetButtonSprite(clickedButton, _selectedSprite);

            if (!_buttonControlUI[clickedButton].activeSelf)
            {
                _buttonControlUI[clickedButton].SetActive(true);

                foreach (var btn in _mainBarButtons.Where(b => b != clickedButton))
                {
                    _buttonControlUI[btn].SetActive(false);
                }
            }

            _activeButton = clickedButton;
        }

        private void SetButtonSprite(Button button, Sprite sprite)
        {
            if (button.image != null)
            {
                button.image.sprite = sprite;
            }
        }

        #endregion

        #region --- Fields ---

        [Header("Main UI Control by Button")]
        [SerializeField] private GameObject _inventory;
        [SerializeField] private GameObject _map;
        [SerializeField] private GameObject _status;
        [SerializeField] private GameObject _menu;
        [SerializeField] private GameObject _settingZone;


        [Header("Main navigation Buttons")]
        [SerializeField] private Button _inventoryButton;
        [SerializeField] private Button _mapButton;
        [SerializeField] private Button _statusButton;
        [SerializeField] private Button _goBackButton;

        [Header("Custom Sprites")]
        [SerializeField] private Sprite _defaultSprite;
        [SerializeField] private Sprite _selectedSprite;

        private List<Button> _mainBarButtons;
        private Dictionary<Button, GameObject> _buttonControlUI;
        private Button _activeButton;

        #endregion
    }
}
