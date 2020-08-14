using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GrassBehaviour : MonoBehaviour
{
    public bool isInGrass = false;
    public bool battleTrigger = false;
    public Animator anim;
    public int seconds = 4;
    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        StartCoroutine(SetRandomTrigger());
    }

    // Update is called once per frame
    void Update()
    {
        if (isInGrass && battleTrigger)
        {
            LoadBattleScene();
        }
    }

    private void LoadBattleScene()
    {
        player.enabled = false;

        StartCoroutine(LoadBattle());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInGrass = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isInGrass = false;
    }

    IEnumerator SetRandomTrigger()
    {
        while (true)
        {
            int randomNum = UnityEngine.Random.Range(1, 100);

            if (randomNum <= 10)
            {
                battleTrigger = true;
            }
            else
            {
                battleTrigger = false;
            }

            yield return new WaitForSeconds(0.2f);
        }
    }

    IEnumerator LoadBattle()
    {
        anim.SetTrigger("BattleTrigger");

        yield return new WaitForSeconds(seconds);

        SceneManager.LoadScene(1);
    }
}
