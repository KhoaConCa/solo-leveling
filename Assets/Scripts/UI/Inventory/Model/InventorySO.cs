using System;
using System.Collections.Generic;
using UnityEngine;

namespace Platform2D.UI.InventorySystem
{
    /// <summary>
    /// InventorySO - ScriptableObject đại diện cho kho đồ.
    /// Tác giả: Dương Nhật Khoa, Ngày tạo: 28/05/2025.
    /// </summary>

    [CreateAssetMenu]
    public class InventorySO : ScriptableObject
    {
        #region --- Methods ---

        /// <summary>
        /// Khởi tạo kho đồ với kích thước đã định.
        /// </summary>
        public void Initialize()
        {
            _inventoryItems = new List<InventoryItem>();
            for (int i = 0; i < size; i++)
            {
                _inventoryItems.Add(InventoryItem.GetEmptyItem());
            }
        }

        /// <summary>
        /// Thêm một vật phẩm vào kho đồ nêú chỗ kho đồ đó còn trống.
        /// </summary>
        /// <param name="item">Loại vật phẩm được thêm vào</param>
        /// <param name="quantity">Số lượng vật phẩm</param>
        public void AddItem(ItemSO item, int quantity)
        {
            for (int i = 0; i < _inventoryItems.Count; i++)
            {
                if (_inventoryItems[i].IsEmpty)
                {
                    _inventoryItems[i] = new InventoryItem
                    {
                        item = item,
                        quantity = quantity
                    };
                }
            }
        }

        /// <summary>
        /// Lấy trạng thái hiện tại của kho đồ, bao gồm các vật phẩm và số lượng của chúng.
        /// </summary>
        /// <returns>Trạng thái của kho đồ hiện tại</returns>
        public Dictionary<int, InventoryItem> GetCurrentInventoryState()
        {
            Dictionary<int, InventoryItem> returnValue = new Dictionary<int, InventoryItem>();
            for (int i = 0; i < _inventoryItems.Count; i++)
            {
                if (!_inventoryItems[i].IsEmpty)
                {
                    returnValue[i] = _inventoryItems[i];
                }
            }

            return returnValue;
        }

        /// <summary>
        /// Lấy vật phẩm tại một chỉ mục nhất định trong kho đồ.
        /// </summary>
        /// <param name="itemIndex">Vị trí của mục chỉ định</param>
        /// <returns>Vật phẩm tại vị trí chỉ định</returns>
        public InventoryItem GetItemAt(int itemIndex)
        {
            return _inventoryItems[itemIndex];
        }

        #endregion

        #region --- Fields ---

        [SerializeField] private List<InventoryItem> _inventoryItems;

        [field: SerializeField] public int size { get; private set; } = 20;

        #endregion
    }

    [Serializable]
    public struct InventoryItem
    {
        #region --- Methods ---

        /// <summary>
        /// Thay đổi số lượng của vật phẩm trong kho đồ.
        /// </summary>
        /// <param name="newQuantity">Số lượng vật phẩm mới</param>
        /// <returns>Chỗ kho đồ được thay đổi với một số lượng vật phẩm mới</returns>
        public InventoryItem ChangeQuantity(int newQuantity)
        {
            return new InventoryItem
            {
                item = this.item,
                quantity = newQuantity,
            };
        }

        /// <summary>
        /// Đặt lại vị trí kho đồ về trạng thái trống.
        /// </summary>
        /// <returns>Một vị trí trống không có vật phẩm</returns>
        public static InventoryItem GetEmptyItem()
            => new InventoryItem
            {
                item = null,
                quantity = 0,
            };

        #endregion

        #region --- Properties ---

        public bool IsEmpty => item == null;

        #endregion

        #region --- Fields ---

        public ItemSO item;
        public int quantity;

        #endregion
    }
}