using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gamecontroller : MonoBehaviour
{
    public Text HealthText;
    public GameObject EndGame;
    
    private Data _data;
    private int _health;

    private void Start()
    {
        _data = GameObject.Find("Data").GetComponent<Data>();
        _health = _data.HealthCount;
        HealthText.text = _health.ToString();
    }

    public void WinTheGame()
    {
        EndGame.transform.GetChild(0).GetComponent<Text>().text = "You Win!";
        EndGame.SetActive(true);
        EndGame.transform.GetChild(1).gameObject.SetActive(true);
        EndGame.transform.GetChild(2).gameObject.SetActive(false);
    }

    public void LoseTheGame()
    {
        EndGame.transform.GetChild(0).GetComponent<Text>().text = "You Lose!";
        EndGame.SetActive(true);
        if (_health == 0)
        {
            EndGame.transform.GetChild(1).gameObject.SetActive(true);
            EndGame.transform.GetChild(2).gameObject.SetActive(false);
        }
        else
        {
            _health -= 1;
            EndGame.transform.GetChild(2).gameObject.SetActive(true);
        }
    }

    public void LoadMenu()
    {
        Application.LoadLevel(1);
    }
}
