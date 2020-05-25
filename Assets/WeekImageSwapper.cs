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
            if (currImage != null && weekImages[index-1] != null)
            {
                currImage.sprite = weekImages[index-1];
            }
        } else
        {
            if (currImage != null && weekendImages[index-1] != null)
            {
                currImage.sprite = weekendImages[index-1];
            }
        }
        
    }
}
