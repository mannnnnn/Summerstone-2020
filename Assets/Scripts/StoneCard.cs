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
    Rigidbody2D rb;
    Spellbook spellbook;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        tooltip.SetActive(false);
        spellbook = Spellbook.GetInstance();
        Set(card);
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
            Set(card, !rb.isKinematic);
            if (rb.isKinematic)
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
    }

    public void AgeCard()
    {
        card.age = card.age++;
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
        gameObject.GetComponent<Image>().sprite = spellbook.getCardArt(card.variant);
        rb.isKinematic = !physics;
    }

    void Update()
    {
    }
}
