using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

    public class EnemyBase : MonoBehaviour
    {
        protected NavMeshAgent nav;

        [SerializeField]
        Transform body;
        [SerializeField]
        Animator anim;

        ParticleSystem particles;

        bool isDying = false;

        //bool isWalking = false;

        float moveTimer = 0;
        float moveTimerTotal = 0;
        protected virtual void Awake()
        {
            particles = gameObject.GetComponent<ParticleSystem>();
        }
        protected virtual void Start()
        {
            nav = GetComponent<NavMeshAgent>();
            nav.SetDestination(transform.position + new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10)));
            moveTimerTotal = Random.Range(1.1f, 3.3f);

            nav.isStopped= false;
        }

        protected virtual void ResetMoveTimer()
        {
            moveTimer = 0;
            moveTimerTotal = Random.Range(1.1f, 3.3f);

        }

        protected virtual void Update()
        {

            if (!isDying)
            {
                if (moveTimer > moveTimerTotal)
                {
                    ResetMoveTimer();
                    nav.SetDestination(transform.position + new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10)));
                    nav.speed = 3;

                }
                else if (moveTimer < moveTimerTotal)
                {
                    moveTimer += Time.deltaTime;
                }
            }
            else if (isDying)
            {
                //PLAY DYING ANIMATION
                nav.isStopped = true;
                StartCoroutine(DespawnEnemyCoroutine());
                anim.SetTrigger("Dying");

                //gameObject.GetComponent<ParticleSystem>().Play();
                //ps.Play();

                particles.Play();
            
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

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                //Debug.Log("chicken colliding with player");
                isDying = true;
            }
        }

        IEnumerator DespawnEnemyCoroutine()
        {
            yield return new WaitForSeconds(2);
            Destroy(gameObject);  
        }
    }