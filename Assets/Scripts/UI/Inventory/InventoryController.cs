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
        }

        #endregion

        #region --- Fields ---

        [SerializeField] private UIInventory _inventoryUI;
        [SerializeField] private Button _goBackButton;

        public int inventorySize = 20;

        #endregion
    }
}
