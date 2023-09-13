using Photon.Pun;
using System;
using UnityEngine;

namespace ElementsArena.Damage
{
    public class OnTakeDamage : MonoBehaviour, IDamageable
    {
        [SerializeField] float maxLife;
        public float life { get; private set; }

        public event Action OnDeath;
        PhotonView photonView;

        private void Awake()
        {
            photonView = GetComponent<PhotonView>();
        }

        private void Start()
        {
            life = maxLife;
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.I))
            {
                TakeDamage(15.5f);
            }
        }

        public void TakeDamage(float damage)
        {
            if (!photonView.IsMine) return;

            Debug.Log(gameObject.name  + " " + damage);
            life = Mathf.Max(life - damage, 0);
            Debug.Log(gameObject.name + " life: " + life);
            if (life == 0)
            {
                OnDeath.Invoke();
            }
        }

        public float GetFraction()
        {
            return life / maxLife;
        }
    }
}
