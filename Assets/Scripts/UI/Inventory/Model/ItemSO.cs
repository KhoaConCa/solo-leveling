using UnityEngine;

namespace Platform2D.UI.InventorySystem
{
    /// <summary>
    /// ItemSO - ScriptableObject đại diện cho một mục trong kho đồ.
    /// Tác giả: Dương Nhật Khoa, Ngày tạo: 24/05/2025.
    /// </summary>

    [CreateAssetMenu]
    public class ItemSO : ScriptableObject
    {
        [field: SerializeField] public bool IsStackable { get; set; }
        [field: SerializeField] public int MaxStackSize { get; set; } = 1;
        [field: SerializeField] public string Name { get; set; }
        [field: SerializeField][field: TextArea] public string Description { get; set; }
        [field: SerializeField] public Sprite ItemImage { get; set; }

        public int ID => GetInstanceID();
    }
}
