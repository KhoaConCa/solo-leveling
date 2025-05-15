using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platform2D.Utilities
{
    /// <summary>
    /// Timer - Được tạo ra làm bộ đếm giờ.
    /// Tác giả: Nguyễn Ngọc Phú, Ngày tạo: 09/05/2025.
    /// </summary>
    public class Timer
    {
        #region --- Methods ---

        /// <summary>
        /// Thực hiện Countdown.
        /// </summary>
        /// <param name="conditionCheck">điều kiện cần kiểm tra.</param>
        /// <param name="duration">Thời gian Countdown.</param>
        public bool FixedTimeCountdown(float duration)
        {
            if (_isCounting)
            {
                _timer += Time.fixedDeltaTime;
                if (_timer >= duration)
                {
                    _isCounting = false;
                    _timer = 0f;
                    return true;
                } 
            }

            return false;
        }

        /// <summary>
        /// Reset Countdown.
        /// </summary>
        public void StartCountdown()
        {
            _isCounting = true;
            _timer = 0f;
        }

        #endregion

        #region --- Fields ---

        private float _timer = 0f;
        private bool _isCounting = false;

        #endregion

    }
}
