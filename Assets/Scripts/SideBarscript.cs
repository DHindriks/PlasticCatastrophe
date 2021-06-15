using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideBarscript : MonoBehaviour
{
    public bool Opened = false;
    Animator animator;
    [SerializeField]
    GameObject SideButton;

    [SerializeField]
    GameObject Icon;

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
        if (Opened)
        {
            GameManager.Instance.overworldCam.SetControls(false);
            Icon.transform.localScale = new Vector3(1, 1, 1);
        }else
        {
            GameManager.Instance.overworldCam.SetControls(true);
            Icon.transform.localScale = new Vector3(1, -1, 1);
        }
    }

    public void StopApp()
    {
        Application.Quit();
    }
}
