using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangerOnglet : MonoBehaviour
{
    [SerializeField] private GameObject ecranArticle, ecranMail, ecranCoin, fondRessources;
    [SerializeField] private int startOnglet = 1;


    private void Start()
    {
        //CHANGER_ONGLET(startOnglet);
        ecranCoin.transform.GetChild(1).GetChild(0).GetChild(2).GetComponent<Text>().text = string.Format("{0} $", GameManager.argent.ToString());
        fondRessources.transform.GetChild(2).GetComponent<Text>().text = string.Format("{0} $", GameManager.argent.ToString());

        ecranCoin.transform.GetChild(2).gameObject.SetActive(GameManager.argent >= 10000);
    }

    public void CHANGER_ONGLET(int ongletAOuvrir)
    {
        AudioManager.instance.Play("Selection");

        if (ChoiceUI.instance.titreco != null || ChoiceUI.instance.texteco != null || ChoiceUI.instance.imgco != null)
        {
            return;
        }


        ChoiceUI.instance.HideChoiceUI();
        MenuDemarrerBoutons.instance.HIDE_MENU_DEMARRER();

        ecranArticle.SetActive(ongletAOuvrir == 0);
        ecranMail.SetActive(ongletAOuvrir == 1);
        ecranCoin.SetActive(ongletAOuvrir == 2);
    }

}
