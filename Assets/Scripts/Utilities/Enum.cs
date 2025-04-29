using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platform2D.Vector
{
    /// <summary>
    /// AXIS_1D - Sử dụng giá trị của NEGATIVE và POSITIVE để xác định chiều và hướng đi cho nhân vật 2D.
    /// Tác giả: Nguyễn Ngọc Phú, Ngày tạo: 28/04/2025
    /// </summary>
    enum AXIS_1D
    {
        NEGATIVE = -1,
        POSITIVE = 1,
    }
}

namespace Platform2D.Movement
{
    /// <summary>
    /// MOVEMENT_FUNCTION - Sử dụng các giá trị có trong enum MOVEMENT_FUNCTION 
    /// để xác định chức năng của từng Button trên UI, đối với việc điều khiển trên Mobile.
    /// Tác giả: Nguyễn Ngọc Phú, Ngày tạo: 28/04/2025
    /// </summary>
    enum MOVEMENT_FUNCTION
    {
        LEFT,
        RIGHT,
        JUMP,
    }
}

namespace Platform2D.BaseStats
{
    /// <summary>
    /// BASE_STATS - Sử dụng các giá trị có trong enum BASE_STATS làm chỉ số mặc định cho các nhân vật.
    /// Tác giả: Nguyễn Ngọc Phú, Ngày tạo: 28/04/2025
    /// </summary>
    public enum BASE_STATS
    {
        MOVEMENT_SPEED = 3,
    }
}
