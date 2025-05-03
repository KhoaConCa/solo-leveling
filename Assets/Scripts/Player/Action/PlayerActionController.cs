using Platform2D.CharacterController;
using Platform2D.CharacterInterface;
using Platform2D.CharacterStats;
using Platform2D.Utilities;
using Platform2D.Vector;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// PlayerActionController - Được tạo ra để quản lý và xử lý các chức năng liên quan đến hành động của nhân vật.
/// Tác giả: Nguyễn Ngọc Phú, Ngày tạo: 02/05/2025
/// </summary>
public class PlayerActionController : IAction
{

    #region --- Constructor ---

    public PlayerActionController(
        PlayerController playerController
    )
    {
        _playerController = playerController;
    }

    #endregion

    #region --- Overrides ---

    /// <summary>
    /// Thực hiện tấn công của nhân vật.
    /// </summary>
    public void OnAttack()
    {
       
    }

    

    #endregion

    #region --- Fields ---

    private readonly PlayerController _playerController;

    #endregion

}
