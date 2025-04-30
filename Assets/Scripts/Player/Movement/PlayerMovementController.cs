using Platform2D.CharacterController;
using Platform2D.CharacterInterface;
using Platform2D.CharacterStates;
using Platform2D.CharacterStats;
using Platform2D.GlobalChecker;
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
        float velY = _playerController.Rg2D.velocity.y;
        float velX = _playerController.PlayerStates.IsMoving;

        float movementSpeed = _playerController.PlayerStats.MovementSpeed;

        _playerController.Rg2D.velocity = new Vector2(velX * movementSpeed, velY);
    }

    /// <summary>
    /// Thực hiện thao tác nhảy cho nhân vật
    /// </summary>
    public void OnJump()
    {
        float velY = _playerController.PlayerStates.IsJumping;
        float velX = _playerController.Rg2D.velocity.x;

        float jumpSpeed = _playerController.PlayerStats.JumpSpeed;

        _playerController.Rg2D.velocity = new Vector2(velX, velY * jumpSpeed);
    }

    /// <summary>
    /// Thực hiện kiểm tra xem nhân vật có đang chạm vào mặt đất không
    /// </summary>
    public void IsGrounded()
    {
        _playerController.PlayerStates.IsGrounded = _playerController.GroundChecker.Cast(Vector2.down, _contactFilter, _hit2Ds, GROUND_DISTANCE) > 0;
    }

    #endregion

    #region --- Fields ---

    private readonly PlayerController _playerController;

    private ContactFilter2D _contactFilter;
    private readonly RaycastHit2D[] _hit2Ds = new RaycastHit2D[5];

    private const float GROUND_DISTANCE = 0.05f;

    #endregion

}
