using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platform2D.GlobalChecker
{
    /// <summary>
    /// PlatformChecker - Sử dụng để kiểm tra thiết bị hiện tại thuộc nền tảng nào.
    /// Tác giả: Nguyễn Ngọc Phú, Ngày tạo: 28/04/2025
    /// </summary>
    public class PlatformChecker
    {

        #region --- Methods ---

        /// <summary>
        /// Kiếm tra xem nền tảng hiện tại có phải là Mobile không?
        /// </summary>
        /// <returns>
        /// True - Thiết bị hiện tại thuộc nền tảng Mobile.
        /// False - Thiết bị hiện tại không phải là Mobile.
        /// </returns>
        public bool IsMobilePlatform()
        {
            return Application.platform == RuntimePlatform.Android ||
                   Application.platform == RuntimePlatform.IPhonePlayer;
        }

        #endregion

    }
}
