using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceUI : MonoBehaviour
{
    public GameObject imageCanvas, titreCanvas, texteCanvas;
    [HideInInspector] public Article articleEnCours;


    [HideInInspector] public Coroutine titreco, texteco, imgco;

    public static ChoiceUI instance;



    private void Awake()
    {
        if(instance != null)
        {
            Destroy(this);
            return;
        }

        instance = this;
    }





    // Start is called before the first frame update
    void Start()
    {
        HideChoiceUI();
    }






    
    public void ModifierArticle(int censurerTitreOuImage)
    {
        AudioManager.instance.Play("Selection");
        if (censurerTitreOuImage == 0)
        {
            titreco = StartCoroutine(articleEnCours.ChangerTitre());
        }
        else if(censurerTitreOuImage == 1)
        {
            imgco = StartCoroutine(articleEnCours.ChangerImage());
        }
        else
        {
            texteco = StartCoroutine(articleEnCours.ChangerTextePrincipal());
        }

        articleEnCours.onArticleCensuré?.Invoke();

        HackPoints.instance.UpdateHackPointUI();


        imageCanvas.SetActive(false);
        titreCanvas.SetActive(false);
        texteCanvas.SetActive(false);
    }

    public void HideChoiceUI()
    {

        imageCanvas.SetActive(false);
        titreCanvas.SetActive(false);
        texteCanvas.SetActive(false);
    }




    public void SetupNewImageAndTitle(int setupTitreOuImage)
    {
        if(setupTitreOuImage == 0)
        {
            titreCanvas.SetActive(!titreCanvas.activeSelf);
            imageCanvas.SetActive(false);
            texteCanvas.SetActive(false);
        }
        else if(setupTitreOuImage == 1)
        {
            imageCanvas.SetActive(!imageCanvas.activeSelf);
            titreCanvas.SetActive(false);
            texteCanvas.SetActive(false);
        }
        else
        {
            texteCanvas.SetActive(!texteCanvas.activeSelf);
            titreCanvas.SetActive(false);
            imageCanvas.SetActive(false);
        }


        imageCanvas.transform.GetChild(0).GetComponent<Image>().sprite = articleEnCours.imageDeRemplacement;
        titreCanvas.transform.GetChild(0).GetComponent<Text>().text = string.Format("{0}{1}", "> ", articleEnCours.titreDeRemplacement);
        texteCanvas.transform.GetChild(0).GetComponent<Text>().text = string.Format("{0}{1}", "> ", articleEnCours.texteDeRemplacement);
    }
}
