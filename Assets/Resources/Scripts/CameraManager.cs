using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    public GameObject target;
    public float height;
    public float distance;

    Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        //pos.x = Mathf.Lerp(pos.x, target.transform.position.x, Time.deltaTime * 5);

        if (GameController._instance.isPlay && !GameController._instance.isPause)
        {
            pos.x = target.transform.position.x;
            pos.y = Mathf.Lerp(pos.y, target.transform.position.y + height, Time.deltaTime * 5);
            //pos.z = Mathf.Lerp(pos.z, target.transform.position.z - distance, Time.deltaTime * 5);
            pos.z = target.transform.position.z - distance;
            transform.position = pos;
        }
    }
}
