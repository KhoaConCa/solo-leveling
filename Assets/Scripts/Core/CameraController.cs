using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platform2D.CameraController
{
    /// <summary>
    /// CameraController - Đóng vai trò trung tâm nhằm thực hiện việc di chuyển theo người chơi.
    /// Tác giả: Dương Nhật Khoa, Ngày tạo: 01/05/2025
    /// </summary>
    public class CameraController : MonoBehaviour
    {
        #region --- Unity Methods ---
        void Awake()
        {
            _playerPosistion = GameObject.FindGameObjectWithTag("MainPlayer").transform;
        }

        void LateUpdate()
        {
            Vector3 targetPosition = _playerPosistion.position + _offset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, _smoothTime);
        }
        #endregion

        #region --- Fields ---
        private Transform _playerPosistion;
        private Vector3 _velocity = Vector3.zero;

        [Range(0,1)]
        [SerializeField] private float _smoothTime;
        [SerializeField] private Vector3 _offset;
        #endregion
    }
}
