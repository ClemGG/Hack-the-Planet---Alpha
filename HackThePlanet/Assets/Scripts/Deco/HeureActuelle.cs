using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeureActuelle : MonoBehaviour
{

    Text textHeureActuelle;


    // Start is called before the first frame update
    void Start()
    {
        textHeureActuelle = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        textHeureActuelle.text = System.DateTime.Now.ToString("HH : mm");
    }
}
