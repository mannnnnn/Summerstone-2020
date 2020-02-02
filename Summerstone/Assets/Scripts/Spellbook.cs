using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Spellbook : MonoBehaviour
{

    public CardType type = CardType.Mortal;
    public CardSubType subType = CardSubType.Work;
    public CardVariants variant = CardVariants.Build;
    public float age = 0;


     [SerializeField] public enum CardType
     {
         Mortal,
         Magic,
         Patience,
         Luck
     }

     [SerializeField] public enum CardSubType
     {
         Work,
         BandTogether,
         Alter,
         RiseAbove,
         Understand,
         Master,
         Guess,
         Friends,
     }

    [SerializeField] public enum CardVariants
     {
         Build,
         Delegate,
         Assist,
         Fight,
         Legion,
         Workforce,
         SearchParty,
         Inclusion,
         Reality,
         Time,
         Outcomes,
         Creature,
         Sorcery,
         Leader,
         AllKnowing,
         Mystery,
         Seek,
         Examine,
         Remember,
         Observe,
         Weaponry,
         Stealth,
         Words,
         Tricks,
         History,
         Rumors,
         Readings,
         Experience,
         Canines,
         Vermin,
         Commoners,
         Leaders,

     }

    public GameObject card;
    public GameObject canvas;

    //rune images
    public Sprite work1;
    public Sprite work2;
    public Sprite work3;
    public Sprite work4;
    public Sprite work5;
    public Sprite work6;
    public Sprite work7;
    public Sprite work8;

    public Sprite magic1;
    public Sprite magic2;
    public Sprite magic3;
    public Sprite magic4;
    public Sprite magic5;
    public Sprite magic6;
    public Sprite magic7;
    public Sprite magic8;

    public Sprite time1;
    public Sprite time2;
    public Sprite time3;
    public Sprite time4;
    public Sprite time5;
    public Sprite time6;
    public Sprite time7;
    public Sprite time8;

    public Sprite luck1;
    public Sprite luck2;
    public Sprite luck3;
    public Sprite luck4;
    public Sprite luck5;
    public Sprite luck6;
    public Sprite luck7;
    public Sprite luck8;






    public CardType getCardType(CardSubType type){
        switch(type){
            case CardSubType.Work:
            case CardSubType.BandTogether:
            return CardType.Mortal;

            case CardSubType.Alter:
            case CardSubType.RiseAbove:
            return CardType.Magic;

            case CardSubType.Understand:
            case CardSubType.Master:
            return CardType.Patience;

            case CardSubType.Guess:
            case CardSubType.Friends:
            return CardType.Luck;

            default: 
            Debug.Log("Could not retrieve card type.");
            return CardType.Mortal;
        }
    }

    public List<CardSubType> getCardSubtypes(CardType type){
        List<CardSubType> typeList = new List<CardSubType>();

         switch(type){
            case CardType.Mortal:
                typeList.Add(CardSubType.Work);
                typeList.Add(CardSubType.BandTogether);
                break;
            case CardType.Magic:
                typeList.Add(CardSubType.Alter);
                typeList.Add(CardSubType.RiseAbove);
                break;
            case CardType.Patience:
                typeList.Add(CardSubType.Understand);
                typeList.Add(CardSubType.Master);
                break;
            case CardType.Luck:
                typeList.Add(CardSubType.Guess);
                typeList.Add(CardSubType.Friends);
                break;
        }

        if(typeList.Count <= 0){
            Debug.Log("Could not retrieve card subtypes.");
        }
            
        return typeList;
    }
    
    public List<CardVariants> getCardVariants(CardSubType type){
        List<CardVariants> typeList = new List<CardVariants>();

         switch(type){
            case CardSubType.Work:
                typeList.Add(CardVariants.Build);
                typeList.Add(CardVariants.Delegate);
                typeList.Add(CardVariants.Assist);
                typeList.Add(CardVariants.Fight);
                break;

            case CardSubType.BandTogether:
                typeList.Add(CardVariants.Legion);
                typeList.Add(CardVariants.Workforce);
                typeList.Add(CardVariants.SearchParty);
                typeList.Add(CardVariants.Inclusion);
                break;

            case CardSubType.Alter:
                typeList.Add(CardVariants.Reality);
                typeList.Add(CardVariants.Time);
                typeList.Add(CardVariants.Outcomes);
                typeList.Add(CardVariants.Creature);
                break;

            case CardSubType.RiseAbove:
                typeList.Add(CardVariants.Sorcery);
                typeList.Add(CardVariants.Leader);
                typeList.Add(CardVariants.AllKnowing);
                typeList.Add(CardVariants.Mystery);
                break;

            case CardSubType.Understand:
                typeList.Add(CardVariants.Seek);
                typeList.Add(CardVariants.Examine);
                typeList.Add(CardVariants.Remember);
                typeList.Add(CardVariants.Observe);
                break;

            case CardSubType.Master:
                typeList.Add(CardVariants.Weaponry);
                typeList.Add(CardVariants.Stealth);
                typeList.Add(CardVariants.Words);
                typeList.Add(CardVariants.Tricks);
                break;

             case CardSubType.Guess:
                typeList.Add(CardVariants.History);
                typeList.Add(CardVariants.Rumors);
                typeList.Add(CardVariants.Readings);
                typeList.Add(CardVariants.Experience);
                break;

            case CardSubType.Friends:
                typeList.Add(CardVariants.Canines);
                typeList.Add(CardVariants.Vermin);
                typeList.Add(CardVariants.Commoners);
                typeList.Add(CardVariants.Leaders);
                break;   


           
        }

        if(typeList.Count <= 0){
            Debug.Log("Could not retrieve card variants.");
        }
            
        return typeList;
    }

     public CardSubType getCardSubType(CardVariants type){
        switch(type){
            case CardVariants.Build:
            case CardVariants.Delegate:
            case CardVariants.Assist:
            case CardVariants.Fight:
            return CardSubType.Work;

            case CardVariants.Legion:
            case CardVariants.Workforce:
            case CardVariants.SearchParty:
            case CardVariants.Inclusion:
            return CardSubType.BandTogether;
            
            case CardVariants.Reality:
            case CardVariants.Time:
            case CardVariants.Outcomes:
            case CardVariants.Creature:
            return CardSubType.Alter;

            case CardVariants.Sorcery:
            case CardVariants.Leader:
            case CardVariants.AllKnowing:
            case CardVariants.Mystery:
            return CardSubType.RiseAbove;

            case CardVariants.Seek:
            case CardVariants.Examine:
            case CardVariants.Remember:
            case CardVariants.Observe:
            return CardSubType.Understand;

            case CardVariants.Weaponry:
            case CardVariants.Stealth:
            case CardVariants.Words:
            case CardVariants.Tricks:
            return CardSubType.Master;

            case CardVariants.History:
            case CardVariants.Rumors:
            case CardVariants.Readings:
            case CardVariants.Experience:
            return CardSubType.Guess;

            case CardVariants.Canines:
            case CardVariants.Vermin:
            case CardVariants.Commoners:
            case CardVariants.Leaders:
            return CardSubType.Friends;

            default: 
            Debug.Log("Could not retrieve card subtype.");
            return CardSubType.Work;
        }
    }


public void dropRandomCard(){
    GameObject dropMe = Instantiate(card) as GameObject;
    dropMe.transform.SetParent(canvas.transform, false);
    dropMe.GetComponent<StoneCard>().RandomizeMe();
}



 public Sprite getCardArt(CardVariants type){
        switch(type){
            case CardVariants.Build: return work1;
            case CardVariants.Delegate:  return work2;
            case CardVariants.Assist: return work3;
            case CardVariants.Fight: return work4;
            case CardVariants.Legion:  return work5;
            case CardVariants.Workforce:  return work6;
            case CardVariants.SearchParty:  return work7;
            case CardVariants.Inclusion:  return work8;
            case CardVariants.Reality:  return magic1;
            case CardVariants.Time:  return magic2;
            case CardVariants.Outcomes: return magic3;
            case CardVariants.Creature:return magic4;
            case CardVariants.Sorcery: return magic5;
            case CardVariants.Leader: return magic6;
            case CardVariants.AllKnowing: return magic7;
            case CardVariants.Mystery:return magic8;
            case CardVariants.Seek: return time1;
            case CardVariants.Examine:  return time2;
            case CardVariants.Remember: return time3;
            case CardVariants.Observe: return time4;
            case CardVariants.Weaponry: return time5;
            case CardVariants.Stealth: return time6;
            case CardVariants.Words: return time7;
            case CardVariants.Tricks: return time8;
            case CardVariants.History: return luck1;
            case CardVariants.Rumors: return luck2;
            case CardVariants.Readings: return luck3;
            case CardVariants.Experience:return luck4;
            case CardVariants.Canines:return luck5;
            case CardVariants.Vermin:return luck6;
            case CardVariants.Commoners:return luck7;
            case CardVariants.Leaders:return luck8;
            default: return work1;
        }
 }


}
