using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Platform2D.UI
{
    /// <summary>
    /// PauseMenu - Quản lý giao diện menu tạm dừng trong trò chơi.
    /// Tác giả: Dương Nhật Khoa, Ngày tạo: 29/05/2025.
    /// </summary>

    public class PauseMenu : MonoBehaviour
    {
        #region --- Unity Methods ---

        private void Awake()
        {
            foreach (var button in new[] { _continueButton, _optionsButton, _returnToMenuButton })
            {
                Button btn = button;
                btn.onClick.AddListener(() => OnButtonClick(btn));
            }
        }

        #endregion

        #region --- Methods ---

        private void OnButtonClick(Button btn)
        {
            if (btn == _continueButton)
            {
                _mainNavigation.ContinueGame();
                this.gameObject.SetActive(false);
            }
            else if (btn == _optionsButton)
            {
                //_mainNavigation.OpenSettingAndTools();
            }
            else if (btn == _returnToMenuButton)
            {
                SceneManager.LoadScene("Menu");
            }
        }

        #endregion

        #region --- Fields ---

        [SerializeField] private UIMainNavigation _mainNavigation;

        [Header("Button")]
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _optionsButton;
        [SerializeField] private Button _returnToMenuButton;

        #endregion
    }
}
