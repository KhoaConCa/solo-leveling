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
                { _openTools, _toolsPannel }//,
                //{ _openSettings, _settingPannel }
            };

            foreach (var button in _buttons.Keys)
            {
                Button btn = button;
                btn.onClick.AddListener(() => OnClickButton(btn));
            }
        }

        #endregion

        #region --- Methods ---

        public void OnClickButton(Button btn)
        {
            if (btn == null)
                return;
            else if (btn == _openTools)
            {
                _buttons[btn].SetActive(true);
                this.gameObject.SetActive(false);
                _goBackToHome.gameObject.SetActive(true);
            }
            else if (btn == _openSettings)
            {
                Debug.LogWarning($"Button {btn.name} does not have an associated panel.");
            }
        }

        #endregion

        #region --- Fields ---

        //[SerializeField] private GameObject _settingPannel;
        [SerializeField] private GameObject _toolsPannel;

        [SerializeField] private Button _openSettings;
        [SerializeField] private Button _openTools;
        [SerializeField] private Button _goBackToHome;

        private Dictionary<Button, GameObject> _buttons = new Dictionary<Button, GameObject>();

        #endregion
    }
}

