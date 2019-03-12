using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject machinePrefab;

    public Reward[] rewards;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Debug.Log("GameManager singleton is already running");
            Destroy(this.gameObject); //Destroys duplicate GameManagers
        }
        else if (instance != this)

            if (instance != this)
            {
                instance = this;
            }
        Instantiate(machinePrefab);
        Machine.instance.Init();
            
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            Machine.instance.StartSpinning();
        }

        if (Input.GetKeyDown("m")) //shows line icons
        {
            int[] matches;

            matches = Machine.instance.FindMatches();

            for(int i=0; i<Machine.instance.GetNumIcons(); i++)
            {
                Debug.Log(matches[i]);
            }
            
        }
    }

    public void ReadyToMatch() //where the rewards are calculated
    {
        Debug.Log("all slots have stopped spinning. ready to match");

        StartCoroutine("Rewards");
    }


    IEnumerator Rewards()
    {
        int[] matches;

        int dittoCount = 0;

        int dittoMatches = 0;

        int multiplier = 0;

        int rewardTotal = 0;

        matches = Machine.instance.FindMatches();

        yield return new WaitForSeconds(1);

        for(int i = 0; i<Machine.instance.GetNumIcons(); i++)
        {
            foreach(Reward reward in rewards)
            {
                if (reward.iconType == (ICON_TYPE)i && reward.reqMatches == matches[i])
                {
                    if (reward.iconType != ICON_TYPE.Ditto) //excluding the multiplier from the others
                    {
                        if (reward.rewardType == REWARD_TYPE.WinCoins)
                        {
                            rewardTotal += reward.rewardAmount;
                        }

                        Debug.Log("Matched " + matches[i] + ((ICON_TYPE)i).ToString() + ". " + reward.rewardType.ToString() + " " + reward.rewardAmount);
                    }
                }
                else if (reward.iconType == (ICON_TYPE)i && reward.reqMatches - 1 == matches[i])
                {
                    if (reward.iconType == ICON_TYPE.Ditto) //excluding the multiplier from the others
                    {
                        if (reward.rewardType == REWARD_TYPE.WinCoins)
                        {
                            rewardTotal += reward.rewardAmount;
                        }

                        Debug.Log("Matched " + matches[i] + ((ICON_TYPE)i).ToString() + ". " + reward.rewardType.ToString() + " " + reward.rewardAmount);
                    }
                    Debug.Log("Matched " + matches[i] + ((ICON_TYPE)i).ToString() + ". " + reward.rewardType.ToString() + " " + reward.rewardAmount);
                }                  }
                }
    

        if(multiplier >0 && dittoCount > 0 && rewardTotal > 0)
        {
            Debug.Log("Reward amount " + rewardTotal + "has been multiplied by " + multiplier + " to equal " + (rewardTotal * multiplier) + ".");
        }
    }
}
