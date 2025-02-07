using TMPro;
using UnityEngine;

public class FSGameControl : MonoBehaviour
{
    [SerializeField] int vScore;
    [SerializeField] TextMeshProUGUI tScore;
    [SerializeField] float vTimer;
    [SerializeField] float vTotalTime;
    [SerializeField] TextMeshProUGUI tTimer;
    [SerializeField] GameObject tGameOverScreen;
    [SerializeField] TextMeshProUGUI tFinalText;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
     vTimer = vTotalTime;
        
    }

    // Update is called once per frame
    void Update()
    {
        vTimer -= Time.deltaTime * Time.timeScale;   

        if (vTimer < 0)
        {
            FnGameOver();
                }

        tTimer.text = "Time to go: " + Mathf.RoundToInt(vTimer);

    }

    public void FnScoreUpdate(int plus)
    {
        vScore = vScore+ plus;
        tScore.text = "Money: " + vScore;

    }


    void FnGameOver()
    {
        Time.timeScale = 0f;
        tFinalText.text = "Game Over \n You made £"+vScore;
        tGameOverScreen.SetActive(true);



    }


}
