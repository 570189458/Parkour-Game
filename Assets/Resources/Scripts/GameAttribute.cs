using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameAttribute : MonoBehaviour
{
    public static GameAttribute _instance;

    public int coin;

    public int multiply = 1;

    public int life = 1;
    public int init_life = 1;

    public Text Text_Coin;

    public bool SoundOn = true;

    // Start is called before the first frame update
    void Start()
    {
        _instance = this;
        coin = 0;
    }

    public void ResetAll()
    {
        life = init_life;
        coin = 0;
        multiply = 1;
    }

    // Update is called once per frame
    void Update()
    {
        Text_Coin.text = coin.ToString();
    }

    public void AddCoin(int coinNumber)
    {
        coin += multiply * coinNumber;
    }
}
