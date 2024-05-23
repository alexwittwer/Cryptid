using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class NPCMovement : MonoBehaviour
{
    public Transform playerTransform;
    public SpriteRenderer sr;
    public IAnimateSprite anim;
    public float playerOffset;
    public float speed = 1f;
    public float distanceMax = 1f;
    private bool playerInRange = false;
    public float MinMagnitudeOfAgentVelocity = 0.1f;
    public Vector3 startingPosition;
    public NavMeshAgent agent;
    public float RandomPositionTimer = 2f;
    private float randomTimer = 0f;
    public bool CanMove = true;
    public float SleepTime = 15f;
    public float sleepTimer = 0f;

    void Start()
    {
        playerTransform = playerTransform != null ? playerTransform : GameObject.FindWithTag("Player").transform;
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<IAnimateSprite>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        startingPosition = transform.position;
    }

    public void Immobilize()
    {
        CanMove = false;
        agent.stoppingDistance = 0;
        agent.velocity = Vector3.zero;
    }

    void Update()
    {
        if (!CanMove)
            return;

        if (RandomPositionTimer > 0)
        {
            randomTimer += Time.deltaTime;
            if (randomTimer >= RandomPositionTimer)
            {
                randomTimer = 0f;
                StartCoroutine(MoveToRandomPosition());
            }
        }

        CheckIfInRange();
        UpdateAnimation();
        UpdateMovement();
        Flip();
    }

    private void Move()
    {
        agent.SetDestination(playerTransform.position);
    }

    private void MoveAway()
    {
        agent.SetDestination(transform.position - playerTransform.position);
    }

    private void UpdateMovement()
    {
        if (playerInRange)
        {
            sleepTimer = 0f;
            if (gameObject.GetComponentInChildren<NPCHitbox>().Health >= 3)
                Move();
            else
                MoveAway();
        }
    }

    private void UpdateAnimation()
    {
        if (playerInRange || (Mathf.Abs(agent.velocity.magnitude) >= MinMagnitudeOfAgentVelocity))
        {
            anim.OnMove();
        }
        else
        {
            anim.OnIdle();
        }
    }


    private void CheckIfInRange()
    {
        playerInRange = Vector2.Distance(transform.position, playerTransform.position) < distanceMax;
    }

    private void Flip()
    {
        if (playerTransform.position.x < transform.position.x && !sr.flipX)
        {
            sr.flipX = true;
        }
        else if (playerTransform.position.x > transform.position.x && sr.flipX)
        {
            sr.flipX = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distanceMax);
        Gizmos.DrawLine(transform.position, playerTransform.position);
    }

    private IEnumerator MoveToRandomPosition()
    {
        yield return new WaitForSeconds(5f);
        if (!playerInRange)
        {
            Vector3 randomPositionWithinRange = startingPosition + new Vector3(Random.Range(-distanceMax, distanceMax), Random.Range(-distanceMax, distanceMax), 0);
            agent.SetDestination(randomPositionWithinRange);
        }
    }

    private IEnumerator GoToSleep()
    {
        yield return new WaitForSeconds(5f);
        if (sleepTimer >= SleepTime)
        {
            anim.OnSleep();
        }
    }
}
