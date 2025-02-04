using Unity.AI.Navigation;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;

public class MBSNavMeshConnect : MonoBehaviour
{

    [SerializeField] NavMeshSurface navMeshMain;
    [SerializeField] float vCheckRadius;
    [SerializeField] float vCheckRadiusTmp;
    [SerializeField] NavMeshHit hit;
    [SerializeField] Vector3 vDir;
    [SerializeField] float vMoveInc;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        FnConnectNavMesh(transform, navMeshMain);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FnConnectNavMesh(Transform connect, NavMeshSurface navMesh)
    {
        NavMeshAgent agent = connect.GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            Debug.Log(connect.name + " has no Navmesh agent");
            return;

        }

        float vCheckRadiusTmp = vCheckRadius;
        


        while (!agent.isOnNavMesh)

        {
         
            Debug.Log(connect.name + " is not yet on NavMesh");

           if( NavMesh.SamplePosition(connect.position, out hit, vCheckRadiusTmp, NavMesh.AllAreas))
            {
                Debug.Log("Closest " + hit.position);

                vDir = hit.position - connect.position;

                Vector3 vDirMove = vDir.normalized * vMoveInc;

                connect.Translate(vDirMove);

            }
            else
            {

 Debug.Log("No navmesh in range");

                vCheckRadiusTmp *= 1.5f;
                Debug.Log("Range increse" + vCheckRadiusTmp);
               
            }
           

          




        }

        Debug.Log("Now on NavMesh");
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, vCheckRadiusTmp);
    }

}
