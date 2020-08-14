using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleHud : MonoBehaviour
{
    public TextMeshProUGUI pokemonName;
    public TextMeshProUGUI lvl;
    public TextMeshProUGUI maxHealth;
    public TextMeshProUGUI currentHealth;
    public Slider healthSlider;

    public Image sliderFill;


    public void UpdateUI(Pokemon pokemon)
    {
        pokemonName.text = pokemon.pokemonName;
        lvl.text = pokemon.lvl.ToString();

        if (maxHealth || currentHealth != null)
        {
            maxHealth.text = pokemon.maxHp.ToString();
            currentHealth.text = pokemon.currentHp.ToString();
        }

        healthSlider.maxValue = pokemon.maxHp;
        healthSlider.value = pokemon.currentHp;
    }

    public void SetHp(int hp)
    {
        healthSlider.value = hp;
        if (currentHealth != null)
        {
            currentHealth.text = hp.ToString();
        }

        if (healthSlider.value <= 0)
        {
            sliderFill.color = Color.black;
        }
        else if (healthSlider.value <= healthSlider.maxValue * 0.3f)
        {
            Debug.Log("Pokemon is below 30% hp!");
            sliderFill.color = Color.red;
        }
        else if (healthSlider.value <= healthSlider.maxValue * 0.7f)
        {
            Debug.Log("Pokemon is below 70% hp!");
            sliderFill.color = Color.yellow;
        }
    }
}
