using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machine : MonoBehaviour 
{

    public static Machine instance;

    public int numSlots;
    public float minSlotSpeed;
    public float maxSlotSpeed;
    public float slotSlowdownStartTime;
    public float slotSlowdownStartNext;
    public float slotSlowdownInterval;
    public float slotSlowdownDelta;
    public float slotSlowestSpinSpeed;

    public GameObject slotPrefab;

    public Icons[] icons;

    private bool isSpinning = false;

    private GameObject[] slots;

    private int slotsSpinning;

    void Awake()
    {
        if(instance != null && instance != this)
        {
            Debug.Log("Machine singleton is already running");
            Destroy(this.gameObject); //Destroys duplicate Machines
        }
        else if (instance !=this)

        if(instance != this)
        {
            instance = this;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame 
    void Update()
    {
        
    }

    public void Init()
    {
        SpawnSlots();
    }

    public int GetNumIcons() //so that we dont access the array directly will get count of icons defined 
    {
        return icons.Length;
    }

    public int GetNumSlots()
    {
        return numSlots;
    }

    public Icons GetIcons(int i) //returning an icon through the loop
    {
        return icons[i];
    }

    private void SpawnSlots()
    {
        slots = new GameObject[numSlots];

        for (int i=0; i<numSlots; i++)
        {
            slots[i] = Instantiate(slotPrefab) as GameObject;

            Slot slotScript = slots[i].GetComponent<Slot>();

            if(slotScript == null)
            {
                Debug.Log("No slot script on object");
            }
            else
            {
                slotScript.Init(i);
            }
        }
    }

    public void StartSpinning()
    {
        //float speed = Random.Range(minSlotSpeed, maxSlotSpeed); all reels moving the same speed

        for(int i=0; i<numSlots; i++)
        {
            slots[i].BroadcastMessage("StartSpinning", Random.Range(minSlotSpeed, maxSlotSpeed)); //,speed
        }

        slotsSpinning = numSlots;

        isSpinning = true;

        StartCoroutine(SlotSlowdownTimer(slotSlowdownStartTime, slotSlowdownStartNext));
    }

    public void SlotStopped() //when slots stopped ready to match
    {
        slotsSpinning--;

        if(slotsSpinning == 0)
        {
            GameManager.instance.ReadyToMatch();
        }
    }

    public int[] FindMatches()
    {
        int[] iconCountArray;

        iconCountArray = new int[GetNumIcons()];

        for(int i=0; i<numSlots; i++)
        {
            Slot slotScript = slots[i].GetComponent<Slot>();


            //iconCountArray[i] = (int)slotScript.GetIconType(); //shows whats on line
            iconCountArray[(int)slotScript.GetIconType()]++; //adds to the index whats in the line
        }

        return iconCountArray;
    }

    IEnumerator SlotSlowdownTimer(float slotSlowdownStartTime, float slotSlowdownStartNext) //games where you want things to spawn or happen at a certain time
    {
        yield return new WaitForSeconds(slotSlowdownStartTime); //pausing method for x amount of time than continue after x

        for(int i=0; i<numSlots; i++)
        {
            slots[i].BroadcastMessage("StopSpinning");

            yield return new WaitForSeconds(slotSlowdownStartNext); //between each broadcast message its going to delay by start next time
        }

        yield break;
    }
}
