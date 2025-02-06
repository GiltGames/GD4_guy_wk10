using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]

public class ShootableBox : MonoBehaviour {

	//The box's current health point total
	public int currentHealth = 30;
	[SerializeField] int maxHealth = 30;
	[SerializeField] GameObject aura;
	[SerializeField] float vFade =.5f;
	

	void Start()
	{
		
		vFade = 0.3f;
	
	}


	public void Damage(int damageAmount)
	{
		//subtract damage amount when Damage function is called
		currentHealth -= damageAmount;

		Color auraColor = Color.white;
		auraColor.a = currentHealth/maxHealth * vFade;
		auraColor.b = 0;
		auraColor.r = 1 - (currentHealth/maxHealth);
		auraColor.g = currentHealth/maxHealth;
			aura.GetComponent<Renderer>().material.color = auraColor;
		



		//Check if health has fallen below zero
		if (currentHealth <= 0) 
		{
			//if health has fallen below zero, deactivate it 

		
			GetComponent<Collider>().enabled = false;	

		}



	}
}
