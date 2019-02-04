using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HackPoints : MonoBehaviour
{
    [SerializeField] private Image barHackPoints;
    [SerializeField] private Text textHackPoints;
    [SerializeField] private int maxHackPoints;
    [HideInInspector] public int currentHackPoints;


    public static HackPoints instance;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(this);
            return;
        }

        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHackPoints = maxHackPoints;
        textHackPoints.text = string.Format("{0}/{1}", currentHackPoints.ToString(), maxHackPoints.ToString());
    }

    private void Update()
    {
        barHackPoints.fillAmount = Mathf.Lerp(barHackPoints.fillAmount, (float)currentHackPoints / (float)maxHackPoints, .5f);
    }


    // Update is called once per frame
    public void UpdateHackPointUI()
    {
        currentHackPoints--;
        textHackPoints.text = string.Format("{0}/{1}", currentHackPoints.ToString(), maxHackPoints.ToString());
    }
}
