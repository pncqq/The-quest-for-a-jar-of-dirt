using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StrengthTimer : MonoBehaviour
{
    [SerializeField] private TMP_Text strengthText;
    private CanvasGroup cg;
    [SerializeField] private PlayerCombat pc;

    private double count;
    // Start is called before the first frame update
    void Start()
    {
        cg = GetComponent<CanvasGroup>();
        count = Math.Round(pc.potionTime - Time.time);
        strengthText.text = ": " + count;
    }

    // Update is called once per frame
    void Update()
    {
        count = Math.Round(pc.potionTime - Time.time);
        strengthText.text = ": " + count;
        if (count <= 0)
        {
            cg.alpha = 0f;
            cg.blocksRaycasts = false;
        }
        else
        {
            cg.alpha = 1f;
            cg.blocksRaycasts = true;
        }
            

    }
}
