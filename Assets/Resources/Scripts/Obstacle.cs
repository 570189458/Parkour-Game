using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public int hurtValue = 1;
    public int moveSpeed = 0;
    public Vector3 moveDirection = Vector3.back;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(0, 0, -moveSpeed * Time.deltaTime);
        transform.Translate(moveSpeed * Time.deltaTime * moveDirection);
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        {
            GameAttribute._instance.life -= hurtValue;
        }
        if (other.tag != "Road" && other.tag != "MagnetCollider")
        {
            moveSpeed = 0;
        }
    }
}
