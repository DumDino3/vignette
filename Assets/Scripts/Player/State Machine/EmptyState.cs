using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EmptyState : BaseState
{
    string[] raycastTags;
    public override void EnterState(StateManager stateManager, string[] raycastTag, GameObject litObject, GameObject instantiatedHolding)
    {
        GameObject.Destroy(instantiatedHolding);
        litObject = null;

        raycastTag = new string[] {"Resource", "Weapon"};
        raycastTags = raycastTag;
    }

    public override void UpdateState(StateManager stateManager)
    {
        stateManager?.EmptyRaycastForward(raycastTags);
        stateManager?.EmptyClickLogic();
    }
}
