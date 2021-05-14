using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] private Animator anim;
    private bool isTrigger = false;
    private bool isOn = false;

    public void InteractToDoor()
    {
        anim.SetBool("character_nearby", isTrigger ^= true);
    }

    public void InteractToProjector()
    {
        gameObject.SetActive(isOn ^= true);
    }

    public void ActivatePanel()
    {

    }
}
