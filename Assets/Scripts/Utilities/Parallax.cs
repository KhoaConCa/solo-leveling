using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platform2D.Utilities
{
    /// <summary>
    /// Parallax - Đóng vai trò tạo hiệu ứng Illusion of Depth cho người chơi khi di chuyển.
    /// Tác giả: Dương Nhật Khoa, Ngày tạo: 02/05/2025
    /// </summary>
    public class Parallax : MonoBehaviour
    {
        #region --- Unity Methods ---

        void Start()
        {
            _startPosition = transform.position;
            _startZPosition = transform.position.z;
        }

        void Update()
        {
            Vector2 newPosition = _startPosition + _newPosition * _parallaxFactor;
            transform.position = new Vector3(newPosition.x, newPosition.y, _startZPosition);
        }

        #endregion

        #region --- Fields ---

        [SerializeField] private Camera _camera;
        [SerializeField] private Transform _player;

        private Vector2 _startPosition;
        private Vector2 _newPosition => (Vector2)_camera.transform.position - _startPosition;
        private Vector2 _parallaxEffect;

        private float _startZPosition;
        private float _distanceFromPlayer => transform.position.z - _player.position.z;
        private float _clippingPlane => (_camera.transform.position.z 
            + (_distanceFromPlayer > 0 ? _camera.farClipPlane : _camera.nearClipPlane));
        private float _parallaxFactor => Mathf.Abs(_distanceFromPlayer) / _clippingPlane;

        #endregion
    }
}