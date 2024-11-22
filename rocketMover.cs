using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class rocketMover : MonoBehaviour
{
    CSVReader csv;
    public GameObject gameObj;
    //public GameObject titleObj;
    public GameObject antennaObj;

    public TextMeshPro msg;
    //public TextMeshPro antennaMsg;
    private TextMeshProUGUI antennaTxt;
    [SerializeField] public Vector3[] storedPositions;
    [SerializeField] public float[] storedSpeed;
    [SerializeField] public int increaseSpeed=1;
    [SerializeField] AudioSource audioICPS;


    private int pointsIndex;

    void Awake()
    {
        csv = gameObj.GetComponent<CSVReader>();
        msg = GetComponentInChildren<TextMeshPro>();
        //antennaMsg = titleObj.GetComponent<TextMeshPro>();
        antennaTxt = antennaObj.GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        storedPositions = csv.artimiesPositions;
        storedSpeed = csv.artimiesSpeed;
    }

    void Update()
    {
        storedPositions = csv.artimiesPositions;
        storedSpeed = csv.artimiesSpeed;
        if (pointsIndex == 0)
        {
            transform.position = storedPositions[pointsIndex];
        }


        if(pointsIndex <= storedPositions.Length -1)
        {

            transform.position = Vector3.MoveTowards(transform.position, storedPositions[pointsIndex], storedSpeed[pointsIndex]*increaseSpeed*Time.deltaTime);

            if(transform.position == storedPositions[pointsIndex])
            {
                pointsIndex +=1;
                string availableAntenna = " ";
                string priorityAntenna = " ";
                float antenna = 0.0f;
                msg.text = "Mission Time : " + csv.myOrbitList.orbit[pointsIndex].missiontime + " mins" + "\n Velocity : " + storedSpeed[pointsIndex] + " km/s";
                if(pointsIndex == 116)
                {
                    PlayOrionICPS();
                }
                if (csv.myOrbitList.orbit[pointsIndex].WPSA == 1)
                {
                    availableAntenna = availableAntenna + "<color=green>WPSA</color> ";
                    if (antenna < float.Parse((csv.myOrbitList.orbit[pointsIndex].BudgetWPSA)))
                    {
                        priorityAntenna = "<color=red>WPSA</color>";
                        antenna = float.Parse(csv.myOrbitList.orbit[pointsIndex].BudgetWPSA);
                    }
                        
                }
                else
                {
                    availableAntenna = availableAntenna + "<color=white>WPSA</color> ";
                }
                if (csv.myOrbitList.orbit[pointsIndex].DS54 == 1)
                {
                    availableAntenna = availableAntenna + "<color=green>DS54</color> ";
                    if (antenna < float.Parse((csv.myOrbitList.orbit[pointsIndex].BudgetDS54)))
                    {
                        antenna = float.Parse(csv.myOrbitList.orbit[pointsIndex].BudgetDS54);
                        priorityAntenna = "<color=red>DS54</color>";
                    }
                }
                else
                {
                    availableAntenna = availableAntenna + "<color=white>DS54</color> ";
                }
                if (csv.myOrbitList.orbit[pointsIndex].DS24 == 1)
                {
                    availableAntenna = availableAntenna + "<color=green>DS24</color> ";
                    if (antenna < float.Parse((csv.myOrbitList.orbit[pointsIndex].BudgetDS24)))
                    {
                        antenna = float.Parse(csv.myOrbitList.orbit[pointsIndex].BudgetDS24);
                        priorityAntenna = "<color=red>DS24</color>";
                    }
                }
                else
                {
                    availableAntenna = availableAntenna + "<color=white>DS24</color> ";
                }
                if (csv.myOrbitList.orbit[pointsIndex].DS34 == 1)
                {
                    availableAntenna = availableAntenna + "<color=green>DS34</color> ";
                    if (antenna < float.Parse((csv.myOrbitList.orbit[pointsIndex].BudgetDS34)))
                    {
                        antenna = float.Parse(csv.myOrbitList.orbit[pointsIndex].BudgetDS34);
                        priorityAntenna = "<color=red>DS34</color> ";
                    }
                }
                else
                {
                    availableAntenna = availableAntenna + "<color=white>DS34</color> ";
                }
                //antennaMsg.richText = true;
                //antennaMsg.text = "Available Antennas = " + availableAntenna + "\n Priority Antenna = "+ priorityAntenna;
                antennaTxt.text = "Available Antennas = " + availableAntenna + "\nPriority Antenna = "+ priorityAntenna;
                antennaTxt.richText=true;
                //antennaTxt.text = "<b> Antenna Prioritization Info </b> \n Antenna availability Info will be updated soon";
                if(pointsIndex > 2500)
                {
                    msg.transform.localRotation=Quaternion.Euler(36.60f, 85.90f, -94.20f);
                }
                if(pointsIndex > 10000)
                {
                    msg.transform.localRotation=Quaternion.Euler(175.73f, 147.20f, -94.20f);
                }
            }
        }
    }

    void PlayOrionICPS()
    {
        audioICPS.Play();
    }
}
