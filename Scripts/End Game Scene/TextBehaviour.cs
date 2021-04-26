using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TextBehaviour : MonoBehaviour
{
    Text mainText;
    string letters;
    [SerializeField] Text subtext;
    float _fadeInSpeed;

    void Start()
    {
        mainText = GetComponent<Text>();
        letters = mainText.text;
        _fadeInSpeed = 0.01f;
        subtext.color = new Color(255, 0, 0, 0);
        StartCoroutine("TypeText");
    }

    IEnumerator TypeText()
    {
        mainText.text = "";

        //Add a letter from the poll at a certain rate
        foreach (char letter in letters)
        {
            mainText.text += letter;
            yield return new WaitForSeconds(0.5f);
        }
        
        //Fade in the subtext
        while(subtext.color.a < 0.6f)
        {
            subtext.color = Color.Lerp(subtext.color, new Color(255, 0, 0, 1), _fadeInSpeed);
            yield return new WaitForSeconds(0.1f);
        }

        Application.Quit(); // Literaly
    }
}
