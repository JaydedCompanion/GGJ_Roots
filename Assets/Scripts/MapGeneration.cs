using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneration : MonoBehaviour
{
    [SerializeField] private GameObject boneMeal;
    [SerializeField] private GameObject water;
    [SerializeField] private float interval = 5f;
    [SerializeField] private float x_area = 15f;
    [SerializeField] private float y_area = 13f;
    [SerializeField] private float shift_min = 0f;
    [SerializeField] private float shift_max = 1f;


    // Start is called before the first frame update
    void Start()
    {
        //solution1
        // float size = 7f;
        // float myAngle;
        // float myDepth;
        // for(int i=1; i<= 6; i++){
        //     amount = Random.Range(i*size, 2*i*size);
        //     for(int j=1; j <= amount; j++){
        //         if(i==5)
        //             myAngle = Random.Range(1.3f * Mathf.PI, 1.7f * Mathf.PI);
        //         else if(i==6)
        //             myAngle = Random.Range(1.35f * Mathf.PI, 1.65f * Mathf.PI);
        //         else
        //             myAngle = Random.Range(1.05f * Mathf.PI, 1.95f * Mathf.PI);

        //         if(i==1)
        //             myDepth = Random.Range(10f,radius);
        //         else
        //             myDepth = Random.Range((i-1) * radius, i * radius);
        //         GameObject bm = Instantiate(boneMeal, new Vector2(Mathf.Cos(myAngle) * myDepth, Mathf.Sin(myAngle) * myDepth), Quaternion.identity);
        //     }
        // }
        // for(int i=1; i<= 6; i++){
        //     amount = Random.Range(i*size, 2*i*size);
        //     for(int j=1; j <= amount; j++){
        //         if(i==5)
        //             myAngle = Random.Range(1.3f * Mathf.PI, 1.7f * Mathf.PI);
        //         else if(i==6)
        //             myAngle = Random.Range(1.35f * Mathf.PI, 1.65f * Mathf.PI);
        //         else
        //             myAngle = Random.Range(1.05f * Mathf.PI, 1.95f * Mathf.PI);

        //         if(i==1)
        //             myDepth = Random.Range(10f,radius);
        //         else
        //             myDepth = Random.Range((i-1) * radius, i * radius);
        //         GameObject bm = Instantiate(water, new Vector2(Mathf.Cos(myAngle) * myDepth, Mathf.Sin(myAngle) * myDepth), Quaternion.identity);
        //     }
        // }

        //solution2
        float myAngle;
        float myDepth;
        float myType;
        Vector2 location = new Vector2(transform.position.x - interval, transform.position.y);
        for(int i=1; i<= x_area; i++){
            location.x += interval;
            location.y = transform.position.y + interval;
            for(int j=1; j<= y_area; j++){
                location.y -= interval;
                myAngle = Random.Range(0f, 1.99f * Mathf.PI);
                myDepth = Random.Range(shift_min,shift_max);
                Vector2 mylocation = new Vector2(location.x + Mathf.Cos(myAngle) * myDepth, location.y + Mathf.Sin(myAngle) * myDepth);
                myType = Random.Range(-1f,1f);
                if(myType >= 0){
                    GameObject bm = Instantiate(water, mylocation, Quaternion.identity);
                }else{
                    GameObject bm = Instantiate(boneMeal, mylocation, Quaternion.identity);
                }
            }
        }

        Vector2 location2 = new Vector2(transform.position.x - 0.5f * interval, transform.position.y);
        for(int i=1; i<= x_area; i++){
            location2.x += interval;
            location2.y = transform.position.y +0.5f * interval;
            for(int j=1; j<= y_area-1; j++){
                location2.y -= interval;
                myAngle = Random.Range(0f, 1.99f * Mathf.PI);
                myDepth = Random.Range(shift_min,shift_max);
                Vector2 mylocation2 = new Vector2(location2.x + Mathf.Cos(myAngle) * myDepth, location2.y + Mathf.Sin(myAngle) * myDepth);
                myType = Random.Range(-1f,1f);
                if(myType >= 0){
                    GameObject bm = Instantiate(water, mylocation2, Quaternion.identity);
                }else{
                    GameObject bm = Instantiate(boneMeal, mylocation2, Quaternion.identity);
                }
            }
        }

        //solution3
        // float myAngle;
        // float myDepth;
        // float interval = 20f;
        // Vector2 location = new Vector2(transform.position.x - interval, transform.position.y);
        // for(int i=1; i<= 11; i++){
        //     location.x += interval;
        //     location.y = 15f;
        //     for(int j=1; j<= 10; j++){
        //         location.y -= interval;
        //         myAngle = Random.Range(0f, 1.99f * Mathf.PI);
        //         myDepth = Random.Range(1f,5f);
        //         Vector2 mylocation = new Vector2(location.x + Mathf.Cos(myAngle) * myDepth, location.y + Mathf.Sin(myAngle) * myDepth);
        //         GameObject bm = Instantiate(water, mylocation, Quaternion.identity);
        //     }
        // }
        // Vector2 location2 = new Vector2(transform.position.x - 0.5f * interval, transform.position.y);
        // for(int i=1; i<= 11; i++){
        //     location2.x += interval;
        //     location2.y = 7.5f;
        //     for(int j=1; j<= 9; j++){
        //         location2.y -= interval;
        //         myAngle = Random.Range(0f, 1.99f * Mathf.PI);
        //         myDepth = Random.Range(1f,5f);
        //         Vector2 mylocation2 = new Vector2(location2.x + Mathf.Cos(myAngle) * myDepth, location2.y + Mathf.Sin(myAngle) * myDepth);
        //         GameObject bm = Instantiate(boneMeal, mylocation2, Quaternion.identity);
        //     }
        // }
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
        Vector2 location = new Vector2(transform.position.x - interval, transform.position.y);
        Gizmos.color = Color.blue;
        for(int i=1; i<= x_area; i++){
            location.x += interval;
            Gizmos.DrawLine(new Vector2(location.x, transform.position.y), new Vector2(location.x, transform.position.y - (y_area-1)*interval));
        }
        for(int j=1; j<= y_area-1; j++){
            location.y -= interval;
            Gizmos.DrawLine(new Vector2(transform.position.x, location.y + interval), new Vector2(transform.position.x + (x_area)*interval, location.y + interval));     
        }
    }
}
