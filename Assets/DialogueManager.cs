using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SST
{
    public class DialogueManager : MonoBehaviour
    {
        InputHandler inputHandler;

        public NPC npc;

        bool isTalking = false;

        float distance;
        float currentResponseTracker = 0;

        public GameObject player;
        public GameObject dialogueUI;

        public Text npcName;
        public Text npcDialogueBox;
        public Text playerResponse;

        // Start is called before the first frame update
        void Start()
        {
            dialogueUI.SetActive(false);
        }

        void OnMouseOver()
        {
            distance = Vector3.Distance(player.transform.position, this.transform.position);
            if (distance <= 2.5f)
            {
                if (Input.GetKeyDown(KeyCode.V) && isTalking == false)
                {
                    StartConversation();
                }
                else if (Input.GetKeyDown(KeyCode.V) && isTalking == true)
                {
                    EndDialogue();
                }
            }
            
        }

        void StartConversation()
        {
            isTalking = true;
            currentResponseTracker = 0;
            dialogueUI.SetActive(true);
            npcName.text = npc.name;
            npcDialogueBox.text = npc.dialogue[0];
        }

        void EndDialogue()
        {
            isTalking = false;
            dialogueUI.SetActive(false);
        }

        
    }
}