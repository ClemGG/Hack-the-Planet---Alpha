using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMusic : MonoBehaviour
{
    public string nomMusique;


    private void Start()
    {
        FindObjectOfType<AudioManager>().Play(nomMusique);
    }
}
