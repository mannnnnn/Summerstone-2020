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
    public GameObject variantLabel;
    public GameObject tooltip;
    public GameObject button;
    public GameObject goldenWheel;
    public GameObject passWheel;
    public GameObject percentPassFail;

    public GameObject cardImage;

    Image passWheelFill;

    bool gold = false;
    float passFill = 0;
    long aniDelay = 1000;

    int gracePeriod = 0;
    private Chimera chimera;
    void Awake()
    {
        gold = false;
        aniDelay = 1000;
        tooltip.SetActive(false);
        chimera = Chimera.GetInstance();
        passWheelFill = passWheel.GetComponent<Image>();
        passWheelFill.fillAmount = 0;
        if (chimera.currState == Chimera.MainGameState.CardPick)
        {
            button.SetActive(true);
            float chance = RuneStats.GetInstance().GetRuneChance(Chimera.GetInstance().week, card);
            percentPassFail.SetActive(true);
            percentPassFail.GetComponent<Text>().text = "0%";
            if (chance > 1)
            {
                goldenWheel.SetActive(true);
                gold = true;
                percentPassFail.GetComponent<Text>().text = "100%";
            } else
            {
                goldenWheel.SetActive(false);
            }
            passFill = chance;
        }
        else
        {
            button.SetActive(false);
            gold = false;
        }
    }

    public void showHideTooltip()
    {
        if (tooltip.activeSelf)
        {
            tooltip.SetActive(false);
        }
        else
        {
            tooltip.SetActive(true);
            gracePeriod += 10;
            Set(card, !cardImage.GetComponent<Rigidbody2D>().isKinematic);
            if (cardImage.GetComponent<Rigidbody2D>().isKinematic)
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
        variantLabel.GetComponent<Text>().text = Spellbook.GetInstance().getFancyCardName(card.variant);
        typeLabel.GetComponent<Text>().text = card.type.ToString();
        cardImage.GetComponent<Image>().sprite = Spellbook.GetInstance().getCardArt(card.variant);
        cardImage.GetComponent<Rigidbody2D>().isKinematic = !physics;
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
        if (gracePeriod > 0)
        {
            gracePeriod--;
        }

        if (Input.GetMouseButtonUp(0) && tooltip.activeSelf && gracePeriod <= 0)
        {
            tooltip.SetActive(false);
        }
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (aniDelay > 0)
        {
            aniDelay--;
        }
        else
        {
            if (button.activeSelf && passWheelFill.fillAmount < passFill && !gold)
            {
                passWheelFill.fillAmount += 0.01f;
                percentPassFail.GetComponent<Text>().text = (int)(passWheelFill.fillAmount * 100) + "%";
            }
        }
    }
}