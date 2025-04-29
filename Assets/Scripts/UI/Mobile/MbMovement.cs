using Platform2D.CanvasController;
using Platform2D.CharacterInterface;
using Platform2D.Movement;
using Platform2D.Vector;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Platform2D.UIMovement
{
    /// <summary>
    /// MbMovement - Nhận Input của người chơi, đối với người chơi trên nền tảng Mobile.
    /// Tác giả: Nguyễn Ngọc Phú, Ngày tạo: 28/04/2025
    /// </summary>
    public class MbMovement : MonoBehaviour, IMoveable, IPointerDownHandler, IPointerUpHandler
    {

        #region --- Overrides ---

        /// <summary>
        /// Ghi nhận thao tác của người chơi khi người thả Button có trên UI
        /// </summary>
        /// <param name="eventData"> Các sự kiện khi người chơi thả Button </param>
        public void OnPointerUp(PointerEventData eventData)
        {
            UIController.MainPlayer.PlayerStates.isMoving = 0.0f;
        }

        /// <summary>
        /// Ghi nhận thao tác của người chơi khi người chơi ấn vào Button có trên UI
        /// </summary>
        /// <param name="eventData"> Các sự kiện khi người chơi ấn vào Button </param>
        public void OnPointerDown(PointerEventData eventData)
        {
            switch (moveDirection)
            {
                case MOVEMENT_FUNCTION.LEFT:
                case MOVEMENT_FUNCTION.RIGHT:
                    OnMove();
                    break;
                case MOVEMENT_FUNCTION.JUMP:
                    OnJump();
                    break;
            }
        }

        /// <summary>
        /// Thực hiện chức năng di chuyển nhân vật khi người chơi thao tác di chuyển.
        /// </summary>
        public void OnMove()
        {
            UIController.MainPlayer.PlayerStates.isMoving = moveDirection == MOVEMENT_FUNCTION.LEFT ? (float)AXIS_1D.NEGATIVE : (float)AXIS_1D.POSITIVE;
        }

        /// <summary>
        /// Thực hiện chức năng nhảy của nhân vật khi người chơi thao tác nhảy.
        /// </summary>
        public void OnJump()
        {

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

        [SerializeField] private MOVEMENT_FUNCTION moveDirection;
        [SerializeField] private string _tagMainUI;

        #endregion

    }
}
