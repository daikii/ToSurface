using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gameController : MonoBehaviour
{

    public GameObject[] planets;
    public int planetCount;
    public Vector3 planetsSpawnValues;
    public float planetsStartWait;
    public float planetsWaveWait;

    void Start()
    {
        StartCoroutine(spawnPlanets());
    }


    IEnumerator spawnPlanets()
    {
        yield return new WaitForSeconds(planetsStartWait);
        while (true)
        {
            for (int i = 0; i < planetCount; i++)
            {
                GameObject planet = planets[Random.Range(0, planets.Length)];
                Vector3 planetsSpawnPosition = new Vector3(Random.Range(-planetsSpawnValues.x, planetsSpawnValues.x), planetsSpawnValues.y, planetsSpawnValues.z);
                Quaternion planetsSpawnRotation = Quaternion.identity;
                Instantiate(planet, planetsSpawnPosition, planetsSpawnRotation);
                yield return new WaitForSeconds(Random.Range(3.0f, 8.0f));
            }
        }
    }
}