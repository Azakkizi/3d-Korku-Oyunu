using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;
using StarterAssets;

public class SanityManager : MonoBehaviour
{   //Slider referansi
    Slider SanitySlider;
    //Layer katmanina yaptigimiz postprocessing manipulasyonu dosyasi referansi
    public PostProcessProfile profile;
    //Efekt referansi
    Vignette vignette;
    //fullsanity mizi belirleyebiliriz
    public int fullSanity;
    public GameObject image1;
    public GameObject image2;
    public GameObject image3;
    public GameObject image4;
    public GameObject image5;
    public GameObject image6;
    public GameObject image7;
    public GameObject image8;
    // sanity -4 seviyeye dusunce axe bulma
    public AudioSource axeFind;
    bool isPlayedAxe;
    //sanity -3. seviyeye dusunce whisperlar duyulacak referansi burda
    public AudioSource whisper;
    public AudioSource iDontFeelGood;
    //sanity -2. seviyeye dusunce hunt duration manipulasyonu referansi
    public Animator animator;
    //sanity -1. seviyeye dusunce flashlight referans,
    public AudioSource flashlighDrop;
    public GameObject flashlight;
    // ses caldi mi kontroller.
    bool isPlayedFlash;
    // sanity +1. seviyeye gerince hareket hızı için

    float percent;
    void Start()
    {   
       //Animator init 
       //animator = GetComponent<Animator>();     
       profile.TryGetSettings(out vignette);
       SanitySlider = GetComponent<Slider>();
       SanitySlider.maxValue = fullSanity;
       SanitySlider.value = 0; 
       vignette.intensity.value = 0;
       StartCoroutine(LoseSanity(1));
       isPlayedFlash = false;
       isPlayedAxe = false; 

    }
    void Update()
    {   
        if(SanitySlider.value < 25000 && SanitySlider.value > -25000)
        {
            image1.SetActive(true);
            image2.SetActive(true);
            image3.SetActive(true);
            image4.SetActive(true);
            image5.SetActive(true);
            image6.SetActive(true);
            image7.SetActive(true);
            image8.SetActive(true);
        }
        if (SanitySlider.value >= 25000 && SanitySlider.value < 50000)
        {   
            image1.SetActive(false);
            image2.SetActive(true);
            image3.SetActive(true);
            image4.SetActive(true);
            image5.SetActive(true);
            image6.SetActive(true);
            image7.SetActive(true);
            image8.SetActive(true);
        }
        if (SanitySlider.value >= 50000 && SanitySlider.value < 75000)
        {  
            image1.SetActive(false);
            image2.SetActive(false);
            image3.SetActive(true);
            image4.SetActive(true);
            image5.SetActive(true);
            image6.SetActive(true);
            image7.SetActive(true);
            image8.SetActive(true);
            //sanity +2. seviyede huntduration dustu
            animator.GetBehaviour<PatrolState>().huntDuration = 30;
        }
        if (SanitySlider.value >= 75000 && SanitySlider.value < 99000)
        {   
            image1.SetActive(false);
            image2.SetActive(false);
            image3.SetActive(false);
            image4.SetActive(true);
            image5.SetActive(true);
            image6.SetActive(true);
            image7.SetActive(true);
            image8.SetActive(true);
        }
        if (SanitySlider.value >= 99000)
        {   
            image1.SetActive(false);
            image2.SetActive(false);
            image3.SetActive(false);
            image4.SetActive(false);
            image5.SetActive(true);
            image6.SetActive(true);
            image7.SetActive(true);
            image8.SetActive(true);
        }
        if (SanitySlider.value <= -25000)
        {   
            image5.SetActive(false);
            // monologu caldik.
            if (!flashlighDrop.isPlaying && isPlayedFlash == false)
            {
                flashlighDrop.Play();
                isPlayedFlash = true;
            }
            //flashlighti dusurduk.
            flashlight.GetComponent<Light>().spotAngle = 25;
        }
        if (SanitySlider.value <= -50000)
        {   
            image6.SetActive(false);
            //-2 skill gelince hunduration ve chaserange artti
            animator.GetBehaviour<PatrolState>().huntDuration = 50;
            animator.GetBehaviour<PatrolState>().chaseRange = 8;
        }
        if (SanitySlider.value <= -75000)
        {   
            image7.SetActive(false);
            //curselendik ve sesler duymaya basladik
            if (!whisper.isPlaying && !iDontFeelGood.isPlaying) 
            {   
                whisper.Play();
                iDontFeelGood.Play();
            }
        }
        if (SanitySlider.value <= -100000)
        {  
            image8.SetActive(false);
            //Axe'i bulup bad endingi acmamiz gerektigini anladik.
            if (!axeFind.isPlaying && isPlayedAxe == false)
            {
                axeFind.Play();
                isPlayedAxe = true;
            }
        }

        
    }
    public IEnumerator LoseSanity(int decreaseNumber)
    {
        while(SanitySlider.value >= -100000)
        {
            SanitySlider.value -= decreaseNumber;
            //akil sagligimiz 0 in altindaysa, yuzdelik olarak bilgisini alip, o oranda ekrani degistiriyor. Akil sagligimiza gore gorusumuz kotulesiyor.
            percent = (SanitySlider.value / SanitySlider.maxValue ) *-1;
            if (SanitySlider.value < 0)
            {
            vignette.intensity.value = percent;
            }
            yield return null;
        }
    }
    public IEnumerator LoseSanityFromAnother(int decreaseNumber)
    {
        while(SanitySlider.value >= -100000)
        {   for(int i =0; i < decreaseNumber/100; i++){
            SanitySlider.value -= 100;
            yield return null;
            }
            break;
        }
    }
    public IEnumerator GainSanityFromAnother(int increaseNumber)
    {
        while(SanitySlider.value <= 100000)
        {   for(int i =0; i < increaseNumber/100; i++){
            SanitySlider.value += 100;
            yield return null;
            }
            break;
        }
    }
}
