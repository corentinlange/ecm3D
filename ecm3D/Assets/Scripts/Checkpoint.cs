using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ActionTrigger))]
public class Checkpoint : MonoBehaviour
{   
    private void OnTriggerEnter(Collider other) {
        GetComponent<ActionTrigger>().onTriggered();
    }
}
