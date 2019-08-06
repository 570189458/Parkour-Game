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

    void RemoveItem(GameObject floor)
    {
#pragma warning disable CS0618 // 类型或成员已过时
        var item = floor.transform.FindChild("Item");
#pragma warning restore CS0618 // 类型或成员已过时
        if (item != null)
        {
            foreach (var child in item)
            {
                Transform childTranform = child as Transform;
                if (childTranform != null)
                {
                    Destroy(childTranform.gameObject);
                }
            }
        }
    }

    void AddItem(GameObject floor)
    {
#pragma warning disable CS0618 // 类型或成员已过时
        var item = floor.transform.FindChild("Item");
#pragma warning restore CS0618 // 类型或成员已过时
        if (item != null)
        {
            var patternManager = PatternManager._instance;
            if(patternManager!=null&&patternManager.patterns!=null&&patternManager.patterns.Count>0)
            {
                var pattern = patternManager.patterns[Random.Range(0, patternManager.patterns.Count)];
                if(pattern!=null&&pattern.patternItems!=null&&pattern.patternItems.Count>0)
                {
                    foreach (var patternItem in pattern.patternItems)
                    {
                        var go = Instantiate(patternItem.gameObject);
                        go.transform.parent = item;
                        go.transform.localPosition = patternItem.pos;
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z > floorOnRuning.transform.position.z+32)
        {
            RemoveItem(floorOnRuning);
            AddItem(floorOnRuning);

            floorOnRuning.transform.position = new Vector3(0, 0, floorForword.transform.position.z + 32);
            GameObject t = floorForword;
            floorForword = floorOnRuning;
            floorOnRuning = t;
        }
    }
}
