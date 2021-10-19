using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatController : MonoBehaviour
{

    [SerializeField] float flyingSpeed = 5f;
    [SerializeField] float flockingRadius = 5f;
    [SerializeField] float huntingRadius = 5f; // If the player enters this radius, start flying towards them
    [SerializeField] float huntingChaseSpeedModifier = 2f; // How many times faster are the bats when chasing the player?
   
    Vector3 nextPoint;
    bool isChasing;


    void Start()
    {
        nextPoint = Random.insideUnitSphere * flockingRadius;
        GetComponent<SphereCollider>().radius = huntingRadius;
    }

    void Update()
    {
        // Keep selecting random positions until we chase the player
        if(Vector3.Distance(nextPoint, transform.position) < 0.5f)
        {
            if(!isChasing)
            {
                nextPoint = Random.insideUnitSphere * flockingRadius;
                transform.LookAt(nextPoint, Vector3.up);
            }
            else
                Destroy(gameObject);
        }
        else
        {
            var dir = (nextPoint - transform.position).normalized;
            transform.position += flyingSpeed * dir * Time.deltaTime;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(255, 0, 0, 0.2f);
        Gizmos.DrawWireSphere(transform.position, huntingRadius * transform.localScale.x);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isChasing = true;
            nextPoint = other.transform.position;
            flyingSpeed *= huntingChaseSpeedModifier;
        }
    }
}
