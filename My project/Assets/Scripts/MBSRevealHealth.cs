using UnityEngine;

public class MBSRevealHealth : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            GetComponent<Renderer>().enabled = true;

        }

        if (Input.GetKeyUp(KeyCode.Tab))
        {
            GetComponent<Renderer>().enabled = false;

        }


    }
}
