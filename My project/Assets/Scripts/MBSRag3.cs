using UnityEngine;

public class MBSRag3 : MonoBehaviour

{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        FnRagDollOff();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void FnRagDollOn()
    {

        foreach (Rigidbody bone in GetComponentsInChildren<Rigidbody>())
        {

            bone.isKinematic = false;

        }

    }


    public void FnRagDollOff()
    {
        foreach (Rigidbody bone in GetComponentsInChildren<Rigidbody>())
        {

            bone.isKinematic = true;

        }

    }

}

