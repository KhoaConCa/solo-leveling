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
    public class MbJoystick : MonoBehaviour, IDragHandler, IEndDragHandler
    {

        #region --- Overrides ---

        #region -- Drag UI --
        /// <summary>
        /// Ghi nhận thao tác của người chơi khi kéo Joystick trên UI.
        /// </summary>
        /// <param name="eventData"> Các sự kiện khi người chơi kéo Joystick </param>
        public void OnDrag(PointerEventData eventData) {
            if (_joystick != null)
                _playerController.States.OnMove = _joystick.Value;

        }

        /// <summary>
        /// Ghi nhận thao tác của người chơi khi thả Joystick trên UI.
        /// </summary>
        /// <param name="eventData"> Các sự kiện khi người chơi thả Joystick </param>
        public void OnEndDrag(PointerEventData eventData)
        {
            if (_joystick != null)
                _playerController.States.OnMove = Vector2.zero;
        }
        #endregion

        #endregion

        #region --- Unity Methods ---

        public void Awake()
        {
            _playerController = GameObject.FindGameObjectWithTag(_tagMainPlayer).GetComponent<PlayerCore>();

            if (_joystick == null) _joystick = this.gameObject.GetComponent<SimpleJoystick>();
        }

        #endregion

        #region --- Fields ---

        [SerializeField] private PlayerCore _playerController;

        [SerializeField] private SimpleJoystick _joystick;
        [SerializeField] private string _tagMainPlayer;

        #endregion

    }
}
