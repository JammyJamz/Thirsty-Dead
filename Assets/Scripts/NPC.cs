using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class NPC : MonoBehaviour
{
    public Transform chatBackground;
    public Transform NPCCharacter;

    private DialogueSystem dialogueSystem;

    public string name;
    [TextArea(5, 10)]
    public string[] sentences;

    // Start is called before the first frame update
    void Start()
    {
        dialogueSystem = FindObjectOfType<DialogueSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 Pos = Camera.main.WorldToScreenPoint(NPCCharacter.position);

        // make a tag for the other cameras
       // if (Camera.main.enabled == false)
         //   Vector3 Pos = 
        Pos.y += 175;
        chatBackground.position = Pos;
    }

  

    public void OnTriggerStay(Collider other)
    {
        this.gameObject.GetComponent<NPC>().enabled = true;
        FindObjectOfType<DialogueSystem>().EnterRangeOfNPC();
        
        if (other.gameObject.tag == "Player")
            FindObjectOfType<DialogueSystem>().dialogueGUI.SetActive(true);

        if ((other.gameObject.tag == "Player") && Input.GetKeyDown(KeyCode.Space))
        {
            this.gameObject.GetComponent<NPC>().enabled = true;
            dialogueSystem.Names = name;
            dialogueSystem.dialogueLines = sentences;
            FindObjectOfType<DialogueSystem>().NPCName();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        FindObjectOfType<DialogueSystem>().OutOfRange();
        this.gameObject.GetComponent<NPC>().enabled = false;
        FindObjectOfType<DialogueSystem>().dialogueGUI.SetActive(false);

    }
}
