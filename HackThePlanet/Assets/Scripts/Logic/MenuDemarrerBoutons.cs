using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class MenuDemarrerBoutons : MonoBehaviour
{
    [SerializeField] private GameObject demarrerCanvas, optionsCanvas, menuCanvas;

    [SerializeField] private Image volumeFillBar;
    [SerializeField] private AudioSource[] allAudios;

    [SerializeField] private GameObject[] prefabsToEnableOnPlay;    // Ne pas oublier de vider ce tableau pour les prochains niveaux


    public delegate void OnArticleIntact();
    public OnArticleIntact onArticleIntact;


    public static MenuDemarrerBoutons instance;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(this);
            return;
        }

        instance = this;

        GetComponent<Animator>().enabled = false;
    }


    private void OnLevelWasLoaded(int level)
    {

        if (level != 0)
        {
            DestroyImmediate(menuCanvas);
        }
    }

    private void Start()
    {
        if (!PlayerPrefs.HasKey("volume"))
        {
            PlayerPrefs.SetFloat("volume", .5f);
        }

        demarrerCanvas.SetActive(false);
        optionsCanvas.SetActive(false);

        if (menuCanvas)
            menuCanvas.SetActive(true);


        volumeFillBar.fillAmount = PlayerPrefs.GetFloat("volume", .5f);
        allAudios = FindObjectsOfType<AudioSource>();

        AudioManager.instance.SetupVolume();

        for (int i = 0; i < prefabsToEnableOnPlay.Length; i++)
        {
            prefabsToEnableOnPlay[i].SetActive(false);
        }
    }










    public void MENU_DEMARRER()
    {
        AudioManager.instance.Play("ActionPointAquired");
        ChoiceUI.instance.HideChoiceUI();
        demarrerCanvas.SetActive(!demarrerCanvas.activeSelf);
        optionsCanvas.SetActive(false);
    }


    public void JOUER()
    {
        AudioManager.instance.Play("Selection");
        StartCoroutine(SceneFader.instance.StartSemaine());
    }

    public void StartGame()
    {
        optionsCanvas.SetActive(false);

        if (menuCanvas)
            menuCanvas.SetActive(false);

        for (int i = 0; i < prefabsToEnableOnPlay.Length; i++)
        {
            prefabsToEnableOnPlay[i].SetActive(true);
        }

        GameObject.Find("GameLogic").GetComponent<ChangerOnglet>().CHANGER_ONGLET(1);

    }




    public void HIDE_MENU_DEMARRER()
    {
        demarrerCanvas.SetActive(false);
        optionsCanvas.SetActive(false);
    }
    
    public void OPTIONS()
    {
        AudioManager.instance.Play("Selection");
        optionsCanvas.SetActive(!optionsCanvas.activeSelf);
    }

    public void METTRE_A_JOUR_ET_REDEMARRER()
    {
        AudioManager.instance.Play("Selection");
        onArticleIntact?.Invoke();


        GameManager.CalculerPointsDuJoueur();

        if (SceneManager.GetActiveScene().buildIndex < 4)
            JouerAnimAccueil(3);
        else
            JouerAnimAccueil(1);

    }


    public void PARTIR_AVEC_MARIA()
    {
        AudioManager.instance.Play("Selection");
        if (GameManager.argent >= 10000)
        {
            GameManager.factionDuJoueur = GameManager.Faction.Maria;
            JouerAnimAccueil(1);
        }
    }


    public void ARRETER()
    {
        AudioManager.instance.Play("Selection");
        JouerAnimAccueil(2);
    }




    public void JouerAnimAccueil(int animNumber)
    {
        HIDE_MENU_DEMARRER();

        Animator a = GetComponent<Animator>();
        a.enabled = true;
        AudioManager.instance.Play("Computer_StartUp");

        if (animNumber == 0)
        {
            a.Play("a_accueil_start");
            
        }
        else if (animNumber == 1)
        {
            a.Play("a_accueil_partir");

        }
        else if (animNumber == 2)
        {
            a.Play("a_accueil_quit");
        }
        else if (animNumber == 3)
        {
            StartCoroutine(CoNiveauSuivant(a));
            
        }
    }

    private IEnumerator CoNiveauSuivant(Animator a)
    {
        a.Play("a_accueil_next");
        yield return new WaitForSeconds(2f);
        SceneFader.instance.FadeToScene(SceneManager.GetActiveScene().buildIndex + 1);
    }





    public void MONTER_BAISSER_VOLUME(float volumeCoef)
    {
        AudioManager.instance.Play("Selection");

        volumeFillBar.fillAmount = (float)Mathf.RoundToInt(volumeFillBar.fillAmount * 10f + volumeCoef) / 10f;

        PlayerPrefs.SetFloat("volume", volumeFillBar.fillAmount);

        AudioManager.instance.SetupVolume();

    }
}
