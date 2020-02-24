using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Call the AddRunes method in this class from Chimera.nextGameState before showing this screen
// to add runes to the inventory. Runes automatically remove themselves when played.
public class RuneChoiceScreen : MonoBehaviour
{
    public GameObject runeParent;
    public GameObject cardPrefab;
    public GameObject spawnPosition;

    // Start is called before the first frame update
    void Start()
    {
        AddRunes(new List<Card>()
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

    public void AddRunes(List<Card> addedRunes)
    {
        // create new runes
        int i = 0;
        foreach (Card c in addedRunes)
        {
            // create rune
            Instantiate(cardPrefab, spawnPosition.transform.position + Vector3.right * i * 40, Quaternion.identity, runeParent.transform)
                .GetComponent<StoneCard>().Set(c, true);
            // prevent runes from overlapping
            if (i > 0)
            {
                i = -i;
            }
            else
            {
                i = -i;
                i += 1;
            }
        }
    }
}
