using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DiamondCounter : MonoBehaviour
{
    public TextMeshProUGUI diamondLevel1;
    public TextMeshProUGUI diamondLevel2;
    public TextMeshProUGUI diamondLevel3;
    // Start is called before the first frame update
    void Awake()
    {
        DiamondsUpdate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void DiamondsUpdate()
    {
        diamondLevel1.text = PlayerPrefs.GetInt("Diamonds1") + " / 5";
        diamondLevel2.text = PlayerPrefs.GetInt("Diamonds2") + " / 5";
        diamondLevel3.text = PlayerPrefs.GetInt("Diamonds3") + " / 5";
        if (PlayerPrefs.GetInt("AllDiamonds1") == 1)
        {
            diamondLevel1.text =  "5 / 5";
        }
        if (PlayerPrefs.GetInt("AllDiamonds2") == 1)
        {
            diamondLevel2.text =  "5 / 5";
        }
        if (PlayerPrefs.GetInt("AllDiamonds3") == 1)
        {
            diamondLevel3.text =  "5 / 5";
        }
    }
}
