using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StoneCard : MonoBehaviour
{
    public Spellbook card;
    public Spellbook myCardCopy;
    public GameObject typeLabel;
    public GameObject subTypeLabel;
    public GameObject variantLabel;
    public GameObject ageLabel;
    public GameObject tooltip;

    void Awake()
    {
        tooltip.SetActive(false);

        if(card == null){
            card = GameObject.FindGameObjectsWithTag("chimera")[0].GetComponent<Spellbook>();
            myCardCopy = new Spellbook();
        }

        if(myCardCopy == null){
            myCardCopy.type = card.type;
            myCardCopy.subType = card.subType;
            myCardCopy.variant = card.variant;
            myCardCopy.age = 0;
        }
        typeLabel.GetComponent<Text>().text = myCardCopy.type.ToString();
        subTypeLabel.GetComponent<Text>().text = myCardCopy.subType.ToString();
        variantLabel.GetComponent<Text>().text = myCardCopy.variant.ToString();
        ageLabel.GetComponent<Text>().text = myCardCopy.age.ToString();


    }


    public void RandomizeMe(){
        //card.type = (Spellbook.CardType)Random.Range(1, 4);
        //card.subtype = (Spellbook.CardSubType)Random.Range(1, 8);
        myCardCopy.variant = (Spellbook.CardVariants)Random.Range(1, 32);
        myCardCopy.subType = myCardCopy.getCardSubType(myCardCopy.variant);
        myCardCopy.type = myCardCopy.getCardType(myCardCopy.subType);


        gameObject.GetComponent<Image>().sprite = card.getCardArt(myCardCopy.variant);
    }

    public void showHideTooltip(){
        if(tooltip.active){
            tooltip.SetActive(false);
        } else {
        tooltip.SetActive(true);
        typeLabel.GetComponent<Text>().text = myCardCopy.type.ToString();
        subTypeLabel.GetComponent<Text>().text = myCardCopy.subType.ToString();
        variantLabel.GetComponent<Text>().text = myCardCopy.variant.ToString();
        ageLabel.GetComponent<Text>().text = myCardCopy.age.ToString();
        }
    }

    public void OnPointerDown(PointerEventData eventData){
        if(tooltip.active){
            tooltip.SetActive(false);
        }
    }

    public void PlayCard(){

    }

    public void AgeCard(){
        myCardCopy.age = myCardCopy.age++;
    }

    public void DiscardCard(){
        
    }


    void Update()
    {
        //ow this feels expensive
        typeLabel.GetComponent<Text>().text = myCardCopy.type.ToString();
        subTypeLabel.GetComponent<Text>().text = myCardCopy.subType.ToString();
        variantLabel.GetComponent<Text>().text = myCardCopy.variant.ToString();
        ageLabel.GetComponent<Text>().text = myCardCopy.age.ToString();
    }
}
