using UnityEngine;

public class MBSStartScreen : MonoBehaviour
{
    [SerializeField] MBSPlayerMove mbsPlayerMove;
    [SerializeField] GameObject gStartScreen;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        Time.timeScale = 0;
        mbsPlayerMove = FindFirstObjectByType<MBSPlayerMove>();
        mbsPlayerMove.isInput = false;

    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void FnStart()
    {
        Time.timeScale = 1.0f;
        mbsPlayerMove.isInput = true;
        gStartScreen.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

}
