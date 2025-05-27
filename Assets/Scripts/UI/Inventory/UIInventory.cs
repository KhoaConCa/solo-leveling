using Platform2D.UI.Inventory;
using System.Collections.Generic;
using UnityEngine;

namespace Platform2D.UI.InventorySystem
{
    /// <summary>
    /// UIInventory - Quản lý giao diện người dùng cho hệ thống kho đồ.
    /// Tác giả: Dương Nhật Khoa, Ngày tạo: 24/05/2025.
    /// </summary>

    public class UIInventory : MonoBehaviour
    {
        #region --- Unity Methods ---

        private void Awake()
        {
            Hide();
            _touchFolower.Toggle(false);
            _descriptionPanel.ResetDescription();
        }

        #endregion

        #region --- Methods ---

        /// <summary>
        /// InitializInventoryUI - Khởi tạo giao diện kho đồ với số lượng ô nhất định.
        /// </summary>
        /// <param name="size">Số lượng ô mong muốn</param>
        public void InitializInventoryUI(int size)
        {
            for (int i = 0; i < size; i++)
            {
                UIInventoryItem slot = Instantiate(_slotPrefab, Vector3.zero, Quaternion.identity);
                slot.transform.SetParent(_contentPannel);
                _listOfSlot.Add(slot);

                slot.OnItemClicked += HandleItemSelection;
                slot.OnItemBeginDrag += HandleItemBeginDrag;
                slot.OnItemEndDrag += HandleItemEndDrag;
                slot.OnItemDroppedOn += HandleSwap;
            }
        }

        private void HandleSwap(UIInventoryItem item)
        {
            Debug.Log("Swap");
        }

        private void HandleItemEndDrag(UIInventoryItem item)
        {
            _touchFolower.Toggle(false);
        }

        private void HandleItemBeginDrag(UIInventoryItem item)
        {
            _touchFolower.Toggle(true);
            _touchFolower.SetData(image, quantity);
        }

        private void HandleItemSelection(UIInventoryItem item)
        {
            _descriptionPanel.SetDescription(image, title, description);
            _listOfSlot[0].Select();
        }

        /// <summary>
        /// Show - Hiển thị giao diện kho đồ và đặt dữ liệu cho các ô kho đồ.
        /// </summary>
        public void Show()
        {
            gameObject.SetActive(true);
            _descriptionPanel.ResetDescription();

            _listOfSlot[0].SetData(image, quantity);
            _listOfSlot[1].SetData(image, quantity);
        }

        /// <summary>
        /// Hide - Ẩn giao diện kho đồ, không hiển thị các ô kho đồ và mô tả vật phẩm.
        /// </summary>
        public void Hide()
        {
            gameObject.SetActive(false);
        }

        #endregion

        #region --- Fields ---

        [SerializeField] private UIInventoryItem _slotPrefab;
        [SerializeField] private RectTransform _contentPannel;
        [SerializeField] private UIInventoryDescription _descriptionPanel;
        [SerializeField] private DragFollower _touchFolower;

        private List<UIInventoryItem> _listOfSlot = new List<UIInventoryItem>();
        public Sprite image;

        public int quantity;
        public string title, description;

        #endregion
    }
}
