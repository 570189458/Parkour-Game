using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternManager : MonoBehaviour
{

    public static PatternManager _instance;

    public List<Pattern> patterns = new List<Pattern>();
    // Start is called before the first frame update
    void Start()
    {
        _instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[System.Serializable]
public class Pattern
{
    public List<PatternItem> patternItems = new List<PatternItem>();
}

[System.Serializable]
public class PatternItem
{
    public GameObject gameObject;
    public Vector3 pos;
}
