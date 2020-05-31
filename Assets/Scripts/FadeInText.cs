using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInText : MonoBehaviour
{
    [SerializeField] private Text textToUse;
    [TextAreaAttribute(4, 15)]
    [SerializeField] private string textToShow;
    [SerializeField] private float fadeSpeedMultiplier = 0.25f;
    private float colorFloat = 0.1f;
    private int colorInt;
    private int letterCounter = 0;
    private string shownText;

    private void Start()
    {
        Fade();
    }

    private IEnumerator FadeInTextString()
    {
        while (letterCounter < textToShow.Length)
        {
            if (colorFloat < 1.0f)
            {
                colorFloat += Time.deltaTime * fadeSpeedMultiplier;
                colorInt = (int)(Mathf.Lerp(0.0f, 1.0f, colorFloat) * 255.0f);
                textToUse.text = shownText + "<color=\"#FFFFFF" + string.Format("{0:X}", colorInt) + "\">" + textToShow[letterCounter] + "</color>";
            }
            else
            {
                colorFloat = 0.1f;
                shownText += textToShow[letterCounter];
                letterCounter++;
            }
            yield return null;
        }
    }
    public void Fade()
    {
        StartCoroutine(FadeInTextString());
    }
}
