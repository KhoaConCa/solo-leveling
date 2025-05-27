using UnityEngine;
using UnityEngine.UI;

namespace Platform2D.UI.InventorySystem
{
    /// <summary>
    /// InventoryController - Quản lý kho đồ trong trò chơi.
    /// Tác giả: Dương Nhật Khoa, Ngày tạo: 24/05/2025.
    /// </summary>

    public class InventoryController : MonoBehaviour
    {
        #region --- Unity Methods ---

        void Start()
        {
            _inventoryUI.InitializInventoryUI(inventorySize);
            _goToTools.onClick.AddListener(ToggleInventoryUI);
            _goBackButton.onClick.AddListener(HideInventory);
        }

        //private void Update()
        //{
        //    if (_goToTools.Invoke)
        //    {
        //        if (_inventoryUI.isActiveAndEnabled == false)
        //        {
        //            _inventoryUI.Show();
        //        }
        //        else
        //        {
        //            _inventoryUI.Hide();
        //        }
        //    }
        //}

        #endregion

        #region --- Methods ---

        /// <summary>
        /// ToggleInventoryUI - Chuyển đổi hiển thị của giao diện kho đồ.
        /// </summary>
        private void ToggleInventoryUI()
        {
            if (_inventoryUI.isActiveAndEnabled)
                _inventoryUI.Hide();
            else
                _inventoryUI.Show();
        }

        /// <summary>
        /// HideInventory - Ẩn giao diện kho đồ khi người dùng nhấn nút "Go Back".
        /// </summary>
        private void HideInventory()
        {
            _inventoryUI.Hide();
        }

        #endregion

        #region --- Fields ---

        [SerializeField] private UIInventory _inventoryUI;
        [SerializeField] private Button _goBackButton;
        [SerializeField] private Button _goToTools;

        public int inventorySize = 20;

        #endregion
    }
}
