using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    private GameObject target = null;
    private Vector3 offset;
    public List<Transform> waypoints;
    public GameObject WorldConstraint;
    public float Speed;
    public int waypointTarget;

    private void Start()
    {
        target = null;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, waypoints[waypointTarget].position, Speed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        if (transform.position == waypoints[waypointTarget].position)
        {
            if (waypointTarget == waypoints.Count - 1)
            {
                waypointTarget = 0;
            }
            else
            {
                waypointTarget += 1;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        target = collision.gameObject;
        offset = target.transform.position - transform.position;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        target = null;
    }

    private void LateUpdate()
    {
        if (target != null)
        {
            target.transform.position = transform.position + offset;
        }
    }

    private void OnDrawGizmo()
    {
        
    }
}
