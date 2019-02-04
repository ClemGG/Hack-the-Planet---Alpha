using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailCensureTotale : Mail
{


    protected override void Start()
    {
        base.Start();
    }

    protected override void Mission()
    {
        if (ArticleEstEntièrementCensuré())
        {
            DonnerRecompenseAuJoueur();
            base.Mission();
        }
    }
}
