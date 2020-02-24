using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Call the Set method in this class from Chimera.nextGameState before showing this screen
// to specify its contents.
public class ResultScreen : MonoBehaviour
{
    public Text title;
    public Text result;
    public GameObject earnedRunes;
    public GameObject spentRunes;
    public GameObject cardPrefab;

    // Start is called before the first frame update
    void Start()
    {
        // test
        Set("Stop creating projects", "I've won... but at what cost?",
            new List<Card>
            {
                Card.Random(),
                Card.Random(),
                Card.Random(),
                Card.Random(),
            },
            new List<Card>
            {
                Card.Random(),
                Card.Random(),
                Card.Random(),
            });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Set(string title, string result, List<Card> earned, List<Card> spent)
    {
        this.title.text = title;
        this.result.text = result;
        // clear earned and spent
        foreach (Transform child in earnedRunes.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in spentRunes.transform)
        {
            Destroy(child.gameObject);
        }
        // create new runes
        foreach (Card c in earned)
        {
            Instantiate(cardPrefab, earnedRunes.transform).GetComponent<StoneCard>().Set(c, false);
        }
        foreach (Card c in spent)
        {
            Instantiate(cardPrefab, spentRunes.transform).GetComponent<StoneCard>().Set(c, false);
        }
    }
}
