using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAttribute : MonoBehaviour
{
    public static GameAttribute _instance;

    public int coinNum;

    // Start is called before the first frame update
    void Start()
    {
        _instance = this;
        coinNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
