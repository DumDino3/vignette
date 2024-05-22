using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState
{
    public abstract void EnterState(StateManager stateManager, string[] raycastTag, GameObject litObject, GameObject instantiatedHolding);
    public abstract void UpdateState(StateManager stateManager);
}
