using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(LineRenderer))]

public class NavMeshController : MonoBehaviour
{
    private NavMeshAgent myNavMeshAgent;
    private LineRenderer myLineRenderer;
    // Start is called before the first frame update
    void Start()
    {
        myNavMeshAgent = GetComponent<NavMeshAgent>();
        myLineRenderer = GetComponent<LineRenderer>();

        myLineRenderer.startWidth = 0.1f;
        myLineRenderer.endWidth = 0.1f;
        myLineRenderer.positionCount = 0;

    }

    // Update is called once per frame
    void Update()
    {

        if (Vector3.Distance(transform.position, myNavMeshAgent.destination) <= myNavMeshAgent.stoppingDistance)
        {

        }
        else if(myNavMeshAgent.hasPath)
        {
            DrawPath();
        }
        
    }

    public void SetDestination(Vector3 target)
    {
        myNavMeshAgent.SetDestination(target);
    }

    void DrawPath()
    {
        myLineRenderer.positionCount = myNavMeshAgent.path.corners.Length;
        myLineRenderer.SetPosition(0,transform.position);

        if( myNavMeshAgent.path.corners.Length < 2)
        {
            return;
        }

        for(int i = 1; i < myNavMeshAgent.path.corners.Length; i++)
        {
            Vector3 linePosition = new Vector3(0f, 3f, 0f);
            Vector3 pointPosition = myNavMeshAgent.path.corners[i] + linePosition;
            myLineRenderer.SetPosition(i, pointPosition);
        }
    }

}
