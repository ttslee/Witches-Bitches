using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    public List<Transform> players;
    public float moveSpd = 1f;
    public float stoppingDistance;
    public bool isWandering = false;
    public Animator animator;
    public Transform moveSpot;
    private float wait;
    public float waitTime = 2f;
    public float minX = -9.5f;
    public float maxX = 9.5f;
    public float minY = -12.5f;
    public float maxY = 6.5f;


    private void Start()
    {
        wait = waitTime;
        moveSpot.position = CreateMoveSpot();
    }

    void Update()
    {
        if (isWandering)
        {
            Wander();
        } else if (players.Count == 0) return;
        else Move();
    }

    void Wander()
    {
        if (transform.position.x > moveSpot.position.x)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else if (transform.position.x < moveSpot.position.x)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        transform.position = Vector2.MoveTowards(transform.position, moveSpot.position, moveSpd * Time.deltaTime);
        if (Vector2.Distance(transform.position, moveSpot.position) < 0.2f)
        {
            if (wait <= 0)
            {
                moveSpot.position = CreateMoveSpot();
                wait = waitTime;
                animator.SetFloat("Speed", 1f);
            } else
            {
                wait -= Time.deltaTime;
                animator.SetFloat("Speed", 0f);
            }
        }

    }

    void Move()
    {
        Transform target = GetClosestTarget(players[0], players[1]);
        if (Vector2.Distance(transform.position, target.position) > stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpd * Time.deltaTime);
        } //else shoot projectile or something
    }

    Transform GetClosestTarget(Transform object1, Transform object2)
    {
        float target1 = GetDistance(gameObject.transform, object1);
        float target2 = GetDistance(gameObject.transform, object2);
        if (target1 <= target2) return object1;
        else return object2;
    }

    float GetDistance(Transform object1, Transform object2)
    {
        return Vector2.Distance(object1.transform.position, object2.transform.position); 
    }

    Vector2 CreateMoveSpot()
    {
        return new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
    }


}
