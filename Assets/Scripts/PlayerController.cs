using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed = 10;
    public float runSpeed;
    public float stamina = 100f;
    public float maxStamina = 100f;

    public Slider slider;
    private Rigidbody rb;

    private float haxis;
    private float vaxis;
    private float staminaRegen;

    private float staminaDecrease = 10f;
    private float StaminaIncrease = 5f;
    private float staminaTimeToRegen = 1.5f;

    private bool isRunning;
    private bool isMoving = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        runSpeed = speed * 2;

        slider.maxValue = maxStamina;
        slider.value = maxStamina;
    }

    private void FixedUpdate()
    {
        // Need to find a better way to do this instead of hard coding values.
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S))
            isMoving = true;

        else
            isMoving = false;

        haxis = Input.GetAxis("Horizontal");
        vaxis = Input.GetAxis("Vertical");
        

        Vector3 move = new Vector3(haxis, 0, vaxis) * speed * Time.deltaTime;
        rb.MovePosition(transform.position + move);
        Vector3 run;

        isRunning = Input.GetKey(KeyCode.LeftShift);
        if (isRunning && isMoving)
        {
            if (stamina == 0 || slider.value <= 0 && isRunning)
            {
                runSpeed = 10;
                slider.value = 0;
            }

            run = new Vector3(haxis, 0, vaxis) * runSpeed * Time.deltaTime;
            rb.MovePosition(transform.position + run);
            stamina = Mathf.Clamp(stamina - (staminaDecrease * Time.deltaTime), 0f, maxStamina);
            slider.value = stamina;
            staminaRegen = 0f;
        }

        else if (stamina < maxStamina)
        {

            if (staminaRegen >= staminaTimeToRegen)
            {
                stamina = Mathf.Clamp(stamina + (StaminaIncrease * Time.deltaTime), 0f, maxStamina);
                slider.value = stamina;
            }
            else
                staminaRegen += Time.deltaTime;
        }

        else if (slider.value >= maxStamina)
            slider.value = maxStamina;

        
    }

}
