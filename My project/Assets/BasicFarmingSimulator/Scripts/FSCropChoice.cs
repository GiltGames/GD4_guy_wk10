using UnityEngine;

public class FSCropChoice : MonoBehaviour
{
    [SerializeField] FSPlant fsPlant;

    public void FnTomato()
    {
        fsPlant = FindFirstObjectByType<FSPlant>();
        fsPlant.vVegType = 0;

    }

    public void FnCabbage()
    {
        fsPlant = FindFirstObjectByType<FSPlant>();
        fsPlant.vVegType = 1;

    }


}
