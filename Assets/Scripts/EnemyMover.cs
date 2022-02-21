using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float chaseRange = 7.0f;


    private NavMeshAgent navMeshAgent;
    private float distanceToTargetSqr = Mathf.Infinity;
    private Vector3 startPos;


    private bool isProvoked = false;


    // Start is called before the first frame update
    void Start()
    {
        this.navMeshAgent = this.gameObject.GetComponent<NavMeshAgent>();

        this.startPos = this.gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        this.distanceToTargetSqr = (this.target.transform.position - this.gameObject.transform.position).sqrMagnitude;

        if (this.isProvoked)
        {
            EngageTarget();
            return;
        }
        else if(this.distanceToTargetSqr <= (this.chaseRange * this.chaseRange))
        {
            this.isProvoked = true;
            this.navMeshAgent.SetDestination(this.target.position);
        }
        else if (!Vector3.Equals(this.distanceToTargetSqr, this.startPos))
        {
            this.isProvoked = false;
            this.navMeshAgent.SetDestination(this.startPos);
        }
    }

    private void EngageTarget()
    {
        if(this.distanceToTargetSqr > (this.navMeshAgent.stoppingDistance*this.navMeshAgent.stoppingDistance))
        {
            this.navMeshAgent.SetDestination(this.target.position);
        }
        else
        {
            Debug.Log("Attack!!! RAAAARGGHHHH!!!");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.gameObject.transform.position, this.chaseRange);
    }
}
