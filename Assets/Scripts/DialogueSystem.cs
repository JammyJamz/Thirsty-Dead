using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogueSystem : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;
    public Text money;

    public int amountOfMoney;

    public Vector3 pickupLocation;
    public Transform pickup;

    public GameObject dialogueGUI;
    public Transform dialogueBoxGUI;

    public float letterDelay = .1f;
    public float letterMultiplier = .5f;

    public KeyCode DialogueInput = KeyCode.Space;
    public string Names;
    public string[] dialogueLines;

    public bool letterIsMult = false;
    public bool dialogueActive = false;
    public bool dialogueEnd = false;
    public bool outOfRange = true;

    // Start is called before the first frame update
    void Start()
    {
        dialogueText.text = "";
        amountOfMoney = 0;
        money.text = amountOfMoney.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnterRangeOfNPC()
    {
        outOfRange = false;
       // dialogueGUI.SetActive(true);
      //  if (dialogueActive == true)
        //    dialogueGUI.SetActive(false);
    }

    public void NPCName()
    {
        outOfRange = false;
        dialogueBoxGUI.gameObject.SetActive(true);
        nameText.text = Names;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(!dialogueActive)
            {
                dialogueActive = true;
                StartCoroutine(StartDialogue());
            }
        }
        StartDialogue();
    }

    private IEnumerator StartDialogue()
    {
        if (outOfRange == false)
        {
            int dialogueLength = dialogueLines.Length;
            int currentDialogueIndex = 0;

            while (currentDialogueIndex < dialogueLength || !letterIsMult)
            {
                if (!letterIsMult)
                {
                    letterIsMult = true;
                    StartCoroutine(DisplayString(dialogueLines[currentDialogueIndex++]));

                    if (currentDialogueIndex >= dialogueLength)
                    {
                        dialogueEnd = true;
                        Instantiate(pickup, new Vector3(pickupLocation.x, pickupLocation.y, pickupLocation.z), Quaternion.identity);
                        
                    }
                       
                }
            }
            yield return 0;
        }

        while (true)
        {
            if (Input.GetKeyDown(DialogueInput) && dialogueEnd == false)
                break;

            yield return 0;
        }

        dialogueEnd = false;
        dialogueActive = false;
        DropDialogue();
    }

    private IEnumerator DisplayString(string stringToDisplay)
    {
        if (outOfRange == false)
        {
            int stringLength = stringToDisplay.Length;
            int currentCharacterIndex = 0;

            dialogueText.text = "";

            while (currentCharacterIndex < stringLength)
            {
                dialogueText.text += stringToDisplay[currentCharacterIndex];
                currentCharacterIndex++;

                if (currentCharacterIndex < stringLength)
                {
                    if (Input.GetKey(DialogueInput))
                    {
                        yield return new WaitForSeconds(letterDelay * letterMultiplier);
                    }

                    else
                    {
                        yield return new WaitForSeconds(letterDelay);
                    }
                }

                else
                {
                    dialogueEnd = false;
                    break;
                }

            }

            while (true)
            {
                if (Input.GetKeyDown(DialogueInput))
                    break;
                yield return 0;
            }
            dialogueEnd = false;
            letterIsMult = false;
            dialogueText.text = "";
            
        }
    }

    public void DropDialogue()
    {
       // dialogueGUI.SetActive(false);
        dialogueBoxGUI.gameObject.SetActive(false);
    }

    public void OutOfRange()
    {
        outOfRange = true;
        if (outOfRange == true)
        {
            letterIsMult = false;
            dialogueActive = false;
            StopAllCoroutines();
         //   dialogueGUI.SetActive(false);
            dialogueBoxGUI.gameObject.SetActive(false);
        }
    }
}
