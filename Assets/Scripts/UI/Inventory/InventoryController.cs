using System.Collections.Generic;
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
            PrepareForUI();
            PrepareForInventoryData();

            _goToTools.onClick.AddListener(OpenInventory);
            _goBackButton.onClick.AddListener(HideInventory);
        }

        private void Update()
        {
            //if (Input.GetKeyDown(KeyCode.I))
            //{
            //    _inventoryUI.Show();
            //    foreach (var item in _inventoryData.GetCurrentInventoryState())
            //    {
            //        _inventoryUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
            //    }
            //}
            //else
            //{
            //    _inventoryUI.Hide();
            //}
        }

        #endregion

        #region --- Methods ---

        /// <summary>
        /// Chuẩn bị dữ liệu kho đồ, bao gồm khởi tạo kho đồ và đăng ký các sự kiện cần thiết.
        /// </summary>
        private void PrepareForInventoryData()
        {
            _inventoryData.Initialize();
            _inventoryData.OnInventoryChanged += UpdateInventoryUI;

            foreach (InventoryItem item in initialItems)
            {
                if (item.IsEmpty)
                    continue;

                _inventoryData.AddItem(item);
            }
        }

        /// <summary>
        /// Cập nhật giao diện kho đồ dựa trên trạng thái hiện tại của kho đồ.
        /// </summary>
        /// <param name="inventoryState">Trạng thái kho đồ</param>
        private void UpdateInventoryUI(Dictionary<int, InventoryItem> inventoryState)
        {
            _inventoryUI.ResetAllItems();

            foreach (var item in inventoryState)
            {
                _inventoryUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
            }
        }

        /// <summary>
        /// Chuẩn bị giao diện người dùng cho kho đồ, bao gồm khởi tạo UI và đăng ký các sự kiện cần thiết.
        /// </summary>
        private void PrepareForUI()
        {
            _inventoryUI.InitializInventoryUI(_inventoryData.Size);
            _inventoryUI.OnDescriptionRequested += HandleDescriptionRequest;
            _inventoryUI.OnSwapItems += HandleSwapItems;
            _inventoryUI.OnStartDragging += HandleDragging;
            _inventoryUI.OnItemActionRequest += HandleItemActionRequest;
        }

        #region -- Events ---
        private void HandleItemActionRequest(int itemIndex)
        {

        }

        private void HandleDragging(int itemIndex)
        {
            InventoryItem inventoryItem = _inventoryData.GetItemAt(itemIndex);

            if (inventoryItem.IsEmpty)
                return;

            _inventoryUI.CreateDragItem(inventoryItem.item.ItemImage, inventoryItem.quantity);
        }

        private void HandleSwapItems(int itemIndex_1, int itemIndex_2)
        {
            _inventoryData.SwapItems(itemIndex_1, itemIndex_2);
        }

        private void HandleDescriptionRequest(int itemIndex)
        {
            InventoryItem inventoryItem = _inventoryData.GetItemAt(itemIndex);

            if (inventoryItem.IsEmpty)
            {
                _inventoryUI.ResetSelection();
                return;
            }

            ItemSO item = inventoryItem.item;
            _inventoryUI.UpdateDescription(itemIndex, item.ItemImage, item.Name, item.Description, inventoryItem.quantity);
        }
        #endregion


        /// <summary>
        /// Ẩn giao diện kho đồ khi người dùng nhấn nút "Go Back".
        /// </summary>
        private void HideInventory()
        {
            _inventoryUI.Hide();
        }

        /// <summary>
        /// Mở giao diện kho đồ và cập nhật dữ liệu hiển thị khi nhấn nút "Open Tools".
        /// </summary>
        private void OpenInventory()
        {
            _inventoryUI.Show();

            foreach (var item in _inventoryData.GetCurrentInventoryState())
            {
                _inventoryUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
            }
        }

        #endregion

        #region --- Fields ---

        [SerializeField] private UIInventory _inventoryUI;
        [SerializeField] private Button _goBackButton;
        [SerializeField] private Button _goToTools;
        [SerializeField] private InventorySO _inventoryData;

        public List<InventoryItem> initialItems = new List<InventoryItem>();

        #endregion
    }
}
