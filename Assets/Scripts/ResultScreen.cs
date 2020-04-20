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

    public GameObject baseWheel;
    public GameObject passWheel;
    public GameObject goldenWheel;
    public GameObject percentPassFail;

    Image passWheelFill;

    bool gold = false;
    float passFill = 0;
    bool pass = false;

    long aniDelay = 1000;

    List<Card> earned;
    List<Card> spent;

    // Start is called before the first frame update
    void Start()
    {
        passWheelFill = passWheel.GetComponent<Image>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if(aniDelay > 0)
        {
            aniDelay--;
        } else
        {
            if (passWheel.activeSelf && passWheelFill.fillAmount < passFill && !gold)
            {
                passWheelFill.fillAmount += 0.01f;
                percentPassFail.GetComponent<Text>().text = (int)(passWheelFill.fillAmount * 100) + "%";
            }

            if (passWheel.activeSelf && passWheelFill.fillAmount >= passFill)
            {
                if (pass)
                {
                    percentPassFail.GetComponent<Animator>().SetTrigger("pass");
                }
                else
                {
                    percentPassFail.GetComponent<Animator>().SetTrigger("fail");
                }
            }
        }

    }

    public void Set(string title, string result, List<Card> earned, List<Card> spent, bool pass, float chance)
    {
        this.title.text = title;
        this.result.text = result;
        this.earned = earned;
        this.spent = spent;
        gold = false;
        passFill = 0;
        aniDelay = 10;
        this.pass = pass;
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

        if (spent.Count > 0)
        {
            baseWheel.SetActive(true);
            passWheel.SetActive(true);
            percentPassFail.SetActive(true);
            percentPassFail.GetComponent<Animator>().ResetTrigger("pass");
            percentPassFail.GetComponent<Animator>().ResetTrigger("fail");
            passWheelFill = passWheel.GetComponent<Image>();
            passWheelFill.fillAmount = 0;
            percentPassFail.GetComponent<Text>().text = "0%";
            if (chance > 1)
            {
                goldenWheel.SetActive(true);
                gold = true;
                percentPassFail.GetComponent<Text>().text = "100%";
            }
            passFill = chance;
        } else{
                baseWheel.SetActive(false);
                passWheel.SetActive(false);
                goldenWheel.SetActive(false);
                percentPassFail.SetActive(false);
            }
         }

}
