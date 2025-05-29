using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Platform2D.UI
{
    /// <summary>
    /// MainMenu - Quản lý giao diện chính của trò chơi.
    /// Tác giả: Dương Nhật Khoa, Ngày tạo: 30/05/2025.
    /// </summary>

    public class MainMenu : MonoBehaviour
    {
        #region --- Unity Methods ---

        private void Awake()
        {
            foreach (var button in new[] { _startGameButton, _openSettingsButton, _exitButton })
            {
                Button btn = button;
                btn.onClick.AddListener(() => OnButtonClick(btn));
            }
        }

        #endregion

        #region --- Methods ---

        private void OnButtonClick(Button btn)
        {
            if (btn == _startGameButton)
            {
                SceneManager.LoadScene("MapDemo");
                Time.timeScale = 1;
            }
            else if (btn == _openSettingsButton)
            {
                StartCoroutine(ShowNotification());
            }
            else if (btn == _exitButton)
            {
                Application.Quit();
            }
        }

        private IEnumerator ShowNotification()
        {
            _notificationPanel.SetActive(true);

            yield return new WaitForSecondsRealtime(2f);

            _notificationPanel.SetActive(false);
        }

        #endregion

        #region --- Fields ---

        [Header("Buttons")]
        [SerializeField] private Button _startGameButton;
        [SerializeField] private Button _openSettingsButton;
        [SerializeField] private Button _exitButton;

        [Header("Pannel")]
        [SerializeField] private GameObject _notificationPanel;

        #endregion
    }
}
