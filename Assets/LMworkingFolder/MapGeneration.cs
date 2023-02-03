using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneration : MonoBehaviour
{
    [SerializeField] private GameObject boneMeal;
    [HideInInspector] private float amount;
    [SerializeField] private float radius = 30f;


    // Start is called before the first frame update
    void Start()
    {
        float size = 3f;
        float myAngle;
        float myDepth;
        for(int i=1; i<= 6; i++){
            amount = Random.Range(i*size, 2*i*size);
            for(int j=1; j <= amount; j++){
                if(i==5)
                    myAngle = Random.Range(1.3f * Mathf.PI, 1.7f * Mathf.PI);
                else if(i==6)
                    myAngle = Random.Range(1.35f * Mathf.PI, 1.65f * Mathf.PI);
                else
                    myAngle = Random.Range(Mathf.PI, 2 * Mathf.PI);

                if(i==1)
                    myDepth = Random.Range(10f,radius);
                else
                    myDepth = Random.Range((i-1) * radius, i * radius);
                Instantiate(boneMeal, new Vector2(Mathf.Cos(myAngle) * myDepth, Mathf.Sin(myAngle) * myDepth), Quaternion.identity);
            }
        }
        /*
        //generate in zone1
        amount = Random.Range(size, 2*size);
        for(int i=1; i <= amount; i++){
            float myAngle = Random.Range(Mathf.PI, 2 * Mathf.PI);
            float myDepth = Random.Range(10f,radius);
            Instantiate(boneMeal, new Vector2(Mathf.Cos(myAngle) * myDepth, Mathf.Sin(myAngle) * myDepth), Quaternion.identity);
        }

        //generate in zone2
        amount = Random.Range(2*size, 4*size);
        for(int i=1; i <= amount; i++){
            float myAngle = Random.Range(Mathf.PI, 2 * Mathf.PI);
            float myDepth = Random.Range(radius, 2 * radius);
            Instantiate(boneMeal, new Vector2(Mathf.Cos(myAngle) * myDepth, Mathf.Sin(myAngle) * myDepth), Quaternion.identity);
        }

        //generate in zone3
        amount = Random.Range(4*size, 6*size);
        for(int i=1; i <= amount; i++){
            float myAngle = Random.Range(Mathf.PI, 2 * Mathf.PI);
            float myDepth = Random.Range(2 * radius, 3 * radius);
            Instantiate(boneMeal, new Vector2(Mathf.Cos(myAngle) * myDepth, Mathf.Sin(myAngle) * myDepth), Quaternion.identity);
        }

        //generate in zone4
        amount = Random.Range(6*size, 8*size);
        for(int i=1; i <= amount; i++){
            float myAngle = Random.Range(Mathf.PI, 2 * Mathf.PI);
            float myDepth = Random.Range(3 * radius, 4 * radius);
            Instantiate(boneMeal, new Vector2(Mathf.Cos(myAngle) * myDepth, Mathf.Sin(myAngle) * myDepth), Quaternion.identity);
        }

        //generate in zone5
        amount = Random.Range(6*size, 8*size);
        for(int i=1; i <= amount; i++){
            float myAngle = Random.Range(1.3f * Mathf.PI, 1.7f * Mathf.PI);
            float myDepth = Random.Range(4 * radius, 5 * radius);
            Instantiate(boneMeal, new Vector2(Mathf.Cos(myAngle) * myDepth, Mathf.Sin(myAngle) * myDepth), Quaternion.identity);
        }

        //generate in zone6
        amount = Random.Range(6*size, 8*size);
        for(int i=1; i <= amount; i++){
            float myAngle = Random.Range(1.35f * Mathf.PI, 1.65f * Mathf.PI);
            float myDepth = Random.Range(5 * radius, 6 * radius);
            Instantiate(boneMeal, new Vector2(Mathf.Cos(myAngle) * myDepth, Mathf.Sin(myAngle) * myDepth), Quaternion.identity);
        }
        */
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 2*radius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 3*radius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 4*radius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 5*radius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 6*radius);
    }
}
