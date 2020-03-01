using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeekImageSwapper : MonoBehaviour
{
    public Image currImage;

    public Sprite[] weekImages = new Sprite[10];
    public Sprite[] weekendImages = new Sprite[10];

    public void updateImage(bool isWeek, int index)
    {
        if (isWeek)
        {
            if (currImage != null && weekImages[index] != null)
            {
                currImage.sprite = weekImages[index];
            }
        } else
        {
            if (currImage != null && weekendImages[index] != null)
            {
                currImage.sprite = weekendImages[index];
            }
        }
        
    }
}
