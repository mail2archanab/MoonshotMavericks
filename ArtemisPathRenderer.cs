using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtemisPathRenderer : MonoBehaviour
{
    CSVReader csv;
    public GameObject gameObj;
    private LineRenderer lr;
    // Start is called before the first frame update

    void Start()
    {
        csv = gameObj.GetComponent<CSVReader>();
        lr = GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Sprites/Default"));

        var gradient = new Gradient();

/*
        //Blend color from green at 0% to red at 40% to blue at 70%
        var colors = new GradientColorKey[3];
        colors[0] = new GradientColorKey(Color.green, 0.0f);
        colors[1] = new GradientColorKey(Color.red, 0.40f);
        colors[2] = new GradientColorKey(Color.blue, 0.70f);
*/
        var colors = new GradientColorKey[3];
        colors[0] = new GradientColorKey(Color.green, 0.24f);
        colors[1] = new GradientColorKey(Color.red, 0.61f);
        colors[2] = new GradientColorKey(Color.blue, 1.0f);

        //Blend alpha from opaque at 0% to transparent at 100%

        var alphas = new GradientAlphaKey[3];
        alphas[0] = new GradientAlphaKey(1.0f, 1.0f);
        alphas[1] = new GradientAlphaKey(1.0f, 1.0f);
        alphas[2] = new GradientAlphaKey(1.0f, 1.0f);

        gradient.mode = GradientMode.Fixed;
        gradient.SetKeys(colors, alphas);

        lr.colorGradient = gradient;
        lr.positionCount = csv.artimiesPositions.Length;
        lr.SetPositions(csv.artimiesPositions);


        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
