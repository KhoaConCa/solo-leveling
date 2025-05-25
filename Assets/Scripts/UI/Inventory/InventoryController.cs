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

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                if (_inventoryUI.isActiveAndEnabled == false)
                {
                    _inventoryUI.transform.parent.gameObject.SetActive(true);

                    _inventoryUI.Show();

                    _goBackButton.gameObject.SetActive(true);
                }
                else
                {
                    _inventoryUI.Hide();
                }
            }
        }

        #endregion

        #region --- Fields ---

        [SerializeField] private UIInventory _inventoryUI;
        [SerializeField] private Button _goBackButton;

        public int inventorySize = 20;

        #endregion
    }
}
