using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Always remember to re-bake the NavMesh Surface in order for this to work correctly
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : CharacterManager
{
    private Transform playerTransform;
    private int waypointIndex;
    private Vector3 nextWaypoint;

    [Header("Behaviour Settings")]
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private EnemyBehaviour enemyBehaviour;
    [SerializeField] private Transform[] waypoints;

    private enum EnemyBehaviour
    {
        Idle,
        Patrol,
        Chase
    }

    private void Awake()
    {
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        nextWaypoint = waypoints[waypointIndex].position;
    }

    private void FixedUpdate()
    {
        // Apparently the NavMesh doesn't need a gravity system
        //_characterController.Move(characterSpeed * Time.deltaTime * nextPosition);
        // ApplyGravity();

        BehaviourState(enemyBehaviour);
    }

    private void BehaviourState(EnemyBehaviour enemyBehaviour)
    {
        switch (enemyBehaviour)
        {
            case EnemyBehaviour.Idle:
                agent.SetDestination(transform.position);
                return;
            case EnemyBehaviour.Patrol:
                if (Vector3.Distance(transform.position, nextWaypoint) < 1)
                {
                    waypointIndex++;
                    if (waypointIndex == waypoints.Length) waypointIndex = 0;

                    nextWaypoint = waypoints[waypointIndex].position;
                }
                agent.SetDestination(nextWaypoint);
                return;
            case EnemyBehaviour.Chase:
                agent.SetDestination(playerTransform.position);
                return;
        }
    }
}
