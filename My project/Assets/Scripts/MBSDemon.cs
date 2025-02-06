using UnityEngine;
using UnityEngine.AI;

public class MBSDemon : MonoBehaviour
{
   public int iDemonState;
    public int iDemonType;

    
   
    public float vDemonPower;
    public float vDemonPowerMax;
    public float vDemonPowerIncrease;
    [SerializeField] Transform gLeftHand;
    [SerializeField] float vNormalSize;
    [SerializeField] float vMaxSize;
    [SerializeField] float vCurrentSize;
    [SerializeField] float vSpeedBase;
    [SerializeField] float vSpeed;
    [SerializeField] NavMeshAgent navDemon;
    [SerializeField] Vector3 vHoming;
    [SerializeField] float vHomeSpeed;

   
    [SerializeField] float vBaseSpeed;
    [SerializeField] float vTopSpeedMultiple;
   
   
    [SerializeField] Rigidbody rb;
    public Vector3 vTargetLocation;
    [SerializeField] MBSAim mbsAim;
    [SerializeField] Transform vDirPoint;
    [SerializeField] SphereCollider colliderDemon;
    [SerializeField] GameObject vExpodeSource;

    [Header("Current Demon")]
    [SerializeField] Transform gCurrentDemon;
    [SerializeField] float vCurrentDamageMult;
    [SerializeField] ParticleSystem gCurrentPower;
    [SerializeField] float vDamageBase;
    [SerializeField] float vCurrentSpeedMult;
    [SerializeField] float vCurrentForceMult;
    [SerializeField] GameObject gCurrentShield;


    [Header("Force Demon")]
    [SerializeField] float vForceSpeedMult =1;
    [SerializeField] float vForceDamageMult =1;
    [SerializeField] ParticleSystem gForcePower;
    [SerializeField] float vForceForceMult =1;
    [SerializeField] Transform gForceDemon;
    [SerializeField] GameObject gForceExpode;
    [SerializeField] GameObject gForceShield;

    [Header("Fire Demon")]
    [SerializeField] float vFireDamageMult =2;
    [SerializeField] float vFireSpeedMult = 2;
    [SerializeField] float vFireForceMult = .3f;
    [SerializeField] ParticleSystem gFirePower;
    [SerializeField] Transform gFireDemon;
    [SerializeField] GameObject gFireExpode;
    [SerializeField] GameObject gFireShield;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        navDemon = GetComponent<NavMeshAgent>();
       // navDemon.isStopped = true;
      colliderDemon=GetComponent<SphereCollider>();  

    }

    // Update is called once per frame
    void Update()
    {
        
        if (iDemonType == 0)

        {
            gCurrentDemon = gForceDemon;
            gCurrentPower = gForcePower;
            
            vCurrentDamageMult = vForceDamageMult;
            vCurrentSpeedMult = vForceSpeedMult;
            vCurrentForceMult = vForceForceMult;
            
            var mainMod = gCurrentPower.GetComponent<ParticleSystem>().main;
            gFireDemon.gameObject.SetActive(false);
            gCurrentDemon.gameObject.SetActive(true);
            vExpodeSource = gForceExpode;
            gForceShield.SetActive(true);
            gFireShield.SetActive(false);
        }


        if (iDemonType == 1)
        {

            gCurrentDemon = gFireDemon;
            gCurrentPower = gFirePower;

            vCurrentDamageMult = vFireDamageMult;
            vCurrentSpeedMult = vFireSpeedMult;
            vCurrentForceMult = vFireForceMult;

            var mainMod = gCurrentPower.GetComponent<ParticleSystem>().main;
            vExpodeSource = gFireExpode;

            gForceDemon.gameObject.SetActive(false);
            gCurrentDemon.gameObject.SetActive(true);
            gForceShield.SetActive(false);
            gFireShield.SetActive(true);

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

        if(iDemonState ==3)
        {
            FnDemonState3();

        }



    }

    void FnDemonState0()

    {
        colliderDemon.enabled = false;
        // navDemon.isStopped = true;
        transform.position = gLeftHand.position;
        transform.localScale = new Vector3(vNormalSize, vNormalSize, vNormalSize);
        vDemonPower = 0;
        transform.rotation = gLeftHand.rotation;

    }

    void FnDemonState1()
    {
      
        transform.position = gLeftHand.position;
        vCurrentSize = vNormalSize + (vMaxSize - vNormalSize) * (vDemonPower / vDemonPowerMax);
        transform.localScale = new Vector3(vCurrentSize, vCurrentSize, vCurrentSize);
        var mainMod = gCurrentPower.GetComponent<ParticleSystem>().main;
        
        mainMod.startSpeed = vBaseSpeed + (vDemonPower / vDemonPowerMax) * vTopSpeedMultiple;



    }


    void FnDemonState2()
    {

        colliderDemon.enabled = true;
        vSpeed = vSpeedBase * (vDemonPower/ vDemonPowerMax) *Time.deltaTime;

        vDirPoint.localPosition = vTargetLocation;
       
        transform.rotation = Quaternion.identity;
        transform.Translate(vTargetLocation * vSpeed * vCurrentSpeedMult);

        


    }

    void FnDemonState3()
    {
        colliderDemon.enabled = false;
        vHoming = gLeftHand.transform.position - transform.position;
        vDirPoint.localPosition = vHoming.normalized;
        transform.rotation = Quaternion.identity;
        transform.Translate(vHoming.normalized * Time.deltaTime * vHomeSpeed);

        var mainMod = gCurrentPower.GetComponent<ParticleSystem>().main;

        mainMod.startSpeed = vBaseSpeed;

        if (vHoming.magnitude <.5f)
        {
            iDemonState = 0;

        }
        


    }



    void OnTriggerEnter(Collider other)
    {

        Debug.Log("hit: " + other.tag);
        if (other.tag == "Finish" && iDemonState ==3)

        {
            iDemonState = 0;


        }

        if (other.tag == "Solid")
        {
            iDemonState = 3;

            FnExplode();
        }

        if (other.tag == "Breakable")
        {
            iDemonState = 3;

            FnExplode();

            FnKnock(other.transform);

            other.GetComponent<ShootableBox>().Damage(Mathf.FloorToInt(vDemonPower * vCurrentDamageMult * vDamageBase));

        }



    }


    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collided with" + collision.transform.name);
        iDemonState = 3;

    }



    public void FnExplode()

    {
        Destroy(Instantiate(vExpodeSource, transform.position, Quaternion.identity),3);

    }

    void FnKnock(Transform other)
    {

        Rigidbody rb = other.GetComponent<Rigidbody>();

        if (rb != null)
        {

            rb.AddForce((other.position - transform.position).normalized * vDemonPower*vCurrentForceMult,ForceMode.Impulse);

        }


    }


}
