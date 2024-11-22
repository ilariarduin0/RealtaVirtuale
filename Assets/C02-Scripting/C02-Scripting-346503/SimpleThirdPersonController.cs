using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace c02.cognome.nome
{
    public class SimpleThirdPersonController : MonoBehaviour
    {
        //Which public variables do you need?
        // A Camera
        public Camera mainCamera;
        // A Rotation Speed
        public float rotationSpeed = 8f;
        // A Movement Speed
        public float moveSpeed = 8f;

        void Update()
        {
            //Get the Input using Input.GetAxis() & assign the values to a new direction Vector3
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            //Compute direction According to Camera Orientation (use function TransformDirection)
            //Reference: https://docs.unity3d.com/ScriptReference/Transform.TransformDirection.html
            Vector3 inputDirection = new Vector3(horizontalInput, 0f, verticalInput);
            float magnitude = inputDirection.magnitude;
            inputDirection = inputDirection.normalized;
            Vector3 targetDirection = mainCamera.transform.TransformDirection(inputDirection);
            targetDirection.y = 0f;

            //Calculate the new direction vector between the current forward and the target direction calculated previously.
            //To calculate it use the RotateTowards method
            //Reference: https://docs.unity3d.com/ScriptReference/Vector3.RotateTowards.html
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, rotationSpeed * Time.deltaTime, 0f);

            //Rotate the object, you can use Quaternion.LookRotation() function.
            //Reference: https://docs.unity3d.com/ScriptReference/Quaternion.LookRotation.html
            transform.rotation = Quaternion.LookRotation(newDirection);

            //Translate along forward
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime * magnitude);
        }
    }
}
