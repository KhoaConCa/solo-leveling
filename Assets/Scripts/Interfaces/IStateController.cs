using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStateController<TState>
{
    TState CurrentState { get; set; }
}

