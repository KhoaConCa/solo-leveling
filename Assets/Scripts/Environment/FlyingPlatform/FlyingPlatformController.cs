using Platform2D.Utilities;
using System.Collections;
using System.Collections.Generic;
using Unity.Services.Lobbies.Models;
using UnityEngine;

public class FlyingPlatformController : MonoBehaviour
{
    #region --- Unity Methods ---

    private void Awake()
    {
        _lastestPos = _lowestPos.transform.position;

        _isTrigger = true;

        _changeCountDown = new Timer();
        _changeCountDown.StartCountdown();
    }

    private void FixedUpdate()
    {
        OnMove();
    }

    #endregion

    #region --- Methods ---

    private void OnMove()
    {
        Vector2 posLength = _lastestPos - (Vector2)_collider.bounds.center;

        if(RayCastPlayer(0.05f) && _isTrigger)
        {
            _lastestPos = _lastestPos == (Vector2)_highestPos.transform.position ? _lowestPos.transform.position : _highestPos.transform.position;
            _isTrigger = false;
            return;
        }

        if(posLength.magnitude <= _maxRange)
        {
            ChangeLastestPos();
            _rigidbody.velocity = Vector2.zero;
            return;
        }

        _rigidbody.velocity = new Vector2(posLength.normalized.x * _moveSpeed, posLength.normalized.y * _moveSpeed);
    }

    private void ChangeLastestPos()
    {
        var trigger = _changeCountDown.FixedTimeCountdown(_changeDuration);

        if (trigger)
        {
            _lastestPos = _lastestPos == (Vector2)_highestPos.transform.position ? _lowestPos.transform.position : _highestPos.transform.position;
            _changeCountDown.StartCountdown();
            _isTrigger = true;
        }
    }

    private bool RayCastPlayer(float dis)
    {
        return _collider.Cast(Vector2.down, _filter, _groundHits, dis) > 0;
    }

    #endregion

    #region --- Fields ---

    [SerializeField] private ContactFilter2D _filter;

    [SerializeField] private GameObject _highestPos;
    [SerializeField] private GameObject _lowestPos;

    [SerializeField] private Vector2 _lastestPos;

    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private Collider2D _collider;

    [SerializeField] private Timer _changeCountDown;

    private readonly RaycastHit2D[] _groundHits = new RaycastHit2D[1];

    private bool _isTrigger = true;

    [SerializeField] private float _moveSpeed = 1f;
    [SerializeField] private float _maxRange = 1f;
    [SerializeField] private float _changeDuration = 1f;

    #endregion
}
