using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DoorClick : MonoBehaviour
{
    [SerializeField] private DoorAnimated _door;
    private bool isInRange = false;
    [SerializeField] private TMP_Text text;

    private void Start()
    {
        text.enabled = false;
    }

    private void Update()
    {
        if (isInRange == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                _door.OpenDoorClick();
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                _door.CloseDoorClick();
            }

            text.enabled = true;
        }
        else
            text.enabled = false;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            isInRange = true;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            isInRange = false;
        }
    }
}
