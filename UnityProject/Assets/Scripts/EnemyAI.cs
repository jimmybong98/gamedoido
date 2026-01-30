using UnityEngine;
using UnityEngine.AI;

namespace Gamedoido
{
    public class EnemyAI : MonoBehaviour
    {
        public enum AIState
        {
            Patrol,
            Chase,
            Attack,
            Flee,
            Search
        }

        public Transform target;
        public Transform[] patrolPoints;

        public float sightRange = 18f;
        public float hearingRange = 12f;
        public float attackRange = 2.2f;
        public float fleeHealthPercent = 0.2f;
        public float searchDuration = 6f;
        public float loseSightDelay = 2f;
        public float patrolWaitTime = 2f;
        public float fleeDistance = 10f;

        public LayerMask lineOfSightMask = ~0;

        private EnemyBase _enemy;
        private NavMeshAgent _agent;
        private AIState _state = AIState.Patrol;
        private int _patrolIndex;
        private float _stateEnterTime;
        private float _lastSeenTime;
        private Vector3 _lastKnownPosition;

        private void Awake()
        {
            _enemy = GetComponent<EnemyBase>();
            _agent = GetComponent<NavMeshAgent>();
        }

        private void OnEnable()
        {
            NoiseSystem.NoiseEmitted += OnNoiseHeard;
        }

        private void OnDisable()
        {
            NoiseSystem.NoiseEmitted -= OnNoiseHeard;
        }

        private void Update()
        {
            if (_enemy == null || _agent == null)
            {
                return;
            }

            if (target == null)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                if (player != null)
                {
                    target = player.transform;
                }
            }

            bool canSeeTarget = CanSeeTarget();
            float distanceToTarget = target != null ? Vector3.Distance(transform.position, target.position) : float.MaxValue;

            if (canSeeTarget)
            {
                _lastKnownPosition = target.position;
                _lastSeenTime = Time.time;
            }

            if (_enemy.HealthPercent <= fleeHealthPercent)
            {
                TransitionToState(AIState.Flee);
            }
            else if (canSeeTarget && distanceToTarget <= attackRange)
            {
                TransitionToState(AIState.Attack);
            }
            else if (canSeeTarget)
            {
                TransitionToState(AIState.Chase);
            }
            else if (Time.time - _lastSeenTime <= loseSightDelay)
            {
                TransitionToState(AIState.Search);
            }
            else if (_state == AIState.Search && Time.time - _stateEnterTime < searchDuration)
            {
                TransitionToState(AIState.Search);
            }
            else
            {
                TransitionToState(AIState.Patrol);
            }

            switch (_state)
            {
                case AIState.Patrol:
                    HandlePatrol();
                    break;
                case AIState.Chase:
                    HandleChase();
                    break;
                case AIState.Attack:
                    HandleAttack();
                    break;
                case AIState.Flee:
                    HandleFlee();
                    break;
                case AIState.Search:
                    HandleSearch();
                    break;
            }
        }

        private void TransitionToState(AIState nextState)
        {
            if (_state == nextState)
            {
                return;
            }

            _state = nextState;
            _stateEnterTime = Time.time;
        }

        private bool CanSeeTarget()
        {
            if (target == null)
            {
                return false;
            }

            float distance = Vector3.Distance(transform.position, target.position);
            if (distance > sightRange)
            {
                return false;
            }

            Vector3 origin = transform.position + Vector3.up * 1.6f;
            Vector3 direction = (target.position + Vector3.up) - origin;
            if (Physics.Raycast(origin, direction.normalized, out RaycastHit hit, sightRange, lineOfSightMask))
            {
                return hit.transform == target;
            }

            return false;
        }

        private void HandlePatrol()
        {
            if (patrolPoints == null || patrolPoints.Length == 0)
            {
                _agent.isStopped = true;
                return;
            }

            _agent.isStopped = false;
            Transform waypoint = patrolPoints[_patrolIndex];
            if (waypoint == null)
            {
                return;
            }

            _agent.SetDestination(waypoint.position);

            if (!_agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance)
            {
                if (Time.time - _stateEnterTime >= patrolWaitTime)
                {
                    _patrolIndex = (_patrolIndex + 1) % patrolPoints.Length;
                    _stateEnterTime = Time.time;
                }
            }
        }

        private void HandleChase()
        {
            if (target == null)
            {
                return;
            }

            _agent.isStopped = false;
            _agent.SetDestination(target.position);
        }

        private void HandleAttack()
        {
            if (target == null)
            {
                return;
            }

            _agent.isStopped = true;
            Vector3 lookDir = target.position - transform.position;
            lookDir.y = 0f;
            if (lookDir != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookDir), Time.deltaTime * 6f);
            }

            _enemy.TryAttack(target.gameObject);
        }

        private void HandleFlee()
        {
            if (target == null)
            {
                return;
            }

            Vector3 direction = (transform.position - target.position).normalized;
            Vector3 fleeTarget = transform.position + direction * fleeDistance;
            _agent.isStopped = false;
            _agent.SetDestination(fleeTarget);
        }

        private void HandleSearch()
        {
            _agent.isStopped = false;
            _agent.SetDestination(_lastKnownPosition);
        }

        private void OnNoiseHeard(NoiseSystem.NoiseEvent noiseEvent)
        {
            if (noiseEvent.source == gameObject)
            {
                return;
            }

            float distance = Vector3.Distance(transform.position, noiseEvent.position);
            if (distance <= hearingRange + noiseEvent.loudness)
            {
                _lastKnownPosition = noiseEvent.position;
                _lastSeenTime = Time.time;
                TransitionToState(AIState.Search);
            }
        }
    }
}
