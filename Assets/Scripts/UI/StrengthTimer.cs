using System;
using TMPro;
using UnityEngine;

public class StrengthTimer : MonoBehaviour
{
    [SerializeField] private TMP_Text strengthText;
    [SerializeField] private PlayerCombat pc;
    private CanvasGroup _cg;
    private double _count;

    private void Start()
    {
        _cg = GetComponent<CanvasGroup>();
        strengthText.text = ": " + _count;
    }

    private void Update()
    {
        _count = Math.Round(pc.potionTime);

        // Zapobieganie ujemnym wartościom
        if (_count < 0)
        {
            _count = 0;
        }

        strengthText.text = ": " + _count;

        // Ukrywanie lub wyświetlanie UI w zależności od wartości count
        _cg.alpha = _count > 0 ? 1f : 0f;
        _cg.blocksRaycasts = _count > 0;
    }
}