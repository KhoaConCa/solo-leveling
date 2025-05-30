using Platform2D.CharacterController;
using Platform2D.Utilities;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    #region --- Unity Methods ---

    private void Start()
    {
        ResetScene();
    }

    #endregion

    #region --- Methods ---

    public void RecoveryHandle()
    {
        foreach (var spawner in _spawners)
        {
            var spawnCtrl = spawner.GetComponent<Spawner>();
            spawnCtrl.Spawn(_enemyPrefab);
        }

        var playerCtrl = _player.GetComponent<PlayerCore>();
        if (playerCtrl.States.IsDead) return;

        if (playerCtrl.States.IsInteracted && playerCtrl.States.TagInteract == TagLayerName.Checkpoint)
            _checkPoint = playerCtrl.States.SavePoint;
    }

    public void RevivePlayer()
    {
        RecoveryHandle();

        _player.transform.position = _checkPoint.transform.position;
        var playerCtrl = _player.GetComponent<PlayerCore>();
        var bossCtrl = _bossPrefab.GetComponentInChildren<BossController>();

        if (!bossCtrl.States.IsDead)
            bossCtrl.ResetStats();

        if (!playerCtrl.States.IsDead) return;

        playerCtrl.States.IsRevived = true;
    }

    public void ShowDeadMenu(bool isActive)
    {
        _deadMenu.SetActive(isActive);
        _settingAndToolsMenu.SetActive(!isActive);

        if (isActive)
            Time.timeScale = 0f;
        else
            Time.timeScale = 1f;
    }

    public void ShowVictoryMenu(bool isActive)
    {
        _victoryMenu.SetActive(isActive);
        _settingAndToolsMenu.SetActive(!isActive);

        if (isActive)
            Time.timeScale = 0f;
        else
            Time.timeScale = 1f;
    }

    // Gọi hàm này để reset Scene hiện tại
    public void ResetScene()
    {
        var bossCtrl = _bossPrefab.GetComponentInChildren<BossController>();

        if (bossCtrl.States.IsDead)
        {
            bossCtrl.ResetStats();
            bossCtrl.States.IsDead = false;
        }
        _checkPoint = _baseCheckpoint;

        RevivePlayer();
    }

    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    #endregion

    #region --- Fields ---

    [Header("Entities")]
    [SerializeField] private GameObject _player;
    [SerializeField] private List<GameObject> _spawners;
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _bossPrefab;

    [Header("Static Entities")]
    [SerializeField] private GameObject _baseCheckpoint;
    [SerializeField] private GameObject _checkPoint;

    [Header("UI Object")]
    [SerializeField] private GameObject _deadMenu;
    [SerializeField] private GameObject _victoryMenu;
    [SerializeField] private GameObject _settingAndToolsMenu;

    #endregion
}
