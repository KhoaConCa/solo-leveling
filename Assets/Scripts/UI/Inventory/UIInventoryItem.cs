using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Platform2D.UI.Inventory
{
    /// <summary>
    /// UIInventoryItem - Quản lý một ô trong giao diện của kho đồ.
    /// Tác giả: Dương Nhật Khoa, Ngày tạo: 26/05/2025.
    /// </summary>

    public class UIInventoryItem : MonoBehaviour
    {
        #region --- Unity Methods ---

        void Awake()
        {
            ResetData();
            DeSelect();
        }

        #endregion

        #region --- Methods ---

        /// <summary>
        /// ResetData - Đặt lại dữ liệu của ô kho đồ về trạng thái mặc định, không có vật phẩm nào.
        /// </summary>
        public void ResetData()
        {
            this._itemIcon.gameObject.SetActive(false);
            _empty = true;
        }

        /// <summary>
        /// DeSelect - Bỏ chọn ô kho đồ, ẩn hình ảnh được chọn.
        /// </summary>
        public void DeSelect()
        {
            _itemSelected.enabled = false;
        }

        /// <summary>
        /// SetData - Thiết lập dữ liệu cho ô kho đồ với hình ảnh và số lượng vật phẩm.
        /// </summary>
        /// <param name="sprite">Hình ảnh vật phẩm</param>
        /// <param name="quantity">Số lượng của vật phẩm đó</param>
        public void SetData(Sprite sprite, int quantity)
        {
            this._itemIcon.gameObject.SetActive(true);
            this._itemIcon.sprite = sprite;
            this._itemQuantity.text = quantity.ToString();
            _empty = false;
        }

        /// <summary>
        /// Select - Chọn ô kho đồ, hiển thị hình ảnh được chọn để người dùng biết đây là ô đang được chọn.
        /// </summary>
        public void Select()
        {
            _itemSelected.enabled = true;
        }

        /// <summary>
        /// OnBeginDrag - Xử lý sự kiện bắt đầu kéo thả ô kho đồ.
        /// </summary>
        public void OnBeginDrag()
        {
            if (_empty)
                return;

            OnItemBeginDrag?.Invoke(this);
        }

        /// <summary>
        /// OnDrop - Xử lý sự kiện khi ô kho đồ được thả vào một vị trí khác.
        /// </summary>
        public void OnDrop()
        {
            OnItemDroppedOn?.Invoke(this);
        }

        /// <summary>
        /// OnEndDrag - Xử lý sự kiện khi kết thúc kéo thả ô kho đồ.
        /// </summary>
        public void OnEndDrag()
        {
            OnItemEndDrag?.Invoke(this);
        }

        /// <summary>
        /// OnPointerClick - Xử lý sự kiện khi người dùng nhấp chuột vào ô kho đồ.
        /// </summary>
        /// <param name="data">Event cần phải thực hiện</param>
        public void OnPointerClick(BaseEventData data)
        {
            if (_empty)
                return;

            PointerEventData pointerData = (PointerEventData)data;

            if (pointerData.button == PointerEventData.InputButton.Right)
            {
                OnRightMouseBtnClick?.Invoke(this);
            }
            else
            {
                OnItemClicked?.Invoke(this);
            }
        }

        #endregion

        #region --- Fields ---

        [SerializeField] private Image _itemIcon;
        [SerializeField] private TMP_Text _itemQuantity;
        [SerializeField] private Image _itemSelected;

        public event Action<UIInventoryItem> OnItemClicked, OnItemDroppedOn, OnItemBeginDrag, OnItemEndDrag, OnRightMouseBtnClick;

        private bool _empty = true;

        #endregion
    }
}
