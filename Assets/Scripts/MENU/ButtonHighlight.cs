using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;


namespace MENU
{
    public class ButtonHighlight : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public TextMeshProUGUI buttonText; // Przypisz tutaj Twój tekst przycisku

        public void OnPointerEnter(PointerEventData eventData)
        {
            buttonText.text = "<u>" + buttonText.text + "</u>";
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            buttonText.text = buttonText.text.Replace("<u>", "").Replace("</u>", "");
        }
    }
}