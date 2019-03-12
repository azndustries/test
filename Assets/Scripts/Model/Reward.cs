using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum REWARD_TYPE { WinCoins = 0, LoseCoins = 1, Multiplier = 2, Wild =3}

[System.Serializable]
public class Reward
{
    public ICON_TYPE iconType;

    public int reqMatches;

    public REWARD_TYPE rewardType;

    public int rewardAmount;
}
