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
        #region --- Methods ---

        public void InitializInventoryUI(int size)
        {
            for (int i = 0; i < size; i++)
            {
                UIInventoryItem slot = Instantiate(_slotPrefab, Vector3.zero, Quaternion.identity);
                slot.transform.SetParent(_contentPannel);
                _listOfSlot.Add(slot);
            }
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        #endregion

        #region --- Fields ---

        [SerializeField] private UIInventoryItem _slotPrefab;
        [SerializeField] private RectTransform _contentPannel;

        private List<UIInventoryItem> _listOfSlot = new List<UIInventoryItem>();

        #endregion
    }
}
