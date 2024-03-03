using ElementsArena.Damage;
using System;
using UnityEngine;

public class Shield : MonoBehaviour, IDamageable
{
	[SerializeField] float durability = 100;

	public float life { get; private set; }

	public event Action OnDeath;
	IDamageable instigator;

	private void Start()
	{
		life = durability;
	}

	private void OnTriggerEnter(Collider other)
	{
		TriggerDamage damager = other.GetComponent<TriggerDamage>();
		if (damager != null && damager.GetInstigator() != instigator)
		{
			Destroy(other.gameObject);
		}
	}

	public void SetInstigator(IDamageable instigator)
	{
		this.instigator = instigator;
	}

	public float GetFraction()
	{
		return life / durability;
	}

	public void TakeDamage(float damage)
	{
		life -= damage;

		if (life < 0) Destroy(gameObject);
	}
}
