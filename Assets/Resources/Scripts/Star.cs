﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : Item
{
    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if(other.tag=="Player")
        {
            PlayerControl._instance.QuickMove();
        }
    }
}
