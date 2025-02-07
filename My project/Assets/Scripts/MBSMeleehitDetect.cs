using UnityEngine;

public class MBSMeleehitDetect : MonoBehaviour
{
    [SerializeField] MBSEnemy mBSEnemy;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")

        {

            Debug.Log("Melee Hit");
            mBSEnemy.isHitPlayer = true;

            gameObject.SetActive(false);




        }
    }

}
