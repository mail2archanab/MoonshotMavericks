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
    private GameObject icpsModule;
    public GameObject missionTimeObj;
    public GameObject trajectoryInfoObj;
    public int curSwitchAntenna;
    [SerializeField] public Slider missionTimeSlider;    
    public TextMeshPro msg;
    //public TextMeshPro antennaMsg;
    private TextMeshProUGUI antennaTxt;
    private TextMeshProUGUI missionTimeTxt;
    private TextMeshProUGUI trajectoryInfoTxt;
    [SerializeField] public Vector3[] storedPositions;
    [SerializeField] public float[] storedSpeed;
    [SerializeField] public int increaseSpeed=1;
    [SerializeField] AudioSource audioICPS;
    private float distance = 0;


    private int pointsIndex;

    void Awake()
    {
        csv = gameObj.GetComponent<CSVReader>();
        msg = GetComponentInChildren<TextMeshPro>();
        //antennaMsg = titleObj.GetComponent<TextMeshPro>();
        antennaTxt = antennaObj.GetComponent<TextMeshProUGUI>();
        missionTimeTxt = missionTimeObj.GetComponent<TextMeshProUGUI>();
        trajectoryInfoTxt = trajectoryInfoObj.GetComponent<TextMeshProUGUI>();
        icpsModule = GameObject.Find("ICPSModule");
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
            if (pointsIndex <= 116)
            {
                icpsModule.transform.position = Vector3.MoveTowards(transform.position, storedPositions[pointsIndex], storedSpeed[pointsIndex]*increaseSpeed*Time.deltaTime);
            }
            if ((pointsIndex >= 117) && (pointsIndex < 122))
            {
                icpsModule.transform.position = Vector3.MoveTowards(transform.position, storedPositions[pointsIndex], storedSpeed[pointsIndex]*increaseSpeed*Time.deltaTime*0.5f);
                icpsModule.transform.localScale = new Vector3(2,2,2);

            }
            if (pointsIndex == 138)
            {
                icpsModule.transform.localScale = new Vector3(0,0,0);
            }
            if(transform.position == storedPositions[pointsIndex])
            {
                pointsIndex +=1;
                missionTimeSlider.value = csv.myOrbitList.orbit[pointsIndex].missiontime;
                distance = distance + Mathf.Sqrt(((csv.myOrbitList.orbit[pointsIndex].Rx - csv.myOrbitList.orbit[pointsIndex-1].Rx) * (csv.myOrbitList.orbit[pointsIndex].Rx - csv.myOrbitList.orbit[pointsIndex-1].Rx)) + ((csv.myOrbitList.orbit[pointsIndex].Ry - csv.myOrbitList.orbit[pointsIndex-1].Ry) * (csv.myOrbitList.orbit[pointsIndex].Ry - csv.myOrbitList.orbit[pointsIndex-1].Ry)) + ((csv.myOrbitList.orbit[pointsIndex].Rz - csv.myOrbitList.orbit[pointsIndex-1].Rz) * (csv.myOrbitList.orbit[pointsIndex].Rz - csv.myOrbitList.orbit[pointsIndex-1].Rz)));
                msg.text = "Distance Traveled: " + distance + " km" + "\n Velocity : " + storedSpeed[pointsIndex] + " km/s";
                missionTimeTxt.text = "Mission Time : " + csv.myOrbitList.orbit[pointsIndex].missiontime + " mins";
                trajectoryInfoTxt.text = "<b>Trajectory Information </b> \nMission Time : " + csv.myOrbitList.orbit[pointsIndex].missiontime + " mins \nVelocity : " + storedSpeed[pointsIndex] + " km/s \nDistance Traveled : " + distance + " km";

 /*               //Antenna availability and prioratization based on link budget 
                string availableAntenna = " ";
                string priorityAntenna = " ";
                float antenna = 0.0f;
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
                antennaTxt.text = "Available Antennas = " + availableAntenna + "\nPriority Antenna(Link Budget) = "+ priorityAntenna;

*/
                //Antenna availability and prioratization 
                string availableAntenna = " ";
                string priorityAntenna = "Not Available";
                float antenna = 0.0f;
                int switchAvailable = 0;
                bool doSwitch = true;
                if (csv.myOrbitList.orbit[pointsIndex].WPSA == 1)
                {
                    availableAntenna = availableAntenna + "<color=green>WPSA</color> ";
                    if (antenna < float.Parse((csv.myOrbitList.orbit[pointsIndex].BudgetWPSA)))
                    {
                        priorityAntenna = "<color=red>WPSA</color>";
                        antenna = float.Parse(csv.myOrbitList.orbit[pointsIndex].BudgetWPSA);
                    }
                    if (curSwitchAntenna == 1)
                    {
                        doSwitch = false;
                    }
                    else
                    {
                        switchAvailable += 1;
                    }
                        
                }
                else
                {
                    availableAntenna = availableAntenna + "<color=white>WPSA</color> ";
                    if (curSwitchAntenna == 1)
                    {
                        doSwitch = true;
                        curSwitchAntenna=0;
                    }
                }
                if (csv.myOrbitList.orbit[pointsIndex].DS54 == 1)
                {
                    availableAntenna = availableAntenna + "<color=green>DS54</color> ";
                    if (antenna < float.Parse((csv.myOrbitList.orbit[pointsIndex].BudgetDS54)))
                    {
                        antenna = float.Parse(csv.myOrbitList.orbit[pointsIndex].BudgetDS54);
                        priorityAntenna = "<color=red>DS54</color>";
                    }
                    if (curSwitchAntenna == 2)
                    {
                        doSwitch = false;
                    }
                    else
                    {
                        switchAvailable += 1;
                    }
                }
                else
                {
                    availableAntenna = availableAntenna + "<color=white>DS54</color> ";
                    if (curSwitchAntenna == 2)
                    {
                        doSwitch = true;
                        curSwitchAntenna=0;
                    }
                }
                if (csv.myOrbitList.orbit[pointsIndex].DS24 == 1)
                {
                    availableAntenna = availableAntenna + "<color=green>DS24</color> ";
                    if (antenna < float.Parse((csv.myOrbitList.orbit[pointsIndex].BudgetDS24)))
                    {
                        antenna = float.Parse(csv.myOrbitList.orbit[pointsIndex].BudgetDS24);
                        priorityAntenna = "<color=red>DS24</color>";
                    }
                    if (curSwitchAntenna == 3)
                    {
                        doSwitch = false;
                    }
                    else
                    {
                        switchAvailable += 1;
                    }
                }
                else
                {
                    availableAntenna = availableAntenna + "<color=white>DS24</color> ";
                    if (curSwitchAntenna == 3)
                    {
                        doSwitch = true;
                        curSwitchAntenna=0;
                    }
                }
                if (csv.myOrbitList.orbit[pointsIndex].DS34 == 1)
                {
                    availableAntenna = availableAntenna + "<color=green>DS34</color> ";
                    if (antenna < float.Parse((csv.myOrbitList.orbit[pointsIndex].BudgetDS34)))
                    {
                        antenna = float.Parse(csv.myOrbitList.orbit[pointsIndex].BudgetDS34);
                        priorityAntenna = "<color=red>DS34</color> ";
                    }
                    if (curSwitchAntenna == 4)
                    {
                        doSwitch = false;
                    }
                    else
                    {
                        switchAvailable += 1;
                    }
                }
                else
                {
                    availableAntenna = availableAntenna + "<color=white>DS34</color> ";
                    if (curSwitchAntenna == 4)
                    {
                        doSwitch = true;
                        curSwitchAntenna=0;
                    }
                }
                antennaTxt.text = "Antennas = " + availableAntenna + "\nPriority Antenna (Link Budget) = "+ priorityAntenna;

                if (doSwitch == true & switchAvailable >0)
                {
                    switchAntenna(pointsIndex);
                }
                if (curSwitchAntenna == 1)
                {
                    antennaTxt.text = antennaTxt.text + "\nPriority Antenna (Least Switch) = "+ "<color=yellow>WPSA</color>";
                }
                else if (curSwitchAntenna == 2)
                {
                    antennaTxt.text = antennaTxt.text + "\nPriority Antenna (Least Switch) = "+ "<color=yellow>DS54</color>";
                }
                else if (curSwitchAntenna == 3)
                {
                    antennaTxt.text = antennaTxt.text + "\nPriority Antenna (Least Switch) = "+ "<color=yellow>DS24</color>";
                }
                else if (curSwitchAntenna == 4)
                {
                    antennaTxt.text = antennaTxt.text + "\nPriority Antenna (Least Switch) = "+ "<color=yellow>DS34</color>";
                }
                else
                {
                    antennaTxt.text = antennaTxt.text + "\nPriority Antenna (Least Switch) = "+ "Not Available";
                }
                
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

       void switchAntenna(int i)
    {
        int cWPSA=0;
        int cDS54=0;
        int cDS24=0;
        int cDS34=0;
        for (int x=i; x < storedPositions.Length -1; x++)
        {
            if(csv.myOrbitList.orbit[x].WPSA == 1)
            {
                cWPSA+=1;
            }
            else
            {
                break;
            }
        }
        for (int x=i; x < storedPositions.Length -1; x++)
        {
            if(csv.myOrbitList.orbit[x].DS54 == 1)
            {
                cDS54+=1;
            }
            else
            {
                break;
            }
        }
        for (int x=i; x < storedPositions.Length -1; x++)
        {
            if(csv.myOrbitList.orbit[x].DS24 == 1)
            {
                cDS24+=1;
            }
            else
            {
                break;
            }
        }
        for (int x=i; x < storedPositions.Length -1; x++)
        {
            if(csv.myOrbitList.orbit[x].DS34 == 1)
            {
                cDS34+=1;
            }
            else
            {
                break;
            }
        }

        if ((cWPSA > cDS54) & (cWPSA > cDS24) & (cWPSA > cDS34))
        {
            curSwitchAntenna = 1;
        }
        else if ((cDS54 > cWPSA) & (cDS54 > cDS24) & (cDS54 > cDS34))
        {
            curSwitchAntenna = 2;
        }
        else if ((cDS24 > cDS54) & (cDS24 > cWPSA) & (cDS24 > cDS34))
        {
            curSwitchAntenna = 3;
        }
        else if ((cDS34 > cDS54) & (cDS34 > cDS24) & (cDS34 > cWPSA))
        {
            curSwitchAntenna = 4;
        }
        else
        {
            curSwitchAntenna = 0;
        }
    }

}
