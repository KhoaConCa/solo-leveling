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

namespace Platform2D.EnumFunction
{
    /// <summary>
    /// MOVEMENT_FUNCTION - Sử dụng các giá trị có trong enum MOVEMENT_FUNCTION 
    /// để xác định chức năng của từng Button trên UI, đối với việc điều khiển trên Mobile.
    /// Tác giả: Nguyễn Ngọc Phú, Ngày tạo: 28/04/2025
    /// </summary>
    enum MOVEMENT_FUNCTION
    {
        NONE,
        LEFT,
        RIGHT,
        JUMP,
        CROUCH,
        DASH
    }

    enum ACTION_FUNCTION
    {
        ATTACK
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
        JUMP_SPEED = 3,
    }
}

namespace Platform2D.CameraSystem
{
    /// <summary>
    /// CAMERA_PAN_DIRECTION - Sử dụng các giá trị có trong enum CAMERA_PAN_DIRECTION để xác định hướng di chuyển của Camera.
    /// Tác giả: Dương Nhật Khoa, Ngày tạo: 14/05/2025.
    /// </summary>
    public enum CAMERA_PAN_DIRECTION
    {
        UP,
        DOWN,
        LEFT,
        RIGHT
    }
}