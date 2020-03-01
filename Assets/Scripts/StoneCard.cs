using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StoneCard : MonoBehaviour
{
    public Card card;
    public GameObject typeLabel;
    public GameObject subTypeLabel;
    public GameObject variantLabel;
    public GameObject ageLabel;
    public GameObject tooltip;
    public GameObject button;

    void Awake()
    {
        tooltip.SetActive(false);
    }

    public void showHideTooltip()
    {
        if(tooltip.activeSelf)
        {
            tooltip.SetActive(false);
        }
        else
        {
            tooltip.SetActive(true);
            Set(card, !GetComponent<Rigidbody2D>().isKinematic);
            if (GetComponent<Rigidbody2D>().isKinematic)
            {
                button.SetActive(false);
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (tooltip.activeSelf)
        {
            tooltip.SetActive(false);
        }
    }

    public void PlayCard()
    {
        Chimera.GetInstance().card = card;
        Chimera.GetInstance().nextGameState();
        DiscardCard();
    }

    public void AgeCard()
    {
        card.age++;
    }

    public void DiscardCard()
    {
        Destroy(gameObject);
    }

    // display a card
    public void Set(Card c, bool physics = true)
    {
        card = c;
        typeLabel.GetComponent<Text>().text = card.type.ToString();
        subTypeLabel.GetComponent<Text>().text = card.subType.ToString();
        variantLabel.GetComponent<Text>().text = card.variant.ToString();
        ageLabel.GetComponent<Text>().text = card.age.ToString();
        Debug.Log(GetComponent<Image>());
        Debug.Log(Spellbook.GetInstance());
        Debug.Log(card);
        GetComponent<Image>().sprite = Spellbook.GetInstance().getCardArt(card.variant);
        Debug.Log(GetComponent<Rigidbody2D>());
        GetComponent<Rigidbody2D>().isKinematic = !physics;
    }

    void Update()
    {
    }
}
