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
            ResetDraggedItem();
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
                slot.transform.localScale = Vector3.one;
                _listOfSlot.Add(slot);

                slot.OnItemClicked += HandleItemSelection;
                slot.OnItemBeginDrag += HandleItemBeginDrag;
                slot.OnItemEndDrag += HandleItemEndDrag;
                slot.OnItemDroppedOn += HandleSwap;
            }
        }

        #region -- Events --
        private void HandleSwap(UIInventoryItem inventoryItemUI)
        {
            int index = _listOfSlot.IndexOf(inventoryItemUI);

            if (index == -1)
            {
                return;
            }

            OnSwapItems?.Invoke(_currentDragItemIndex, index);
            HandleItemSelection(inventoryItemUI);
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

        private void HandleItemSelection(UIInventoryItem inventoryItemUI)
        {
            int index = _listOfSlot.IndexOf(inventoryItemUI);
            if (index == -1) return;
            OnDescriptionRequested?.Invoke(index);
        }
        #endregion

        /// <summary>
        /// Cập nhật dữ liệu cho một ô kho đồ cụ thể với hình ảnh và số lượng vật phẩm.
        /// </summary>
        /// <param name="itemIndex">Vị trí được chọn hiện tại</param>
        /// <param name="image">Hình ảnh vật phẩm</param>
        /// <param name="quantity">Số lượng vật phẩm</param>
        public void UpdateData(int itemIndex, Sprite image, int quantity)
        {
            if (_listOfSlot.Count > itemIndex)
            {
                _listOfSlot[itemIndex].SetData(image, quantity);
            }
        }

        /// <summary>
        /// Đặt lại trạng thái của vật phẩm đang kéo thả.
        /// </summary>
        private void ResetDraggedItem()
        {
            _touchFollower.Toggle(false);
            _currentDragItemIndex = -1;
        }

        /// <summary>
        /// Tạo một vật phẩm kéo thả mới với hình ảnh và số lượng nhất định.
        /// </summary>
        /// <param name="sprite">Hình ảnh vật phẩm</param>
        /// <param name="quantity">Số lượng</param>
        public void CreateDragItem(Sprite sprite, int quantity)
        {
            _touchFollower.Toggle(true);
            _touchFollower.SetData(sprite, quantity);
        }

        /// <summary>
        /// Hiển thị giao diện kho đồ và đặt dữ liệu cho các ô kho đồ.
        /// </summary>
        public void Show()
        {
            gameObject.SetActive(true);
            //_descriptionPanel.ResetDescription();
            ResetSelection();
        }

        /// <summary>
        /// Bỏ chọn tất cả các ô kho đồ và đặt mô tả về trạng thái mặc định.
        /// </summary>
        public void ResetSelection()
        {
            _descriptionPanel.ResetDescription();
            DeselectAllItems();
        }

        /// <summary>
        /// Bỏ chọn tất cả các ô kho đồ, không hiển thị mô tả vật phẩm nào.
        /// </summary>
        private void DeselectAllItems()
        {
            foreach (UIInventoryItem slot in _listOfSlot)
            {
                slot.DeSelect();
            }
        }

        /// <summary>
        /// Ẩn giao diện kho đồ, không hiển thị các ô kho đồ và mô tả vật phẩm.
        /// </summary>
        public void Hide()
        {
            gameObject.SetActive(false);
            ResetDraggedItem();
        }

        /// <summary>
        /// Cập nhật mô tả cho một ô kho đồ cụ thể với hình ảnh, tên, mô tả và số lượng vật phẩm.
        /// </summary>
        /// <param name="itemIndex">Vị trí chỉ định</param>
        /// <param name="itemImage">Hình ảnh vật phẩm</param>
        /// <param name="name">tên vật phẩm</param>
        /// <param name="description">Mô tả vật phẩm</param>
        /// <param name="quantity">Số lượng vật phẩm</param>
        public void UpdateDescription(int itemIndex, Sprite itemImage, string name, string description, int quantity)
        {
            _descriptionPanel.SetDescription(itemImage, name, description);
            DeselectAllItems();
            _listOfSlot[itemIndex].Select();
        }

        /// <summary>
        /// Đặt lại dữ liệu của các ô về trạng thái mặc định.
        /// </summary>
        public void ResetAllItems()
        {
            for (int i = 0; i < _listOfSlot.Count; i++)
            {
                if (_listOfSlot[i] != null)
                {
                    _listOfSlot[i].ResetData();
                    _listOfSlot[i].DeSelect();
                }
            }
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
