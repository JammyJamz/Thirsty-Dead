using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class CustomerManager : MonoBehaviour
{
    public Transform[] points;
    public Transform target;
    public float lookRadius = 5f;

    private int destPoint = 0;
    private NavMeshAgent agent;

    public GameObject spaceToChat; 


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        // Disabling auto-braking allows for continuous movement
        // between points (ie, the agent doesn't slow down as it
        // approaches a destination point).
//        agent.autoBraking = false;

        GotoNextPoint();
        spaceToChat.SetActive(false);
    }


    void GotoNextPoint()
    {
        // Returns if no points have been set up
        if (points.Length == 0)
            return;

        // Set the agent to go to the currently selected destination.
        agent.destination = points[destPoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % points.Length;
    }


    void Update()
    {
        // Choose the next destination point when the agent gets
        // close to the current one.

        float distance = Vector3.Distance(target.position, transform.position);

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            GotoNextPoint();

        if (distance <= lookRadius)
        {
            agent.isStopped = true;

            if (distance <= agent.stoppingDistance)
            {
                spaceToChat.SetActive(true);
                FaceTarget();

            }
        }

        else
            agent.isStopped = false;
    }

    public void FaceTarget()
    {
        
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
