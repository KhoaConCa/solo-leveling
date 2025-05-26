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

        public void ResetData()
        {
            this._itemIcon.gameObject.SetActive(false);
            _empty = true;
        }

        public void DeSelect()
        {
            Debug.Log("DeSelect");
            _itemSelected.enabled = false;
        }

        public void SetData(Sprite sprite, int quantity)
        {
            this._itemIcon.gameObject.SetActive(true);
            this._itemIcon.sprite = sprite;
            this._itemQuantity.text = quantity.ToString();
            _empty = false;
        }

        public void Select()
        {
            _itemSelected.enabled = true;
        }

        public void OnBeginDrag()
        {
            if (_empty)
                return;

            OnItemBeginDrag?.Invoke(this);
        }

        public void OnDrop()
        {
            OnItemDroppedOn?.Invoke(this);
        }

        public void OnEndDrag()
        {
            OnItemEndDrag?.Invoke(this);
        }

        public void OnPointerClick(BaseEventData data)
        {
            if (_empty)
                return;

            PointerEventData pointerData = (PointerEventData)data;

            if (pointerData.button == PointerEventData.InputButton.Left)
            {
                OnItemClicked?.Invoke(this);
                Select();
            }
            else
            {
                OnRightMouseBtnClick?.Invoke(this);
                DeSelect();
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
