using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Always remember to re-bake the NavMesh Surface in order for this to work correctly
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : CharacterManager
{
    private Transform _playerTransform;
    private int _waypointIndex;
    private Vector3 _nextWaypoint;

    [Header("Enemy Settings")]
    [SerializeField] int attackPower;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private EnemyBehaviour _enemyBehaviour;
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private float attackRangeRadius = 7f;
    [SerializeField] private float _timeBetweenAttacks = 0.8f;
    private float _attackTimer;

    private enum EnemyBehaviour
    {
        Idle,
        Patrol,
        Chase
    }

    private void Awake()
    {
        _playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        _nextWaypoint = _waypoints[_waypointIndex].position;
        _agent = GetComponent<NavMeshAgent>();
    }

    private void FixedUpdate()
    {
        // Apparently the NavMesh doesn't need a gravity system
        //_characterController.Move(characterSpeed * Time.deltaTime * nextPosition);
        // ApplyGravity();

        _attackTimer += Time.deltaTime;

        if (_playerTransform == null)
            return;

        if (Vector3.Distance(transform.position, _playerTransform.position) < attackRangeRadius)
            _enemyBehaviour = EnemyBehaviour.Chase;

        BehaviourState(_enemyBehaviour);
    }

    private void BehaviourState(EnemyBehaviour enemyBehaviour)
    {
        switch (enemyBehaviour)
        {
            case EnemyBehaviour.Idle:
                _agent.SetDestination(transform.position);
                return;
            case EnemyBehaviour.Patrol:
                if (Vector3.Distance(transform.position, _nextWaypoint) < 1)
                {
                    _waypointIndex++;
                    if (_waypointIndex == _waypoints.Length) _waypointIndex = 0;

                    _nextWaypoint = _waypoints[_waypointIndex].position;
                }
                _agent.SetDestination(_nextWaypoint);
                return;
            case EnemyBehaviour.Chase:
                _agent.SetDestination(_playerTransform.position);
                if (Vector3.Distance(transform.position, _playerTransform.position) < attackRangeRadius && _attackTimer >= _timeBetweenAttacks)
                {
                    _playerTransform.gameObject.GetComponent<CharacterManager>().ReceiveDamage(attackPower);
                    _attackTimer = 0;
                }
                return;
        }
    }

    public override void ReceiveDamage(float damageAmount)
    {
        _enemyBehaviour = EnemyBehaviour.Chase;
        base.ReceiveDamage(damageAmount);
    }

    private void Attack()
    {

    }
}
