using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// !!Not Used!!
/// Script for testing zombie movement with NavMesh
/// </summary>
public class MoveZombie : MonoBehaviour
{
    public NavMeshAgent zombie;
    public Camera playerCam;
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hit;
            if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit))
            {
                zombie.SetDestination(hit.point);

            } 
        }
    }
}
