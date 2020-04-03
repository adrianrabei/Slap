using UnityEngine;
using System.Collections;

public class RagdollToggle : MonoBehaviour {


    Transform[] rs;
    bool ragDollActivated = false;

	// Use this for initialization
	void Start () {

        rs = transform.GetComponentsInChildren<Transform>();
        DisableRagDoll();
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (ragDollActivated == false)
            {
                EnableRagDoll();
                ragDollActivated = true;
            }
            else
            {
                DisableRagDoll();
                ragDollActivated = false;
            }
        }

	}

    void EnableRagDoll()
    {
        foreach (Transform r in rs)
        {
            Rigidbody rigidbody = r.GetComponent<Rigidbody>();
            if (rigidbody!=null)
            {
                rigidbody.isKinematic = false;
                rigidbody.detectCollisions = true;
                rigidbody.drag = 1;
                rigidbody.AddForceAtPosition((Vector3.up + Random.insideUnitSphere) * Random.Range(2000,6000), this.transform.position);
            }

          
        }

        this.GetComponent<Animator>().enabled = false;
        this.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        this.GetComponent<Agent>().enabled = false;

    }

   

    public void DisableRagDoll()
    {

     
        foreach (Transform r in rs)
        {
            Rigidbody rigidbody = r.GetComponent<Rigidbody>();
            if (rigidbody!=null)
            {
                rigidbody.isKinematic = true;
                rigidbody.detectCollisions = false;
            }
        }
        this.GetComponent<Animator>().enabled = true;
        this.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
        this.GetComponent<Agent>().enabled = true;

    }
}
