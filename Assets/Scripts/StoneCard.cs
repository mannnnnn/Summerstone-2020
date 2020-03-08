using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class StoneCard : MonoBehaviour
{
    public Card card;
    public GameObject typeLabel;
    public GameObject subTypeLabel;
    public GameObject variantLabel;
    public GameObject ageLabel;
    public GameObject tooltip;
    public GameObject button;

    public Text chance;
    
    int gracePeriod = 0;
    private Chimera chimera; 
    void Awake()
    {
        tooltip.SetActive(false);
        chimera = Chimera.GetInstance();

        if(chimera.currState == Chimera.MainGameState.CardPick)
        {
            button.SetActive(true);
        } else
        {
            button.SetActive(false);
        }
    }

    public void showHideTooltip()
    {
        if(tooltip.activeSelf)
        {
            tooltip.SetActive(false);
        }
        else
        {
            chance.text = (int)(RuneStats.GetInstance().GetRuneChance(chimera.week, card) * 100) + "%";
            tooltip.SetActive(true);
            gracePeriod += 10;
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
        chimera.card = card;
        DiscardCard();
        chimera.nextGameState();
    }

    public void AgeCard()
    {
        card.age++;
    }

    public void DiscardCard()
    {
        chimera.cards.Remove(this);
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
        GetComponent<Image>().sprite = Spellbook.GetInstance().getCardArt(card.variant);
        GetComponent<Rigidbody2D>().isKinematic = !physics;
    }

    public string ToSaveString()
    {
        return $"{transform.position.x},{transform.position.y},{transform.eulerAngles.z},{card.ToSaveString()}";
    }
    public void FromSaveString(string s)
    {
        var values = s.Split(',');
        transform.position = new Vector3(float.Parse(values[0]), float.Parse(values[1]), transform.position.z);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, float.Parse(values[2]));
        card.FromSaveString(values[3]);
        Set(card);
    }

    void Update()
    {
        if(gracePeriod > 0)
        {
            gracePeriod--;
        }

        if (Input.GetMouseButtonUp(0) && tooltip.activeSelf && gracePeriod<=0)
        {
            tooltip.SetActive(false);
        }
    }
}
