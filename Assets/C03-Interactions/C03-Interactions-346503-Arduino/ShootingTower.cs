using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace c03.exercise
{
	public class ShootingTower : MonoBehaviour
	{
		//Define here all the variables you will be needing in the script and also all the objects you with to set a reference via the inspector
		public GameObject bullet;
		public GameObject target;
		public GameObject gun;
     
        public float minDistance = 100f;
		public float minAngle = 100f;

        void Start()
		{
			//Retrieve here all the references to other object you will need in the script.
		}

		void Update()
		{
			// Constantly Rotate Tower if Target is NOT in Sight
			//You can use the function transform.Rotate(): https://docs.unity3d.com/ScriptReference/Transform.Rotate.html
			
            float distance = Vector3.Distance(transform.position, target.transform.position);
            Vector3 targetDir = target.transform.position - transform.position;
            targetDir.y = 0;
            float angle = Vector3.Angle(targetDir, transform.forward);
            

			//Check if Target is visible to the tower
			if (IsTargetVisible(distance, angle))
			{
				PointTarget(target);

                //Start Shooting, if already started Shooting don't invoke again

                //Have a peek to the way we manage an automatic repeated function call in the ShootingTower.cs script. 
                //As a suggestion, we use the function InvokeRepeating(): https://docs.unity3d.com/ScriptReference/MonoBehaviour.InvokeRepeating.html
                if (!IsInvoking("Shoot"))
                {
                    InvokeRepeating("Shoot", 0.2f, 0.3f); // Spara ogni 0.3 secondi
                }
                
			}
            else
            {
                // Interrompi il ciclo di sparo se il bersaglio non è più visibile
                if (IsInvoking("Shoot"))
                {
                    CancelInvoke("Shoot");
                }

                // Ruota la torretta quando il bersaglio non è visibile
                transform.Rotate(0, 50f * Time.deltaTime, 0);
            }
        }

		private void Shoot()
		{
            Vector3 rayOrigin = transform.position;
            Ray ray = new Ray(rayOrigin, transform.forward);
            RaycastHit hit;
            //Find a creative way to display the shooting behaviour
            //CHECK IF THERE ARE NO OBSTACLES
            if (Physics.Raycast(ray, out hit, minDistance))
            {
                //Se l'oggetto colpito è il bersaglio e non altro
                if (hit.transform.gameObject == target)
                {
                    //Get information on the hitted object
                    Debug.Log($"Raycast Hit GameObject: {hit.transform.name}");
                    Vector3 targetDir = target.transform.position - transform.position;
                    targetDir.y = 0;
                    Vector3 gunPosition = gun.transform.position;

                    Quaternion bulletRotation = Quaternion.LookRotation(targetDir);
                    GameObject instantiatedBullet = Instantiate(bullet, gunPosition, bulletRotation);
                    Rigidbody bulletRigidbody = instantiatedBullet.GetComponent<Rigidbody>();
                    bulletRigidbody.AddForce(targetDir * 300f, ForceMode.Impulse);

                }
            }
            
        }

		private bool IsTargetVisible(float distance, float angle )
		{
            //In this function you need to check if the target is visible to the tower. This is achieved by checking the three below condition
            

            Debug.Log("ANGLE:" + angle);
            //CHECK IF IS WITHIN VIEW DISTANCE //CHECK IF FALLS WITHIN VIEW ANGLE
            if (distance < minDistance && angle < minAngle)
            {
                return true;
                
			}
			return false;
		}

		private void PointTarget(GameObject target)
		{
            
            //Rotate the tower in order to always face the target
            Vector3 targetDirection = target.transform.position - transform.position;
            targetDirection.y = 0;
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 6f);
            Debug.DrawRay(transform.position, targetDirection, Color.red); // Linea di debug

        }
	}
}

