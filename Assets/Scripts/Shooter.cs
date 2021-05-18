using UnityEngine;
using System.Collections;

public class Shooter : MonoBehaviour {

    private Transform myTransform;

	public GameObject magicAttack1;

	public GameObject magicAura;

	private GameObject shooter;

	private GameObject spawner;

	private GameObject auraSpawner;

	public static bool activeMagic = false;

	public static float magicDelay;



	//public Movementcharacter shootingState;


	
	void Start(){
		shooter = GameObject.Find ("Mago");
		spawner = GameObject.Find ("Spawner");
		auraSpawner = GameObject.Find ("AuraSpawner");

        myTransform = auraSpawner.gameObject.transform;

	}


	//private float timerLanzador = 3f;
	//private bool magicReleased;


	// Update is called once per frame
	void Update () {

		magicDelay = Movementcharacter.shootingDelay;


		//Debug.Log (transform.localRotation);

		if(Movementcharacter.shooting == true && MagicMovement.aliveMagic == false) //&& activeMagic == false)
		{
            Debug.Log(Movementcharacter.shooting);
			StartCoroutine(MagicReleased());

			
		}
	
	}

	IEnumerator MagicReleased() {

		if (Movementcharacter.shooting && !MagicMovement.aliveMagic) {

			Debug.Log ("Debe aparecer");
            //activeMagic = true;
            Instantiate(magicAura,auraSpawner.transform.position,myTransform.rotation);
			Movementcharacter.shooting = false;
			MagicMovement.aliveMagic = true;
			yield return new WaitForSeconds (1);
			Instantiate (magicAttack1, spawner.transform.position, shooter.transform.rotation);

		}
		//activeMagic = false;

	}

	void MagicDelay()
	{
		//Debug.Log ("Entre a ShootDelay");
		
		
		
		magicDelay -= Time.deltaTime;
		
		if (magicDelay <= 0)
		{
			magicDelay = 0;
		}
		if (magicDelay == 0)
		{
			activeMagic = false;
		}
		
	}
}
