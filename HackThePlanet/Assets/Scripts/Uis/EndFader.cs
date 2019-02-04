using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndFader : MonoBehaviour
{

    [SerializeField] private Image fadeImg, endImg;
    [SerializeField] private Text endText;

    [SerializeField] private float fadeSpeed = 1f, delayBeforeFadeOut = 5f, delayBeforeFadeIn = 1f;

    [SerializeField] private Color fadeColor = Color.black;
    [SerializeField] private AnimationCurve fadeCurve;

    [Space(20)]
    [SerializeField] private Fin[] finsPossibles;
    private Fin finActuelle;
    private int currentIndex = 0;

    public static EndFader instance;




    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
            return;
        }

        instance = this;
    }





    private void Start()
    {
        fadeImg.gameObject.SetActive(true);

        for (int i = 0; i < finsPossibles.Length; i++)
        {
            if(finsPossibles[i].factionDeLaFin == GameManager.factionDuJoueur)
            {
                finActuelle = finsPossibles[i];
                endImg.sprite = finActuelle.finSprite;

                StartCoroutine(FadeIn());
            }
        }
    }




    private IEnumerator FadeIn()
    {
        if(currentIndex < finActuelle.pages.Length)
        {
            endText.text = finActuelle.pages[currentIndex].pageText;

            float t = 1f;

            while (t > 0f)
            {
                t -= Time.deltaTime * fadeSpeed;
                float a = fadeCurve.Evaluate(t);
                fadeImg.color = new Color(fadeColor.r, fadeColor.g, fadeColor.b, a);
                yield return 0;
            }


            Vector3 scale = fadeImg.rectTransform.localScale;
            scale.y = .4f;
            fadeImg.transform.localScale = scale;


            fadeImg.gameObject.SetActive(false);
            StartCoroutine(WaitBeforeFadeOut());
        }
    }





    private IEnumerator WaitBeforeFadeOut()
    {

        if (currentIndex == finActuelle.pages.Length - 1)
        {
            Vector3 scale = fadeImg.rectTransform.localScale;
            scale.y = 1f;
            fadeImg.transform.localScale = scale;
        }

        yield return new WaitForSeconds(delayBeforeFadeOut);

        StartCoroutine(FadeOut());
    }





    private IEnumerator FadeOut()
    {
        
        fadeImg.gameObject.SetActive(true);
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * fadeSpeed;
            float a = fadeCurve.Evaluate(t);
            fadeImg.color = new Color(fadeColor.r, fadeColor.g, fadeColor.b, a);
            yield return 0;
        }

        StartCoroutine(WaitBeforeFadeIn());
 
    }





    private IEnumerator WaitBeforeFadeIn()
    {
        currentIndex++;
        print(currentIndex);

        yield return new WaitForSeconds(delayBeforeFadeIn);

        if (currentIndex == finActuelle.pages.Length)
        {
            SceneManager.LoadScene(0);
        }

        StartCoroutine(FadeIn());
    }


}
