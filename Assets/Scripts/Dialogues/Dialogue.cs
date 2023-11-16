using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Dialogues
{
    public class Dialogue : MonoBehaviour
    {
        public GameObject window;
        public GameObject indicator;
        public List<string> dialogues;
        public TMP_Text dialogueText;
        public float writingSpeed = 0.05f;

        private int _index;
        private int _charIndex;
        private bool _started;
        private bool _waitForNext;

        private void Awake()
        {
            ToggleWindow(false);
            ToggleIndicator(false);
        }

        public void ToggleWindow(bool show)
        {
            window.SetActive(show);
        }
 
        public void ToggleIndicator(bool show)
        {
            indicator.SetActive(show);
        }
        
        public void StartDialogueWithSentences(List<string> sentences)
        {
            if (sentences != null && sentences.Count > 0)
            {
                dialogues = sentences;
                StartDialogue();
            }
            else
            {
                Debug.LogWarning("Empty or null list of sentences provided.");
            }
        }
 
        //Start dialogue
        public void StartDialogue()
        {
            if (_started)
            {
                return;
            }
            _started = true;
            ToggleWindow(true);
            ToggleIndicator(false);
            GetDialogue(0);
        }

        private void GetDialogue(int i)
        {
            _index = i;
            _charIndex = 0;
            dialogueText.text = string.Empty;
            StartCoroutine(Writing());

        }
        //End dialogue
        public void EndDialogue()
        {
            _started = false;
            _waitForNext = false;
            StopAllCoroutines();
            ToggleWindow(false);
   
        }
        //logic
        IEnumerator Writing()
        {
            yield return new WaitForSeconds(writingSpeed);
            string currentDialogue = dialogues[_index];
            dialogueText.text += currentDialogue[_charIndex];
            _charIndex++;
            if (_charIndex < currentDialogue.Length)
            {
                yield return new WaitForSeconds(writingSpeed);
                StartCoroutine(Writing());
            }
            else
            {
                _waitForNext = true;
            }
    
        }

        private void Update()
        {
            if (!_started)
            {
                return;
            }
            if (_waitForNext && Input.GetKeyDown(KeyCode.E))
            {
                _waitForNext = false;
         
                _index++;
         
                if (_index<dialogues.Count)
                {
                    GetDialogue(_index);
                }
                else
                {
                    ToggleIndicator(true);
                    EndDialogue();
                }
            }
        }
    }
}
