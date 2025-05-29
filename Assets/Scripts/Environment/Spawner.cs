using Platform2D.CharacterController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    #region --- Methods ---

    public void Spawn(GameObject prefab)
    {
        if (Enemy == null)
            Enemy = Instantiate(prefab, transform.position, transform.rotation, transform);

        var enemyCtrl = Enemy.GetComponent<EnemyController>();
        enemyCtrl.SpawnerCtrl = this;
        enemyCtrl.ResetStats();

        Enemy.SetActive(true);
    }

    #endregion

    #region --- Properties ---

    public GameObject Enemy { get; set; }

    #endregion
}
