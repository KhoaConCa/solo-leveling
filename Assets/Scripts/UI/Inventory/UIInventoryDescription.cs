using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Platform2D.UI.Inventory
{
    /// <summary>
    /// UIInventoryDescription - Quản lý mô tả của một mục trong kho đồ.
    /// Tác giả: Dương Nhật Khoa, Ngày tạo: 26/05/2025.
    /// </summary>

    public class UIInventoryDescription : MonoBehaviour
    {
        #region --- Unity Methods ---

        private void Awake()
        {
            ResetDescription();
        }

        #endregion

        #region --- Methods ---

        /// <summary>
        /// ResetDescription - Đặt lại mô tả của mục thông tin vật phẩm trong kho đồ về trạng thái mặc định là không có thông tin gì.
        /// </summary>
        public void ResetDescription()
        {
            this._itemImage.gameObject.SetActive(false);
            this._itemName.text = "";
            this._itemDescription.text = "";
        }

        /// <summary>
        /// SetDescription - Thiết lập mô tả cho một mục thông tin vật phẩm trong kho đồ với hình ảnh, tiêu đề và mô tả cụ thể.
        /// </summary>
        /// <param name="sprite">Hình ảnh vật phẩm</param>
        /// <param name="title">Tên vật phẩm</param>
        /// <param name="description">Mô tả</param>
        public void SetDescription(Sprite sprite, string title, string description)
        {
            this._itemImage.gameObject.SetActive(true);
            this._itemImage.sprite = sprite;
            this._itemName.text = title;
            this._itemDescription.text = description;
        }

        #endregion

        #region --- Fields ---

        [SerializeField] private Image _itemImage;
        [SerializeField] private TMP_Text _itemName;
        [SerializeField] private TMP_Text _itemDescription;

        #endregion
    }
}
