using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class CSVReader : MonoBehaviour
{

    public TextAsset textAssetData;

    [System.Serializable]

    public class Orbit
    {
        public float missiontime;
        public float Rx;
        public float Ry;
        public float Rz;
         public float Vx;
        public float Vy;
        public float Vz;
        public float MASS;
        public int WPSA;
        public string BudgetWPSA;
        public int DS54;
        public string BudgetDS54;
        public int DS24;
        public string BudgetDS24;
        public int DS34;
        public string BudgetDS34;

    }

    [System.Serializable]
    public class OrbitList
    {
        public Orbit[] orbit;
    }

    [SerializeField] public Vector3[] artimiesPositions;

    [SerializeField] public float[] artimiesSpeed;

    public OrbitList myOrbitList = new OrbitList();

    // Start is called before the first frame update
    
    void Awake()
    {
        ReadCSV();
        artimiesPositions = new Vector3[12978];
        artimiesSpeed = new float[12978];

        //artimiesPositions[0] = new Vector3(0,0,0);
        
        for (int i=0; i < 12978; i++)
        {
            artimiesPositions[i] = new Vector3((myOrbitList.orbit[i].Rx)/500, (myOrbitList.orbit[i].Ry)/500, (myOrbitList.orbit[i].Rz)/500);
            artimiesSpeed[i] = Mathf.Sqrt((myOrbitList.orbit[i].Vx * myOrbitList.orbit[i].Vx) + (myOrbitList.orbit[i].Vy * myOrbitList.orbit[i].Vy) + (myOrbitList.orbit[i].Vz * myOrbitList.orbit[i].Vz));

        }
    }

    void ReadCSV()
    {
        string[] data = textAssetData.text.Split(new string[] { ",", "\n"}, StringSplitOptions.None);

        int tableSize = data.Length / 17 - 1;
        myOrbitList.orbit = new Orbit[tableSize];

        for(int i=0; i<tableSize; i++)
        {
            myOrbitList.orbit[i] = new Orbit();
            myOrbitList.orbit[i].missiontime = float.Parse(data[17 * (i+1)]);
            myOrbitList.orbit[i].Rx = float.Parse(data[17 * (i+1) + 1]);
            myOrbitList.orbit[i].Ry = float.Parse(data[17 * (i+1) + 2]);
            myOrbitList.orbit[i].Rz = float.Parse(data[17 * (i+1) + 3]);
            myOrbitList.orbit[i].Vx = float.Parse(data[17 * (i+1) + 4]);
            myOrbitList.orbit[i].Vy = float.Parse(data[17 * (i+1) + 5]);
            myOrbitList.orbit[i].Vz = float.Parse(data[17 * (i+1) + 6]);
            myOrbitList.orbit[i].MASS = float.Parse(data[17 * (i+1) + 7]);
            myOrbitList.orbit[i].WPSA = int.Parse(data[17 * (i+1) + 8]);
            myOrbitList.orbit[i].BudgetWPSA = data[17 * (i+1) + 9];
            myOrbitList.orbit[i].DS54 = int.Parse(data[17 * (i+1) + 10]);
            myOrbitList.orbit[i].BudgetDS54 = data[17 * (i+1) + 11];
            myOrbitList.orbit[i].DS24 = int.Parse(data[17 * (i+1) + 12]);
            myOrbitList.orbit[i].BudgetDS24 = data[17 * (i+1) + 13];
            myOrbitList.orbit[i].DS34 = int.Parse(data[17 * (i+1) + 14]);
            myOrbitList.orbit[i].BudgetDS34 = data[17 * (i+1) + 15];
        }
    }
}
