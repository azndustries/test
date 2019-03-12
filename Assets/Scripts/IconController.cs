using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconController : MonoBehaviour
{
    public ICON_TYPE iconType;

    private Slot slotRef;
    private bool isSpinning = false;
    private bool isStopping = false;
    private bool isSlowing = false;
    private float stopPoint = 0; //stop position on y axis
    private float spinSpeed = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    // where the icons actually move
    void Update()
    {
        

        if(spinSpeed < Machine.instance.slotSlowestSpinSpeed)
        {
            spinSpeed = Machine.instance.slotSlowestSpinSpeed;
            isSlowing = false;
            isStopping = true;

            stopPoint = Mathf.Floor(transform.position.y);

        }

        if (isSpinning)
        {
            transform.Translate(Vector3.down * Time.deltaTime * spinSpeed, Space.World); //moving icons down, updates things based on time instead of computer specs

            if (!isStopping)
            {

                if (transform.position.y < 0)
                {
                    transform.position += new Vector3(0, Machine.instance.GetNumIcons(), 0); //if you add more icons change the number of y
                }

            }
        }

        if (isStopping) //stopping the reel and setting it to a certain point
        {
            if(transform.position.y <= stopPoint)
            {
                transform.position = new Vector3(transform.position.x, stopPoint, transform.position.z);

                isSpinning = false;
                isStopping = false;
                stopPoint = 0;

                slotRef.StoppedSpinning();
            }
        }

    }

    public void StartSpinning(float slotSpeed)
    {
        StopAllCoroutines();

        //Debug.Log("Received message to spin");

        isSpinning = true;
        isStopping = false;
        isSlowing = false;

        spinSpeed = slotSpeed;


    }

    public void StopSpinning()
    {
        //isSpinning = false;
        //isStopping = true;
        isSlowing = true;

        StartCoroutine(SlowSpinOverTime(Machine.instance.slotSlowdownInterval, Machine.instance.slotSlowdownDelta));
    }

    public ICON_TYPE GetIconType()
    {
        return (iconType);
    }

    public void SetSlotRef(Slot slot)
    {
        slotRef = slot;
    }

      IEnumerator SlowSpinOverTime(float interval, float delta)//how often do we want to slow it down, how much do we slow it down every interval
    {
        while(true)
        {
            yield return new WaitForSeconds(interval);

            if (isStopping || !isSpinning) yield break;

            spinSpeed -= delta;
        }
    }
}
