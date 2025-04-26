using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class MbMovement : MonoBehaviour, IMoveable, IPointerDownHandler, IPointerUpHandler
{

    #region --- Overrides ---

    public void OnPointerUp(PointerEventData eventData)
    {
        UIController.mainPlayer.playerStates.isMoving = 0.0f;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        switch (moveDirection)
        {
            case ENUM_DIRECTION.LEFT:
            case ENUM_DIRECTION.RIGHT:
                OnMove();
                break;
        }
    }

    public void OnMove()
    {
        UIController.mainPlayer.playerStates.isMoving = moveDirection == ENUM_DIRECTION.LEFT ? ((float)ENUM_DIRECTION.LEFT) : ((float)ENUM_DIRECTION.RIGHT);
    }

    #endregion

    #region --- Unity Methods ---

    public void Awake()
    {
        UIController = GameObject.FindGameObjectWithTag(_tagMainUI).GetComponent<UIController>();
    }

    #endregion

    #region --- Properties ---

    public UIController UIController { get; set; }

    #endregion

    #region --- Fields ---

    private enum ENUM_DIRECTION { LEFT = -1, RIGHT = 1 }
    [SerializeField] private ENUM_DIRECTION moveDirection;
    [SerializeField] private string _tagMainUI;

    #endregion

}
