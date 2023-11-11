using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeyCountTextScript : MonoBehaviour
{
    
    [SerializeField] private TMP_Text coinText;
    void Start()
    {
        coinText.text = ": " + CollectibleCounter.instance.kCount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        coinText.text = ": " + CollectibleCounter.instance.kCount.ToString();
    }
}
