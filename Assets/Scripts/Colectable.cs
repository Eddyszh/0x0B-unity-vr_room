using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colectable : MonoBehaviour
{
    [SerializeField] private Material gazedMaterial;
    private Material normalMaterial;
    private readonly float timer = 6f;

    private void Start()
    {
        normalMaterial = GetComponent<Renderer>().material;
    }

    public void OnPointerEnter()
    {
        Highligthed(true);
    }

    public void OnPointerExit()
    {
        Highligthed(false);
    }

    public void PickUpDropItem(Transform t)
    {
        int childCount = t.childCount;
        if (childCount < 1)
        {
            transform.SetParent(t);
            transform.position = t.position;
            transform.GetComponent<Rigidbody>().isKinematic = true;
        }
        Invoke("DropItem", timer);
    }

    public void DropItem()
    {
        transform.GetComponent<Rigidbody>().isKinematic = false;
        transform.parent = null;
    }

    public void Highligthed(bool isGazed)
    {
        gameObject.GetComponent<Renderer>().material = isGazed ? gazedMaterial : normalMaterial;
    }
}
