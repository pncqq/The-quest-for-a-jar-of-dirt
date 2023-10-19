using UnityEngine;
using TMPro;

public class CoinCounter : MonoBehaviour
{
    public static CoinCounter instance;
    public int coinCount;
    public TMP_Text coinText;

    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        coinText.text = "Coins: " + coinCount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void IncreaseCoinCount(int val)
    {
        coinCount += val;
        coinText.text = "Coins: " + coinCount.ToString();
    }
}
