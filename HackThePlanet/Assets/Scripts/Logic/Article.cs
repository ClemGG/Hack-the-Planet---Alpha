using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Article : MonoBehaviour
{
    Transform t;
    public GameObject articleCanvas, blocTexteVide;

    [Space(10)]

    public Button titreBtn, imgBtn, texteBtn;
    [HideInInspector] public Image imageArticle, logoArticle;
    [HideInInspector] public Text titreArticle, texteArticle;

    [Space(10)]

    public float imageFadeSpeed = .5f;
    public float texteTypeSpeed = .01f;
    public Sprite imageOriginale, imageDeRemplacement, imageLogoJournal;
    [TextArea(2, 10)] public string titreOriginal, titreDeRemplacement;
    [TextArea(5, 10)] public string texteOriginal, texteDeRemplacement;

    [Space(10)]

    public GameManager.Faction factionAvantChangement;
    public GameManager.Faction factionAprèsChangement;
    public int argentMinAGagner, argentMaxAGagner;
    public bool gagnerUneSeuleFoisArgentParCensuee;
    [HideInInspector] public int humainPts, androidePts, argentPts;


    public delegate void OnArticleCensuré();
    public OnArticleCensuré onArticleCensuré;



    private void Awake()
    {
        t = transform;
        imageArticle = t.GetChild(3).GetComponent<Image>();
        logoArticle = t.GetChild(1).GetComponent<Image>();
        titreArticle = t.GetChild(0).GetComponent<Text>();
        texteArticle = t.GetChild(4).GetChild(0).GetComponent<Text>();
        

        titreArticle.text = titreOriginal;
        texteArticle.text = texteOriginal;
        imageArticle.sprite = imageOriginale;
        logoArticle.sprite = imageLogoJournal;

        texteBtn.interactable = texteOriginal != string.Empty;
        blocTexteVide.SetActive(texteOriginal == string.Empty);


        onArticleCensuré += CalculerPointsArticle;
    }





    private void CalculerPointsArticle()
    {
        if(factionAprèsChangement == factionAvantChangement)
        {
            if(factionAprèsChangement == GameManager.Faction.Humain)
            {
                humainPts++;
                argentPts += Random.Range(argentMinAGagner, argentMaxAGagner);
            }
            else if(factionAprèsChangement == GameManager.Faction.Androide)
            {
                androidePts++;
            }
        }
        else
        {
            if (factionAprèsChangement == GameManager.Faction.Humain)
            {
                humainPts++;
                androidePts--;
                argentPts += Random.Range(argentMinAGagner, argentMaxAGagner);
            }
            else if (factionAprèsChangement == GameManager.Faction.Androide)
            {
                androidePts++;
                humainPts--;
            }
        }

        if (gagnerUneSeuleFoisArgentParCensuee)
        {
            onArticleCensuré -= CalculerPointsArticle;
        }
    }






    public void ActivateChoiceUI(int setupTitreOuImage)  //Appelée par les boutons de l'article quand on clique dessus
    {
        AudioManager.instance.Play("ActionPointAquired");

        MenuDemarrerBoutons.instance.HIDE_MENU_DEMARRER();

        if(HackPoints.instance.currentHackPoints > 0)
        {
            ChoiceUI.instance.articleEnCours = this;
            ChoiceUI.instance.SetupNewImageAndTitle(setupTitreOuImage);
        }
    }











    public IEnumerator ChangerTitre()
    {
        titreBtn.enabled = false;

        while (titreArticle.text.Length > 0)
        {
            AudioManager.instance.Play("ChangingText");
            titreArticle.text = titreArticle.text.Remove(titreArticle.text.Length - 1);
            yield return new WaitForSeconds(texteTypeSpeed);
        }
        AudioManager.instance.Stop("ChangingText");

        StringBuilder sb = new StringBuilder(titreDeRemplacement.Length);
        int index = 0;
        yield return new WaitForSeconds(.5f);

        while (sb.ToString().CompareTo(titreDeRemplacement) != 0)
        {
            AudioManager.instance.Play("ChangingText");
            sb.Append(titreDeRemplacement[index]);
            titreArticle.text = sb.ToString();
            yield return new WaitForSeconds(texteTypeSpeed);
            index++;
        }

        AudioManager.instance.Stop("ChangingText");
        ChoiceUI.instance.StopCoroutine(ChoiceUI.instance.titreco);
        ChoiceUI.instance.titreco = null;
    }





    public IEnumerator ChangerTextePrincipal()
    {
        texteBtn.enabled = false;

        while (texteArticle.text.Length > 0)
        {
            AudioManager.instance.Play("ChangingText");
            texteArticle.text = texteArticle.text.Remove(texteArticle.text.Length - 1);
            yield return new WaitForSeconds(texteTypeSpeed);
        }
        AudioManager.instance.Stop("ChangingText");

        StringBuilder sb = new StringBuilder(texteDeRemplacement.Length);
        int index = 0;
        yield return new WaitForSeconds(.5f);

        while (sb.ToString().CompareTo(texteDeRemplacement) != 0)
        {
            AudioManager.instance.Play("ChangingText");
            sb.Append(texteDeRemplacement[index]);
            texteArticle.text = sb.ToString();
            yield return new WaitForSeconds(texteTypeSpeed);
            index++;
        }

        AudioManager.instance.Stop("ChangingText");

        ChoiceUI.instance.StopCoroutine(ChoiceUI.instance.texteco);
        ChoiceUI.instance.texteco = null;

    }





    public IEnumerator ChangerImage()
    {
        AudioManager.instance.Play("DocumentOpening");
        imgBtn.enabled = false;
        Color c = imageArticle.color;
        float t = 1f;

        while (t > 0f)
        {
            t -= Time.deltaTime * imageFadeSpeed;
            imageArticle.color = new Color(c.r, c.g, c.b, t);
            yield return null;
        }
        
        imageArticle.sprite = imageDeRemplacement;
        t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * imageFadeSpeed;
            imageArticle.color = new Color(c.r, c.g, c.b, t);
            yield return null;
        }

        ChoiceUI.instance.StopCoroutine(ChoiceUI.instance.imgco);
        ChoiceUI.instance.imgco = null;

    }





}
