using Cinemachine;
using UnityEngine;

namespace Platform2D.Utilities
{
    /// <summary>
    /// CameraOffsetAdjuster - Thiết lập chuyển đổi Scale sang Rotation cho Cinemachine.
    /// Tác giả: Dương Nhật Khoa, Ngày tạo: 07/05/2025
    /// </summary>
    public class CameraOffsetAdjuster : MonoBehaviour
    {
        #region --- Unity Methods ---

        void Update()
        {
            float playerScaleX = _player.localScale.x;

            FlipBaseOnScale(playerScaleX);
        }

        #endregion

        #region --- Methods ---

        /// <summary>
        /// FlipBaseOnScale - Đảo ngược giá trị offset của camera dựa trên Scale của nhân vật.
        /// </summary>
        /// <param name="scaleX">Giá trị của Scale X của nhân vật</param>
        private void FlipBaseOnScale(float scaleX)
        {
            Vector3 newOffset;

            if (scaleX < 0)
                newOffset = new Vector3(-_baseOffset.x, _baseOffset.y, _baseOffset.z);
            else
                newOffset = _baseOffset;

            CinemachineFramingTransposer transposer = _virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
            if (transposer != null)
            {
                transposer.m_TrackedObjectOffset = newOffset;
            }
        }

        #endregion

        #region --- Fields ---

        [SerializeField] private CinemachineVirtualCamera _virtualCamera;
        [SerializeField] private Transform _player;

        private Vector3 _baseOffset = new Vector3(0.7f, 0, 0);

        #endregion
    }
}
