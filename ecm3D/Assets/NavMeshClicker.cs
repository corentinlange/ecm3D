using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavMeshClicker : MonoBehaviour
{
    private GameObject player;
    private NavMeshController navMeshController;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        navMeshController = player.GetComponent<NavMeshController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            bool hasHit = Physics.Raycast(ray, out hit);
            if (hasHit)
            {
                navMeshController.SetDestination(hit.point);
            }
        }
       
    }
}
