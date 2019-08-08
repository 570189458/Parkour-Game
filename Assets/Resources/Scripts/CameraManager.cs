using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager _instance;

    public GameObject target;
    public float height;
    public float distance;

    Vector3 pos;

    bool isShaking = false;

    // Start is called before the first frame update
    void Start()
    {
        _instance = this;
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CameraShake()
    {
        if (!isShaking)
            StartCoroutine(ShakeCoroutine());
    }

    IEnumerator ShakeCoroutine()
    {
        isShaking = true;
        float time = 0.5f;
        while(time>0)
        {
            transform.position = new Vector3(
                target.transform.position.x+Random.Range(-0.1f,0.1f),
                target.transform.position.y + height,
                target.transform.position.z - distance);
            time -= Time.deltaTime;
            yield return null;
        }
        isShaking = false;
    }

    private void LateUpdate()
    {
        //pos.x = Mathf.Lerp(pos.x, target.transform.position.x, Time.deltaTime * 5);

        if (!isShaking&&GameController._instance.isPlay && !GameController._instance.isPause)
        {
            pos.x = target.transform.position.x;
            pos.y = Mathf.Lerp(pos.y, target.transform.position.y + height, Time.deltaTime * 5);
            //pos.z = Mathf.Lerp(pos.z, target.transform.position.z - distance, Time.deltaTime * 5);
            pos.z = target.transform.position.z - distance;
            transform.position = pos;
        }
    }
}
