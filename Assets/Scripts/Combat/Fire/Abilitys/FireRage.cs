using DG.Tweening;
using ElementsArena.Damage;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace ElementsArena.Combat
{
    public class FireRage : Ability
    {
        [SerializeField] FireScrtiptableProjectile[] projectiles;
        [SerializeField] Transform launchTransform;
        [SerializeField] float timeWithoutReaction = 1.8f;
        [SerializeField] Rig shootRig = null;

        PhotonView photonView;
        FireBreath breath;
        float timeSinceLastLaunch = 0;
        int counter = 0;

        protected override void Awake()
        {
            base.Awake();
            breath = GetComponent<FireBreath>();
            photonView = GetComponent<PhotonView>();
        }

        protected override void Update()
        {
            base.Update();
            timeSinceLastLaunch += Time.deltaTime;
        }

        protected override void OnReady()
        {
            if(called && breath.TakeBreath(projectiles[counter].breathCost))
            {
                LaunchProjectile();
                FinishState();
            }
        }

        protected override void OnActive()
        {
            if (counter >= projectiles.Length || timeSinceLastLaunch > timeWithoutReaction) 
            {
                counter = 0;
                FinishState();
                return;
            }

            if(called && timeSinceLastLaunch > projectiles[counter].launchTime && breath.TakeBreath(projectiles[counter].breathCost))
            {
                LaunchProjectile();
            }
        }

        protected override void OnCooldown()
        {
            if (IsTimeToChangeState()) FinishState();
        }

        private void LaunchProjectile()
        {
            if (shootRig)
            {
                DOVirtual.Float(0, 1, .1f, (x) => shootRig.weight = x).OnComplete(() => DOVirtual.Float(1, 0, .3f, (x) => shootRig.weight = x));
            }

            photonView.RPC("InstantiateProjectile", RpcTarget.All, counter, launchTransform.position, launchTransform.rotation);

            timeSinceLastLaunch = 0;
            counter++;
        }

        [PunRPC]
        void InstantiateProjectile(int projectileIndex, Vector3 spawnPosition, Quaternion spawnRotation)
        {
            projectiles[projectileIndex].Lauch(GetComponent<IDamageable>(), spawnPosition, spawnRotation);
        }
    }
}
