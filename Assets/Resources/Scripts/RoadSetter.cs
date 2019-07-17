using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadSetter : MonoBehaviour
{

    public GameObject floorOnRuning;
    public GameObject floorForword;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z > floorOnRuning.transform.position.z+32)
        {
            floorOnRuning.transform.position = new Vector3(0, 0, floorForword.transform.position.z + 32);
            GameObject t = floorForword;
            floorForword = floorOnRuning;
            floorOnRuning = t;
        }
    }
}
