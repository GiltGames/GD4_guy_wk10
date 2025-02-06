using System.Collections;
using UnityEngine;

public class MBSWandAttack : MonoBehaviour
{
    [SerializeField] float vWandDelay=3;
    [SerializeField] MBSAim mbsAim;
    public bool isWandAttack;
    [SerializeField] Transform gTargetLightning;
    [SerializeField] Transform gTarget;
    [SerializeField] MBSDemon mbsDemon;
    [SerializeField] Transform gWandEnd;
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] float vLightningOn=.3f;
    [SerializeField] Vector3 vMouseFinal;
    [SerializeField] float vVarianceOnAttack =.1f;
    [SerializeField] float vPathVar = 0.2f;
    [SerializeField] float vFlickertime    ;
    [SerializeField] MBSFlicker mbsFlicker;
    [SerializeField] Transform gObjectHit;
    [SerializeField] int vWandDamage;
    [SerializeField] int vWandForce;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mbsAim = FindFirstObjectByType<MBSAim>();
        mbsDemon = FindFirstObjectByType<MBSDemon>();
        mbsFlicker = FindFirstObjectByType<MBSFlicker>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.LeftControl) && mbsDemon.iDemonState == 0)
        {
            StartCoroutine(FnWand());
           // Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            isWandAttack = true;
            gTarget.gameObject.SetActive(true);
            gTargetLightning.gameObject.SetActive(true);
        }

        if (isWandAttack)
        {
            mbsAim.FnAim(Input.mousePosition   );



        }



    }

    IEnumerator FnWand()
    {



        yield return new WaitForSeconds(vWandDelay);
       // Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

   
        gTarget.gameObject.SetActive(false);
        gTargetLightning.gameObject.SetActive(false);

        vMouseFinal = Input.mousePosition;
        vMouseFinal.x = vMouseFinal.x * Random.Range(1 - vVarianceOnAttack, 1 + vVarianceOnAttack);
        vMouseFinal.y = vMouseFinal.y * Random.Range(1 - vVarianceOnAttack, 1 + vVarianceOnAttack);

        gObjectHit = mbsAim.FnAim(vMouseFinal);

        Debug.Log("lightning hits " +  gObjectHit.name);

        if (gObjectHit.GetComponent<ShootableBox>() != null)
        {

            
            gObjectHit.GetComponent<Rigidbody>().AddForce((gObjectHit.position - gWandEnd.position).normalized * vWandForce, ForceMode.Impulse);

            if (gObjectHit.tag == "Enemy")
            {
                gObjectHit.GetComponent<ShootableBox>().Damage(vWandDamage);

            }

        }

isWandAttack = false;

        vFlickertime = Time.time + vLightningOn;
        float vPathVarTmp = (mbsAim.vRayEnd - gWandEnd.position).magnitude * vPathVar * vVarianceOnAttack;



        mbsFlicker.FnFlicker(vFlickertime, gWandEnd.position, mbsAim.vRayEnd, vPathVarTmp);

        


        yield return null;

    }



   

}
