using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    #region --- Unity Methods ---

    public void Awake()
    {
        rigidbody2D = gameObject.GetComponent<Rigidbody2D>();

        playerStats = gameObject.GetComponentInChildren<PlayerStats>();
        playerStates = gameObject.GetComponentInChildren<PlayerStates>();
    }

    #endregion

    #region --- Properties ---

    public Rigidbody2D rigidbody2D {  get; set; }

    public PlayerStates playerStates {  get; private set; }
    public PlayerStats playerStats {  get; private set; }

    #endregion

}
