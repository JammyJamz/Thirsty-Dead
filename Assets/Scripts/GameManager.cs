using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public Text uiText;
    public float availableTime;

    public Camera fpsCam;
    public Camera overheadCam;
    public Camera thirdPersonCam;

    private float seconds;
    private float minutes;
    private float gameTime;

    private string remainingTime;

    private bool canCount = true;
    private bool doOnce = false;

    public GameObject[] customers;
    public bool[] availableSeatForCustomers;
    public Vector3[] seatPositions;
    public GameObject nextButton;			//next button loads the next available level when player beats a level

    private int delay;
    private bool canCreateNewCustomer;

    static public int totalMoneyMade;
    private int totalMoneyLost;
    static public bool gameIsFinished;

    private void Awake()
    {
        delay = 11;
        canCreateNewCustomer = false;

        for (int i = 0; i < availableSeatForCustomers.Length; i++)
            availableSeatForCustomers[i] = true;
    }
    private void Start()
    {

        ShowFPSView();
        
    }

    private List<int> freeSeatIndex = new List<int>();

    IEnumerator summonCustomer()
    {
        yield return new WaitForSeconds(2);
        canCreateNewCustomer = true;
    }
    private void Update()
    {
        StartCoroutine(summonCustomer());
        gameTime = (int)(availableTime - Time.timeSinceLevelLoad);
        seconds = Mathf.CeilToInt(availableTime - Time.timeSinceLevelLoad) % 60;
        minutes = Mathf.CeilToInt(availableTime - Time.timeSinceLevelLoad) / 60;
        remainingTime = string.Format("{0:00} : {1:00}", minutes, seconds);
       
        uiText.text = remainingTime;


        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            ShowFPSView();
        }

        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
           // print("correct number");
            ShowOverheadView();
        }

        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            ShowThirdPerson();
        }



        if (canCreateNewCustomer)
        {
            freeSeatIndex = new List<int>();
            if (availableSeats() != 0)
            {
                //  canCreateNewCustomer(freeSeatIndex[Random.Range(0, freeSeatIndex.Count)]);
                canCreateNewCustomer = false;
                StartCoroutine(reactiveCustomerCreation());
                GameObject tmpCustomer = customers[Random.Range(0, customers.Length)];
                int seats = freeSeatIndex[Random.Range(0, freeSeatIndex.Count)];
                Vector3 seat = seatPositions[seats];
                availableSeatForCustomers[seats] = false;
                int offset = -11;
                GameObject newCustomer = Instantiate(tmpCustomer, new Vector3(offset, 0.8f, 0.2f), Quaternion.Euler(0, 180, 0)) as GameObject;

                //any post creation special Attributes?
              //  newCustomer.GetComponent<CustomerController>().mySeat = _seatIndex;
                //set customer's destination
             //   newCustomer.GetComponent<CustomerController>().destination = seat;

            }
        }
    }
    IEnumerator reactiveCustomerCreation()
    {
        yield return new WaitForSeconds(delay);
        canCreateNewCustomer = true;
        yield break;
    }
    int availableSeats()
    {
        for (int i = 0; i < availableSeatForCustomers.Length; i++)
            if (availableSeatForCustomers[i] == true)
                freeSeatIndex.Add(i);
        if (freeSeatIndex.Count > 0)
            return -1;
        else
            return 0;
    }
    public void ShowFPSView()
    {
        fpsCam.enabled = true;
        overheadCam.enabled = false;
        thirdPersonCam.enabled = false;
    }

    public void ShowOverheadView()
    {
        fpsCam.enabled = false;
        overheadCam.enabled = true;
        thirdPersonCam.enabled = false;
    }

    public void ShowThirdPerson()
    {
        fpsCam.enabled = false;
        overheadCam.enabled = false;
        thirdPersonCam.enabled = true;

    }


}
