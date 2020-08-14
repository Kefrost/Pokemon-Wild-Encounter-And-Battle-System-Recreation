using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WIN, LOST, CAPTURE}

public class BattleSystem : MonoBehaviour
{
    public BattleState state;
    public GameObject player;
    public GameObject enemy;
    public TextMeshProUGUI dialogue;
    public BattleHud playerHud;
    public BattleHud enemyHud;
    public PlayableDirector capture;

    private Pokemon enemyPokemon;
    private Pokemon playerPokemon;
    private Animator playerHudAnim;
    private Animator playerAnim;
    private Animator enemyAnim;


    void Start()
    {
        enemyPokemon = enemy.GetComponent<Pokemon>();

        playerPokemon = player.GetComponent<Pokemon>();

        playerAnim = playerPokemon.GetComponentInChildren<Animator>();

        enemyAnim = enemyPokemon.GetComponentInChildren<Animator>();

        playerHudAnim = playerHud.GetComponentInParent<Animator>();

        state = BattleState.START;

        StartCoroutine(SetupBattle());
    }

    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }

        state = BattleState.ENEMYTURN;

        StartCoroutine(PlayerAttack());

    }

    public void OnCaptureButton()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }

        state = BattleState.CAPTURE;

        StartCoroutine(UseCapture());
    }

    void PlayerTurn()
    {
        dialogue.text = $"What should {playerPokemon.pokemonName.ToUpper()} do?";
    }

    void EndBattle()
    {
        if (state == BattleState.WIN)
        {
            dialogue.text = $"You defeated {enemyPokemon.pokemonName.ToUpper()}!";
        }
        else if(state == BattleState.LOST)
        {
            dialogue.text = $"You was defeated!";
        }
        else if (state == BattleState.CAPTURE)
        {
            dialogue.text = $"You captured {enemyPokemon.pokemonName.ToUpper()}!";
        }
    }

    IEnumerator SetupBattle()
    {
        dialogue.text = $"Wild {enemyPokemon.pokemonName} appeared...";

        yield return new WaitForSeconds(4f);

        dialogue.text = $"Go! {playerPokemon.pokemonName}!";

        yield return new WaitForSeconds(2f);

        playerHud.UpdateUI(playerPokemon);
        enemyHud.UpdateUI(enemyPokemon);

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    IEnumerator PlayerAttack()
    {
        dialogue.text = $"{playerPokemon.pokemonName.ToUpper()} used Tackle!";
        
        yield return new WaitForSeconds(3f);

        playerAnim.SetTrigger("Attack");

        enemyAnim.SetTrigger("Hurt");

        playerHudAnim.SetTrigger("EnemyHurt");

        bool isDead = enemyPokemon.TakeDmg(playerPokemon.dmg);
        enemyHud.SetHp(enemyPokemon.currentHp);

        if (isDead)
        {
            state = BattleState.WIN;
            EndBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator EnemyTurn()
    {
        dialogue.text = $"Wild {enemyPokemon.pokemonName.ToUpper()} used Tackle!";

        yield return new WaitForSeconds(2f);
        
        enemyAnim.SetTrigger("Attack");
        playerAnim.SetTrigger("Hurt");
        playerHudAnim.SetTrigger("PlayerHurt");

        bool isDead = playerPokemon.TakeDmg(enemyPokemon.dmg);
        playerHud.SetHp(playerPokemon.currentHp);

        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }

    IEnumerator UseCapture()
    {
        state = BattleState.CAPTURE;

        dialogue.text = "You used a Pokeball!";

        capture.Play();

        yield return new WaitForSeconds(8f);

        dialogue.text = $"You captured {enemyPokemon.pokemonName}!";
    }

}
