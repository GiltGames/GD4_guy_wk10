using UnityEngine;

public class FSSelectHarvester : MonoBehaviour
{
    [SerializeField] GameObject gMarker;
    [SerializeField] GameObject gActive;
    [SerializeField] FSHarvest fsHarvest;
    [SerializeField] bool vSelectTmp;
    [SerializeField] FSPlant fsPlant;
    [SerializeField] GameObject gTractorActive;

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
       // vSelectTmp = false;
        fsPlant.isReadyCommand = false;
        gTractorActive.SetActive(false);
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            gMarker.SetActive(false);
            gActive.SetActive(true);

            fsHarvest.isHarvesting = false;

          //  vSelectTmp = true;

        }

        if (Input.GetMouseButtonUp(0) )
        {
            fsHarvest.isReadyCommand = true;
         //   vSelectTmp = false;
        }

    }

    private void OnMouseExit()
    {
        gMarker.SetActive(false);

    }


}
