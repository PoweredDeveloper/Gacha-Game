using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GachaManager : MonoBehaviour
{
    private IDictionary<string, float> chances = new Dictionary<string, float>();
    private IDictionary<string, int> guarantees = new Dictionary<string, int>();

    public List<ScriptableCharacter> uniqueCharacters = new List<ScriptableCharacter>();
    public List<ScriptableCharacter> legendaryCharacters = new List<ScriptableCharacter>();
    public List<ScriptableCharacter> epicCharacters = new List<ScriptableCharacter>();
    public List<ScriptableCharacter> rareCharacters = new List<ScriptableCharacter>();

    public Text wishCounter;
    public Text gemAmount;

    public SplashController sc;

    public int rollCost = 130;
    public int multipleRollSale = 10;

    private int wished = 0;
    private int gems = 0;

    private int lastUnique = 0; 
    private int lastLegendary = 0; 
    private int lastEpic = 0; 
    private int lastRare = 0;

    private void Awake()
    {
        chances["unique"] = 0.7f;
        chances["legendary"] = 2.4f;
        chances["epic"] = 5.1f;
        chances["rare"] = 10.3f;

        guarantees["unique"] = 240;
        guarantees["legendary"] = 120;
        guarantees["epic"] = 60;
        guarantees["rare"] = 30;

        wished = PlayerPrefs.GetInt("wished", 0);

        wishCounter.text = "Wished: " + wished.ToString();

        gems = PlayerPrefs.GetInt("gemAmount", 9999);

        gemAmount.text = 'x' + gems.ToString();

        lastUnique = PlayerPrefs.GetInt("lastUnique", 0);
        lastLegendary = PlayerPrefs.GetInt("lastLegendary", 0);
        lastEpic = PlayerPrefs.GetInt("lastEpic", 0);
        lastRare = PlayerPrefs.GetInt("lastRare", 0);
    }

    public void ToGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void rollOnce()
    {
        if (gems < rollCost) return;
        spendGems(rollCost);
        dropCharacter(randomNumber());
        sc.Load();
    }

    public void rollMultiple()
    {
        if (gems < rollCost * 9) return;
        spendGems(rollCost * 9);

        for (int i = 0; i < 10; i++)
        {
            dropCharacter(randomNumber());
        }
    }

    private void dropCharacter(float randNumber)
    {
        wished++;
        if (randNumber <= chances["unique"] || lastUnique >= guarantees["unique"]) addCharacter("Unique");
        else if ((randNumber <= chances["legendary"] && randNumber > chances["unique"]) || lastLegendary >= guarantees["legendary"]) addCharacter("Legendary");
        else if ((randNumber <= chances["epic"] && randNumber > chances["legendary"]) || lastEpic >= guarantees["epic"]) addCharacter("Epic");
        else if ((randNumber <= chances["rare"] && randNumber > chances["epic"]) || lastRare >= guarantees["rare"]) addCharacter("Rare");

        lastUnique++;
        lastLegendary++;
        lastEpic++;
        lastRare++;

        UpdatePrefs();
    }

    private void addCharacter(string characterRarity)
    {
        ScriptableCharacter character = null;
        switch (characterRarity)
        {
            case "Unique":
            {
                lastUnique = 0;
                if (uniqueCharacters.Count != 0) character = uniqueCharacters[Random.Range(0, uniqueCharacters.Count)];
                break;
            }
            case "Legendary":
            {
                lastLegendary = 0;
                if (legendaryCharacters.Count != 0) character = legendaryCharacters[Random.Range(0, legendaryCharacters.Count)];
                break;
                }
            case "Epic":
            {
                lastEpic = 0;
                if (epicCharacters.Count != 0) character = epicCharacters[Random.Range(0, epicCharacters.Count)];
                break;
                }
            case "Rare":
            {
                lastRare = 0;
                if (rareCharacters.Count != 0) character = rareCharacters[Random.Range(0, rareCharacters.Count)];
                break;
            }
        }
        Debug.Log(character);
        UpdatePrefs();
    }

    private float randomNumber()
    {
        return (Random.Range(0.0f, 100.0f));
    }

    private void spendGems(int amountToSpend)
    {
        gems -= amountToSpend;
        UpdatePrefs();
    }

    private void UpdatePrefs()
    {
        PlayerPrefs.SetInt("gemAmount", gems);

        PlayerPrefs.SetInt("lastUnique", lastUnique);
        PlayerPrefs.SetInt("lastLegendary", lastLegendary);
        PlayerPrefs.SetInt("lastEpic", lastEpic);
        PlayerPrefs.SetInt("lastRare", lastRare);

        wishCounter.text = "Wished: " + wished.ToString();
        gemAmount.text = 'x' + gems.ToString();
    }
}
