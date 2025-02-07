using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class FSPlant : MonoBehaviour
{
    public bool isReadyCommand;
    [SerializeField] Vector3 vStart;
    [SerializeField] Vector3 vEnd;
    [SerializeField] Vector3 vEndTmp;
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] GameObject gActive;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] bool isGoingToPlant;
    [SerializeField] bool isPlanting;
    [SerializeField] float vTolerance =0.1f;
    [SerializeField] float vPlantSeparation = 1f;
    [SerializeField] Vector3 vLastPlantPosition;
    [SerializeField] Transform gTractorStart;
    [SerializeField] GameObject gPloughedLandSource;
    [SerializeField] bool isGroundPloughed;
    [SerializeField] Transform gPloughParent;
    public int vVegType;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        gTractorStart = GameObject.FindWithTag("TStart").transform;

    }

    // Update is called once per frame
    void Update()
    {
        
        if (isReadyCommand)
        {
            FnPloughIns();

        }

        if (isGoingToPlant)
        {
            FnMovetoPlant();
        }


        if (isPlanting)
        {

            FnPlanting();

        }


    }


    void FnPloughIns()
    {

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                vStart = hit.point;

                lineRenderer.enabled = true;
                lineRenderer.SetPosition(0, vStart);
            }


        }

        if (Input.GetMouseButton(1))
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                vEndTmp = hit.point;


            }

            lineRenderer.SetPosition(1, vEndTmp);

        }




        if (Input.GetMouseButtonUp(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                vEnd = hit.point;

                lineRenderer.enabled = false;

                isReadyCommand = false;
                gActive.SetActive(false);

                isGoingToPlant = true;
                agent.destination = vStart;


            }

            

        }

    }

    void FnMovetoPlant()
    {

        if (Vector3.Distance(vStart,transform.position) < vTolerance)
        {
            isGoingToPlant = false;
            isPlanting = true;
            agent.destination = vEnd;
            vLastPlantPosition = vStart;

        }



    }


    void FnPlanting()
    {

        if (Vector3.Distance(vEnd, transform.position) > vTolerance)
        {
            
            if (Vector3.Distance(vLastPlantPosition, transform.position) > vPlantSeparation)

            {
                GameObject gPlanted = Instantiate(gPloughedLandSource,transform.position, transform.rotation, gPloughParent);

                gPlanted.GetComponent<FSGrowing>().isGrowing = true;
                gPlanted.GetComponent<FSGrowing>().vVegType = vVegType;

                vLastPlantPosition = transform.position;


            }




        }

        else
        {
            isPlanting = false;
            agent.destination = gTractorStart.position;
        }

    }



}
