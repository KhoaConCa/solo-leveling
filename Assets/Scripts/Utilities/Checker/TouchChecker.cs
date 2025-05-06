using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

namespace Platform2D.CharacterChecker
{
    public class TouchChecker
    {
        #region --- Methods ---

        public bool ColliderChecker(Color color, CapsuleCollider2D checker, Vector2 direction, float distance, RaycastHit2D[] hit2Ds, ContactFilter2D contactFilter)
        {
            if(checker.Cast(direction, contactFilter, hit2Ds, distance) > 0)
            {
                DebugRaycast(color, checker.bounds.center, direction);
                return true;
            }
            return false;
        }

        public Collider2D TransformChecker(Color color, Vector2 startPos, Vector2 direction, float distance)
        {
            RaycastHit2D hit2D = Physics2D.Raycast(startPos, direction, distance);
            DebugRaycast(color, startPos, direction);
            return hit2D.collider;
        }
        public Collider2D TransformChecker(Color color, Vector2 startPos, Vector2 direction, float distance, int layers)
        {
            RaycastHit2D hit2D = Physics2D.Raycast(startPos, direction, distance, layers);
            DebugRaycast(color, startPos, direction);
            return hit2D.collider;
        }

        public void DebugRaycast(Color color, Vector2 startPos, Vector2 direction)
        {
            Debug.DrawRay(startPos, direction * 1f, color);
        }

        #endregion

        #region --- Fields ---



        #endregion
    }
}