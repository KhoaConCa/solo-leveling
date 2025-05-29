using Platform2D.CharacterController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region --- Unity Methods ---

    private void Start()
    {
        RecoveryAllEnemies();
    }

    #endregion

    #region --- Methods ---

    public void RecoveryAllEnemies()
    {
        foreach(var spawner in _spawners)
        {
            var spawnCtrl = spawner.GetComponent<Spawner>();
            spawnCtrl.Spawn(_enemyPrefab);
        }
    }

    #endregion

    #region --- Fields ---

    [SerializeField] private GameObject _player;

    [SerializeField] private List<GameObject> _spawners;

    [SerializeField] private GameObject _enemyPrefab;

    #endregion
}
