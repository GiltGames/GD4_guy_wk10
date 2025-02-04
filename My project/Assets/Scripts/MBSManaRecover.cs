using Unity.VisualScripting;
using UnityEngine;

public class MBSManaRecover : MonoBehaviour
{
    [SerializeField] MBSPlayerDemonControll mBSPlayer;
    [SerializeField] float vRecoveryStartTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mBSPlayer = FindFirstObjectByType<MBSPlayerDemonControll>();
    }

    // Update is called once per frame
    void Update()
    {
        {


            if (Input.GetMouseButton(0))
            {
                vRecoveryStartTime = Time.time + mBSPlayer.vManaRecoveryDelay;
                mBSPlayer.gManaRecovery.SetActive(false);

            }

            if (Time.time > vRecoveryStartTime)
            {


                if (mBSPlayer.vMana < mBSPlayer.vManaMax)
                {
                    mBSPlayer.gManaRecovery.SetActive(true);
                }

                else
                {
                    mBSPlayer.gManaRecovery.SetActive(false);
                }

                mBSPlayer.vMana += mBSPlayer.vManaRecovery * Time.deltaTime;

                if (mBSPlayer.vMana > mBSPlayer.vManaMax)
                {
                    mBSPlayer.vMana = mBSPlayer.vManaMax;
                    mBSPlayer.gManaRecovery.SetActive(false);
                }


            }
        }
    }
}
