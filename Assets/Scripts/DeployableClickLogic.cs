using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeployableClickLogic : MonoBehaviour
{
    public SpriteRenderer[] woodSprite;
    private Color ogColor;
    public BarrierHealthManager barrierHealthManager;

    private void Start()
    {
        barrierHealthManager = GetComponent<BarrierHealthManager>();
        ogColor = woodSprite[0].color;
    }

    public void LitUp()
    {
        foreach (var wood in woodSprite)
        {
            wood.color = Color.green;
        }
    }

    public void DeLit()
    {
        foreach (var wood in woodSprite)
        {
            wood.color = ogColor;
        }
    }

    public void TriggerLogic()
    {
        barrierHealthManager?.Heal(2);
    }
}
