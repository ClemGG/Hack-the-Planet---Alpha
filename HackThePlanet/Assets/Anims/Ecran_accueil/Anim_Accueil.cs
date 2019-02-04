using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim_Accueil : MonoBehaviour
{


    public void Quitter()
    {
        SceneFader.instance.FadeToQuitScene();
    }

    public void Next(int index)
    {
        SceneFader.instance.FadeToScene(index);
    }
}
