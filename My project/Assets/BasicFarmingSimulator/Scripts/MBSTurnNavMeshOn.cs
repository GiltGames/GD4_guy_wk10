using UnityEngine;
using UnityEngine.AI;

public class MBSTurnNavMeshOn : MonoBehaviour
{
    [SerializeField] float vDelay = 3.0f;
    [SerializeField] float vTimeOn;
    [SerializeField] bool isOn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        vTimeOn = Time.time + vDelay;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Time.time > vTimeOn && !isOn)
        {
            isOn = true;
            GetComponent<NavMeshObstacle>().enabled = true;

        }


    }
}
