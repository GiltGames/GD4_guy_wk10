using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class FSHarvest : MonoBehaviour
{
    public bool isReadyCommand;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Vector3 vStart;
    [SerializeField] GameObject gActive;
    public  bool isHarvesting;
    [SerializeField] float vDetectRange;
    [SerializeField] float vGrabRange;
    [SerializeField] int vCarried;
    [SerializeField] int vCapacity;
    [SerializeField] int vValueCarried;

    [SerializeField] Vector3 gClosestPos;
    [SerializeField] float vMinDist;
    [SerializeField] GameObject vHarvestStart;
    [SerializeField] FSGameControl fsGameControl;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isReadyCommand)
        {
            FnRelocate();

        }

        if (Vector3.Distance(transform.position, vStart) < vGrabRange  && !isHarvesting)
        {

            isHarvesting = true;
        }


        if (isHarvesting)
        {
            FnHarvest();

        }



    }


    void FnRelocate()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            

            if (Physics.Raycast(ray, out hit))
            {

                vStart = hit.point;

                agent.destination = hit.point;
                isReadyCommand = false;
               
                gActive.SetActive(false);
                
            }


        }


       
    }

    void FnHarvest()
    {
      
        
        vMinDist = vDetectRange;
        gClosestPos = vHarvestStart.transform.position;

        if (vCarried < vCapacity)
        {

            FSGrowing[] fsGrowing = FindObjectsByType<FSGrowing>(FindObjectsSortMode.None);



            foreach (FSGrowing fs in fsGrowing)
            {
                if (fs.isRipe)
                {
                    float vDistTmp = Vector3.Distance(transform.position, fs.transform.position);

                    if (vDistTmp < vGrabRange)
                    {


                        fs.FnHarvest();
                        vCarried++;
                        vValueCarried += fs.vValueCrop;

                    }



                    if (vDistTmp < vMinDist)
                    {
                        vMinDist = vDistTmp;
                        gClosestPos = fs.transform.position;

                    }



                }


            }
        }
        agent.destination = gClosestPos;


        



        if (Vector3.Distance(transform.position, vHarvestStart.transform.position) < vGrabRange)

        {
            isHarvesting = false;
            

            fsGameControl.FnScoreUpdate(vValueCarried);
            vCarried = 0;
            vValueCarried = 0;
        }


    }

}
