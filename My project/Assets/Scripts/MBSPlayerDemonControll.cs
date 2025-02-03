using UnityEngine;

public class MBSPlayerDemonControll : MonoBehaviour
{
    [SerializeField] MBSDemon mbsDemon;
    [SerializeField] bool isMouseDown;
    [SerializeField] bool isClickFrame;
    [SerializeField] bool isMouseUp;
    [SerializeField] float vMouseX;
    [SerializeField] float vMouseY;
    [SerializeField] Transform gDemon;
    [SerializeField] float vRotateX;
    [SerializeField] float vRotateY;
    [SerializeField] float vRotateSensitivity;

    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mbsDemon = FindFirstObjectByType<MBSDemon>();
        gDemon = FindFirstObjectByType<MBSDemon>().transform;
    }

    // Update is called once per frame
    void Update()
    {
    
        isClickFrame = Input.GetMouseButtonDown(0);
        isMouseDown = Input.GetMouseButton(0);
        isMouseUp = Input.GetMouseButtonUp(0);
        vMouseX = Input.GetAxis("MouseX");
        vMouseY = Input.GetAxis("MouseY");




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

            }

            vRotateY = vMouseX * vRotateSensitivity *Time.deltaTime;
            vRotateX = vMouseY * vRotateSensitivity * Time.deltaTime;

            gDemon.Rotate(vRotateX, vRotateY, 0);



        }




    }



}
