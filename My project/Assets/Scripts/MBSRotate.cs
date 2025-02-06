using UnityEngine;

public class MBSRotate : MonoBehaviour
{

    [SerializeField] float vRotate;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0,vRotate * Time.deltaTime,0));
    }
}
