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

    public class UIInventoryItem : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IEndDragHandler, IDropHandler, IDragHandler
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
        /// Đặt lại dữ liệu của ô kho đồ về trạng thái mặc định, không có vật phẩm nào.
        /// </summary>
        public void ResetData()
        {
            _itemIcon.gameObject.SetActive(false);
            _empty = true;
        }

        /// <summary>
        ///  chọn ô kho đồ, ẩn hình ảnh được chọn.
        /// </summary>
        public void DeSelect()
        {
            _itemSelected.enabled = false;
        }

        /// <summary>
        /// Thiết lập dữ liệu cho ô kho đồ với hình ảnh và số lượng vật phẩm.
        /// </summary>
        /// <param name="sprite">Hình ảnh vật phẩm</param>
        /// <param name="quantity">Số lượng của vật phẩm đó</param>
        public void SetData(Sprite sprite, int quantity)
        {
            _itemIcon.gameObject.SetActive(true);
            _itemIcon.sprite = sprite;
            _itemQuantity.text = quantity.ToString();
            _empty = false;
        }

        /// <summary>
        /// Chọn ô kho đồ, hiển thị hình ảnh được chọn để người dùng biết đây là ô đang được chọn.
        /// </summary>
        public void Select()
        {
            _itemSelected.enabled = true;
        }

        #region -- Events --
        public void OnPointerClick(PointerEventData pointerData)
        {
            if (pointerData.button == PointerEventData.InputButton.Right)
            {
                OnRightMouseBtnClick?.Invoke(this);
            }
            else
            {
                OnItemClicked?.Invoke(this);
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (_empty)
                return;

            OnItemBeginDrag?.Invoke(this);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            OnItemEndDrag?.Invoke(this);
        }

        public void OnDrop(PointerEventData eventData)
        {
            OnItemDroppedOn?.Invoke(this);
        }

        public void OnDrag(PointerEventData eventData)
        {

        }
        #endregion

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
