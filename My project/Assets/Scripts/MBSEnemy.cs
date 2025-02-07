using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MBSEnemy : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] float vListenDistance;
    [SerializeField] float vLookDistance;
    [SerializeField] int vState;
    [SerializeField] Transform gPlayer;
    [SerializeField] Vector3 vWalkTarget;
    [SerializeField] float vChangeInterval;
    [SerializeField] float vChangeChance;
    [SerializeField] float vNextChange;
    [SerializeField] Animator animator;
    [SerializeField] Transform gEye;
    [SerializeField] float vDistance;
    [SerializeField] float vAngle;
    [SerializeField] float vViewAngle;
    [SerializeField] RaycastHit vRaycastHit;
    [SerializeField] Vector3 vLookVector;
    [SerializeField] float vMeleeRange;
    [SerializeField] float vTimeAttackEnd;
    [SerializeField] float vTimeAttackTakes;
    [SerializeField] float vMagicAttackBuildUp;
    [SerializeField] float vMagicAttackIncreaseThreat;
    [SerializeField] float vMagicAttackThreshold;
    [SerializeField] GameObject gFirepoint;
    [SerializeField] GameObject gLine;
    [SerializeField] float vFireSummonThreshold = .75f;
    [SerializeField] float vMagicAttackTimeMulti = 1.0f;
    [SerializeField] float vMagicCastTime;
    [SerializeField] float vCastTime =1.0f;
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] Vector3 vTargetLine;
    [SerializeField] float vAttackAimError;
    [SerializeField] float vAttackAimErrorSpecific;
    [SerializeField] float vForceMagic;
    [SerializeField] float vForceMelee;
    [SerializeField] Transform gWeapon;
    public bool isHitPlayer;

    [SerializeField] float vDamageMagic;
    [SerializeField] float vDamageMelee;
    [SerializeField] float vStopThreshold;
    [SerializeField] Vector3 vShove;


    [SerializeField] Transform[] gWaypoint;



    [SerializeField] float maxHealth;
    [SerializeField] float currentHealth;
    [SerializeField] float vFade = .5f;
    [SerializeField] GameObject aura;
    [SerializeField] MBSRag3 mbsRag;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gPlayer = FindFirstObjectByType<MBSPlayerMove>().transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        lineRenderer = gLine.GetComponent<LineRenderer>();
        mbsRag = GetComponent<MBSRag3>();
        mbsRag.FnRagDollOff();
        GetComponent<Collider>().enabled = true;
        
    }

    // Update is called once per frame
    void Update()
    {

        vDistance = Vector3.Distance(gPlayer.position, transform.position);
        vAngle = Vector3.Angle(vLookVector, gEye.transform.forward);


        // checks if idle or patrol
        if (vState == 0 || vState == 1)
        {


            if (vDistance < vListenDistance)
            {
                vState = 2;

            }

            if (vDistance < vLookDistance)

            {
                Debug.Log("Loooking");

                vLookVector = gPlayer.position - gEye.position;
                if (Physics.Raycast(gEye.position, vLookVector, out vRaycastHit, vLookDistance))
                {



                    Debug.Log("Looks at " + vRaycastHit.collider.name);
                    Debug.DrawRay(gEye.position, vLookVector);

                    if (vRaycastHit.collider.tag == "Player" && vAngle < vViewAngle)
                    {

                        vState = 2;

                    }



                }
            }


            if (Time.time > vNextChange)
            {
                vNextChange = Time.time + vChangeInterval;

                if (Random.Range(0, 1f) < vChangeChance)
                {
                    if (vState == 1)
                    {
                        vState = 0;
                    }

                    if (vState == 0)
                    {
                        vState = 1;
                        agent.destination = gWaypoint[Random.Range(0, gWaypoint.Length)].transform.position;

                    }


                }



            }
        }
        //check is closing

        if (vState ==2  && vDistance < vMeleeRange)
        {
            vState = 4;
            isHitPlayer = false;
            animator.SetTrigger("Attack");
            gWeapon.gameObject.SetActive(true);
           
            vTimeAttackEnd = Time.time + vTimeAttackTakes;

        }


        if (vState == 2 && vDistance > vMeleeRange)

        {
            // increase buildup - change this
            
            vMagicAttackBuildUp += vMagicAttackIncreaseThreat * Time.deltaTime;

            if (vMagicAttackBuildUp > vMagicAttackThreshold * vFireSummonThreshold)
            {
                gFirepoint.SetActive(true);

            }

           


            if (vMagicAttackBuildUp > vMagicAttackThreshold)
            {
                vMagicAttackBuildUp = 0;
                vState = 3;
                animator.SetTrigger("Magic");

                //same time as melee -- may need to change - change the multiple at the end
                vTimeAttackEnd = Time.time + vTimeAttackTakes * vMagicAttackTimeMulti;
                   vMagicCastTime = Time.time + vCastTime; 

            }


        }

        
        switch (vState)

        {
            case 0:

                FnIdle();

            break;


            case 1:

                FnPatrol();

            break;

            case 2:

                FnClose();

            break;

            case 3:

                FnMagic();

            break;


            case 4:
                FnAttack();
            break;


                case 5:
                //dead so do nothing

                break;
        }



    }



    void FnIdle()
    {
        agent.destination = transform.position;
        animator.SetBool("Walk",false);

    }    

    void FnPatrol()
    {
        animator.SetBool("Walk", true);


            if(Vector3.Distance(transform.position, agent.destination) < vStopThreshold)
            {
                vState = 0;
                agent.destination = transform.position;


            }



    }

    void FnClose()
    {
        agent.destination = gPlayer.position;
        animator.SetBool("Walk", true);

    }

    void FnAttack()
    {
        agent.destination = transform.position;
        
        if(isHitPlayer)
        {


        }

        
        if (vTimeAttackEnd < Time.time)
        {
            animator.SetBool("Walk", true);
            vState = 2;
            gWeapon.gameObject.SetActive(true);

        }



    }

    void FnMagic()

    {
        agent.destination = transform.position;

        if (vMagicCastTime < Time.time)
        {
            gLine.SetActive(true);
            gFirepoint.SetActive(false);




            
            


            vTargetLine = gPlayer.position;
            vAttackAimErrorSpecific = vAttackAimError * Vector3.Distance(gPlayer.position, gFirepoint.transform.position);
            vTargetLine = vTargetLine + new Vector3(Random.Range(-vAttackAimErrorSpecific, vAttackAimErrorSpecific), Random.Range(-0, vAttackAimErrorSpecific), Random.Range(-vAttackAimErrorSpecific, vAttackAimErrorSpecific));

            Vector3 vAim = (vTargetLine - gFirepoint.transform.position).normalized;

            RaycastHit hit;
            if (Physics.Raycast(gFirepoint.transform.position,vAim, out hit,vLookDistance))
            {

                vTargetLine = hit.point;

                if(hit.collider.tag =="Player")

                {

                    // vShove = vAim *vForceMagic *Time.deltaTime;
                    vShove = Vector3.up * vForceMagic * Time.deltaTime;

                    gPlayer.GetComponent<Rigidbody>().isKinematic = false;
                    gPlayer.GetComponent<Rigidbody>().AddForce(vShove,ForceMode.Impulse);

                    gPlayer.GetComponent<MBSPlayerHealth>().FnPlayerDamage(Mathf.CeilToInt( vDamageMagic * Time.deltaTime));

                    Debug.Log("Ray hits player");


                }

            }



            lineRenderer.SetPosition(0, gFirepoint.transform.position);
            lineRenderer.SetPosition(1, vTargetLine);






        }


        if (vTimeAttackEnd < Time.time)
        {
            animator.SetBool("Walk", true);
            vState = 2;
            gLine.SetActive(false);
            gPlayer.GetComponent<Rigidbody>().isKinematic = true;
        }


    }



    public void Damage(int damage)
    {
        currentHealth -= damage;

        Color auraColor = Color.white;
        auraColor.a = currentHealth / maxHealth * vFade;
        auraColor.b = 0;
        auraColor.r = 1 - (currentHealth / maxHealth);
        auraColor.g = currentHealth / maxHealth;
        aura.GetComponent<Renderer>().material.color = auraColor;




        //Check if health has fallen below zero
        if (currentHealth <= 0)
        {
            //if health has fallen below zero, deactivate it 


            GetComponent<Collider>().enabled = false;
            mbsRag.FnRagDollOn();
            animator.enabled = false;
            agent.enabled = false;
            vState = 5;

        }


    }

   
}
