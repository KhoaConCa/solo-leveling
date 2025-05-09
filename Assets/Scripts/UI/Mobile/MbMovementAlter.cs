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
    /// MbMovementAlter - Nhận Input của người chơi, đối với người chơi trên nền tảng Mobile.
    /// Tác giả: Nguyễn Ngọc Phú, Ngày tạo: 28/04/2025
    /// </summary>
    public class MbMovementAlter : MonoBehaviour, IMoveable, IPointerDownHandler, IPointerUpHandler
    {

        #region --- Overrides ---

        #region -- Pointer UI --
        /// <summary>
        /// Ghi nhận thao tác của người chơi khi người thả Button có trên UI
        /// </summary>
        /// <param name="eventData"> Các sự kiện khi người chơi thả Button </param>
        public void OnPointerUp(PointerEventData eventData)
        {
            switch (_moveDirection)
            {
                case MOVEMENT_FUNCTION.JUMP:
                    break;
                case MOVEMENT_FUNCTION.CROUCH:
                    if (_playerController.States.OnGround && !_playerController.States.IsCeiling)
                        _playerController.States.IsCrouch = false;

                    _playerController.States.UnholdCrouch = true;
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
                case MOVEMENT_FUNCTION.JUMP:
                    _playerController.States.IsJumping = true;
                    break;
                case MOVEMENT_FUNCTION.CROUCH:
                    if(Mathf.Abs(_playerController.States.OnMove.y) > 0.7f && _playerController.MovementChecker.IsOneWay)
                    {
                        _playerController.States.IsPenetrable = true;
                        _playerController.States.UnholdCrouch = false;
                    }
                    else if (_playerController.States.OnMove != Vector2.zero && _playerController.States.OnGround)
                    {
                        _playerController.States.IsCrouch = true;
                        _playerController.States.UnholdCrouch = false;
                    }    
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
            
        }

        /// <summary>
        /// Thực hiện chức năng nhảy của nhân vật khi người chơi thao tác nhảy.
        /// </summary>
        public void OnJump()
        {
            
        }
         
        /// <summary>
        /// Thực hiện chức năng cúi người khi người chơi thao tác cúi.
        /// </summary>
        public void OnCrouch()
        {
           
        }

        /// <summary>
        /// Thực hiện chức năng lướt khi người chơi thao tác lướt.
        /// </summary>
        public void OnDash()
        {
            
        }

        #endregion

        #endregion

        #region --- Unity Methods ---

        public void Awake()
        {
            _playerController = GameObject.FindGameObjectWithTag(_tagMainPlayer).GetComponent<PlayerCore>();

        }

        #endregion

        #region --- Fields ---

        [SerializeField] private PlayerCore _playerController;

        [SerializeField] private MOVEMENT_FUNCTION _moveDirection;

        [SerializeField] private string _tagMainPlayer;

        #endregion

    }
}
