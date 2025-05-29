using Platform2D.CharacterController;
using Platform2D.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region --- Unity Methods ---

    private void Start()
    {
        RecoveryHandle();

    }

    #endregion

    #region --- Methods ---

    public void RecoveryHandle()
    {
        foreach(var spawner in _spawners)
        {
            var spawnCtrl = spawner.GetComponent<Spawner>();
            spawnCtrl.Spawn(_enemyPrefab);
        }

        var playerCtrl = _player.GetComponent<PlayerCore>();
        if (playerCtrl.States.IsDead) return;

        if(playerCtrl.States.IsInteracted && playerCtrl.States.TagInteract == TagLayerName.Checkpoint)
            _checkPoint = playerCtrl.States.SavePoint;
    }

    public void RevivePlayer()
    {
        RecoveryHandle();

        _player.transform.position = _checkPoint.transform.position;
        var playerCtrl = _player.GetComponent<PlayerCore>();
        var bossCtrl = _bossPrefab.GetComponentInChildren<BossController>();

        if(!bossCtrl.States.IsDead)
            bossCtrl.ResetStats();

        if (!playerCtrl.States.IsDead) return;

        playerCtrl.States.IsRevived = true;
    }

    public void ShowDeadMenu(bool isActive)
    {
        _deadMenu.SetActive(isActive);

        if(isActive)
            Time.timeScale = 0f;
        else
            Time.timeScale = 1f;
    }

    #endregion

    #region --- Fields ---

    [Header("Entities")]
    [SerializeField] private GameObject _player;
    [SerializeField] private List<GameObject> _spawners;
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _bossPrefab;

    [Header("Static Entities")]
    [SerializeField] private GameObject _checkPoint;

    [Header("UI Object")]
    [SerializeField] private GameObject _deadMenu;

    #endregion
}
