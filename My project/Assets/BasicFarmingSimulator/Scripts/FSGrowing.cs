using Unity.Hierarchy;
using UnityEngine;

public class FSGrowing : MonoBehaviour
{
    [SerializeField] GameObject gTomatoSource;
    [SerializeField] GameObject gCabbageSource;
    [SerializeField] GameObject gVegNew;

    [SerializeField] GameObject gTomatoReadySource;
    [SerializeField] GameObject gCabbageReadySource;
    [SerializeField] GameObject gVegOld;
    
    public int vVegType;
    [SerializeField] float vGrowTimeTomato;
    [SerializeField] float vGrowTimeCabbage;
    public float vSproutTime=5;
    public bool isGrowing;

    [SerializeField] float vRipeTimeTomato;
    [SerializeField] float vRipeTimeCabbage;
    public float vRipeTime;
    public bool isRipe;


    [SerializeField] float vDieTimeTomato;
    [SerializeField] float vDieTimeCabbage;
    [SerializeField] float vDieTime;

    [SerializeField] int vValueTomato;
    [SerializeField] int vValueCabbage;
    public int vValueCrop;

    [SerializeField] int vCostTomato;
    [SerializeField] int vCostCabbage;

    [SerializeField] FSGameControl fsGame;


    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fsGame = FindFirstObjectByType<FSGameControl>();

        if (vVegType==0)
        {
        fsGame.FnScoreUpdate(-vCostTomato);    
        }

        if (vVegType == 1)
        {
            fsGame.FnScoreUpdate(-vCostCabbage);
        }

        FnStartGrow();

    }

    // Update is called once per frame
    void Update()
    {
        
        if (Time.time  > vSproutTime && isGrowing)
        {
            gVegNew.SetActive(true);
            

        }

        if (Time.time > vRipeTime && isGrowing)
        {
            isGrowing = false;
            gVegOld.SetActive(true);
            isRipe = true;
        }

        if (Time.time > vDieTime && isRipe)

        {
          
            FnHarvest();

        }


    }


    public void FnHarvest()
    {
        isGrowing = true;
        gVegNew.SetActive (false);
        gVegOld .SetActive (false);
        FnStartGrow ();

    }



     void FnStartGrow()

    {
        isRipe = false;
        isGrowing = true;

        if (vVegType == 0)
        {
            gVegNew = gTomatoSource;
            vSproutTime = vGrowTimeTomato + Time.time;
            vRipeTime = vRipeTimeTomato + Time.time;
            gVegOld = gTomatoReadySource;
            vDieTime = vDieTimeTomato + Time.time;
            vValueCrop = vValueTomato;


        }

        if (vVegType == 1)
        {
            gVegNew = gCabbageSource;
            vSproutTime = vGrowTimeCabbage + Time.time;
            vRipeTime = vRipeTimeCabbage + Time.time;
            gVegOld = gCabbageReadySource;
            vDieTime = vDieTimeCabbage + Time.time;
            vValueCrop = vValueCabbage;
        }

        
    }


}
