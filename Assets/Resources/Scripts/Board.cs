using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : Obstacle
{

    public override void OnTriggerEnter(Collider other)
    {
        if(!PlayerControl._instance.isRoll)
            base.OnTriggerEnter(other);
    }
}
