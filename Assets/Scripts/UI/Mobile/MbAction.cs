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
    /// MbAction - Nhận Input của người chơi, đối với người chơi trên nền tảng Mobile.
    /// Tác giả: Nguyễn Ngọc Phú, Ngày tạo: 02/05/2025
    /// </summary>
    public class MbAction : MonoBehaviour, IAction, IPointerDownHandler, IPointerUpHandler
    {

        #region --- Overrides ---

        #region -- Pointer UI --
        /// <summary>
        /// Ghi nhận thao tác của người chơi khi người thả Button có trên UI
        /// </summary>
        /// <param name="eventData"> Các sự kiện khi người chơi thả Button </param>
        public void OnPointerUp(PointerEventData eventData)
        {
            
        }

        /// <summary>
        /// Ghi nhận thao tác của người chơi khi người chơi ấn vào Button có trên UI
        /// </summary>
        /// <param name="eventData"> Các sự kiện khi người chơi ấn vào Button </param>
        public void OnPointerDown(PointerEventData eventData)
        {
            switch (_action)
            {
                case ACTION_FUNCTION.ATTACK:
                    OnAttack();
                    break;
            }
        }
        #endregion

        #region -- Player Action Handle --
        /// <summary>
        /// Thực hiện chức năng tấn công của nhân vật khi người chơi thao tác tấn công.
        /// </summary>
        public void OnAttack()
        {
            _playerController.PlayerStates.IsAttacking = true;
        }

        #endregion

        #endregion

        #region --- Unity Methods ---

        public void Awake()
        {
            _playerController = GameObject.FindGameObjectWithTag(_tagMainPlayer).GetComponent<PlayerController>();
        }

        #endregion

        #region --- Fields ---

        [SerializeField] private PlayerController _playerController;

        [SerializeField] private ACTION_FUNCTION _action;

        [SerializeField] private string _tagMainPlayer;

        #endregion

    }
}
