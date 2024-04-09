using UnityEngine;
using UnityEngine.AI;

public class MoveZombie : MonoBehaviour
{
    public NavMeshAgent zombie;

    public Camera playerCam;
    

    // Update is called once per frame
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
