using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierHealthManager : MonoBehaviour
{
    public GameObject[] bars;
    public int totalHealth;

    private void Start()
    {
        totalHealth = 6;
        UpdateChildren();
    }

    public void TakeDamage(int damageAmount)
    {
        totalHealth -= damageAmount;
        totalHealth = Mathf.Max(totalHealth, 0);

        UpdateChildren();
    }

    public void Heal(int healAmount)
    {
        totalHealth += healAmount;
        totalHealth = Mathf.Min(totalHealth, 6);

        UpdateChildren();
    }

    private void UpdateChildren()
    {
        if (totalHealth >= 5)
        {
            SetActiveChildren(3);
        }
        else if (totalHealth >= 3)
        {
            SetActiveChildren(2);
        }
        else if (totalHealth >= 1)
        {
            SetActiveChildren(1);
        }
        else
        {
            SetActiveChildren(0);
        }
    }

    private void SetActiveChildren(int number)
    {
        for (int i = 0; i < bars.Length; i++)
        {
            bars[i].SetActive(i < number);
        }
    }
}
