﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Item
{
    public override void HitItem()
    {
        base.HitItem();
        GameAttribute._instance.AddCoin(1);
    }
}
