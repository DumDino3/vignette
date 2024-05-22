using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceState : BaseState
{
    public override void EnterState(StateManager stateManager, string[] raycastTag, GameObject litObject, GameObject instantiatedHolding)
    {
        litObject = null;
    }

    public override void UpdateState(StateManager stateManager)
    {
        stateManager?.ResourceRayCastForward();
        stateManager?.ResourceClickLogic();
    }
}
