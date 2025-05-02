using Platform2D.CharacterController;
using Platform2D.CharacterInterface;
using Platform2D.CharacterStats;
using Platform2D.Vector;
using System.Collections.Generic;
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
        float movementSpeed = _playerController.PlayerStats.CurrentMovementSpeed;

        if (!_playerController.PlayerStates.IsGrounded && _playerController.PlayerStates.IsOnWall)
            movementSpeed = 0;

        float velY = _playerController.Rg2D.velocity.y;
        float velX = _playerController.PlayerStates.Horizontal;

        _playerController.Rg2D.velocity = new Vector2(velX * movementSpeed, velY);
    }

    /// <summary>
    /// Thực hiện thao tác nhảy cho nhân vật
    /// </summary>
    public void OnJump()
    {
        float velX = _playerController.Rg2D.velocity.x;

        float jumpSpeed = _playerController.PlayerStats.PlayerStatsSO.jumpSpeed;
        if (_playerController.PlayerStates.IsDoubleJump)
            jumpSpeed = _playerController.PlayerStats.PlayerStatsSO.DoubleJumpSpeed;

        _playerController.Rg2D.velocity = new Vector2(velX, (float)AXIS_1D.POSITIVE * jumpSpeed);
    }

    /// <summary>
    /// Thực hiện thao tác cúi người cho nhân vật.
    /// </summary>
    public void OnCrouch()
    {
        float movementSpeed = _playerController.PlayerStats.CrouchSpeed;

        float velY = _playerController.Rg2D.velocity.y;
        float velX = _playerController.PlayerStates.Horizontal;

        _playerController.Rg2D.velocity = new Vector2(velX * movementSpeed, velY);
    }

    /// <summary>
    /// Thực hiện thao tác lướt cho nhân vật.
    /// </summary>
    public void OnDash()
    {
        float movementSpeed = _playerController.PlayerStats.DashSpeed;

        float velY = _playerController.Rg2D.velocity.y;
        float velX = _playerController.PlayerStates.Horizontal;

        _playerController.Rg2D.velocity = new Vector2(velX * movementSpeed, velY);
    }

    /// <summary>
    /// Thực hiện kiểm tra xem nhân vật có đang chạm vào mặt đất không
    /// </summary>
    public void IsGrounded()
    {
        Vector2 origin = _playerController.transform.position;
        Vector2 direction = Vector2.down;

        int groundLayer = LayerMask.GetMask(_groundLayerName);
        int oneWayLayer = LayerMask.GetMask(_oneWayLayerName);

        // Vẽ raycast bằng màu xanh lá trong Scene view
        Debug.DrawRay(origin, direction * 1f, Color.green);

        RaycastHit2D groundHit = new RaycastHit2D();
        RaycastHit2D oneWayHit = new RaycastHit2D();

        if (_playerController.CapCol2D.enabled)
        {
            groundHit = Physics2D.Raycast(origin, direction, GROUND_DISTANCE, groundLayer);
            oneWayHit = Physics2D.Raycast(origin, direction, ONEWAY_DISTANCE, oneWayLayer);
        }

        if (groundHit.collider != null)
        {
            _playerController.PlayerStates.IsGrounded = true;
            _playerController.PlayerStates.IsOneWay = false;
        }
        else if (oneWayHit.collider != null)
        {
            _playerController.PlayerStates.IsGrounded = true;
            _playerController.PlayerStates.IsOneWay = true;
        }
        else
        {
            _playerController.PlayerStates.IsGrounded = false;
            _playerController.PlayerStates.IsOneWay = false;
        }
    }

    #endregion

    #region --- Methods ---

    public void IsOnWall(float direction)
    {
        Vector2 origin = _playerController.transform.position;
        var vecDirection = direction < 0 ? Vector2.left : Vector2.right;

        int layerMask = LayerMask.GetMask(_groundLayerName);

        RaycastHit2D wallHit2D = Physics2D.Raycast(origin, vecDirection, WALL_DISTANCE, layerMask);
        if(wallHit2D.collider != null)
            _playerController.PlayerStates.IsOnWall = true;
        else _playerController.PlayerStates.IsOnWall = false;
    }

    #endregion

    #region --- Fields ---

    private readonly PlayerController _playerController;


    private readonly string _groundLayerName = "Ground";
    private readonly string _oneWayLayerName = "Penetrable";

    private const float GROUND_DISTANCE = 1f;
    private const float ONEWAY_DISTANCE = 1f;
    private const float WALL_DISTANCE = 0.5f;

    #endregion

}
