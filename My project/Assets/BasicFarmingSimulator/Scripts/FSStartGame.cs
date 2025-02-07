using UnityEngine;

public class FSStartGame : MonoBehaviour
{

    [SerializeField] GameObject startScreen;
    [SerializeField] GameObject gameUI;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        Time.timeScale = 0f;



    }


    public void FnStartGame()
    {

        startScreen.SetActive(false);
        gameUI.SetActive(true);
        Time.timeScale = 1.0f;



    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
