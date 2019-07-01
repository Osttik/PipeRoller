using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPipe : MonoBehaviour, IPipe
{
    public Gamecontroller GameContrl;

    private float _time;
    private Animator _anim;

    private void Start()
    {
        GameContrl = GameObject.FindWithTag("EditorOnly").GetComponent<Gamecontroller>();
        _time = 3.03f;
        _anim = transform.GetComponent<Animator>();
        _anim.enabled = false;
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(_time);
        GameContrl.WinTheGame();
    }

    public void StartWatering()
    {
        _anim.enabled = true;

        StartCoroutine("Timer");
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
    }
}
