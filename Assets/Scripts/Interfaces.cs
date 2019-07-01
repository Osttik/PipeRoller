using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPipe {

    void StartWatering();

    void OnCollisionStay2D(Collision2D collision);
}
