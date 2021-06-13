using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Popup : MonoBehaviour
{
    [SerializeField]
    Animator animator;

    [SerializeField]
    TextMeshProUGUI Title;

    [SerializeField]
    TextMeshProUGUI Description;

    [SerializeField]
    Image Icon;

    public Button Btn;

    public void Close()
    {
        animator.SetBool("Closing", true);
        Destroy(gameObject, 5);
    }

    public void SetTitle(string name)
    {
        Title.text = name;
    }

    public void SetDesc(string desc)
    {
        Description.text = desc;
    }

    public void SetIcon(Sprite icon)
    {
        Icon.sprite = icon;
    }
}
