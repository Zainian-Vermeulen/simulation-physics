using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

public enum EnemyState
{
    Idle,
    Chase
}

public class EnemyCharacterController : MonoBehaviour
{
    /// <summary>
    /// This script handles the enemy behaviour via a state machine.
    /// In this state machine, the enemy can patrol between the set amount of patrol points,
    /// Chase the player when they get in range and can be seen
    /// And also go back to its patrol points when vision of the player is lost.
    /// </summary>
    
    private float LookRadius = 12.0f;

    private Transform target;
    private NavMeshAgent agent;
    private int patrolIndex;
    public LayerMask obstaclesLayerMask;
    private EnemyState _currentState;

    private bool isWaiting;
    private bool isTravelling;
    private bool patrolWaiting;
    private bool patrolForward;
    private float waitTimer;


    [SerializeField] private float _switchProbability = 0.2f;
    [SerializeField] private float _totalWaitTime = 1.5f;
    [SerializeField] List<Waypoints> _patrolPoints;

    [SerializeField] Texture _idleTexture;
    [SerializeField] Texture _chaseTexture;
    
    private Material material;



    private void Start()
    {
        target = PlayerManager.Instance.Player.transform;
        agent = GetComponent<NavMeshAgent>();
        material = GetComponent<Renderer>().material;
    }
  
    private void FixedUpdate()
    {
        if (isTravelling && agent.remainingDistance <= 1.0f)
        {
            isTravelling = false;

            if (patrolWaiting)
            {
                isWaiting = true;
                _totalWaitTime = 0.0f;
            }
            else
            {
                ChangePatrolPoint();
                SetDestination();
            }

            if (isWaiting)
            {
                waitTimer += Time.deltaTime;

                if (waitTimer >= _totalWaitTime)
                {
                    isWaiting = false;
                    ChangePatrolPoint();
                    SetDestination();
                }
            }
        }

        Movement();
    }


    //Simple logic for the enemy for the different states it has 
    private void Movement()
    {
        switch (_currentState)
        {
            //Idle state
            case EnemyState.Idle:
            {

                float distanceToTarget = Vector3.Distance(target.position, transform.position);
                if (distanceToTarget <= LookRadius)
                {
                    _currentState = EnemyState.Chase;
                }
                else
                {
                        material.mainTexture = _idleTexture; 
                        SetDestination();
                }
               
                    break;
            }

                //Chase player state
            case EnemyState.Chase:
            {
                if (target == null)
                {
                        _currentState = EnemyState.Idle;
                        return;
                }

                float distanceToTarget = Vector3.Distance(target.position, transform.position);
                Vector3 direction = (target.position - transform.position).normalized;
               
                if ((distanceToTarget <= LookRadius) && (!Physics.Raycast(transform.position, direction, distanceToTarget, obstaclesLayerMask)))
                {
                        material.mainTexture = _chaseTexture;
                    agent.SetDestination(target.position);
                       
                      
                }
                else
                {
                    _currentState = EnemyState.Idle;
                }
                   
                if (distanceToTarget <= agent.stoppingDistance)
                {
                    FaceTarget();
                }

                    break;
            }
        }

    }

    //Move to the desired destination
    private void SetDestination()
    {
        if (_patrolPoints != null)
        {
            Vector3 patrolVector = _patrolPoints[patrolIndex].transform.position;
            agent.SetDestination(patrolVector);
            isTravelling = true;
        }
    }

    //Move between the patrol points
    private void ChangePatrolPoint() 
    {
        if(Random.Range(0.0f, 1.0f) <= _switchProbability)
        {
            patrolForward = !patrolForward;
        }

        if (patrolForward)
        {
            patrolIndex = (patrolIndex + 1) % _patrolPoints.Count;
        }
        else
        {
            if(--patrolIndex < 0)
            {
                patrolIndex = _patrolPoints.Count - 1;
            }
        }
    }

    //Face the direction of the player.
     private void FaceTarget()
     {

        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0.0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5.0f);

     }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, LookRadius);
    }

   
}
