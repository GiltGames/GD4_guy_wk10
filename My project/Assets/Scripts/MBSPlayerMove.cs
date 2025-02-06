using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class MBSPlayerMove : MonoBehaviour
{

    Camera cCamera;
    [SerializeField] Transform gEyes;
    [SerializeField] CharacterController gController;
    [SerializeField] float vTurnSideSpeed;
    [SerializeField] float vTurnSideKeySpeed;
    [SerializeField] float vTurnUpSpeed;
    [SerializeField] float vPlayerMoveSpeed;
    [SerializeField] float vJumpForce;
    [SerializeField] float vJumpSpeed;
    [SerializeField] float vJumpDecay;
    [SerializeField] float vLookUpLimit;
    [SerializeField] float vLookDirUpDown;
    [SerializeField] Vector3 vMoveDir;
    [SerializeField] Vector3 vMoveDirSide;
    [SerializeField] Vector3 vMoveDirUp;
    [SerializeField] float vVerticalVelocity;
    [SerializeField] float vDropRate;
    [SerializeField] bool isOnGround;
    [SerializeField] float vRunMultiplier;

    public bool isInput;

    // Input variables
    float vMouseX;
    float vMouseY;
    float vForward;
    float vKeyTurn;
    float vSideway;
    bool isRunKey;
    bool isJumpUp;
    bool isRunStop;


    [SerializeField] float vStamina;
    [SerializeField] float vStaminaRecovery;
    [SerializeField] float vStaminaMax;
    [SerializeField] float vStaminaRecoveryDelay;
    [SerializeField] float vStaminaTimer;
    [SerializeField] float vStaminaJumpCost;
    [SerializeField] float vStaminaRunCost;
    [SerializeField] Slider gSlider;
    [SerializeField] Slider gSliderDelay;
    [SerializeField] MBSWandAttack mbsWandAttack;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cCamera = Camera.main;
        
        isInput = true;
        gEyes = transform.Find("EyePos");
        gController = GetComponent<CharacterController>();
        mbsWandAttack = FindFirstObjectByType<MBSWandAttack>();
       

    }

    // Update is called once per frame
    void Update()
    {

        // display grounded

        isOnGround = gController.isGrounded;

        FnInput();

        FnMove();

        FnRecovery();

        FnSliderUpdate();

    }

    void FnInput()
    {
        #region input
        if (isInput)
        {
            vMouseX = Input.GetAxis("MouseX");
            vMouseY = Input.GetAxis("MouseY");

            vKeyTurn = Input.GetAxis("Horizontal");

            if (gController.isGrounded)
            {
                vForward = Input.GetAxis("Vertical");
                vSideway = Input.GetAxis("Sidestep");
                isJumpUp = Input.GetKeyDown(KeyCode.Space);
                isRunKey = Input.GetKey(KeyCode.LeftShift);
                isRunStop = Input.GetKeyUp(KeyCode.LeftShift);
            }
        }
        #endregion

        if (vStamina <= 1)
        {
            isRunKey = false;
            
        }
        if (vStamina <= vStaminaJumpCost)
        {
            isJumpUp = false;

        }


        if (isRunStop)
        {
            vStaminaTimer = vStaminaRecoveryDelay;

        }


        if (Input.GetMouseButton(0) || mbsWandAttack.isWandAttack )
        {
            // prevents mouse moving the view if either the mouse button is down, or wand attack is active

            vMouseX = 0;
            vMouseY = 0;

        }


    }

    void FnMove()
    {

        #region Rotate

        // rotate from mouse
        transform.Rotate(transform.up * Time.deltaTime * vTurnSideSpeed * vMouseX);

        // rotate frm key
        transform.Rotate(transform.up * Time.deltaTime * vTurnSideKeySpeed * vKeyTurn);



        vLookDirUpDown += vMouseY * Time.deltaTime * vTurnUpSpeed;
        Mathf.Clamp(vLookDirUpDown, -vLookUpLimit, vLookUpLimit);

        gEyes.transform.localRotation = Quaternion.Euler(vLookDirUpDown, 90, 0);



        #endregion



        #region Move

        if (!gController.isGrounded)

        {

            vVerticalVelocity = vVerticalVelocity + vDropRate;
            vMoveDirUp = new Vector3(0, vVerticalVelocity * Time.deltaTime, 0);

        }

        else
        {
            vVerticalVelocity = 0;

            if (isJumpUp )
            {
                vVerticalVelocity = vJumpForce;

                if (isRunKey)
                {
                    vVerticalVelocity *= vRunMultiplier;
                }

                vMoveDirUp = new Vector3(0, vVerticalVelocity * Time.deltaTime, 0);
                vStaminaTimer = vStaminaRecoveryDelay;
                vStamina -= vStaminaJumpCost;
            }

            vMoveDir = transform.right * vForward;
            vMoveDirSide += transform.forward * -vSideway;
            vMoveDir = vMoveDir * Time.deltaTime * vPlayerMoveSpeed;
            vMoveDirSide *= Time.deltaTime * vPlayerMoveSpeed;


            if (isRunKey)
            {
                vMoveDir *= vRunMultiplier;
                vStamina -= vStaminaRunCost * Time.deltaTime;
            }


        }



        gController.Move(vMoveDirUp + vMoveDir + vMoveDirSide);

        #endregion
    }

    void FnRecovery()
    {
       if (vStamina <0)
        {
            vStamina = 0;

        }
        
        if (vStaminaTimer <= 0)
        {

            
            vStamina += vStaminaRecovery * Time.deltaTime;
          
            if (vStamina > vStaminaMax)
            {
                vStamina = vStaminaMax;

            }


        }

        else
        {

            vStaminaTimer = vStaminaTimer - Time.deltaTime;

        }


    }

    void FnSliderUpdate()

    {
        gSlider.value = vStamina;
        gSliderDelay.value = vStaminaTimer;
    }

}
