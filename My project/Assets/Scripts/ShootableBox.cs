using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]

public class ShootableBox : MonoBehaviour {

	//The box's current health point total
	public int currentHealth = 30;
	[SerializeField] int maxHealth = 30;
	[SerializeField] GameObject aura;
	[SerializeField] float vFade =.5f;
	[SerializeField] Color auraColor;
	

	void Start()
	{
		
		vFade = 0.3f;
	
	}


	public void Damage(int damageAmount)
	{
		//subtract damage amount when Damage function is called
		currentHealth -= damageAmount;

		Debug.Log(gameObject.name + " takes damage of " + damageAmount);

		auraColor = aura.GetComponent<Renderer>().material.color;
		auraColor.a = currentHealth/maxHealth * vFade;
		auraColor.b = 0;
		auraColor.r = 1 - (currentHealth/maxHealth);
		auraColor.g = currentHealth/maxHealth;
			aura.GetComponent<Renderer>().material.color = auraColor;
		



		//Check if health has fallen below zero
		if (currentHealth <= 0) 
		{
			//if health has fallen below zero, deactivate it 

		
			GameObject.Destroy(gameObject);

		}



	}
}
