using Platform2D.UI.Inventory;
using UnityEngine;

namespace Platform2D.UI.InventorySystem
{
    /// <summary>
    /// DragFollower - Lớp theo dõi vị trí của đối tượng khi kéo thả.
    /// Tác giả: Dương Nhật Khoa, Ngày tạo: 27/05/2025.
    /// </summary>
    public class DragFollower : MonoBehaviour
    {
        #region --- Unity Methods ---

        private void Awake()
        {
            _canvas = transform.root.GetComponent<Canvas>();
            _mainCamera = Camera.main;
            _item = GetComponentInChildren<UIInventoryItem>();
        }
        private void Update()
        {
            Vector2 position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)_canvas.transform, Input.mousePosition, _canvas.worldCamera, out position); //PointerEventData.position Input.mousePosition
            transform.position = _canvas.transform.TransformPoint(position);
        }

        #endregion

        #region --- Methods ---

        public void SetData(Sprite sprite, int quantity)
        {
            _item.SetData(sprite, quantity);
        }

        public void Toggle(bool val)
        {
            Debug.Log($"Item toggled {val}");
            gameObject.SetActive(val);
        }

        #endregion

        #region --- Fields ---

        [SerializeField] private Canvas _canvas;
        [SerializeField] private UIInventoryItem _item;

        private Camera _mainCamera;

        #endregion
    }
}
