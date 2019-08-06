using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public float rotateSpeed;
    public GameObject hitEffect;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
    }

    public virtual void HitItem()
    {
        GameObject go = Instantiate(hitEffect);
        go.transform.parent = PlayerControl._instance.transform;
        go.transform.localPosition = new Vector3(0, 0.5f, 0);
        Destroy(gameObject);
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            HitItem();
        }
    }
}
