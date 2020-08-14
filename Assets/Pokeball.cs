using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pokeball : MonoBehaviour
{
    public GameObject player;

    // Start is called before the first frame update
    public void Summon()
    {
        player.SetActive(true);
    }
}
