using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenu : MonoBehaviour
{
    void Start()
    {
        
    }

    public void Pause(int pause)
    {
        Time.timeScale = pause;
    }

    public void BackToMenu()
    {
        Pause(1);
        Application.LoadLevel(1);
    }

    public void Open(GameObject obj)
    {
        obj.SetActive(true);
    }

    public void Close(GameObject obj)
    {
        obj.SetActive(false);
    }

    public void Restart(GameObject obj)
    {
        obj.SetActive(false);
        transform.GetComponent<Spawner>().Spawn();
    }
}
