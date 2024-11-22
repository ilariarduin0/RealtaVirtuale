using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace c02.cognome.nome
{
    public class RandomPositioning : MonoBehaviour
    {
        //What do you need?
        //piano di gioco
        public GameObject plane;
        // A reference to the Character GameObject
        public GameObject character;
        // A variable to store the minimum distance
        public float minDistance = 5f;


        void Start()
        {
            //Position the target in a random position, you can use a custom method
            PositionTargetRandomly();
        }

        void Update()
        {
            //Check if the Chracter reference is valid 
            if (character == null)
            {
                Debug.LogWarning("Riferimento al personaggio mancante!");
                return;
            }
            //If it exists check if the distance between the target and the moving character is smaller than a minimum distance
            //To calculate distance use the function Vector3.Distance();
            //Reference: https://docs.unity3d.com/ScriptReference/Vector3.Distance.html
            float distance = Vector3.Distance(transform.position, character.transform.position);
            if (distance < minDistance)
            {
                PositionTargetRandomly();
            }

            //If the character has reached the target, reposition it (this game object) in a new random position if you defined a function, this is the place
            //to invoke it
        }

        void PositionTargetRandomly()
        {
            if (plane == null)
            {
                Debug.LogError("Piano non assegnato nello script RandomPositioning.");
                return;
            }

            // Ottieni il MeshRenderer del piano e i suoi limiti (bounds)
            MeshRenderer planeRenderer = plane.GetComponent<MeshRenderer>();

            if (planeRenderer == null)
            {
                Debug.LogError("Nessun MeshRenderer trovato sull'oggetto piano.");
                return;
            }

            Bounds planeBounds = planeRenderer.bounds;
            Vector3 min = planeBounds.min;
            Vector3 max = planeBounds.max;

            // Genera una posizione casuale nei limiti del piano, mantenendo la Y invariata
            float randomX = Random.Range(min.x, max.x);
            float randomZ = Random.Range(min.z, max.z);

            // Assegna la nuova posizione al target
            transform.position = new Vector3(randomX, transform.position.y, randomZ);
        }

    }
}
