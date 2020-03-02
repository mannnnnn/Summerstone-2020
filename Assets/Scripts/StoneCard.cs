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
        DiscardCard();
        Chimera.GetInstance().nextGameState();
    }

    public void AgeCard()
    {
        card.age++;
    }

    public void DiscardCard()
    {
        Chimera.GetInstance().cards.Remove(this);
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
    }
}
