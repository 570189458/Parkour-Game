using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MangetCollider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Coin")
        {
            StartCoroutine(HitCoin(other.gameObject));
        }
    }


    IEnumerator HitCoin(GameObject coin)
    {
        bool isLoop = true;
        while(isLoop)
        {
            if(coin==null)
            {
                isLoop = false;
                continue;
            }
            coin.transform.position = Vector3.Lerp(coin.transform.position, PlayerControl._instance.transform.position, Time.deltaTime * 20);
            if(Vector3.Distance(coin.transform.position,PlayerControl._instance.transform.position)<0.5f)
            {
                coin.GetComponent<Coin>().HitItem();
                isLoop = false;
            }
            yield return null;
        }
    }
}
