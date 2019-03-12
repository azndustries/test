using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    private const float CENTER = 4.0f;
    private int slotNumber;

    private bool isSpinning = false;

    private GameObject[] icons;

    public void Init(int slotNumber) //anything in a () is a parameter needs an integer to define
    {
        this.slotNumber = slotNumber;

        icons = new GameObject[Machine.instance.GetNumIcons()]; //gets the total number of icons accessing array directly

        for(int i=0; i<Machine.instance.GetNumIcons(); i++) //i=index i<machine... no higher than max icons i++= add one to index
        {
            icons[i] = Instantiate(Machine.instance.GetIcons(i).iconPrefab) as GameObject;

            icons[i].transform.position += new Vector3((float)slotNumber, i, 0);  //Slot positioning

            icons[i].transform.parent = this.gameObject.transform;

            IconController iconScript = icons[i].GetComponent<IconController>(); //icons know which slot they belong too
            iconScript.SetSlotRef(this);
        }

    }

    public void StartSpinning()
    {
        isSpinning = true;
        //Debug.Log("Received message to spin");
    }

    public void StoppedSpinning()
    {
        if (isSpinning)
        {
            isSpinning = false;

            Machine.instance.SlotStopped();
        }
    }

    public ICON_TYPE GetIconType() //returns icon type of center line in slot
    {
        ICON_TYPE iconType = 0;

        for(int i=0; i<Machine.instance.GetNumIcons(); i++)
        {
            if(Mathf.Round(icons[i].transform.position.y) == CENTER)
            {
                iconType = icons[i].GetComponent<IconController>().GetIconType();
                return iconType;
            }
        }

        Debug.LogError("Error! Returned default icon!!");
        return iconType;
    }
}
