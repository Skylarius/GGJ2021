using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform target, library, player;
    private NavMeshAgent agent;
    public float rotationSpeed = 10f;
    public Vector3 defaultLocalScale;
    public Animator animator;

    void Start () {
        agent = GetComponent<NavMeshAgent>();
        target = player;
        defaultLocalScale = transform.localScale;
        StartCoroutine("SetRandomTarget");
        gameObject.tag = "Thief";
    }

    // Update is called once per frame
    void Update()
    {
        if (!target) {
            return;
        }
        agent.destination = target.position;
        if (target.position.x - transform.position.x > 0) {
            transform.localScale  = -1 * Vector3.right * defaultLocalScale.x + Vector3.up * defaultLocalScale.y + Vector3.forward * defaultLocalScale.z;
        }
        else {
            transform.localScale = defaultLocalScale;
        }
    }

    IEnumerator SetRandomTarget() {
        while (true)
        {
            int chance = Random.Range(0, 10);
            if (chance < 2) {
                print("Going to the library!");
                target = library;
                //Go to the library and STEAL!
                while (Vector3.Magnitude(transform.position - library.position) > 2) {
                    yield return new WaitForSeconds(1f);
                }
                library.SendMessage("StealFragment");
                target = player;
            }
            yield return new WaitForSeconds(5f);
        }
    }

    void Die() {
        EnemiesGenerator.enemiesCounter -=1;
        StartCoroutine("DieCoroutine");
    }

    private void OnTriggerStay(Collider other) {
        if (other.gameObject == player.gameObject) {
            other.gameObject.SendMessage("BeHit", Vector3.Normalize(player.transform.position - transform.position));
        }
    }

    IEnumerator DieCoroutine() {
        GetComponent<BoxCollider>().enabled = false;
        StopCoroutine("SetRandomTarget");
        target = null;
        animator.SetBool("explosion", true);
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }


}
