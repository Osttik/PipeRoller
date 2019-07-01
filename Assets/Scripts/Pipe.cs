using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour, IPipe
{
    public bool HaveWater { get; set; }
    public Gamecontroller GameContrl;

    private bool _giveWaterToNext;
    private Animator _anim;
    private float _time;

    public void OnCollisionStay2D(Collision2D collision)
    {
        Pipe col = collision.gameObject.GetComponent<Pipe>();
        EndPipe end = collision.gameObject.GetComponent<EndPipe>();
        bool nextPipe = collision != null && col != null && !col.HaveWater && _giveWaterToNext && collision.gameObject.tag != "StartPipe";
        bool endPipe = collision != null && end != null && _giveWaterToNext && collision.gameObject.tag != "StartPipe";

        if (nextPipe)
        {
            col.StartWatering();
            _giveWaterToNext = false;
        }

        if (endPipe)
        {
            end.StartWatering();
            _giveWaterToNext = false;
        }

        if (_giveWaterToNext)
        {
            StartCoroutine("LoseTimer");
        }
    }

    private void Start()
    {
        GameContrl = GameObject.FindWithTag("EditorOnly").GetComponent<Gamecontroller>();
        _time = 3.04f;
        _giveWaterToNext = false;
        _anim = transform.GetComponent<Animator>();
        _anim.enabled = false;
        HaveWater = false;
    }

    private IEnumerator LoseTimer()
    {
        yield return new WaitForSeconds(2);
        if (_giveWaterToNext == true)
            GameContrl.LoseTheGame();
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(_time);
        _giveWaterToNext = true;
    }

    public void StartWatering()
    {
        HaveWater = true;
        _anim.enabled = true;
        StartCoroutine("Timer");
    }
}
