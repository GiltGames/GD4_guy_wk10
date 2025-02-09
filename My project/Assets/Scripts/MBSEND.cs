using TMPro;
using UnityEngine;

public class MBSEND : MonoBehaviour
{

    [SerializeField] GameObject gEndScreen;

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
            FnGameEnd();

        }

    }

    void FnGameEnd()

    {
        gEndScreen.SetActive(true);
        Time.timeScale = 0f;

    }


}
