using System;
using System.Collections.Generic;
using System.Linq;
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
            for (int i = 0; i < Size; i++)
            {
                _inventoryItems.Add(InventoryItem.GetEmptyItem());
            }
        }

        /// <summary>
        /// Thêm một vật phẩm vào kho đồ nêú chỗ kho đồ đó còn trống.
        /// </summary>
        /// <param name="item">Loại vật phẩm được thêm vào</param>
        /// <param name="quantity">Số lượng vật phẩm</param>
        public int AddItem(ItemSO item, int quantity)
        {
            if (item.IsStackable == false)
            {
                for (int i = 0; i < _inventoryItems.Count; i++)
                {
                    while (quantity > 0 && IsInventoryFull() == false)
                    {
                        quantity -= AddItemToFirstFreeSlot(item, 1);
                    }

                    InformationChange();
                    return quantity;
                }
            }

            quantity = AddStackableItem(item, quantity);
            InformationChange();
            return quantity;
        }

        /// <summary>
        /// Kiểm tra xem kho đồ có đầy hay không.
        /// </summary>
        /// <returns>True là đã đầy - False là chưa đầy</returns>
        private bool IsInventoryFull() => _inventoryItems.Where(item => item.IsEmpty).Any() == false;

        private int AddStackableItem(ItemSO item, int quantity)
        {
            for (int i = 0; i < _inventoryItems.Count; i++)
            {
                if (_inventoryItems[i].IsEmpty)
                    continue;

                if (_inventoryItems[i].item.ID == item.ID)
                {
                    int amountPossibleToTake = _inventoryItems[i].item.MaxStackSize - _inventoryItems[i].quantity;

                    if (quantity > amountPossibleToTake)
                    {
                        _inventoryItems[i] = _inventoryItems[i].ChangeQuantity(_inventoryItems[i].item.MaxStackSize);
                        quantity -= amountPossibleToTake;
                    }
                    else
                    {
                        _inventoryItems[i] = _inventoryItems[i].ChangeQuantity(_inventoryItems[i].quantity + quantity);
                        InformationChange();
                        return 0;
                    }
                }
            }

            while (quantity > 0 && IsInventoryFull() == false)
            {
                int newQuantity = Mathf.Clamp(quantity, 0, item.MaxStackSize);
                quantity -= newQuantity;
                AddItemToFirstFreeSlot(item, newQuantity);
            }

            return quantity;
        }

        /// <summary>
        /// Thêm một vật phẩm không thể xếp chồng vào kho đồ.
        /// </summary>
        /// <param name="item">Loại vật phẩm</param>
        /// <param name="quantity">Số lượng</param>
        /// <returns>Số lượng vật phẩm đó</returns>
        private int AddItemToFirstFreeSlot(ItemSO item, int quantity)
        {
            InventoryItem newItem = new InventoryItem
            {
                item = item,
                quantity = quantity,
                //itemState = new List<ItemParameter>(itemState == null ? item.DefaultParametersList : itemState)
            };

            for (int i = 0; i < _inventoryItems.Count; i++)
            {
                if (_inventoryItems[i].IsEmpty)
                {
                    _inventoryItems[i] = newItem;
                    return quantity;
                }
            }
            return 0;
        }

        /// <summary>
        /// Lấy trạng thái hiện tại của kho đồ, bao gồm các vật phẩm và số lượng của chúng.
        /// </summary>
        /// <returns>Trạng thái của kho đồ hiện tại</returns>
        public Dictionary<int, InventoryItem> GetCurrentInventoryState()
        {
            Dictionary<int, InventoryItem> returnValue =
                new Dictionary<int, InventoryItem>();

            for (int i = 0; i < _inventoryItems.Count; i++)
            {
                if (_inventoryItems[i].IsEmpty)
                    continue;

                returnValue[i] = _inventoryItems[i];
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

        /// <summary>
        /// Theem một vật phẩm vào kho đồ.
        /// </summary>
        /// <param name="item">Thông tin vật phẩm</param>
        public void AddItem(InventoryItem item)
        {
            AddItem(item.item, item.quantity);
        }

        /// <summary>
        /// Đổi chỗ của hai vật phẩm trong kho đồ.
        /// </summary>
        /// <param name="itemIndex_1">Vật phẩm 1</param>
        /// <param name="itemIndex_2">Vật phẩm 2</param>
        public void SwapItems(int itemIndex_1, int itemIndex_2)
        {
            InventoryItem item1 = _inventoryItems[itemIndex_1];
            _inventoryItems[itemIndex_1] = _inventoryItems[itemIndex_2];
            _inventoryItems[itemIndex_2] = item1;
            InformationChange();
        }

        /// <summary>
        /// Thay đổi thông tin kho đồ và thông báo cho các đối tượng lắng nghe về sự thay đổi này.
        /// </summary>
        private void InformationChange()
        {
            OnInventoryChanged?.Invoke(GetCurrentInventoryState());
        }

        #endregion

        #region --- Fields ---

        [SerializeField] private List<InventoryItem> _inventoryItems;

        public event Action<Dictionary<int, InventoryItem>> OnInventoryChanged;

        [field: SerializeField] public int Size { get; private set; } = 20;

        InventoryItem item = new InventoryItem();

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
                //itemState = new List<ItemParameter>(this.itemState)
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
                //itemState = new List<ItemParameter>()
            };

        #endregion

        #region --- Properties ---

        public bool IsEmpty => item == null;

        #endregion

        #region --- Fields ---

        public ItemSO item;
        public int quantity;
        //public List<ItemParameter> itemState;

        #endregion
    }
}