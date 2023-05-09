using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimated : MonoBehaviour
{
    /// <summary>
    /// Script to change the state of each door when interacted with.
    /// </summary>
    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void OpenDoorClick()
    {
        this.animator.SetBool("Open", true);
    }

    public void CloseDoorClick()
    {
        this.animator.SetBool("Open", false);

    }

    public void OpenDoorPresurePlate()
    {
        this.animator.SetBool("Open1", true);

    }

    public void CloseDoorPresurePlate()
    {
        this.animator.SetBool("Open1", false);
    }
}
