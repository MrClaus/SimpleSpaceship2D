using System.Collections;
using System.Collections.Generic;
using Random = System.Random;
using UnityEngine;

public class Asteroids : MonoBehaviour
{
    public float xLeft = -10f;
    public float xRight = 10f;
    public float yBottom = -6.5f;
    public float yTop = 6.5f;
    public GameObject[] asteroids;
    public float CreateInterval = 2f;
    public int MaxCount;

    private Random rnd = new Random();
    private List<PosVar> listPositions;
    private Vector3 lastPosition = new Vector3(0, 0, 0);

    public static bool isActiveCreating = false;
    private static List<GameObject> aster_arr = new List<GameObject>();

    private class PosVar
    {
        public float x1, x2, y1, y2;
        public PosVar(float xFrom, float xTo, float yFrom, float yTo)
        {
            x1 = xFrom;
            x2 = xTo;
            y1 = yFrom;
            y2 = yTo;
        }
    }


    private void Start()
    {
        StartInitiate();
    }


    // Getting a random asteroid position
    public Vector3 GetRandomPosition()
    {
        float x, y, z = 0;

        do
        {
            int mId = rnd.Next(0, 4);
            PosVar mPos = listPositions[mId];
            x = mPos.x1 + (float)rnd.NextDouble() * (mPos.x2 - mPos.x1);
            y = mPos.y1 + (float)rnd.NextDouble() * (mPos.y2 - mPos.y1);
        }
        while (x == lastPosition.x && y == lastPosition.y);

        lastPosition = new Vector3(x, y, z);
        return lastPosition;
    }


    // Starting the asteroid fabric
    public void StartInitiate()
    {
        listPositions = new List<PosVar>
        {
            new PosVar(xLeft, xRight, yTop, yTop),
            new PosVar(xLeft, xRight, yBottom, yBottom),
            new PosVar(xLeft, xLeft, yTop, yBottom),
            new PosVar(xRight, xRight, yTop, yBottom),
        };
        StartCoroutine(Creating());
    }


    private IEnumerator Creating()
    {
        while (true)
        {
            if (isActiveCreating && Asteroid.count < MaxCount)
            {
                int type = rnd.Next(0, 2);
                GameObject aster = Instantiate(asteroids[type], GetRandomPosition(), Quaternion.identity);
                aster_arr.Add(aster);
            }
            yield return new WaitForSeconds(CreateInterval);
        }
    }

    // Removing all asteroids
    public static void Clear()
    {
        if (aster_arr.Count > 0)
        {
            foreach (GameObject obj in aster_arr)
                Destroy(obj);
        }
    }
}
