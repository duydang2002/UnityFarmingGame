using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class ThiefController : MonoBehaviour
{
    [SerializeField] public Transform spawnPointA;   // Vị trí A
    [SerializeField] public Transform dogHouse;     // Vị trí chuồng chó
    [SerializeField] public float chanceOfTheft = 0.1f;  // Xác suất trộm (10%)
    [SerializeField] public float moveSpeed = 2f;
    [SerializeField] public float timeToRoll = 2f;
    [SerializeField] public float timeToSteal = 2f;

    private float aRoadDogHouse = -1;
    private Animator animatorMove;

    private void Awake()
    {
        animatorMove = GetComponent<Animator>();
    }

    private void Start()
    {
        transform.position = spawnPointA.transform.position;
        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        while (true)
        {
            if (aRoadDogHouse == -1)
            {
                yield return new WaitForSeconds(timeToRoll);  // Chờ 2 giây

                // Ngẫu nhiên xác suất trộm
                if (Random.value <= chanceOfTheft)
                {
                    aRoadDogHouse = -0.5f;
                }
            } else if (aRoadDogHouse == -0.5)
            {
                MoveToDogHouse();
            }else if (aRoadDogHouse == 0.5)
            {
                MoveToA();
            }else if (aRoadDogHouse == 1)
            {
                yield return new WaitForSeconds(timeToSteal);
                aRoadDogHouse = 0.5f;
            }
        }
    }

    private void MoveToDogHouse()
    {
        Vector2 direction = (Vector2)dogHouse.position - (Vector2)transform.position;
        transform.Translate(direction.normalized * moveSpeed * Time.deltaTime);

        // Check if reached the waypoint
        float distance = Vector2.Distance(transform.position, dogHouse.position);
        if (distance < 0.01f)
        {
            aRoadDogHouse = 1;
        }

        // Update animator parameters based on direction
        animatorMove.SetFloat("Horizontal", direction.x);
        animatorMove.SetFloat("Vertical", direction.y);
    }

    private void MoveToA() { }

    IEnumerator ThiefRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(20f);  // Chờ 20 giây

            // Ngẫu nhiên xác suất trộm
            if (Random.value <= chanceOfTheft)
            {
                StartTheft();
            }
        }
    }



    void StartTheft()
    {
        //// Di chuyển nhân vật trộm đến vị trí A
        //navMeshAgent.SetDestination(spawnPointA.position);

        // Khi đến vị trí A, mô phỏng hành động trộm
        StartCoroutine(PerformTheft());
    }

    IEnumerator PerformTheft()
    {
        // Chờ cho nhân vật trộm thực hiện hành động trộm (ví dụ: thực hiện animation)
        yield return new WaitForSeconds(5f);

        //// Di chuyển nhân vật trộm đến vị trí chuồng chó
        //navMeshAgent.SetDestination(dogKennel.position);

        //// Chờ cho nhân vật trộm đến vị trí chuồng chó
        //yield return new WaitUntil(() => navMeshAgent.remainingDistance < 0.1f);

        // Trộm đứng tại vị trí chuồng chó trong 5 giây
        yield return new WaitForSeconds(5f);

        //// Reset vị trí trở về vị trí A và tiếp tục vòng lặp
        //navMeshAgent.SetDestination(spawnPointA.position);
    }
}
