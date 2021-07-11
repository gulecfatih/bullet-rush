using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderControl : MonoBehaviour
{
    Slider slider;

    // Start is called before the first frame update
    void Start()
    {

        slider = GetComponent<Slider>();
        
    }
    public void SliderDeger(int maxDeger, int gecerliDeger)
    {
       
        slider.maxValue = maxDeger;
        slider.value = gecerliDeger;
    }

}
