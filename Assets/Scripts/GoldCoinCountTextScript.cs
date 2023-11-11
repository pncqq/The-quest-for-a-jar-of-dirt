using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoldCoinCountTextScript : MonoBehaviour
{
    
    [SerializeField] private TMP_Text coinText;
    void Start()
    {
        coinText.text = ": " + CollectibleCounter.instance.gCoinCount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        coinText.text = ": " + CollectibleCounter.instance.gCoinCount.ToString();
    }
}