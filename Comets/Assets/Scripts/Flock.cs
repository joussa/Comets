using UnityEngine;

public class Flock : MonoBehaviour {

    public FlockManager myManager;
    float speed;
    bool turning = false;
    //public GameObject NeonRedSquare;


    void Start() {

        speed = Random.Range(myManager.minSpeed , myManager.maxSpeed);
    }

    // Update is called once per frame
    void Update() {

        Bounds b = new Bounds(myManager.transform.position, myManager.navigateLimits * 2);

        RaycastHit hit = new RaycastHit();
        Vector3 direction = Vector3.zero;

        if (!b.Contains(transform.position))
        {
            turning = true;
            direction = myManager.transform.position - transform.position;
        }
       else if (Physics.Raycast(transform.position, this.transform.forward * 50.0f, out hit))
        {

            turning = true;
            // Debug.DrawRay(this.transform.position, this.transform.forward * 50.0f, Color.red);
            direction = Vector3.Reflect(this.transform.forward, hit.normal);
        }
        else
        {

            turning = false;
        }

      if (turning) {

            // Turn towards the centre of the cube
            transform.rotation = Quaternion.Slerp(transform.rotation,
                                                  Quaternion.LookRotation(direction),
                                                  myManager.rotationSpeed * Time.deltaTime);
        }
        else
        {
            // Debug.Log("Hello: " + speed);
            if (Random.Range(0.0f, 100.0f) < 10.0f)
                speed = Random.Range(myManager.minSpeed, myManager.maxSpeed);
            if (Random.Range(0.0f, 100.0f) < 20.0f)
                ApplyRules();
        }
        transform.Translate( 0.0f, 0.0f, Time.deltaTime * speed);
       // transform.position -= Time.deltaTime * transform.forward * 2;
    }

    void ApplyRules() {

        GameObject[] gos;
        gos = myManager.allCube;

        Vector3 vcentre = Vector3.zero;
        Vector3 vavoid = Vector3.zero;
        float gSpeed = 0.01f;
        float nDistance;
        int groupSize = 3;

        foreach (GameObject go in gos) {

            if (go != this.gameObject) {

                nDistance = Vector3.Distance(go.transform.position, this.transform.position);
                if (nDistance <= myManager.neighbourDistance) {

                    vcentre += go.transform.position;
                    groupSize++;

                    if (nDistance < 1.0f) {

                        vavoid = vavoid + (this.transform.position - go.transform.position);
                    }

                    Flock anotherFlock = go.GetComponent<Flock>();
                    gSpeed = gSpeed + anotherFlock.speed;
                }
            }
        }

        
      if (groupSize > 0) {

             vcentre = vcentre / groupSize + (myManager.goalPos - this.transform.position);
             speed = gSpeed / groupSize;

             Vector3 direction = (vcentre + vavoid) - transform.position;
             if (direction != Vector3.zero) {

                 transform.rotation = Quaternion.Slerp(transform.rotation,
                                                       Quaternion.LookRotation(direction),
                                                       myManager.rotationSpeed * Time.deltaTime);
             }
         }
    }
}
