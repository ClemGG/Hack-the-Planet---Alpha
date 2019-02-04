using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailCensurePartielle : Mail
{

    protected override void Start()
    {
        base.Start();
    }


    protected override void Mission()
    {
        if(ArticleEstAltéré() && !ArticleEstEntièrementCensuré())
        {
            DonnerRecompenseAuJoueur();
            base.Mission();
        }
    }
}
