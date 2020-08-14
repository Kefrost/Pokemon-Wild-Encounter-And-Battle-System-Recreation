using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pokemon : MonoBehaviour
{
    public string pokemonName;
    public int lvl;

    public int maxHp;
    public int currentHp;

    public int dmg;

    public bool TakeDmg(int dmg)
    {
        currentHp -= dmg;

        if (currentHp <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
