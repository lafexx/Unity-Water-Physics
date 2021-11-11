//---------------------------------------------------------------
// SCRIPT - Floater.cs / APPLY TO THE FLOATER(S) MENTIONED BELOW
//---------------------------------------------------------------
 
//-------------------------------------------------------
// SCRIPT - ADD 4 EMPTY CHILDREN WITH THE NAME FLOATER
// TO THE OBJECT AFFTECED BY THE WATER PHYSICS AND PLACE
// ONE IN EACH CORNER OF THE SAID OBJECT
//-------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class Floater : MonoBehaviour
{
    public Rigidbody rigidBody;
    public float depthBeforeSubmerged = 1f;
    public float displacementAmount = 3f;
    public int floaterCount = 1;
    public float waterDrag = 0.99f;
    public float waterAngularDrag = 0.5f;
 
    private void FixedUpdate()
    {
        rigidBody.AddForceAtPosition(Physics.gravity / floaterCount, transform.position, ForceMode.Acceleration);
 
        float waveHeight = WaveManager.instance.GetWaveHeight(transform.position.x) + WaveManager.instance.transform.position.y;
        if (transform.position.y < waveHeight)
        {
            float displacementMultiplier = Mathf.Clamp01((waveHeight -transform.position.y) / depthBeforeSubmerged) * displacementAmount;
            rigidBody.AddForceAtPosition(new Vector3(0f, Mathf.Abs(Physics.gravity.y) * displacementMultiplier, 0f), transform.position, ForceMode.Acceleration);
            rigidBody.AddForce(displacementMultiplier * -rigidBody.velocity * waterDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);
            rigidBody.AddTorque(displacementMultiplier * -rigidBody.angularVelocity * waterAngularDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
    }
}
