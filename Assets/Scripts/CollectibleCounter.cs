using UnityEngine;
using TMPro;

public class CollectibleCounter : MonoBehaviour
{
    public static CollectibleCounter instance;
    [SerializeField] private int gCoinCount;
    [SerializeField] private int sCoinCount;
    [SerializeField] private int dCount;
    [SerializeField] private TMP_Text coinText;

    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        coinText.text = "G. C.: " + gCoinCount.ToString() + " S. C.: " + sCoinCount.ToString() + " D: " + dCount.ToString();
    }

    public void IncreaseCoinCount(int val, char type)
    {
        switch (type)
        {
            case 'g':
                gCoinCount += val;
                break;
            case 's':
                sCoinCount += val;
                break;
            case 'd':
                dCount += val;
                break;
        }
        coinText.text = "G. C.: " + gCoinCount.ToString() + " S. C.: " + sCoinCount.ToString() + " D: " + dCount.ToString();
    }
}
