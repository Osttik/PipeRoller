using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPipe : MonoBehaviour, IPipe
{
    private float _time;
    private Animator _anim;
    private bool _giveWaterToNext;

    public Gamecontroller GameContrl;

    public void OnCollisionStay2D(Collision2D collision)
    {
        Pipe col = collision.gameObject.GetComponent<Pipe>();

        bool nextPipe = collision != null && _giveWaterToNext && collision.gameObject.tag == "Pipe";

        
        if (nextPipe)
        {
            col.StartWatering();
            _giveWaterToNext = false;
        }

        if (_giveWaterToNext)
        {
            StartCoroutine("LoseTimer");
        }
    }

    void Start()
    {
        GameContrl = GameObject.FindWithTag("EditorOnly").GetComponent<Gamecontroller>();
        _time = 3.03f;
        _anim = transform.GetComponent<Animator>();
        _anim.enabled = false;
        _giveWaterToNext = false;
        StartWatering();
    }

    public void StartWatering()
    {
        StartCoroutine("Timer");
    }

    private IEnumerator LoseTimer()
    {
        yield return new WaitForSeconds(2);
        if (_giveWaterToNext == true)
            GameContrl.LoseTheGame();
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(10);
        _anim.enabled = true;
        yield return new WaitForSeconds(_time);
        _giveWaterToNext = true;
        print("Start");
    }
}
