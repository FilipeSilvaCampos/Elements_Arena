using ElementsArena.Damage;
using UnityEngine;

namespace ElementsArena.Combat
{
	public class LaunchRock : EarthAbility
	{
		[Header("Launch Attributes")]
		[SerializeField] GameObject deffaultAttack;
		[SerializeField] Transform launchTransform;
		[SerializeField] float moveRockSpeed = 2;
		[SerializeField] ParticleSystem launchParticle;

		GameObject currentRock;
		Animator animator;

		protected override void Awake()
		{
			base.Awake();
			animator = GetComponentInChildren<Animator>();
		}

		protected override void OnReady()
		{
			if (called)
			{
				if (HaveRockOnForward(out currentRock))
				{
					FinishState();
					return;
				}

				InvokeNewRock();
				LockCharacterMovement(true);
				FinishState();
			}
		}

		bool isLaunching = false;
		protected override void OnActive()
		{
			if (isLaunching) return;

			MoveToTarget();

			if (OnTarget())
			{
				animator.SetTrigger(AnimationKeys.LaunchTrigger);
				isLaunching = true;
			}
		}

		protected override void OnCooldown()
		{
			if (IsTimeToChangeState()) FinishState();
		}

		//Animation Event
		public void Launch()
		{
			currentRock.GetComponent<Rock>().Launch(GetComponent<IDamageable>());
			Instantiate(launchParticle, launchTransform.position, launchTransform.rotation);
			UnlockCharacterMovement();
			isLaunching = false;
			FinishState();
		}

		void InvokeNewRock()
		{
			currentRock = Instantiate(deffaultAttack, GetInvokePosition(), launchTransform.rotation);
		}

		bool OnTarget()
		{
			bool onPosition = currentRock.transform.position == GetTargetPosition() ? true : false;
			bool onRotation = currentRock.transform.rotation == launchTransform.rotation ? true : false;

			return onPosition && onRotation;
		}

		void MoveToTarget()
		{
			currentRock.transform.position = Vector3.MoveTowards(currentRock.transform.position, GetTargetPosition(), moveRockSpeed * Time.deltaTime);

			currentRock.transform.rotation = launchTransform.rotation;
		}

		Vector3 GetTargetPosition()
		{
			Vector3 targetPosition = launchTransform.position;
			targetPosition.y = GroundHeight() + 1.8f; //Height character

			return targetPosition;
		}

		Vector3 GetInvokePosition()
		{
			Vector3 invokePosition = launchTransform.position;
			invokePosition.y = GroundHeight() - deffaultAttack.transform.localScale.y / 2;

			return invokePosition;
		}
	}
}

