using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideBarscript : MonoBehaviour
{
    bool Opened = false;
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void ToggleSideBar()
    {
        Opened = !Opened;
        animator.SetBool("SlideOut", Opened);
    }
}
