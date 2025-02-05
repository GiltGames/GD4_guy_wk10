using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class MBSPlayerDemonControll : MonoBehaviour
{
    [SerializeField] MBSDemon mbsDemon;
    public bool isMouseDown;
    [SerializeField] bool isClickFrame;
    [SerializeField] bool isMouseUp;
    [SerializeField] float vMouseX;
    [SerializeField] float vMouseY;
    [SerializeField] bool isMouseWheelPress;
    [SerializeField] Transform gDemon;
    [SerializeField] float vRotateX;
    [SerializeField] float vRotateY;
    [SerializeField] float vRotateSensitivity;
    public float vMana;
   public float vManaMax;
    [SerializeField] float vManaUsed;
    public float vManaRecovery;
    public float vManaRecoveryDelay;
   public GameObject gManaRecovery;
    [SerializeField] TextMeshProUGUI tMana;
   public bool isRecover;
    public float vRecoveryStartTime;
    [SerializeField] Slider sMana;
    [SerializeField] int vNoofDemons =2 ;

    [SerializeField] MBSWandAttack mbsWandAttack;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mbsDemon = FindFirstObjectByType<MBSDemon>();
        gDemon = FindFirstObjectByType<MBSDemon>().transform;
        mbsWandAttack = FindFirstObjectByType<MBSWandAttack>();

        

    }

    // Update is called once per frame
    void Update()
    {
        tMana.text = "Mana: "+vMana;
        sMana.value = vMana;

        isClickFrame = Input.GetMouseButtonDown(0);
        isMouseDown = Input.GetMouseButton(0);
        isMouseUp = Input.GetMouseButtonUp(0);
        vMouseX = Input.GetAxis("MouseX");
        vMouseY = Input.GetAxis("MouseY");
        isMouseWheelPress = Input.GetMouseButtonDown(2);



        if (isMouseWheelPress && mbsDemon.iDemonState ==0)
        {
            mbsDemon.iDemonType +=1;

            if (mbsDemon.iDemonType == vNoofDemons)
            {
                mbsDemon.iDemonType = 0;
            }



        }




        if (mbsDemon.iDemonState == 0 )
        {
            if (isClickFrame)
            {
                mbsDemon.iDemonState = 1;

                




            }


           

        }

        if (mbsDemon.iDemonState ==1)
        {
            
            if (isMouseDown)
            {
                mbsDemon.vDemonPower += mbsDemon.vDemonPowerIncrease * Time.deltaTime;

                if (mbsDemon.vDemonPower > mbsDemon.vDemonPowerMax)
                {
                    mbsDemon.vDemonPower = mbsDemon.vDemonPowerMax;
                }

                vMana -= vManaUsed*Time.deltaTime;
                if (vMana <= 0)
                {
                    vMana = 0;
                    mbsDemon.iDemonState = 0;
                    mbsDemon.FnExplode();
                    isRecover = true;
                }

            }
            if (isMouseUp)
            {
                mbsDemon.iDemonState = 2;

            }


        }

        if (mbsDemon.iDemonState == 2)
        {
            if (isMouseDown)
            {

                mbsDemon.iDemonState = 3;
                isRecover = true;

            }

        /*    vRotateY = vMouseX * vRotateSensitivity *Time.deltaTime;
            vRotateX = vMouseY * vRotateSensitivity * Time.deltaTime;

            gDemon.Rotate(0, vRotateY, vRotateX);

            */

        }




    }

   




}
