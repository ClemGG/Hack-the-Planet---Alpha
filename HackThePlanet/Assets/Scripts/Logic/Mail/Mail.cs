
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mail : MonoBehaviour
{
    public bool AjouterCeMailDansLesContacts = true, marquéCommeLu = false;

    public string nomExpéditeur, nomDestinataire, objetDuMail, dateEnvoi, heureEnvoi;
    [TextArea(10, 30)] public string texteDuMail;



    public int argentSupplémentaire, humainPtsSuppémentaires, androidePtsSupplémentaires;
    public Article articleLié;

    protected virtual void Start()
    {
        AddMissionToArticle();
    }


    public virtual void AddMissionToArticle()
    {
        if (!articleLié)
            return;
        
        MenuDemarrerBoutons.instance.onArticleIntact += Mission;
    }




    protected virtual void Mission()
    {
        MenuDemarrerBoutons.instance.onArticleIntact -= Mission;
    }

    protected void DonnerRecompenseAuJoueur()
    {
        if (marquéCommeLu)
        {
            articleLié.argentPts += argentSupplémentaire;
            articleLié.humainPts += humainPtsSuppémentaires;
            articleLié.androidePts += androidePtsSupplémentaires;
        }
    }



    protected bool ArticleEstIntact()
    {
        if(articleLié.texteArticle != null)
        {

            return articleLié.imageArticle.sprite != articleLié.imageDeRemplacement &&
                   articleLié.titreArticle.text != articleLié.titreDeRemplacement &&
                   articleLié.texteArticle.text != articleLié.texteDeRemplacement;
        }


        return articleLié.imageArticle.sprite != articleLié.imageDeRemplacement &&
               articleLié.titreArticle.text != articleLié.titreDeRemplacement;
    }

    protected bool ArticleEstEntièrementCensuré()
    {
        if (articleLié.texteArticle != null)
        {

            return articleLié.imageArticle.sprite == articleLié.imageDeRemplacement &&
                   articleLié.titreArticle.text == articleLié.titreDeRemplacement &&
                   articleLié.texteArticle.text == articleLié.texteDeRemplacement;
        }


        return articleLié.imageArticle.sprite == articleLié.imageDeRemplacement &&
               articleLié.titreArticle.text == articleLié.titreDeRemplacement;
    }


    protected bool ArticleEstAltéré()
    {
        if (articleLié.texteArticle != null)
        {

            return articleLié.imageArticle.sprite != articleLié.imageDeRemplacement ||
                   articleLié.titreArticle.text != articleLié.titreDeRemplacement ||
                   articleLié.texteArticle.text != articleLié.texteDeRemplacement;
        }


        return articleLié.imageArticle.sprite != articleLié.imageDeRemplacement ||
               articleLié.titreArticle.text != articleLié.titreDeRemplacement;
    }
}
