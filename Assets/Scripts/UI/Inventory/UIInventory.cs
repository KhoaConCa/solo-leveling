using Platform2D.UI.Inventory;
using System;
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
            _touchFollower.Toggle(false);
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

        public void UpdateData(int itemIndex, Sprite image, int quantity)
        {
            if (_listOfSlot.Count > itemIndex)
            {
                _listOfSlot[itemIndex].SetData(image, quantity);
            }
        }

        private void HandleSwap(UIInventoryItem inventoryItemUI)
        {
            int index = _listOfSlot.IndexOf(inventoryItemUI);

            if (index == -1)
            {
                return;
            }

            OnSwapItems?.Invoke(_currentDragItemIndex, index);
        }

        private void ResetDraggedItem()
        {
            _touchFollower.Toggle(false);
            _currentDragItemIndex = -1;
        }

        private void HandleItemEndDrag(UIInventoryItem inventoryItemUI)
        {
            ResetDraggedItem();
        }

        private void HandleItemBeginDrag(UIInventoryItem inventoryItemUI)
        {
            int index = _listOfSlot.IndexOf(inventoryItemUI);
            if (index == -1) return;
            _currentDragItemIndex = index;
            HandleItemSelection(inventoryItemUI);
            OnStartDragging?.Invoke(index);
        }

        public void CreateDragItem(Sprite sprite, int quantity)
        {
            _touchFollower.Toggle(true);
            _touchFollower.SetData(sprite, quantity);
        }

        private void HandleItemSelection(UIInventoryItem inventoryItemUI)
        {
            int index = _listOfSlot.IndexOf(inventoryItemUI);
            if (index == -1) return;
            OnDescriptionRequested?.Invoke(index);
        }

        /// <summary>
        /// Show - Hiển thị giao diện kho đồ và đặt dữ liệu cho các ô kho đồ.
        /// </summary>
        public void Show()
        {
            gameObject.SetActive(true);
            _descriptionPanel.ResetDescription();
            ResetSelection();
        }

        private void ResetSelection()
        {
            _descriptionPanel.ResetDescription();
            DeselectAllItems();
        }

        private void DeselectAllItems()
        {
            foreach (UIInventoryItem slot in _listOfSlot)
            {
                slot.DeSelect();
            }
        }

        /// <summary>
        /// Hide - Ẩn giao diện kho đồ, không hiển thị các ô kho đồ và mô tả vật phẩm.
        /// </summary>
        public void Hide()
        {
            gameObject.SetActive(false);
            ResetDraggedItem();
        }

        #endregion

        #region --- Fields ---

        [SerializeField] private UIInventoryItem _slotPrefab;
        [SerializeField] private RectTransform _contentPannel;
        [SerializeField] private UIInventoryDescription _descriptionPanel;
        [SerializeField] private DragFollower _touchFollower;

        private List<UIInventoryItem> _listOfSlot = new List<UIInventoryItem>();

        public event Action<int> OnDescriptionRequested, OnItemActionRequest, OnStartDragging;
        public event Action<int, int> OnSwapItems;

        private int _currentDragItemIndex = -1;

        #endregion
    }
}
