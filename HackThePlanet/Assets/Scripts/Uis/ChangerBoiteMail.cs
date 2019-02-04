using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangerBoiteMail : MonoBehaviour
{
    [SerializeField] private Transform buttonsContent, detailsContent;
    [SerializeField] private int startOnglet = 1, currentReceptionIndex = 0, currentContactIndex = 0;
    [SerializeField] private int[] startContactIndexes;
    [SerializeField] private ColorBlock mailLuColorBlock, mailNonLuColorBlock;

    [SerializeField] private Mail[] mails;

    private void Start()
    {

        AJOUTER_NOUVEAU_CONTACT(0);
        CHANGER_CONTACT(0);

        for (int i = 0; i < startContactIndexes.Length; i++)
        {
            if (startContactIndexes[i] > -1)
            {
                AJOUTER_NOUVEAU_MAIL(startContactIndexes[i]);
            }
        }

        SetupUIReception(0, true);


        CHANGER_ONGLET(startOnglet);

        detailsContent.GetChild(0).gameObject.SetActive(true);

        //for (int i = mails.Length-1; i > -1; i--)
        //{
        //    AJOUTER_NOUVEAU_MAIL(i);
        //    CHANGER_CONTACT(0);
        //}
        detailsContent.GetChild(1).gameObject.SetActive(false);
    }





    public void CHANGER_ONGLET(int ongletAOuvrir)
    {
        AudioManager.instance.Play("Selection");
        MenuDemarrerBoutons.instance.HIDE_MENU_DEMARRER();

        buttonsContent.GetChild(1).gameObject.SetActive(ongletAOuvrir == 1);
        buttonsContent.GetChild(2).gameObject.SetActive(ongletAOuvrir == 2);


        detailsContent.GetChild(0).gameObject.SetActive(ongletAOuvrir == 1);
        detailsContent.GetChild(1).gameObject.SetActive(ongletAOuvrir == 2);


    }


    public void CHANGER_CONTACT(int contactAOuvrir)
    {
        AudioManager.instance.Play("Selection");
        MenuDemarrerBoutons.instance.HIDE_MENU_DEMARRER();

        detailsContent.GetChild(1).gameObject.SetActive(true);


        for (int i = 0; i < detailsContent.GetChild(1).childCount; i++)
        {
            detailsContent.GetChild(1).GetChild(i).gameObject.SetActive(i == contactAOuvrir);
        }
    }

    public void CHANGER_MAIL(int mailAOuvrir)   //Appelée par les boutons
    {
        AudioManager.instance.Play("Selection");
        detailsContent.GetChild(0).gameObject.SetActive(true);


        Transform buttonToUpdate = buttonsContent.GetChild(1).GetChild(mailAOuvrir);
        buttonToUpdate.GetComponent<Button>().colors = mailLuColorBlock;
        mails[mailAOuvrir].marquéCommeLu = true;

        SetupUIReception(mailAOuvrir, true);

    }







    public void AJOUTER_NOUVEAU_MAIL(int mailAOuvrir)   //Peut être appelée par un script si l'on veut ajouter un nouvel email en cours de partie
    {
        Transform buttonToUpdate = buttonsContent.GetChild(1).GetChild(mailAOuvrir);
        buttonToUpdate.gameObject.SetActive(true);
        buttonToUpdate.GetComponent<Button>().colors = mailNonLuColorBlock;

        if (mails[mailAOuvrir].AjouterCeMailDansLesContacts)
        {
            AJOUTER_NOUVEAU_CONTACT(mailAOuvrir);
        }

        SetupUIReception(mailAOuvrir, false);
    }

    private void AJOUTER_NOUVEAU_CONTACT(int contactAAjouter)
    {
        buttonsContent.GetChild(2).GetChild(contactAAjouter).gameObject.SetActive(true);
        //buttonsContent.GetChild(2).GetChild(contactAAjouter).GetChild(0).GetComponent<Text>().text = string.Format("{0}   {1}", ">", mails[contactAAjouter].nomExpéditeur);

    }









    public void SetupUIReception(int mailAOuvrir, bool marquerCommeLu)
    {
        MenuDemarrerBoutons.instance.HIDE_MENU_DEMARRER();

        Mail mailToDisplay = mails[mailAOuvrir];
        Transform buttonToUpdate = buttonsContent.GetChild(1).GetChild(mailAOuvrir);

        if(marquerCommeLu)
        buttonToUpdate.GetComponent<Button>().colors = mailLuColorBlock;

        //On met à jour le texte des boutons, puis le panel des mails
        buttonToUpdate.GetChild(0).GetComponent<Text>().text = mails[mailAOuvrir].nomExpéditeur;
        buttonToUpdate.GetChild(1).GetComponent<Text>().text = mails[mailAOuvrir].objetDuMail;
        buttonToUpdate.GetChild(2).GetComponent<Text>().text = mails[mailAOuvrir].heureEnvoi;
        buttonToUpdate.GetChild(3).GetComponent<Text>().text = mails[mailAOuvrir].dateEnvoi;

        detailsContent.GetChild(0).GetChild(2).GetChild(0).GetComponent<Text>().text = mails[mailAOuvrir].texteDuMail;

        detailsContent.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = String.Format("{0}{1}", "EXPEDITEUR : ", mails[mailAOuvrir].nomExpéditeur); 
        detailsContent.GetChild(0).GetChild(0).GetChild(2).GetComponent<Text>().text = String.Format("{0}{1}", "DESTINATAIRE : ", mails[mailAOuvrir].nomDestinataire);
        detailsContent.GetChild(0).GetChild(0).GetChild(4).GetComponent<Text>().text = String.Format("{0}{1}", "OBJET : ", mails[mailAOuvrir].objetDuMail);
        detailsContent.GetChild(0).GetChild(0).GetChild(5).GetComponent<Text>().text = string.Format("Le {0}, à {1}", mails[mailAOuvrir].dateEnvoi, mails[mailAOuvrir].heureEnvoi);
    }
}
