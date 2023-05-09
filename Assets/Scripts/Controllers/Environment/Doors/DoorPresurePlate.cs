using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPresurePlate : MonoBehaviour
{
    /// <summary>
    /// Script for how the player interacts with the presure plate.
    /// </summary>
    private bool isOnPlate = false;
    private float timer;
   
    [SerializeField] private DoorAnimated _door;
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private AudioSource audioSource;
    //Audio source can be found at: https://freesound.org/people/proolsen/sounds/466272/

    private void Update()
    {
        if (isOnPlate)
        {
            _door.OpenDoorPresurePlate();    
        }
        else
        {
            //Timer for the door to close after the player steps off of the presure plate
            if (timer > 0)
            {
                timer -= Time.deltaTime;

                if (timer <= 0)
                {
                    Debug.Log("timer: " + timer);
                    _door.CloseDoorPresurePlate();
                }
            }
        }
    }
    
    //Two layers for the player and the enemy
    private void OnTriggerExit(Collider other)
    {
        if ((other.gameObject.layer == 8) || (other.gameObject.layer == 11))
        {
            isOnPlate = false;
        }      
    }


    private void OnTriggerStay(Collider other)
    {
        if  ((other.gameObject.layer == 8) || (other.gameObject.layer == 11))
        {
            isOnPlate = true;           
            timer = 1.5f;
        }
    }

    //Audio is played when the player steps on the presure plate
    private void OnTriggerEnter(Collider other)
    {
       if ((other.gameObject.layer == 8) || (other.gameObject.layer == 11))
       {
          audioSource.PlayOneShot(audioClip, 0.5f);
       }
    }

}
