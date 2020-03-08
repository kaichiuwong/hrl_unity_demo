using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MLAgents;

public class BananaAcademy : Academy
{
    [HideInInspector]
    public GameObject[] agents;
    [HideInInspector]
    public BananaArea[] listArea;

    public int bluetotalScore = 0;
    public int redtotalScore = 0;
    public GameObject scoreText;
    private Text textComponent;
    public override void AcademyReset()
    {
        ClearObjects(GameObject.FindGameObjectsWithTag("banana"));
        ClearObjects(GameObject.FindGameObjectsWithTag("badBanana"));

        agents = GameObject.FindGameObjectsWithTag("agent");
        textComponent = scoreText.GetComponent<Text>();
        listArea = FindObjectsOfType<BananaArea>();
        foreach (BananaArea ba in listArea)
        {
            ba.ResetBananaArea(agents);
        }
    }

    void ClearObjects(GameObject[] objects)
    {
        foreach (GameObject bana in objects)
        {
            Destroy(bana);
        }
    }

    public override void AcademyStep()
    {
        textComponent.text = string.Format(@"Score: {0}", redtotalScore);
    }

    public void SetText(string msg)
    {
        textComponent.text = msg;
    }
}
