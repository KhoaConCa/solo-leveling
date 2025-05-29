using Platform2D.UI.InventorySystem;
using UnityEngine;

namespace Platform2D.UI.PickupSystem
{
    /// <summary>
    /// PickupSystem - Lớp quản lý hệ thống nhặt vật phẩm.
    /// Tác giả: Dương Nhật Khoa, Ngày tạo: 28/05/2025.
    /// </summary>
    public class PickupSystem : MonoBehaviour
    {
        #region --- Methods ---

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Item item = collision.GetComponent<Item>();

            if (item != null)
            {
                int reminder = _inventoryData.AddItem(item.InventoryItem, item.Quantity);

                if (reminder == 0)
                    item.DestroyItem();
                else
                    item.Quantity = reminder;
            }
        }

        #endregion

        #region --- Fields ---

        [SerializeField] private InventorySO _inventoryData;

        #endregion
    }
}
