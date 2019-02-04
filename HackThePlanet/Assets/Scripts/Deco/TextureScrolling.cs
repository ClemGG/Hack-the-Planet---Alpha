using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextureScrolling : MonoBehaviour
{
    [SerializeField] private float vitesseDefilement;
    [Range(0, 1)] [SerializeField] private float minFlickeringAmount;
    [Range(0, 1)] [SerializeField] private float maxFlickeringAmount;

    float offset;
    Image i;
    Color c;
    Material m;

    // Start is called before the first frame update
    void Start()
    {
        i = GetComponent<Image>();
        c = i.color;
        m = i.material;
    }

    // Update is called once per frame
    void Update()
    {
        offset += vitesseDefilement * Time.deltaTime;
        i.material.SetTextureOffset("_MainTex", new Vector2(0, offset));

        i.color = new Color(c.r, c.g, c.b, Random.Range(minFlickeringAmount, maxFlickeringAmount));
    }
}
