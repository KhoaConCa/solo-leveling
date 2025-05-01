using Platform2D.CharacterController;
using Platform2D.CharacterInterface;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// PlayerMovementController - Được tạo ra để quản lý và xử lý các chức năng liên quan đến di chuyển của nhân vật.
/// Tác giả: Nguyễn Ngọc Phú, Ngày tạo: 28/04/2025
/// </summary>
public class PlayerMovementController : IMoveable, ICheckable
{

    #region --- Constructor ---

    public PlayerMovementController(
        PlayerController playerController
    )
    {
        _playerController = playerController;
    }

    #endregion

    #region --- Overrides ---

    /// <summary>
    /// Thực hiện thay đổi vị trí chiều ngang của nhân vật.
    /// </summary>
    public void OnMove()
    {
        float movementSpeed = _playerController.PlayerStats.MovementSpeed;

        if (!_playerController.PlayerStates.IsGrounded && _playerController.PlayerStates.IsOnWall)
            movementSpeed = 0;

        float velY = _playerController.Rg2D.velocity.y;
        float velX = _playerController.PlayerStates.IsMoving;

        _playerController.Rg2D.velocity = new Vector2(velX * movementSpeed, velY);
    }

    /// <summary>
    /// Thực hiện thao tác nhảy cho nhân vật
    /// </summary>
    public void OnJump()
    {
        if (_playerController.PlayerStates.JumpCount == _playerController.PlayerStates.MAXJUMP)
            return;

        float velY = _playerController.PlayerStates.IsJumping;
        if (_playerController.PlayerStates.JumpCount > 1)
            velY = _playerController.PlayerStats.DoubleJumpSpeed;

        float velX = _playerController.Rg2D.velocity.x;

        float jumpSpeed = _playerController.PlayerStats.JumpSpeed;

        _playerController.Rg2D.velocity = new Vector2(velX, velY * jumpSpeed);
    }

    /// <summary>
    /// Thực hiện thao tác cúi người cho nhân vật.
    /// </summary>
    public void OnCrouch()
    {
        float movementSpeed = _playerController.PlayerStats.CrouchSpeed;

        float velY = _playerController.Rg2D.velocity.y;
        float velX = _playerController.PlayerStates.IsMoving;

        _playerController.Rg2D.velocity = new Vector2(velX * movementSpeed, velY);
    }

    /// <summary>
    /// Thực hiện thao tác lướt cho nhân vật.
    /// </summary>
    public void OnDash()
    {
        float movementSpeed = _playerController.PlayerStats.DashSpeed;

        float velY = _playerController.Rg2D.velocity.y;
        float velX = _playerController.PlayerStates.IsMoving;

        _playerController.Rg2D.velocity = new Vector2(velX * movementSpeed, velY);
    }

    /// <summary>
    /// Thực hiện kiểm tra xem nhân vật có đang chạm vào mặt đất không
    /// </summary>
    public void IsGrounded()
    {
        bool isGround = _playerController.GroundChecker.Cast(Vector2.down, _contactFilter, _onGroundHit2Ds, GROUND_DISTANCE) > 0;

        _playerController.PlayerStates.IsGrounded = isGround;
    }

    #endregion

    #region --- Methods ---

    public void IsOnWall(float direction)
    {
        var vecDirection = direction < 0 ? Vector2.left : Vector2.right;
        _playerController.PlayerStates.IsOnWall = _playerController.GroundChecker.Cast(vecDirection, _contactFilter, _onWallHit2Ds, WALL_DISTANCE) > 0;
    }

    #endregion

    #region --- Fields ---

    private readonly PlayerController _playerController;

    private ContactFilter2D _contactFilter;
    private readonly RaycastHit2D[] _onGroundHit2Ds = new RaycastHit2D[5];
    private readonly RaycastHit2D[] _onWallHit2Ds = new RaycastHit2D[5];

    private const float GROUND_DISTANCE = 0.05f;
    private const float WALL_DISTANCE = 0.05f;

    #endregion

}
