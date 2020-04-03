using UnityEngine;
using System.Collections;

public class Agent : MonoBehaviour {

    public GameObject pistolPrefab;
	protected UnityEngine.AI.NavMeshAgent		agent;
	protected Animator			animator;

	protected Locomotion locomotion;
	bool done =false;

    bool hasGun = false;
	// Use this for initialization
	void Start () {
		agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
		agent.updateRotation = false;
		agent.avoidancePriority = Random.Range (0, 100);

		animator = GetComponent<Animator>();
		locomotion = new Locomotion(animator);

        if (Random.Range(0, 100) > 50)
        {
            //to draw a pistol, set the weight of the pistol layer to 1
            animator.SetLayerWeight(1, 1);
            hasGun = true; // we'll use this to randomly play the shoot animation
            GameObject pistol = Instantiate(pistolPrefab, this.transform.position, pistolPrefab.transform.rotation) as GameObject;
            pistol.transform.parent = animator.GetBoneTransform(HumanBodyBones.RightHand);
            pistol.transform.localPosition = Vector3.zero;

            Quaternion pistolRotation = animator.GetBoneTransform(HumanBodyBones.RightHand).localRotation;
            pistolRotation *= Quaternion.Euler(0, -90, 0); // our pistol needs to be rotated left
            pistol.transform.localRotation = pistolRotation;


        }
        SetDestination();
    }

	protected void SetDestination()
	{
		done = false;
		float dist = Random.Range(5,60);
		Vector3 randomDirection = Random.insideUnitSphere * dist;
		randomDirection.y = this.transform.position.y;
		randomDirection += this.transform.position;
		UnityEngine.AI.NavMeshHit hit;
		UnityEngine.AI.NavMesh.SamplePosition(randomDirection, out hit, dist,1);
        if(agent.enabled)
	    	agent.destination = hit.position;

        
	}

	protected void SetupAgentLocomotion()
	{

        if (hasGun && Random.Range(0, 100) > 50)
        {
            animator.SetTrigger("shoot");


        }

        if (AgentDone() && !done)
		{
			done = true;
			locomotion.Do(0, 0);
			Invoke("SetDestination",Random.Range(0f,4f));

		}
		else
		{
			float speed = agent.desiredVelocity.magnitude;

			Vector3 velocity = Quaternion.Inverse(transform.rotation) * agent.desiredVelocity;

			float angle = Mathf.Atan2(velocity.x, velocity.z) * 180.0f / 3.14159f;

			locomotion.Do(speed, angle);
		}
	}

    void OnAnimatorMove()
    {
        agent.velocity = animator.deltaPosition / Time.deltaTime;
		transform.rotation = animator.rootRotation;
    }

	protected bool AgentDone()
	{
		return !agent.pathPending && AgentStopping();
	}

	protected bool AgentStopping()
	{
		return agent.remainingDistance <= agent.stoppingDistance;
	}

	// Update is called once per frame
	void Update () 
	{

        if (agent.enabled)
		    SetupAgentLocomotion();
	}
}
