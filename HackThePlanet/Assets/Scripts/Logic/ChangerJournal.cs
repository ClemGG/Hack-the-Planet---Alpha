using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangerJournal : MonoBehaviour
{
    public Transform journalContent;
    Transform articleContent;

    public Journal[] journaux;
    Journal journalActuel;
    Article articleActuel;

    int[] currentArticleIndexes;
    int currentJournalIndex = 0;




    // Start is called before the first frame update
    void Start()
    {
        currentArticleIndexes = new int[journaux.Length];
        UpdateArticleUI();

    }




    private void UpdateArticleUI()
    {

        ChoiceUI.instance.HideChoiceUI();

        if (journaux.Length > 0)
        {
            journalActuel = journaux[currentJournalIndex];
            articleActuel = journalActuel.articlesDuJournal[currentArticleIndexes[currentJournalIndex]];
        }

        for (int i = 0; i < journalContent.childCount; i++)
        {
            journalContent.GetChild(i).gameObject.SetActive(i == currentJournalIndex);

            if(journalContent.GetChild(i).gameObject.activeSelf)
            articleContent = journalContent.GetChild(i);
        }

        for (int i = 0; i < articleContent.childCount; i++)
        {
            articleContent.GetChild(i).gameObject.SetActive(i == currentArticleIndexes[currentJournalIndex]);
        }
    }





    public void CHOISIR_ARTICLE_SUIVANT(int unOuMoinsUn)    //Appelée par les flèches
    {
        if(ChoiceUI.instance.titreco != null || ChoiceUI.instance.texteco != null || ChoiceUI.instance.imgco != null)
        {
            return;
        }


        AudioManager.instance.Play("Selection");

        if (currentArticleIndexes[currentJournalIndex] < journalActuel.articlesDuJournal.Length - 1 && currentArticleIndexes[currentJournalIndex] > 0)
            currentArticleIndexes[currentJournalIndex] += unOuMoinsUn;

        if(currentArticleIndexes[currentJournalIndex] < journalActuel.articlesDuJournal.Length - 1 && Mathf.Sign(unOuMoinsUn) == 1)
            currentArticleIndexes[currentJournalIndex]++;
        else if(currentArticleIndexes[currentJournalIndex] > 0 && Mathf.Sign(unOuMoinsUn) == -1)
            currentArticleIndexes[currentJournalIndex]--;


        //print(currentArticleIndexes[currentJournalIndex]);

        UpdateArticleUI();
    }



    public void CHOISIR_AUTRE_JOURNAL(int journalIndex)    //Appelée par les boutons du panel de gauche
    {
        AudioManager.instance.Play("Selection");

        currentJournalIndex = journalIndex;

        UpdateArticleUI();
    }
}
