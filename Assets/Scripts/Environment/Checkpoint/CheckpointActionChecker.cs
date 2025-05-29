using Platform2D.GlobalInterface;
using Platform2D.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platform2D.CharacterController
{
    /// <summary>
    /// CheckpointActionChecker - Được tạo ra để thực hiện các tương tác liên quan đến Stats Checkpoint và Player.
    /// Tác giả: Nguyễn Ngọc Phú, Ngày tạo: 09/05/2025.
    /// </summary>
    public class CheckpointActionChecker : MonoBehaviour
    {
        #region --- Unity Methods ---

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision == null) return;

            if (!collision.gameObject.CompareTag(TagLayerName.Player)) return;

            var playerScpt = collision.gameObject.GetComponent<PlayerCore>();
            playerScpt.States.TagInteract = this.gameObject.tag;
            playerScpt.States.SavePoint = this.gameObject;
            playerScpt.UICtrl.ShowInteractButton(true);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision == null) return;

            if (!collision.gameObject.CompareTag(TagLayerName.Player)) return;

            var playerScpt = collision.gameObject.GetComponent<PlayerCore>();
            playerScpt.States.TagInteract = null;
            playerScpt.States.SavePoint = null;
            playerScpt.UICtrl.ShowInteractButton(false);
        }

        #endregion
    }
}
