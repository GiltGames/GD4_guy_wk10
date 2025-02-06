using UnityEngine;
using UnityEngine.UI;

public class MBSPlayerHealth : MonoBehaviour
{
    public float vPlayerHealth;
    [SerializeField] float vPlayerMaxHealth;
    [SerializeField] Slider vHealthSlider;
    [SerializeField] GameObject gGameOverScreen;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FnPlayerDamage(int damage)
    {
        vPlayerHealth -= damage;
        vHealthSlider.value = vPlayerHealth;

        if (vPlayerHealth <= 0)
        {
            Time.timeScale = 0f;

            gGameOverScreen.SetActive(true);
        }





    }

}
