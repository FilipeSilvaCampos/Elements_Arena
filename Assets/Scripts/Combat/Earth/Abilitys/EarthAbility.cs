using UnityEngine;

namespace ElementsArena.Combat
{
	public abstract class EarthAbility : Ability
	{
		[SerializeField] protected float domainDistance = 3;
		[SerializeField] protected LayerMask rockLayer;
		[SerializeField] protected LayerMask groundLayer;

		protected bool HaveRockOnForward(out GameObject rock)
		{
			Vector3 overlapPosition = transform.position + transform.forward * domainDistance;
			Collider[] hits = Physics.OverlapBox(overlapPosition, new Vector3(domainDistance, domainDistance, domainDistance), transform.rotation, rockLayer);

			if (hits.Length != 0)
			{
				foreach (Collider hit in hits)
				{
					if (hit.gameObject.GetComponent<Rock>() != null)
					{
						rock = hit.gameObject;
						return true;
					}
				}
			}

			rock = null;
			return false;

		}

		protected float GroundHeight()
		{
			RaycastHit hit;

			Ray ray = new Ray(transform.position, Vector3.down);
			Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer);

			return hit.point.y;
		}
	}
}
