using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneMealScript : MonoBehaviour
{
    public GameObject player;
    private Vector2 mylocation;
    private PlayerControlfortesting playerData;

    // Start is called before the first frame update
    void Start()
    {
        mylocation = transform.position;
        playerData = player.GetComponent(typeof(PlayerControlfortesting)) as PlayerControlfortesting;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        //Debug.Log(mylocation);
        playerData.EatBoneMeal(mylocation);
        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(mylocation, 10);
    }

}
