using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platform2D.Utilities
{
    public static class TagLayerName
    {
        #region --- Fields ---

        #region -- Layers --
        public readonly static string Interactable = "Interactable";
        public readonly static string Penatrable = "Penatrable";
        public readonly static string StaticLevel = "StaticLevel";
        public readonly static string HazrdsAndTraps = "HazrdsAndTraps";
        public readonly static string AnimationObject = "AnimationObject";
        #endregion

        #region -- Tags --
        public readonly static string OneWay = "OneWay";
        public readonly static string Ground = "Ground";
        #endregion

        #endregion
    }
}
