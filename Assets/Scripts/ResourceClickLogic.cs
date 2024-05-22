using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ResourceClickLogic : MonoBehaviour
{
    public GameObject obtainable;
    public SpriteRenderer[] woodSprite;
    private Color ogColor;
    public string tagToCheck;

    private void Start()
    {
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
}

