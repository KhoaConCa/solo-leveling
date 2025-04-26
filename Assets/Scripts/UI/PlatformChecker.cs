using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformChecker
{

    #region --- Methods ---

    public bool IsMobilePlatform()
    {
        return Application.platform == RuntimePlatform.Android ||
               Application.platform == RuntimePlatform.IPhonePlayer;
    }

    #endregion

}
