using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ICON_TYPE { Bulbasaur=0, Charmander=1, Squirtle=2, Pikachu=3, Magikarp=4, Ditto=5, Articuno=6, Zapdos=7, Moltres=8, Mewtwo=9}

[System.Serializable]
public class Icons
{

    public ICON_TYPE iconType;

    public GameObject iconPrefab;

}
