using UnityEngine;
using System.Collections;

public class MagicMovement : MonoBehaviour
{

    public float speedMagic;
    public static bool aliveMagic = false;



    void Start()
    {
        //shooter = GameObject.Find ("Mago");
    }
    // Update is called once per frame
    void Update()
    {
        //transform.Translate(Vector3.forward * speedMagic * Time.deltaTime, Space.Self) ;

        transform.Translate(Vector3.forward * speedMagic * Time.deltaTime);
        if (aliveMagic)
        {

            StartCoroutine(WaitforDestroy());
        }



    }

    IEnumerator WaitforDestroy()
    {
        Movementcharacter.shooting = false;
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
        aliveMagic = false;

        //Shooter.activeMagic = false;
        //aliveMagic = false;

    }
}
