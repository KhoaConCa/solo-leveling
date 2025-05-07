using UnityEngine;

namespace Platform2D.Utilities
{
    /// <summary>
    /// Parallax - Đóng vai trò tạo hiệu ứng Illusion of Depth cho người chơi khi di chuyển.
    /// Tác giả: Dương Nhật Khoa, Ngày tạo: 02/05/2025.
    /// </summary>
    public class Parallax : MonoBehaviour
    {
        #region --- Unity Methods ---

        private void Start()
        {
            _startPosition = transform.position.x;
            _length = GetComponent<SpriteRenderer>().bounds.size.x;

            float maxDepth = 50f;
            float distanceZ = Mathf.Abs(transform.position.z - _camera.transform.position.z);

            _parallaxEffect = Mathf.Clamp01(1f - (distanceZ / maxDepth));
        }

        private void Update()
        {

        }

        private void FixedUpdate()
        {
            float camX = _camera.transform.position.x;
            float distance = camX * _parallaxEffect;
            float movement = camX * (1 - _parallaxEffect);

            transform.position = new Vector3(_startPosition + distance, transform.position.y, transform.position.z);

            if (movement > _startPosition + _length)
            {
                _startPosition += _length;
            }
            else if (movement < _startPosition - _length)
            {
                _startPosition -= _length;
            }
        }

        #endregion

        #region --- Fields ---

        private float _startPosition;
        private float _length;
        private float _parallaxEffect;

        [SerializeField] private GameObject _camera;

        #endregion
    }
}
