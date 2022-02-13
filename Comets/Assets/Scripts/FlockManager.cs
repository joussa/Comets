using UnityEngine;

public class FlockManager : MonoBehaviour
{

    public GameObject cubePrefab;
    public int numCube = 20;
    public GameObject[] allCube;
    public Vector3 navigateLimits = new Vector3(10, 50, 10);
    public Vector3 goalPos;


    [Header("Cube Settings")]
    [Range(0.0f, 100f)]
    public float minSpeed;
    [Range(0.5f, 100f)]
    public float maxSpeed;
    [Range(0.5f, 10.0f)]
    public float neighbourDistance;
    [Range(-30.0f, 10.0f)]
    public float rotationSpeed;

    void Start()
    {

        allCube = new GameObject[numCube];
        for (int i = 0; i < numCube; ++i)
        {

            Vector3 pos = this.transform.position + new Vector3(Random.Range(-navigateLimits.x, navigateLimits.x),
                                                                Random.Range(-navigateLimits.y, navigateLimits.y),
                                                                Random.Range(-navigateLimits.z, navigateLimits.z));
            allCube[i] = (GameObject)Instantiate(cubePrefab, pos, Quaternion.identity);

            allCube[i].GetComponent<Flock>().myManager = this;
        }
       // goalPos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
         if(Random.Range(0,100)<10)
             goalPos = this.transform.position + new Vector3(Random.Range(-navigateLimits.x, navigateLimits.x),
                                                                 Random.Range(-navigateLimits.y, navigateLimits.y),
                                                                 Random.Range(-navigateLimits.z, navigateLimits.z));
    }

}
