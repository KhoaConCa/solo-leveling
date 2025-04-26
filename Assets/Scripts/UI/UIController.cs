using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{

    #region --- Unity Methods ---

    public void Awake()
    {
        /*if(!platformChecker.IsMobilePlatform())
        {
            foreach (var item in mobileObject)
            {
                item.SetActive(false);
            }
            return;
        }*/
        mainPlayer = GameObject.FindWithTag(_tagMainPlayer).GetComponent<PlayerController>();
    }

    #endregion

    #region --- Properties ---

    public PlayerController mainPlayer {  get; private set; }

    #endregion

    #region --- Fields ---

    [SerializeField] private PlatformChecker platformChecker = new PlatformChecker();

    [SerializeField] private List<GameObject> mobileObject = new List<GameObject>();

    [SerializeField] private string _tagMainPlayer;

    #endregion

}
