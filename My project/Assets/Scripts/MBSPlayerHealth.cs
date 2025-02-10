using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MBSPlayerHealth : MonoBehaviour
{
    public float vPlayerHealth;
    [SerializeField] float vPlayerMaxHealth;
    [SerializeField] Slider vHealthSlider;
    [SerializeField] GameObject gGameOverScreen;
    [SerializeField] MBSDemon mbsDemon;
    [SerializeField] GameObject gHitScreen;
    [SerializeField] Image gHitImage;
    [SerializeField] float vFadeRate = 0.1f;
    [SerializeField] float vHitA;
    [SerializeField] MBSFade mbsFade;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mbsDemon = FindFirstObjectByType<MBSDemon>();
        mbsFade = FindFirstObjectByType<MBSFade>();

    }

    // Update is called once per frame
    void Update()
    {
       // redundant screen effect
        
        /*
        if (vHitA > 0)
        {
            vHitA -= vFadeRate * Time.deltaTime;

            Color coltmp = gHitImage.color;
            coltmp.a = vHitA;

            gHitImage.color = coltmp;


        }

        else
        {
            gHitScreen.SetActive(false);
            

        }
        */
    }

    public void FnPlayerDamage(float damage,int type)
    {
        if (mbsDemon.isShield && type == mbsDemon.iDemonType)
        {
            //visul effect if shield is up


        }
        else
        {

            vPlayerHealth -= damage;

            mbsFade.FadeStart((0.4f + damage / vPlayerMaxHealth *.6f));

            vHealthSlider.value = vPlayerHealth;

            if (vPlayerHealth <= 0)
            {
                Time.timeScale = 0f;

                gGameOverScreen.SetActive(true);
            }

            // redudant damage effect


            // gHitScreen.SetActive(true);
            // vHitA = .3f + damage / 100;
           


        }



    }

   

}
