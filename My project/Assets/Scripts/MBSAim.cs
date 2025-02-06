using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MBSAim : MonoBehaviour
{

    [SerializeField] MBSPlayerMove mbsPlayerMove;
    [SerializeField] MBSDemon mbsDemon;
    [SerializeField] Transform gDemon;
    float vMouseX;
    float vMouseY;
    [SerializeField] float vAimSensitivity=40;
    [SerializeField] RaycastHit vRaycastHit;
    [SerializeField] Vector3 vRayOrigin;
    [SerializeField] Vector3 vRayDirection; 
    public Vector3 vRayEnd;
    [SerializeField] Transform vWandEnd;
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] float vRange;
    [SerializeField] TextMeshProUGUI tAim;
    [SerializeField] TextMeshProUGUI tAim2;
    [SerializeField] Camera cCam;
    [SerializeField] Transform gTarget;
    [SerializeField] Vector3 vMouseonScreenPos;
    [SerializeField] Transform gTargetImage;
    [SerializeField] Transform  gObjectHit;
   

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mbsPlayerMove = FindAnyObjectByType<MBSPlayerMove>();
        mbsDemon =FindAnyObjectByType<MBSDemon>();
        gDemon = FindFirstObjectByType<MBSDemon>().transform;


    }

    // Update is called once per frame
    void Update()
    {
        if (mbsPlayerMove.isInput)
        {
           

            if (Input.GetMouseButtonDown(0))
            {
               
                
                //Cursor.visible = true;
                Cursor.lockState = CursorLockMode.Confined;
                lineRenderer.enabled = true;
                tAim.text = "Aim Mode";
                gTarget.gameObject.SetActive(true);
                gTargetImage.gameObject.SetActive(true);

            }
         

            if (Input.GetMouseButton(0))

            {
                
                FnAim(Input.mousePosition);



                lineRenderer.SetPosition(0, vWandEnd.position);
                lineRenderer.SetPosition(1, vRayEnd);

                mbsDemon.vTargetLocation = (vRayEnd - gDemon.position).normalized;
                







            }


           if (Input.GetMouseButtonUp(0)) 
            {
                Cursor.lockState = CursorLockMode.Locked;
               // Cursor.visible = false;
                lineRenderer.enabled = false;


                tAim.text = "";
                tAim2.text = "";
                gTarget.gameObject.SetActive(false);
                gTargetImage.gameObject.SetActive(false);
            }

        }
    }


    public Transform FnAim(Vector3 vMouseonScreenPosTmp)
    {

        
        vMouseonScreenPosTmp.z = 0.1f;


        vRayOrigin = cCam.ScreenToWorldPoint(vMouseonScreenPosTmp);

        vRayDirection = vRayOrigin - cCam.transform.position;


        if (Physics.Raycast(vRayOrigin, vRayDirection, out vRaycastHit, vRange))
        {


            vRayEnd = vRaycastHit.point;

            gTarget.gameObject.SetActive(true);
            gTarget.position = vRayEnd;
            tAim2.text = "Hit " + vRaycastHit.collider.name;

            gObjectHit = vRaycastHit.collider.transform;
            return gObjectHit;

        }
        else
        {



            vRayEnd = vRayOrigin + vRayDirection * vRange;
            gTarget.gameObject.SetActive(true);
            gTarget.position = vRayOrigin;
            tAim2.text = "";

            return null;
        }

        tAim2.text = "Mouse " + Input.mousePosition + " \n Ray Origin" + vRayOrigin + "\n Ray End" + vRayEnd;


    }

}
