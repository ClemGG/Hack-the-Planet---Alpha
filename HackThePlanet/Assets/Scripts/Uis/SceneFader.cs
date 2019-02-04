using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneFader : MonoBehaviour {
    

    public Image fadeImg, semaineImg;

    [Tooltip("Plus la valeur est haute, plus le fondu sera rapide, et inversement plus la valeur est basse.")]
    public float normalFadeSpeed = 1f;
    [Tooltip("Plus la valeur est haute, plus le fondu sera rapide, et inversement plus la valeur est basse.")]
    public float delayBeforePlay = 3f;

    public Color fadeColor = Color.black;
    public AnimationCurve fadeCurve;


    public static SceneFader instance;




    private void Awake()
    {
        if (instance != null)
        {
            print("More than one SceneFader in scene !");
            return;
        }

        instance = this;


    }


    private void Start()
    {

        fadeImg.gameObject.SetActive(true);
        semaineImg.gameObject.SetActive(false);

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            StartCoroutine(FadeIn(false));
            MenuDemarrerBoutons.instance.JouerAnimAccueil(0);
        }
        else
        {
            StartCoroutine(StartSemaine());
        }
    }


    public IEnumerator StartSemaine()
    {
        fadeImg.gameObject.SetActive(true);

        StartCoroutine(FadeOutSameScene());
        yield return new WaitForSeconds(delayBeforePlay);
        StartCoroutine(FadeIn(true));
        yield return new WaitForSeconds(delayBeforePlay);
        StartCoroutine(FadeOutSameScene());
        yield return new WaitForSeconds(normalFadeSpeed);
        MenuDemarrerBoutons.instance.StartGame();
        StartCoroutine(FadeIn(false));

    }









    /// <summary>
    /// Permet de réaliser un fondu entre les scènes
    /// </summary>
    public void FadeToScene(int sceneIndex)
    {

        StartCoroutine(FadeOut(sceneIndex));
    }



    /// <summary>
    /// Permet de réaliser un fondu avant de quitter le jeu.
    /// </summary>
    public void FadeToQuitScene()
    {
        StartCoroutine(FadeQuit());
    }




















    /// <summary>
    /// Diminue l'alpha du fondu pour faire apparaître la scène
    /// </summary>
    /// <returns></returns>
    public IEnumerator FadeIn(bool showImageOnCall)
    {
        
        semaineImg.gameObject.SetActive(showImageOnCall);

        Time.timeScale = 1f;
        float t = 1f;

        while (t > 0f)
        {
            t -= Time.unscaledDeltaTime * normalFadeSpeed;
            float a = fadeCurve.Evaluate(t);
            fadeImg.color = new Color(fadeColor.r,fadeColor.g,fadeColor.b, a);
            yield return 0;
        }

        fadeImg.gameObject.SetActive(false);


    }








    /// <summary>
    /// Augmente l'alpha du fondu pour faire disparaître la scène
    /// </summary>
    /// <returns></returns>
    public IEnumerator FadeOut(int sceneIndex)
    {
        fadeImg.gameObject.SetActive(true);
        float t = 0f;

        while (t < 1f)
        {
            t += Time.unscaledDeltaTime * normalFadeSpeed;
            float a = fadeCurve.Evaluate(t);
            fadeImg.color = new Color(fadeColor.r, fadeColor.g, fadeColor.b, a);
            yield return 0;
        }

        if (sceneIndex == 0)
            Time.timeScale = 1f;

        SceneManager.LoadScene(sceneIndex);
    }



    /// <summary>
    /// Augmente l'alpha du fondu pour faire disparaître la scène
    /// </summary>
    /// <returns></returns>
    public IEnumerator FadeOutSameScene()
    {
        fadeImg.gameObject.SetActive(true);


        float t = 0f;

        while (t < 1f)
        {
            t += Time.unscaledDeltaTime * normalFadeSpeed;
            float a = fadeCurve.Evaluate(t);
            fadeImg.color = new Color(fadeColor.r, fadeColor.g, fadeColor.b, a);
            yield return 0;
        }
        semaineImg.gameObject.SetActive(false);
    }







    /// <summary>
    /// Augmente l'alpha du fondu pour faire disparaître la scène et quitter le jeu.
    /// </summary>
    /// <returns></returns>
    public IEnumerator FadeQuit()
    {
        fadeImg.gameObject.SetActive(true);
        float t = 0f;

        while (t < 1f)
        {
            t += Time.unscaledDeltaTime * normalFadeSpeed;
            float a = fadeCurve.Evaluate(t);
            fadeImg.color = new Color(fadeColor.r, fadeColor.g, fadeColor.b, a);
            yield return 0;
        }

        Application.Quit();
    }
}
