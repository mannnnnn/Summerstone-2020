using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Overlay : MonoBehaviour
{
    Image image;
    Coroutine current;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Coroutine Set(Color c, float time)
    {
        if (current != null)
        {
            StopCoroutine(current);
        }
        if (image.color.a == 0f)
        {
            image.color = new Color(c.r, c.g, c.b, 0f);
        }
        current = StartCoroutine(TransitionToColor(image.color, c, time));
        return current;
    }
    IEnumerator TransitionToColor(Color start, Color end, float time)
    {
        float timer = 0;
        if (time <= 0.0001)
        {
            image.color = end;
            current = null;
            yield break;
        }
        while (timer < time)
        {
            timer += Time.deltaTime;
            image.color = new Color(Mathf.Lerp(start.r, end.r, timer / time),
                Mathf.Lerp(start.g, end.g, timer / time),
                Mathf.Lerp(start.b, end.b, timer / time),
                Mathf.Lerp(start.a, end.a, timer / time));
            yield return null;
        }
        current = null;
        yield break;
    }

}
