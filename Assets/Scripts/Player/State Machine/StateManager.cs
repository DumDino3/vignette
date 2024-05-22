using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class StateManager : MonoBehaviour
{
    private BaseState currentState;
    private EmptyState EmptyState = new EmptyState();
    private ResourceState ResourceState = new ResourceState();
    private ShovelState ShovelState = new ShovelState();

    public string[] raycastDetectTag = new string[0];
    public float raycastRange;
    public LayerMask defaultLayer;
    public GameObject currentlyHolding = null;
    public GameObject litObject = null;
    public GameObject instantiatedHolding = null;
    public GameObject continousDetection;
    public float tossForce;
    public string tagToCheckWhenHeld;
    void Start()
    {       
        currentState = EmptyState;
        currentState.EnterState(this, raycastDetectTag, litObject, instantiatedHolding);
    }

    void Update()
    {
        currentState.UpdateState(this);
    }

    public void ChangeState(BaseState state)
    {
        currentState = state;
        state.EnterState(this, raycastDetectTag, litObject,instantiatedHolding);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(this.transform.position, this.transform.right * raycastRange);
    }


    //EMPTY STATE
    public void EmptyRaycastForward(string[] tagsToTrigger)
    {
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, this.transform.right, raycastRange, defaultLayer);
        if (hit.collider != null)
        {
            continousDetection = hit.collider.gameObject;
        }
        else
        {
            continousDetection = null;
        }

        foreach (string tag in tagsToTrigger)
        {
            if (hit.collider != null && hit.collider.CompareTag(tag))
            {
                litObject = hit.collider.gameObject;
                ResourceClickLogic resourceLogic = litObject.GetComponent<ResourceClickLogic>();
                resourceLogic?.LitUp();
            }
        }

        if (continousDetection != null && litObject != continousDetection || continousDetection == null && litObject != null)
        {
            ResourceClickLogic resourceLogic = litObject.GetComponent<ResourceClickLogic>();
            resourceLogic?.DeLit();
            litObject = null;
        }
    }

    public void EmptyClickLogic()
    {
        if (Input.GetMouseButtonDown(0) && litObject != null)
        {
            if (litObject.CompareTag("Resource"))
            {
                ResourceClickLogic resourceLogic = litObject.GetComponent<ResourceClickLogic>();
                currentlyHolding = resourceLogic.obtainable;
                tagToCheckWhenHeld = resourceLogic.tagToCheck;

                instantiatedHolding = Instantiate(currentlyHolding, this.transform);
                instantiatedHolding.transform.localPosition = new Vector3 (0.45f, 0f, 0f) ;
                resourceLogic?.DeLit();

                ChangeState(ResourceState);
            }

            //if (litObject.CompareTag("Weapon"))
            //{
            //    Instantiate(currentlyHolding, this.transform.position, Quaternion.identity);
            //    ChangeState(ShovelState);
            //}
        }
    }



    //RESOURCE STATE
    public void ResourceRayCastForward()
    {
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, this.transform.right, raycastRange, defaultLayer);
        if (hit.collider != null)
        {
            continousDetection = hit.collider.gameObject;
        }
        else
        {
            continousDetection = null;
        }

        if (hit.collider != null && hit.collider.CompareTag(tagToCheckWhenHeld))
        {
            litObject = hit.collider.gameObject;
            DeployableClickLogic deployableLogic = litObject.GetComponent<DeployableClickLogic>();
            deployableLogic?.LitUp();
        }

        if (continousDetection != null && litObject != continousDetection || continousDetection == null && litObject != null)
        {
            DeployableClickLogic deployableLogic = litObject.GetComponent<DeployableClickLogic>();
            deployableLogic?.DeLit();
            litObject = null;
        }

        //RaycastHit2D hit = Physics2D.Raycast(this.transform.position, this.transform.right, raycastRange, defaultLayer);
        //if (hit.collider != null)
        //{
        //    //placeholder
        //}

        //if (litObject != continousDetection)
        //{
        //    litObject = null;
        //    //place holder for delit
        //}
    }

    public void ResourceClickLogic()
    {
        if (Input.GetMouseButtonDown(0) && litObject != null)
        {
            DeployableClickLogic deployableLogic = litObject.GetComponent<DeployableClickLogic>();
            deployableLogic?.TriggerLogic();
            deployableLogic?.DeLit();

            ChangeState(EmptyState);
        }

        //else if (Input.GetMouseButton(1))
        //{
        //    Rigidbody2D heldRb = currentlyHolding.GetComponent<Rigidbody2D>();
        //    heldRb.bodyType = RigidbodyType2D.Dynamic;

        //    heldRb.AddForce(this.transform.position * tossForce);
        //}
    }
}
