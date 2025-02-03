using UnityEngine;

public class MBSDemon : MonoBehaviour
{
   public int iDemonState;
    public int iDemonType;

    [SerializeField] Transform gForceDemon;
    [SerializeField] Transform gCurrentDemon;
    public float vDemonPower;
    [SerializeField] float vDemonPowerMax;
    public float vDemonPowerIncrease;
    [SerializeField] Transform gLeftHand;
    [SerializeField] float vNormalSize;
    [SerializeField] float vMaxSize;
    [SerializeField] float vCurrentSize;
    [SerializeField] float vSpeedBase;
    [SerializeField] float vSpeed;
    


    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (iDemonType == 0)

        {
            gCurrentDemon = gForceDemon;


        }


        // Demon states
        // 0 - neutral
        // 1 -powering up
        // 2 - 

        if (iDemonState == 0)
        {
            FnDemonState0();
        }

        if (iDemonState ==1)
        {
            FnDemonState1();

        }

        if (iDemonState ==2)
        {
            FnDemonState2();

        }


    }

    void FnDemonState0()

    {
        transform.position = gLeftHand.position;
        transform.localScale = new Vector3(vNormalSize, vNormalSize, vNormalSize);

    }

    void FnDemonState1()
    {
        transform.position = gLeftHand.position;
        vCurrentSize = vNormalSize + (vMaxSize - vNormalSize) * (vDemonPower / vDemonPowerMax);
        transform.localScale = new Vector3(vCurrentSize, vCurrentSize, vCurrentSize);   



    }


    void FnDemonState2()
    {
        //vSpeed = 

       // transform.Translate()



    }

}
