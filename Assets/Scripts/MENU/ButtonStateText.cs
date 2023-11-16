using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Jeśli używasz TextMeshPro

namespace MENU
{
    public class ButtonStateText : MonoBehaviour
    {
        public Button button;
        public TextMeshProUGUI buttonText;

        private void Update()
        {
            if (button.interactable)
            {
                // Przywróć normalny tekst, gdy przycisk jest aktywny
                buttonText.text = buttonText.text; // użyj oryginalnego tekstu
                buttonText.color = Color.white; // lub inny kolor aktywny
            }
            else
            {
                // Przekreśl i wyszarz tekst, gdy przycisk jest nieaktywny
                buttonText.text = "<s>" + buttonText.text + "</s>";
                buttonText.color = Color.gray; // lub inny kolor dla nieaktywnego stanu
            }
        }
    }
}

