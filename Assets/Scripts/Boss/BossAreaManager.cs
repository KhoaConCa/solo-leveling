using Platform2D.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAreaManager : MonoBehaviour
{
    #region --- Unity Methods ---

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision == null) return;

        if (!collision.gameObject.CompareTag(TagLayerName.Player)) return;
        Debug.Log(collision.name);
        _playerPos = collision.transform.position;
        MoveAnchorSpawner();
    }

    private void MoveAnchorSpawner()
    {
        if (_anchorSpawner.transform.position.x != _playerPos.x)
        {
            Vector2 newAnchorPos = (_playerPos - (Vector2)_anchorSpawner.transform.position);
            float dir = newAnchorPos.normalized.x < 0 ? -0.8f : 0.8f;
            float speed = Mathf.Abs(newAnchorPos.x) * dir * 8f;
            Debug.DrawLine((Vector2)_anchorSpawner.transform.position, _playerPos, Color.yellow);
            var rg = _anchorSpawner.GetComponent<Rigidbody2D>();
            rg.velocity = new Vector3(speed, rg.velocity.x);
            AnchorSpawnerPos = _anchorSpawner.transform.position;
        }
    }

    #endregion

    #region --- Properties ---

    public Vector2 AnchorSpawnerPos { get; set; }

    #endregion

    #region --- Fields ---

    [SerializeField] private GameObject _anchorSpawner;

    private Vector2 _playerPos;

    #endregion
}
