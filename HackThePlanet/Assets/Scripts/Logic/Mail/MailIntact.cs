﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailIntact : Mail
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Mission()
    {
        if (ArticleEstIntact())
        {
            DonnerRecompenseAuJoueur();
            base.Mission();
        }
    }
}
