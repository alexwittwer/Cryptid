using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class NPCRangedMovement : MonoBehaviour
{
    public Transform playerTransform;
    public Rigidbody2D rb;
    public SpriteRenderer sr;
    public IAnimateSprite anim;
    public float playerOffset;
    public float speed = 1f;
    public float distanceMax = 1f;
    private bool playerInRange = false;
    private bool playerIsClose = false;
    public bool isAwake = false;
    public float MinMagnitudeOfAgentVelocity = 0.01f;
    public Vector3 startingPosition;
    public NavMeshAgent agent;
    public float SleepTime = 15f;
    public float timer = 0f;
    public bool CanMove = true;

    void Start()
    {
        playerTransform = playerTransform != null ? playerTransform : GameObject.FindWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
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
        // put agent to dead stop
        agent.stoppingDistance = 0;
        agent.velocity = Vector3.zero;
    }

    void Update()
    {
        if (!CanMove)
        {
            return;
        }

        playerInRange = CheckIfInRange();
        playerIsClose = CheckIfClose();

        Flip();

        if (playerInRange && !playerIsClose)
        {
            MoveIn();
            if (!isAwake)
            {
                isAwake = true;
                anim.OnWake();
                return;
            }
            anim.OnMove();
        }
        else if (Mathf.Abs(agent.velocity.magnitude) >= MinMagnitudeOfAgentVelocity)
        {
            anim.OnMove();
        }
        else if (isAwake)
        {
            if (Mathf.Abs(agent.velocity.magnitude) >= MinMagnitudeOfAgentVelocity)
            {
                StartCoroutine(MoveToRandomPosition());
            }
            else
            {
                StartCoroutine(GoToSleep());
            }
        }
        else
        {
            anim.OnIdle();
        }

        if (playerIsClose)
        {
            MoveAway();
        }

        if (isAwake)
        {
            timer += Time.deltaTime;
            if (timer >= SleepTime)
            {
                isAwake = false;
                timer = 0f;
            }
        }
    }

    private void MoveIn()
    {
        agent.SetDestination(playerTransform.position);
    }

    private bool CheckIfInRange()
    {
        float distance = Vector2.Distance(transform.position, playerTransform.position);
        return distance < distanceMax;
    }

    private bool CheckIfClose()
    {
        float distance = Vector2.Distance(transform.position, playerTransform.position);
        return distance < playerOffset;
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
        // Moves to a random position from the starting position within a range
        Vector3 randomPositionWithinRange = startingPosition + new Vector3(Random.Range(-distanceMax, distanceMax), Random.Range(-distanceMax, distanceMax), 0);
        agent.SetDestination(randomPositionWithinRange);
        yield return new WaitForSeconds(5f);
    }

    private IEnumerator GoToSleep()
    {
        yield return new WaitForSeconds(5f);
        if (timer >= SleepTime)
        {
            anim.OnIdle();
        }
    }

    private void MoveAway()
    {
        Vector3 direction = transform.position - playerTransform.position;
        agent.SetDestination(transform.position + direction);
    }
}
