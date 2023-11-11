using UnityEngine;
using TMPro;

public class CollectibleCounter : MonoBehaviour
{
    public static CollectibleCounter instance;
    public int gCoinCount;
    public int sCoinCount;
    public int dCount;
    public int kCount;
    [SerializeField] private TMP_Text coinText;

    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        coinText.text = "G. C.: " + gCoinCount.ToString() + " S. C.: " + sCoinCount.ToString() + " D: " + dCount.ToString() + " K: " + kCount.ToString();
    }

    public void IncreaseCount(int val, char type)
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
            case 'k':
                kCount += val;
                break;
        }
        coinText.text = "G. C.: " + gCoinCount.ToString() + " S. C.: " + sCoinCount.ToString() + " D: " + dCount.ToString() + " K: " + kCount.ToString();
    }

    public void UseKey()
    {
        kCount--;
        coinText.text = "G. C.: " + gCoinCount.ToString() + " S. C.: " + sCoinCount.ToString() + " D: " + dCount.ToString() + " K: " + kCount.ToString();
    }
    
}
