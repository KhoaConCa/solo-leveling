using Platform2D.CanvasController;
using Platform2D.CharacterInterface;
using Platform2D.EnumFunction;
using Platform2D.Vector;
using UnityEngine;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using SimpleInputNamespace;
using Platform2D.CharacterController;

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
            {
                _playerController.PlayerStates.Horizontal = 0.0f;
                _playerController.PlayerStates.Vertical = 0.0f;
                
            }
        }
        #endregion

        #region -- Pointer UI --
        /// <summary>
        /// Ghi nhận thao tác của người chơi khi người thả Button có trên UI
        /// </summary>
        /// <param name="eventData"> Các sự kiện khi người chơi thả Button </param>
        public void OnPointerUp(PointerEventData eventData)
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
                    _playerController.PlayerStates.IsJumping = false;
                    break;
                case MOVEMENT_FUNCTION.CROUCH:
                    _playerController.PlayerStates.IsCrouching = false;
                    _playerController.PlayerStates.CanDownard = false;
                    break;
                case MOVEMENT_FUNCTION.DASH:
                    _playerController.PlayerStates.IsDashing = false;
                    break;
            }
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
            {
                if (Mathf.Abs(_joystick.Value.y) < 0.6f)
                    _playerController.PlayerStates.Horizontal = _joystick.Value.x < 0 ? (float)AXIS_1D.NEGATIVE : (float)AXIS_1D.POSITIVE;
                else _playerController.PlayerStates.Horizontal = 0;

                _playerController.PlayerStates.Vertical = _joystick.Value.y;
            }
            else
                _playerController.PlayerStates.Horizontal = _moveDirection == MOVEMENT_FUNCTION.LEFT ? (float)AXIS_1D.NEGATIVE : (float)AXIS_1D.POSITIVE;
        }

        /// <summary>
        /// Thực hiện chức năng nhảy của nhân vật khi người chơi thao tác nhảy.
        /// </summary>
        public void OnJump()
        {
            _playerController.PlayerStates.IsJumping = true;
        }
         
        /// <summary>
        /// Thực hiện chức năng cúi người khi người chơi thao tác cúi.
        /// </summary>
        public void OnCrouch()
        {
            if (_playerController.PlayerStates.IsGrounded && !_playerController.PlayerStates.IsOneWay)
            {
                _playerController.PlayerStates.IsCrouching = true;
            }
            else if (_playerController.PlayerStates.IsOneWay && _playerController.PlayerStates.Vertical < 0) 
                _playerController.PlayerStates.CanDownard = true;
        }

        /// <summary>
        /// Thực hiện chức năng lướt khi người chơi thao tác lướt.
        /// </summary>
        public void OnDash()
        {
            _playerController.PlayerStates.IsDashing = true;
        }

        #endregion

        #endregion

        #region --- Unity Methods ---

        public void Awake()
        {
            _playerController = GameObject.FindGameObjectWithTag(_tagMainPlayer).GetComponent<PlayerController>();

            if (_joystick == null) _joystick = this.gameObject.GetComponent<SimpleJoystick>();
        }

        #endregion

        #region --- Fields ---

        [SerializeField] private PlayerController _playerController;

        [SerializeField] private MOVEMENT_FUNCTION _moveDirection;

        [SerializeField] private SimpleJoystick _joystick;
        [SerializeField] private string _tagMainPlayer;

        #endregion

    }
}
