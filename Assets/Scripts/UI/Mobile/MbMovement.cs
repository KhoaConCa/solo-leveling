using Platform2D.CanvasController;
using Platform2D.CharacterInterface;
using Platform2D.Movement;
using Platform2D.Vector;
using UnityEngine;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using SimpleInputNamespace;

namespace Platform2D.UIMovement
{
    /// <summary>
    /// MbMovement - Nhận Input của người chơi, đối với người chơi trên nền tảng Mobile.
    /// Tác giả: Nguyễn Ngọc Phú, Ngày tạo: 28/04/2025
    /// </summary>
    public class MbMovement : MonoBehaviour, IMoveable, IPointerDownHandler, IPointerUpHandler, IDragHandler, IEndDragHandler
    {

        #region --- Overrides ---

        #region -- Drag UI --
        /// <summary>
        /// Ghi nhận thao tác của người chơi khi kéo Joystick trên UI.
        /// </summary>
        /// <param name="eventData"> Các sự kiện khi người chơi kéo Joystick </param>
        public void OnDrag(PointerEventData eventData) {
            if (_joystick != null)
                OnMove();
        }

        /// <summary>
        /// Ghi nhận thao tác của người chơi khi thả Joystick trên UI.
        /// </summary>
        /// <param name="eventData"> Các sự kiện khi người chơi thả Joystick </param>
        public void OnEndDrag(PointerEventData eventData)
        {
            if (_joystick != null)
                _uiController.PlayerController.PlayerStates.IsMoving = 0.0f;
        }
        #endregion

        #region -- Pointer UI --
        /// <summary>
        /// Ghi nhận thao tác của người chơi khi người thả Button có trên UI
        /// </summary>
        /// <param name="eventData"> Các sự kiện khi người chơi thả Button </param>
        public void OnPointerUp(PointerEventData eventData)
        {
            _uiController.PlayerController.PlayerStates.IsJumping = 0.0f;
            _uiController.PlayerController.PlayerStates.IsDashing = false;

            if (_uiController.PlayerController.PlayerStates.IsGrounded)
                _uiController.PlayerController.PlayerStates.IsCrouching = false;
        }

        /// <summary>
        /// Ghi nhận thao tác của người chơi khi người chơi ấn vào Button có trên UI
        /// </summary>
        /// <param name="eventData"> Các sự kiện khi người chơi ấn vào Button </param>
        public void OnPointerDown(PointerEventData eventData)
        {
            switch (_moveDirection)
            {
                case MOVEMENT_FUNCTION.NONE:
                    break;
                case MOVEMENT_FUNCTION.LEFT:
                case MOVEMENT_FUNCTION.RIGHT:
                    OnMove();
                    break;
                case MOVEMENT_FUNCTION.JUMP:
                    OnJump();
                    break;
                case MOVEMENT_FUNCTION.CROUCH:
                    OnCrouch();
                    break;
                case MOVEMENT_FUNCTION.DASH:
                    OnDash();
                    break;
            }
        }
        #endregion

        #region -- Player Move Handle --
        /// <summary>
        /// Thực hiện chức năng di chuyển nhân vật khi người chơi thao tác di chuyển.
        /// </summary>
        public void OnMove()
        {
            if(_joystick != null)
                _uiController.PlayerController.PlayerStates.IsMoving = _joystick.Value.x < 0 ? (float)AXIS_1D.NEGATIVE : (float)AXIS_1D.POSITIVE;
            else
                _uiController.PlayerController.PlayerStates.IsMoving = _moveDirection == MOVEMENT_FUNCTION.LEFT ? (float)AXIS_1D.NEGATIVE : (float)AXIS_1D.POSITIVE;
        }

        /// <summary>
        /// Thực hiện chức năng nhảy của nhân vật khi người chơi thao tác nhảy.
        /// </summary>
        public void OnJump()
        {
            _uiController.PlayerController.PlayerStates.IsJumping = (float)AXIS_1D.POSITIVE;
        }

        /// <summary>
        /// Thực hiện chức năng cúi người khi người chơi thao tác cúi.
        /// </summary>
        public void OnCrouch()
        {
            if (_uiController.PlayerController.PlayerStates.IsGrounded)
            {
                _uiController.PlayerController.PlayerStates.IsCrouching = true;
            }
        }

        /// <summary>
        /// Thực hiện chức năng lướt khi người chơi thao tác lướt.
        /// </summary>
        public void OnDash()
        {
            _uiController.PlayerController.PlayerStates.IsDashing = true;
            Debug.Log(_uiController.PlayerController.PlayerStates.IsDashing);
        }
        #endregion

        #endregion

        #region --- Unity Methods ---

        public void Awake()
        {
            _uiController = GameObject.FindGameObjectWithTag(_tagMainUI).GetComponent<UIController>();

            if (_joystick == null) _joystick = this.gameObject.GetComponent<SimpleJoystick>();
        }

        #endregion

        #region --- Fields ---

        [SerializeField] private UIController _uiController;

        [SerializeField] private MOVEMENT_FUNCTION _moveDirection;

        [SerializeField] private SimpleJoystick _joystick;
        [SerializeField] private string _tagMainUI;

        #endregion

    }
}
