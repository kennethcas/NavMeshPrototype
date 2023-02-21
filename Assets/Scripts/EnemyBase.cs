using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Ken.CharacterPack
{
    public class EnemyBase : MonoBehaviour
    {
        NavMeshAgent nav;

        [SerializeField]
        Transform body;
        [SerializeField]
        Animator anim;

        bool isDying = false;

        bool isWalking = false;

        float moveTimer = 0;
        float moveTimerTotal = 0;
        void Start()
        {
            nav = GetComponent<NavMeshAgent>();
            //ResetMoveTimer();
            nav.SetDestination(transform.position + new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10)));
            moveTimerTotal = Random.Range(1.1f, 3.3f);

            nav.isStopped= false;
        }

        void ResetMoveTimer()
        {
            moveTimer = 0;
            moveTimerTotal = Random.Range(1.1f, 3.3f);
            //nav.SetDestination(transform.position + new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10)));

        }

        void Update()
        {

            if (!isDying)
            {
                if (moveTimer > moveTimerTotal)
                {
                    ResetMoveTimer();
                    //nav.isStopped = false;
                    nav.SetDestination(transform.position + new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10)));
                    nav.speed = 3;

                }
                else if (moveTimer < moveTimerTotal)
                {
                    //ResetMoveTimer();
                    //nav.SetDestination(transform.position + new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10)));
                    moveTimer += Time.deltaTime;
                }
            }
            else if (isDying)
            {
                //PLAY DYING ANIMATION
                StartCoroutine(DespawnEnemyCoroutine());
                nav.isStopped= true;
                anim.SetTrigger("Dying");
            }

            if (nav.velocity.magnitude < 0.1f)
            {
                anim.SetBool("isWalking", false);
            }
            else
            {
                anim.SetBool("isWalking", true);
                //anim.SetFloat("Velocity", nav.velocity.magnitude);
            }

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                Debug.Log("chicken colliding with player");
                isDying = true;
            }
        }

        IEnumerator DespawnEnemyCoroutine()
        {
            yield return new WaitForSeconds(2);
            Destroy(gameObject);
            

        }
    }

}

