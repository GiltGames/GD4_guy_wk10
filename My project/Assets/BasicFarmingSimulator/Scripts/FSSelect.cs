using UnityEngine;

public class FSSelect : MonoBehaviour
{
    [SerializeField] GameObject gMarker;
    [SerializeField] GameObject gActive;
    [SerializeField] FSPlant fsPlant;
    [SerializeField] bool vSelectTmp;
    [SerializeField] FSHarvest fsHarvest;
    [SerializeField] GameObject gHarvestActive;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        gMarker.SetActive(true);
        
        vSelectTmp = false;
        fsHarvest.isReadyCommand = false;
        gHarvestActive.SetActive(false);
    }

    private void OnMouseOver()
    {
       
        if (Input.GetMouseButtonDown(0))
        {
            gMarker.SetActive(false);
            gActive.SetActive(true);
            vSelectTmp = true;
        
        }

        if (Input.GetMouseButtonUp(0) && vSelectTmp)
        {
            fsPlant.isReadyCommand = true;
            vSelectTmp=false;
        }

    }

    private void OnMouseExit()
    {
        gMarker.SetActive(false);
    }


}
