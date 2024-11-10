using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocketMover : MonoBehaviour
{
    CSVReader csv;
    public GameObject gameObj;
    [SerializeField] public Vector3[] storedPositions;
    [SerializeField] public float[] storedSpeed;

    private int pointsIndex;

    void Awake()
    {
        csv = gameObj.GetComponent<CSVReader>();
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

            transform.position = Vector3.MoveTowards(transform.position, storedPositions[pointsIndex], storedSpeed[pointsIndex]*Time.deltaTime);

            if(transform.position == storedPositions[pointsIndex])
            {
                pointsIndex +=1;
            }
        }
    }
}
