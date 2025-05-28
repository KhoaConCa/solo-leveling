using Platform2D.UI.InventorySystem;
using System.Collections;
using UnityEngine;

namespace Platform2D.UI.PickupSystem
{
    /// <summary>
    /// Item - Lớp đại diện cho một vật phẩm trong kho đồ.
    /// Tác giả: Dương Nhật Khoa, Ngày tạo: 28/05/2025.
    /// </summary>

    public class Item : MonoBehaviour
    {
        #region --- Unity Methods ---

        private void Start()
        {
            GetComponent<SpriteRenderer>().sprite = InventoryItem.ItemImage;
        }

        #endregion

        #region --- Methods ---

        public void DestroyItem()
        {
            GetComponent<Collider2D>().enabled = false;
            StartCoroutine(AnimateItemPickup());

        }

        private IEnumerator AnimateItemPickup()
        {
            _audioSource.Play();
            Vector3 startScale = transform.localScale;
            Vector3 endScale = Vector3.zero;
            float currentTime = 0;

            while (currentTime < _duration)
            {
                currentTime += Time.deltaTime;
                transform.localScale =
                    Vector3.Lerp(startScale, endScale, currentTime / _duration);
                yield return null;
            }

            Destroy(gameObject);
        }

        #endregion

        #region --- Properties ---

        [field: SerializeField] public ItemSO InventoryItem { get; private set; }
        [field: SerializeField] public int Quantity { get; set; } = 1;

        #endregion

        #region --- Fields ---

        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private float _duration = 0.3f;

        #endregion
    }
}