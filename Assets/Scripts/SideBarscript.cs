using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideBarscript : MonoBehaviour
{
    public bool Opened = false;
    Animator animator;
    [SerializeField]
    GameObject SideButton;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void SetSideButton(bool Enabled)
    {
        SideButton.SetActive(Enabled);
    }

    public void ToggleSideBar()
    {
        Opened = !Opened;
        animator.SetBool("SlideOut", Opened);
    }
}
