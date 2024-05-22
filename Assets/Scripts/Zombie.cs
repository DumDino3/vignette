using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{ 
    public GameObject player; // Reference to the player GameObject
    public float attackRate = 3f; // Time between attacks in seconds
    public float attackRange = 5f; // Range within which the enemy can attack the target

    private GameObject currentTarget;
    private float nextAttackTime = 0f; // Time when the next attack is allowed

    private void Start()
    {
        ChooseTarget();
    }

    private void Update()
    {
        if (currentTarget == null)
        {
            ChooseTarget();
        }

        if (currentTarget != null)
        {
            float distanceToTarget = Vector3.Distance(transform.position, currentTarget.transform.position);
            if (distanceToTarget <= attackRange)
            {
                if (Time.time >= nextAttackTime)
                {
                    Attack();
                    nextAttackTime = Time.time + attackRate;
                }
            }
        }
    }

    private void Attack()
    {
        BarrierHealthManager healthManager = currentTarget.GetComponent<BarrierHealthManager>();
        if (healthManager != null)
        {
            healthManager.TakeDamage(1);
        }
    }

    private void ChooseTarget()
    {
        GameObject[] barriers = GameObject.FindGameObjectsWithTag("WoodDeploy"); // Find all barriers
        float minDistance = Mathf.Infinity;
        GameObject closestBarrier = null;

        foreach (GameObject barrier in barriers)
        {
            float distance = Vector3.Distance(transform.position, barrier.transform.position);
            if (distance < minDistance)
            {
                closestBarrier = barrier;
                minDistance = distance;
            }
        }

        if (closestBarrier != null && closestBarrier.activeInHierarchy)
        {
            currentTarget = closestBarrier;
        }
        else
        {
            currentTarget = player;
        }
    }
}
