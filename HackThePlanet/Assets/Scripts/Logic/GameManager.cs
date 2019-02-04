using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameManager
{
    public static int argent, humainCompteur, androideCompteur;
    public enum Faction { Humain, Androide, Neutre, Maria};
    public static Faction factionDuJoueur;
    


    public static void CalculerPointsDuJoueur()
    {
        Article[] tousLesArticlesDeLaPartie = Resources.FindObjectsOfTypeAll(typeof(Article)) as Article[];

        for (int i = 0; i < tousLesArticlesDeLaPartie.Length; i++)
        {
            Article a = tousLesArticlesDeLaPartie[i];
            AjouterPointsAuScore(a.humainPts, a.androidePts, a.argentPts);
        }
    }

    private static void AjouterPointsAuScore(int humainPts, int androidePts, int argentPts)
    {
        humainCompteur += humainPts;
        androideCompteur += androidePts;
        argent += argentPts;


        CalculerFaction();
    }






    private static void CalculerFaction()
    {
        bool lesDeuxSontProches = Mathf.Max(androideCompteur, humainCompteur) - Mathf.Min(androideCompteur, humainCompteur) < 5f;

        if (humainCompteur > androideCompteur && !lesDeuxSontProches)
        {
            factionDuJoueur = Faction.Humain;
        }
        else if (androideCompteur > humainCompteur && !lesDeuxSontProches)
        {
            factionDuJoueur = Faction.Androide;
        }
        else if (lesDeuxSontProches)
        {
            factionDuJoueur = Faction.Neutre;
        }
    }

}
