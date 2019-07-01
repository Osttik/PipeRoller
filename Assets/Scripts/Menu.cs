using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public Slider HeightSlider;
    public Slider LenghtSlider;
    public GameObject Data;

    private int _prevValue;

    void Start()
    {
        _prevValue = 7;
    }

    public void StartGame()
    {
        Application.LoadLevel(0);
    }

    public void Open(GameObject obj)
    {
        obj.SetActive(true);
    }

    public void Close(GameObject obj)
    {
        obj.SetActive(false);
    }

    public void Relax()
    {
        Application.Quit();
    }

    public void SetOddNumbers(Slider slider)
    {
        if (slider.value % 2 == 0)
        {
            if (slider.value > _prevValue)
                slider.value += 1;
            else
                slider.value -= 1;
            
            //if (slider.name == "LenghtSlider")
            //    HeightSlider.value = slider.value;
            //else
                LenghtSlider.value = slider.value;
        }
    }

    public void SetNumbers(Text text)
    {
        text.text = LenghtSlider.value.ToString() + "X" + LenghtSlider.value;
        print(LenghtSlider.value);
        Data.GetComponent<Data>().Height = (int)LenghtSlider.value;
        Data.GetComponent<Data>().Lenght = (int)LenghtSlider.value;
    }
}
