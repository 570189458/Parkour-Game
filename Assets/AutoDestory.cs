using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestory : MonoBehaviour
{
    public float DestoryTime;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, DestoryTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
