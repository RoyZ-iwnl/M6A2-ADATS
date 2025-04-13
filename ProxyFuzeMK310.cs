using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GHPC.Player;
using GHPC.Weapons;
using UnityEngine;
using M6A2Adats;
using HarmonyLib;

namespace M6A2Adats
{
    public class ProxySwitchMK310 : MonoBehaviour
    {
        public bool activated = false;
        private float cd = 0f;
        private WeaponSystem weapon = null;
        private PlayerInput player_manager;
        void Awake()
        {
            weapon = GetComponent<WeaponsManager>().Weapons[0].Weapon;
            player_manager = GameObject.Find("_APP_GHPC_").GetComponent<PlayerInput>();
        }

        void Update()
        {
            cd -= Time.deltaTime;

            if (player_manager.CurrentPlayerUnit.gameObject.GetInstanceID() != gameObject.GetInstanceID()) return;

            M6A2_Adats.clip_mk310.Name = activated ? "MK310 PABM-T [Proximity]" : "MK310 PABM-T";

            if (Input.GetKey(KeyCode.Mouse2) && cd <= 0f && weapon.CurrentAmmoType == M6A2_Adats.ammo_mk310)
            {
                cd = 0.2f;

                activated = !activated;
            }
        }

    }

    public class ProxyFuzeMK310 : MonoBehaviour
    {
        private GHPC.Weapons.LiveRound live_round;
        private static GameObject prox_fuse;
        private static HashSet<string> prox_ammos = new HashSet<string>();
        private bool detonated = false;

        // must be called at least once 
        public static void Init()
        {
            if (prox_fuse) return;
            prox_fuse = new GameObject("mk310 prox fuse");
            prox_fuse.layer = 8;
            prox_fuse.SetActive(false);
            prox_fuse.AddComponent<ProxyFuzeMK310>();
        }

        public static void AddFuzeMK310(AmmoType ammo_type)
        {
            if (prox_ammos.Contains(ammo_type.Name)) return;
            prox_ammos.Add(ammo_type.Name);
        }

        void Detonate()
        {
            if (detonated) return;
            live_round._rangedFuseActive = true;
            live_round._rangedFuseCountdown = 0f;
            detonated = true;
        }

        void Update()
        {
            if (!live_round) return;

            RaycastHit hit;
            Vector3 pos = live_round.transform.position;

            if (Physics.Raycast(pos, live_round.transform.forward, out hit, 10f, 1 << 8))
                if (hit.collider.CompareTag("Penetrable"))
                    Detonate();

            RaycastHit hit2;
            if (Physics.Raycast(pos, Vector3.down, out hit2, 30f, 1 << 8))
                if (hit2.collider.CompareTag("Penetrable"))
                    Detonate();

            RaycastHit hit3;
            if (Physics.SphereCast(pos, M6A2_Adats.proxyDistance_mk310.Value, live_round.transform.forward, out hit3, 0.1f, 1 << 8))
                if (hit3.collider.CompareTag("Penetrable"))
                    Detonate();

        }

        [HarmonyPatch(typeof(GHPC.Weapons.LiveRound), "Start")]
        public static class SpawnProximityFuse
        {
            private static void Prefix(GHPC.Weapons.LiveRound __instance)
            {
                if (prox_ammos.Contains(__instance.Info.Name) && __instance.gameObject.transform.Find("mk310 prox fuse(Clone)") == null)
                {
                    GameObject p = GameObject.Instantiate(prox_fuse, __instance.transform);
                    p.GetComponent<ProxyFuzeMK310>().live_round = __instance;
                    p.SetActive(__instance.Shooter.gameObject.GetComponent<ProxySwitchMK310>().activated);
                }
                else if (__instance.gameObject.transform.Find("mk310 prox fuse(Clone)"))
                {
                    GameObject.DestroyImmediate(__instance.gameObject.transform.Find("mk310 prox fuse(Clone)").gameObject);
                }
            }
        }
    }
}
