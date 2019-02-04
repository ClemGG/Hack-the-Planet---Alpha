using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fin : MonoBehaviour
{
    public GameManager.Faction factionDeLaFin;
    public Sprite finSprite;
    public Page[] pages;

    [System.Serializable]
    public struct Page
    {
        [TextArea(10, 30)] public string pageText;
    }

}
