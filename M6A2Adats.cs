using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BehaviorDesigner.Runtime.Tasks.Movement;
using M6A2Adats;
using GHPC.Camera;
using GHPC.Equipment.Optics;
using GHPC.Player;
using GHPC.Vehicle;
using GHPC.Weapons;
using MelonLoader;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using GHPC;
using NWH.VehiclePhysics;
using GHPC.Equipment;
using GHPC.State;
using GHPC.Utility;
using System.Collections;
using GHPC.AI;
using HarmonyLib;


namespace M6A2Adats
{
    public static class M6A2_Adats
    {
        public static AmmoClipCodexScriptable clip_codex_M919;
        public static AmmoType.AmmoClip clip_M919;
        public static AmmoCodexScriptable ammo_codex_M919;
        public static AmmoType ammo_M919;

        public static AmmoClipCodexScriptable clip_codex_M791m6;
        public static AmmoType.AmmoClip clip_M791m6;
        public static AmmoCodexScriptable ammo_codex_M791m6;
        public static AmmoType ammo_M791m6;

        public static AmmoClipCodexScriptable clip_codex_ADATS;
        public static AmmoType.AmmoClip clip_ADATS;
        public static AmmoCodexScriptable ammo_codex_ADATS;
        public static AmmoType ammo_ADATS;

        public static AmmoClipCodexScriptable clip_codex_APEX;
        public static AmmoType.AmmoClip clip_APEX;
        public static AmmoCodexScriptable ammo_codex_APEX;
        public static AmmoType ammo_APEX;

        public static AmmoClipCodexScriptable clip_codex_M920;
        public static AmmoType.AmmoClip clip_M920;
        public static AmmoCodexScriptable ammo_codex_M920;
        public static AmmoType ammo_M920;

        public static AmmoType ammo_m791;
        public static AmmoType ammo_m792;
        public static AmmoType ammo_I_TOW;

        public static WeaponSystemCodexScriptable gun_xm813;

        public static AmmoClipCodexScriptable clip_codex_mk258;
        public static AmmoType.AmmoClip clip_mk258;
        public static AmmoCodexScriptable ammo_codex_mk258;
        public static AmmoType ammo_mk258;

        public static AmmoClipCodexScriptable clip_codex_mk310;
        public static AmmoType.AmmoClip clip_mk310;
        public static AmmoCodexScriptable ammo_codex_mk310;
        public static AmmoType ammo_mk310;

        static MelonPreferences_Entry<bool> useM919, useM920, adatsTandem, superOptics, betterDynamics, betterAI, compositeTurret, compositeHull, rotateAzimuth, stabilityControl, rippleFire;
        static MelonPreferences_Entry<string> gunType;
        static MelonPreferences_Entry<int> apCount, heCount;
        public static MelonPreferences_Entry<float> proxyDistance;

        public static void Config(MelonPreferences_Category cfg)
        {
            gunType = cfg.CreateEntry<string>("GunType", "GunType");
            gunType.Description = "M242 (500 RPM), GAU12 (3600 RPM), XM813 (30mm)";

            useM919 = cfg.CreateEntry<bool>("M919", false);
            useM919.Description = "Replaces M792 with M919 APFSDS";

            useM920 = cfg.CreateEntry<bool>("MPAB", false);
            useM920.Description = "Replaces APEX with MPAB (TD airburst)";

            apCount = cfg.CreateEntry<int>("APCount", 300);
            apCount.Description = "Round type count, give at least 1 per type (max of 1500 for 25mm/400 for 30mm)";
            heCount = cfg.CreateEntry<int>("HECount", 1200);

            adatsTandem = cfg.CreateEntry<bool>("ADATSTandem", false);
            adatsTandem.Description = "Better ERA defeat for ADATS";

            proxyDistance = cfg.CreateEntry<float>("ProxyDistance", 3);
            proxyDistance.Description = "Trigger distance of ADATS proximity fuze (in meters).";

            rippleFire = cfg.CreateEntry<bool>("RippleFire", false);
            rippleFire.Description = "Fire and guide multiple ADATS at the same time";

            rotateAzimuth = cfg.CreateEntry<bool>("RotateAzimuth", false);
            rotateAzimuth.Description = "Horizontal reticle stabilization when leading";

            superOptics = cfg.CreateEntry<bool>("SuperOptics", false);
            superOptics.Description = "More zoom levels/clearer image for main and thermal sights";

            betterDynamics = cfg.CreateEntry<bool>("BetterDynamics", false);
            betterDynamics.Description = "Better engine/transmission/suspension/tracks";

            stabilityControl = cfg.CreateEntry<bool>("StabilityControl", false);
            stabilityControl.Description = "Makes the tank more controllable at higher speeds";

            betterAI = cfg.CreateEntry<bool>("BetterAI", false);
            betterAI.Description = "Better AI spotting and gunnery";

            compositeHull = cfg.CreateEntry<bool>("CompositeHull", false);
            compositeHull.Description = "50% better protection with no weight penalty";
            compositeTurret = cfg.CreateEntry<bool>("CompositeTurret", false);
        }

        public static IEnumerator Convert(GameState _)
        {
            ////UniformArmor pieces
            ///Extra armor under testing
            foreach (GameObject armour in GameObject.FindGameObjectsWithTag("Penetrable"))
            {
                if (armour == null) continue;

                UniformArmor m2UA = armour.GetComponent<UniformArmor>();
                if (m2UA == null) continue;
                if (m2UA.Unit == null) continue;
                if (m2UA.Unit.FriendlyName == "M2 Bradley")
                {
                    if (compositeTurret.Value)
                    {
                        if (m2UA.Name == "turret face")
                        {
                            m2UA.PrimaryHeatRha = 38.1f;
                            m2UA.PrimarySabotRha = 38.1f;
                        }

                        if (m2UA.Name == "turret side")
                        {
                            m2UA.PrimaryHeatRha = 38.1f;
                            m2UA.PrimarySabotRha = 38.1f;
                        }
                        if (m2UA.Name == "turret rear")
                        {
                            m2UA.PrimaryHeatRha = 38.1f;
                            m2UA.PrimarySabotRha = 38.1f;
                        }

                        if (m2UA.Name == "turret roof")
                        {
                            m2UA.PrimaryHeatRha = 38.1f;
                            m2UA.PrimarySabotRha = 38.1f;
                        }
                        if (m2UA.Name == "turret bottom")
                        {
                            m2UA.PrimaryHeatRha = 38.1f;
                            m2UA.PrimarySabotRha = 38.1f;
                        }

                        if (m2UA.Name == "turret inner frame")
                        {
                            m2UA.PrimaryHeatRha = 38.1f;
                            m2UA.PrimarySabotRha = 38.1f;
                        }
                        if (m2UA.Name == "turret ring collar")
                        {
                            m2UA.PrimaryHeatRha = 38.1f;
                            m2UA.PrimarySabotRha = 38.1f;
                        }

                        if (m2UA.Name == "gun mantlet")
                        {
                            m2UA.PrimaryHeatRha = 38.1f;
                            m2UA.PrimarySabotRha = 38.1f;
                        }

                        if (m2UA.Name == "trunnion shield")
                        {
                            m2UA.PrimaryHeatRha = 12.7f;
                            m2UA.PrimarySabotRha = 12.7f;
                        }
                    }

                    /*if (m2UA.Name == "machine gun sleeve")
                    {
                        m2UA.PrimaryHeatRha = 38.1f;
                        m2UA.PrimarySabotRha = 38.1f;
                    }

                    if (m2UA.Name == "cannon sleeve")
                    {
                        m2UA.PrimaryHeatRha = 38.1f;
                        m2UA.PrimarySabotRha = 38.1f;
                    }

                    if (m2UA.Name == "ammunition access panel")
                    {
                        m2UA.PrimaryHeatRha = 38.1f;
                        m2UA.PrimarySabotRha = 38.1f;
                    }

                    if (m2UA.Name == "gunsight doghouse")
                    {
                        m2UA.PrimaryHeatRha = 25.4f;
                        m2UA.PrimarySabotRha = 25.4f;
                    }

                    if (m2UA.Name == "turret bustle")
                    {
                        m2UA.PrimaryHeatRha = 12.7f;
                        m2UA.PrimarySabotRha = 12.7f;
                    }

                    if (m2UA.Name == "commander's hatch")
                    {
                        m2UA.PrimaryHeatRha = 50.8f;
                        m2UA.PrimarySabotRha = 50.8f;
                    }

                    if (m2UA.Name == "driver's hatch")
                    {
                        m2UA.PrimaryHeatRha = 50.8f;
                        m2UA.PrimarySabotRha = 50.8f;
                    }

                    if (m2UA.Name == "loading hatch")
                    {
                        m2UA.PrimaryHeatRha = 25.4f;
                        m2UA.PrimarySabotRha = 25.4f;
                    }

                    if (m2UA.Name == "swim vane")
                    {
                        m2UA.PrimaryHeatRha = 12.7f;
                        m2UA.PrimarySabotRha = 12.7f;
                    }*/

                    if (compositeHull.Value)
                    {
                        if (m2UA.Name == "anti-mine plate")
                        {
                            m2UA.PrimaryHeatRha = 12.7f;
                            m2UA.PrimarySabotRha = 12.7f;
                        }

                        if (m2UA.Name == "hull front")
                        {
                            m2UA.PrimaryHeatRha = 38.1f;
                            m2UA.PrimarySabotRha = 38.1f;
                        }
                        if (m2UA.Name == "hull side")
                        {
                            m2UA.PrimaryHeatRha = 38.1f;
                            m2UA.PrimarySabotRha = 38.1f;
                        }

                        if (m2UA.Name == "hull side reinforcement")
                        {
                            m2UA.PrimaryHeatRha = 12.7f;
                            m2UA.PrimarySabotRha = 12.7f;
                        }

                        if (m2UA.Name == "hull floor")
                        {
                            m2UA.PrimaryHeatRha = 38.1f;
                            m2UA.PrimarySabotRha = 38.1f;
                        }

                        if (m2UA.Name == "hull rear")
                        {
                            m2UA.PrimaryHeatRha = 38.1f;
                            m2UA.PrimarySabotRha = 38.1f;
                        }

                        if (m2UA.Name == "hull roof")
                        {
                            m2UA.PrimaryHeatRha = 38.1f;
                            m2UA.PrimarySabotRha = 38.1f;
                        }
                    }

                    /*if (m2UA.Name == "gearbox cover")
                    {
                        m2UA.PrimaryHeatRha = 50.8f;
                        m2UA.PrimarySabotRha = 50.8f;
                    }
                    if (m2UA.Name == "firing port")
                    {
                        m2UA.PrimaryHeatRha = 38.1f;
                        m2UA.PrimarySabotRha = 38.1f;
                    }

                    if (m2UA.Name == "rear ramp")
                    {
                        m2UA.PrimaryHeatRha = 50.8f;
                        m2UA.PrimarySabotRha = 50.8f;
                    }
                    if (m2UA.Name == "rear ramp door")
                    {
                        m2UA.PrimaryHeatRha = 38.1f;
                        m2UA.PrimarySabotRha = 38.1f;
                    }

                    if (m2UA.Name == "rear ramp armor plate")
                    {
                        m2UA.PrimaryHeatRha = 12.7f;
                        m2UA.PrimarySabotRha = 12.7f;
                    }
                    if (m2UA.Name == "exhaust shield")
                    {
                        m2UA.PrimaryHeatRha = 12.7f;
                        m2UA.PrimarySabotRha = 12.7f;
                    }

                    if (m2UA.Name == "engine vent")
                    {
                        m2UA.PrimaryHeatRha = 12.7f;
                        m2UA.PrimarySabotRha = 12.7f;
                    }

                    if (m2UA.Name == "firewall")
                    {
                        m2UA.PrimaryHeatRha = 12.7f;
                        m2UA.PrimarySabotRha = 12.7f;
                    }*/
                }
            }

            MelonLogger.Msg("Composite armor loaded");

            foreach (GameObject vic_go in AdatsMod.vic_gos)
            {
                Vehicle vic = vic_go.GetComponent<Vehicle>();

                if (vic == null) continue;
                if (vic.FriendlyName != "M2 Bradley") continue;
                if (vic.GetComponent<Util.AlreadyConvertedADATS>() != null) continue;


                vic.gameObject.AddComponent<Util.AlreadyConvertedADATS>();

                vic_go.AddComponent<ProxySwitchADATS>();
                vic_go.AddComponent<RefineRangeKey>();

                WeaponsManager weaponsManager = vic.GetComponent<WeaponsManager>();
                WeaponSystemInfo mainGunInfo = weaponsManager.Weapons[0];
                WeaponSystem mainGun = mainGunInfo.Weapon;

                WeaponSystemInfo towGunInfo = weaponsManager.Weapons[1];
                WeaponSystem towGun = towGunInfo.Weapon;

                // LRF

                FieldInfo fixParallaxField = typeof(FireControlSystem).GetField("_fixParallaxForVectorMode", BindingFlags.Instance | BindingFlags.NonPublic);
                fixParallaxField.SetValue(mainGun.FCS, true);
                mainGun.FCS.MaxLaserRange = 6000;


                //USSR Vehicles/T80B/T80B_rig/HULL/TURRET/gun/---MAIN GUN SCRIPTS---/2A46-2/1G42 gunner's sight/
                //US Vehicles/M2 Bradley/FCS and sights
                mainGun.FCS.SuperleadWeapon = true;
                mainGun.FCS.SuperelevateWeapon = true;
                mainGun.FCS.RegisteredRangeLimits = new Vector2(100, 6000);
                mainGun.FCS.RecordTraverseRateBuffer = true;
                mainGun.FCS.TraverseBufferSeconds = 0.5f;
                mainGun.FCS.DisplayRangeIncrement = 1;//more precision in UI display

                /*if (Input.GetKey(KeyCode.LeftAlt))//Attempt at making a range refinement key for lower increments when manually ranging
                {
                   mainGun.FCS.RangeStep = 1;
                   mainGun.FCS.Awake();
                   MelonLogger.Msg("LAlt pressed");
                }*/


                mainGun.BaseDeviationAngle = 0.035f;
                mainGun.Impulse = 2000;
                mainGun.RecoilBlurMultiplier = 0.5f;


                LoadoutManager loadoutManager = vic.GetComponent<LoadoutManager>();

                switch (gunType.Value)
                {
                    case "M242":
                        vic._friendlyName = "M6A1 ADATS";
                        mainGun.SetCycleTime(0.12f); //3600 vs 500 RPM
                        mainGun.Feed._totalCycleTime = 0.12f;//3600 vs 500 RPM

                        loadoutManager.LoadedAmmoTypes = new AmmoClipCodexScriptable[] { useM919.Value ? clip_codex_M919 : clip_codex_M791m6, useM920.Value ? clip_codex_M920 : clip_codex_APEX };

                        for (int i = 0; i <= 1; i++)
                        {
                            GHPC.Weapons.AmmoRack rack = loadoutManager.RackLoadouts[i].Rack;
                            loadoutManager.RackLoadouts[i].OverrideInitialClips = new AmmoClipCodexScriptable[] { useM919.Value ? clip_codex_M919 : clip_codex_M791m6, useM920.Value ? clip_codex_M920 : clip_codex_APEX };
                            rack.ClipTypes = new AmmoType.AmmoClip[] { useM919.Value ? clip_M919 : clip_M791m6, useM920.Value ? clip_M920 : clip_APEX };
                            Util.EmptyRack(rack);
                        }
                        //
                        break;

                    case "GAU12":
                        vic._friendlyName = "M6A2 ADATS";
                        mainGunInfo.Name = "25mm Cannon GAU-12/U Equalizer";
                        mainGun.SetCycleTime(0.0166f);
                        mainGun.Feed._totalCycleTime = 0.0166f;//3600 vs 500 RPM

                        loadoutManager.LoadedAmmoTypes = new AmmoClipCodexScriptable[] { useM919.Value ? clip_codex_M919 : clip_codex_M791m6, useM920.Value ? clip_codex_M920 : clip_codex_APEX };

                        for (int i = 0; i <= 1; i++)
                        {
                            GHPC.Weapons.AmmoRack rack = loadoutManager.RackLoadouts[i].Rack;
                            loadoutManager.RackLoadouts[i].OverrideInitialClips = new AmmoClipCodexScriptable[] { useM919.Value ? clip_codex_M919 : clip_codex_M791m6, useM920.Value ? clip_codex_M920 : clip_codex_APEX };
                            rack.ClipTypes = new AmmoType.AmmoClip[] { useM919.Value ? clip_M919 : clip_M791m6, useM920.Value ? clip_M920 : clip_APEX };
                            Util.EmptyRack(rack);
                        }
                        //
                        break;

                    case "XM813":
                        vic._friendlyName = "M6A3 ADATS";
                        mainGun.SetCycleTime(0.25f);//240 RPM
                        mainGun.Feed._totalCycleTime = 0.25f;
                        mainGun.BaseDeviationAngle = 0.025f;

                        mainGunInfo.Name = "30mm Gun XM813";
                        FieldInfo codex = typeof(WeaponSystem).GetField("CodexEntry", BindingFlags.NonPublic | BindingFlags.Instance);
                        codex.SetValue(mainGun, gun_xm813);

                        GameObject gunTube = vic_go.transform.Find("M2BRADLEY_rig/HULL/Turret/Mantlet/Main gun").gameObject;
                        gunTube.transform.localPosition = new Vector3(0.0825f, 0.0085f, 2.25f);// default 0.0826,0.0085,2.2239
                        gunTube.transform.localScale = new Vector3(1.2f, 1.2f, 1.15f);//default 1,1,1        

                        GameObject gunTubeStart = vic_go.transform.Find("M2BRADLEY_rig/HULL/Turret/Mantlet/bushmaster start").gameObject;
                        gunTubeStart.transform.localPosition = new Vector3(0.0825f, 0.0085f, 2.25f);//0.0826,0.0085,2.2239

                        GameObject gunTubeEnd = vic_go.transform.Find("M2BRADLEY_rig/HULL/Turret/Mantlet/bushmaster end").gameObject;
                        gunTubeEnd.transform.localPosition = new Vector3(0.0825f, 0.0085f, 2.2f);//0.0826,0.0085,2.176

                        // more powah
                        Transform muzzleFlashes = mainGun.MuzzleEffects[0].transform;
                        muzzleFlashes.localPosition = new Vector3(0.0f, 0.0f, 0.1f);

                        //default localScale values for muzzle flashes is 1,1,1
                        // Gunsmoke Side
                        muzzleFlashes.GetChild(0).transform.localScale = new Vector3(2f, 2f, 2f);
                        // Gunsmoke Side
                        muzzleFlashes.GetChild(1).transform.localScale = new Vector3(2f, 2f, 2f);
                        // Muzzle Flash Front
                        muzzleFlashes.GetChild(2).transform.localScale = new Vector3(2f, 2f, 2f);
                        // Muzzle Flash Side
                        muzzleFlashes.GetChild(3).transform.localScale = new Vector3(2f, 2f, 2f);
                        // Muzzle Flash Sparks Side
                        muzzleFlashes.GetChild(4).transform.localScale = new Vector3(2f, 2f, 2f);
                        // Muzzle Flash Sparks Front
                        muzzleFlashes.GetChild(5).transform.localScale = new Vector3(2f, 2f, 2f);
                        // Gunsmoke Brake Side R
                        muzzleFlashes.GetChild(6).transform.localScale = new Vector3(2f, 2f, 2f);
                        // Gunsmoke Brake Side L
                        muzzleFlashes.GetChild(7).transform.localScale = new Vector3(2f, 2f, 2f);
                        //Gunsmoke Brake Side Top
                        muzzleFlashes.GetChild(8).transform.localScale = new Vector3(2f, 2f, 2f);
                        //Gunsmoke Long
                        muzzleFlashes.GetChild(9).transform.localScale = new Vector3(2f, 2f, 2f);
                        // Gunsmoke Front R
                        muzzleFlashes.GetChild(10).transform.localScale = new Vector3(2f, 2f, 2f);
                        // Gunsmoke Front L
                        muzzleFlashes.GetChild(11).transform.localScale = new Vector3(2f, 2f, 2f);
                        // Muzzle Flash Brake R
                        muzzleFlashes.GetChild(12).transform.localScale = new Vector3(2f, 2f, 2f);
                        // Muzzle Flash Brake L
                        muzzleFlashes.GetChild(13).transform.localScale = new Vector3(2f, 2f, 2f);


                        loadoutManager.LoadedAmmoTypes = new AmmoClipCodexScriptable[] { clip_codex_mk258, clip_codex_mk310 };

                        for (int i = 0; i <= 1; i++)
                        {
                            GHPC.Weapons.AmmoRack rack = loadoutManager.RackLoadouts[i].Rack;
                            loadoutManager.RackLoadouts[i].OverrideInitialClips = new AmmoClipCodexScriptable[] { clip_codex_mk258, clip_codex_mk310 };
                            rack.ClipTypes = new AmmoType.AmmoClip[] { clip_mk258, clip_mk310 };
                            Util.EmptyRack(rack);
                        }
                        //
                        break;

                    default:
                        vic._friendlyName = "M6A1 ADATS";
                        mainGun.SetCycleTime(0.12f); //3600 vs 500 RPM
                        mainGun.Feed._totalCycleTime = 0.12f;//3600 vs 500 RPM

                        loadoutManager.LoadedAmmoTypes = new AmmoClipCodexScriptable[] { useM919.Value ? clip_codex_M919 : clip_codex_M791m6, useM920.Value ? clip_codex_M920 : clip_codex_APEX };

                        for (int i = 0; i <= 1; i++)
                        {
                            GHPC.Weapons.AmmoRack rack = loadoutManager.RackLoadouts[i].Rack;
                            loadoutManager.RackLoadouts[i].OverrideInitialClips = new AmmoClipCodexScriptable[] { useM919.Value ? clip_codex_M919 : clip_codex_M791m6, useM920.Value ? clip_codex_M920 : clip_codex_APEX };
                            rack.ClipTypes = new AmmoType.AmmoClip[] { useM919.Value ? clip_M919 : clip_M791m6, useM920.Value ? clip_M920 : clip_APEX };
                            Util.EmptyRack(rack);
                        }
                        break;

                }

                towGunInfo.Name = "ADATS Launcher";
                towGun.TriggerHoldTime = 0.5f;
                towGun.MaxSpeedToFire = 999f;
                towGun.MaxSpeedToDeploy = 999f;
                towGun.RecoilBlurMultiplier = 0.2f;
                towGun.FireWhileGuidingMissile = rippleFire.Value;
                vic.AimablePlatforms[2].ForcedStowSpeed = 999f;


                GHPC.Weapons.AmmoRack towRack = towGun.Feed.ReadyRack;

                towRack.ClipTypes[0] = clip_ADATS;

                towRack.StoredClips[0] = clip_ADATS;
                towRack.StoredClips[1] = clip_ADATS;
                towRack.StoredClips[2] = clip_ADATS;

                loadoutManager.SpawnCurrentLoadout();

                PropertyInfo roundInBreech = typeof(AmmoFeed).GetProperty("AmmoTypeInBreech");

                roundInBreech.SetValue(mainGun.Feed, null);
                roundInBreech.SetValue(towGun.Feed, null);

                MethodInfo refreshBreech = typeof(AmmoFeed).GetMethod("Start", BindingFlags.Instance | BindingFlags.NonPublic);
                refreshBreech.Invoke(mainGun.Feed, new object[] { });
                refreshBreech.Invoke(towGun.Feed, new object[] { });

                towRack.AddInvisibleClip(clip_ADATS);

                // update ballistics computer
                MethodInfo registerAllBallistics = typeof(LoadoutManager).GetMethod("RegisterAllBallistics", BindingFlags.Instance | BindingFlags.NonPublic);
                registerAllBallistics.Invoke(loadoutManager, new object[] { });

                // Better Thermals
                var gpsOptic = vic_go.transform.Find("M2BRADLEY_rig/HULL/Turret/GPS Optic/").gameObject.transform;
                var flirOptic = vic_go.transform.Find("M2BRADLEY_rig/HULL/Turret/FLIR/").gameObject.transform;

                UsableOptic horizontalGps = gpsOptic.GetComponent<UsableOptic>();
                UsableOptic horizontalFlir = flirOptic.GetComponent<UsableOptic>();

                CameraSlot daysightPlus = gpsOptic.GetComponent<CameraSlot>();
                CameraSlot flirPlus = flirOptic.GetComponent<CameraSlot>();



                if (rotateAzimuth.Value)
                {
                    horizontalGps.RotateAzimuth = true;
                    horizontalFlir.RotateAzimuth = true;
                }

                if (superOptics.Value)
                {
                    daysightPlus.DefaultFov = 16.5f;//8
                    daysightPlus.OtherFovs = new float[] { 8f, 5.5f, 4f, 2.5f, 1.25f, 0.5f };//2.5
                    daysightPlus.BaseBlur = 0;
                    daysightPlus.VibrationBlurScale = 0;

                    flirPlus.DefaultFov = 16.5f;//8
                    flirPlus.OtherFovs = new float[] { 8f, 5.5f, 4f, 2.5f, 1.25f, 0.5f };//2.5
                    flirPlus.BaseBlur = 0;
                    flirPlus.VibrationBlurScale = 0;
                    GameObject.Destroy(flirOptic.transform.Find("Canvas Scanlines").gameObject);

                    MelonLogger.Msg("Super Optics Loaded");
                }

                //Vehicle dynamics under testing
                VehicleController vicVC = vic_go.GetComponent<VehicleController>();
                NwhChassis vicNwhC = vic_go.GetComponent<NwhChassis>();
                UnitAI vicUAI = vic.GetComponentInChildren<UnitAI>();
                DriverAIController vicDAIC = vic.GetComponent<DriverAIController>();

                if (betterDynamics.Value)
                {
                    vicDAIC.maxSpeed = 32;//20

                    vicVC.engine.maxPower = 1200f;//530;
                    vicVC.engine.maxRPM = 4500f;//4000 ;
                    vicVC.engine.maxRpmChange = 3000f;//2000;

                    vicVC.brakes.maxTorque = 55590;//49590

                    vicNwhC._maxForwardSpeed = 32f;//16.4
                    vicNwhC._maxReverseSpeed = 16f;//4.47


                    List<float> fwGears = new List<float>();
                    fwGears.Add(6.28f);
                    fwGears.Add(4.81f);
                    fwGears.Add(2.98f);
                    fwGears.Add(1.76f);
                    fwGears.Add(1.36f);
                    fwGears.Add(1.16f);

                    List<float> rvGears = new List<float>();
                    rvGears.Add(-2.76f);
                    //rvGears.Add(-2.98f);
                    rvGears.Add(-8.28f);

                    List<float> Gears = new List<float>();
                    Gears.Add(-2.76f);
                    //Gears.Add(-2.98f);
                    Gears.Add(-8.28f);
                    Gears.Add(0f);
                    Gears.Add(6.28f);
                    Gears.Add(4.81f);
                    Gears.Add(2.98f);
                    Gears.Add(1.76f);
                    Gears.Add(1.36f);
                    Gears.Add(1.16f);

                    vicVC.transmission.forwardGears = fwGears;//5 2.4 1.9 1.6 1.4 1.2
                    vicVC.transmission.gearMultiplier = 9.918f;//9.918
                    vicVC.transmission.gears = Gears;//
                    vicVC.transmission.reverseGears = rvGears;//-8
                    vicVC.transmission.initialShiftDuration = 0.1f;//.309
                    vicVC.transmission.shiftDurationRandomness = 0f;//.2
                    vicVC.transmission.shiftPointRandomness = 0.05f;//.05


                    for (int i = 0; i < 12; i++)
                    {
                        //m3a2Vc.wheels[i].wheelController.damper.force = 2.3036f;//2.3036
                        vicVC.wheels[i].wheelController.damper.maxForce = 6500;//6500
                        vicVC.wheels[i].wheelController.damper.unitBumpForce = 6500;//6500
                        vicVC.wheels[i].wheelController.damper.unitReboundForce = 9000;//9000

                        //m3a2Vc.wheels[i].wheelController.spring.bottomOutForce = 0f;//0
                        vicVC.wheels[i].wheelController.spring.force = 24079.51f;//24079.51
                        vicVC.wheels[i].wheelController.spring.length = 0.32f;//0.2809
                        vicVC.wheels[i].wheelController.spring.maxForce = 100000;//100000
                        vicVC.wheels[i].wheelController.spring.maxLength = 0.58f;//0.48

                        vicVC.wheels[i].wheelController.fFriction.forceCoefficient = 1.25f;//1.2
                        vicVC.wheels[i].wheelController.fFriction.slipCoefficient = 1f;//1

                        vicVC.wheels[i].wheelController.sFriction.forceCoefficient = 0.85f;//0.8
                        vicVC.wheels[i].wheelController.sFriction.slipCoefficient = 1f;//1 
                    }

                    vic.AimablePlatforms[0].SpeedPowered = 80;//60
                    vic.AimablePlatforms[0].SpeedUnpowered = 20;//5

                    MelonLogger.Msg("Better vehicle dynamics loaded");
                }


                if (betterAI.Value)
                {
                    vicUAI.SpotTimeMaxDistance = 3500;
                    vicUAI.TargetSensor._spotTimeMax = 3;
                    vicUAI.TargetSensor._spotTimeMaxDistance = 500;
                    vicUAI.TargetSensor._spotTimeMaxVelocity = 7f;
                    vicUAI.TargetSensor._spotTimeMin = 1;
                    vicUAI.TargetSensor._spotTimeMinDistance = 50;
                    //vicUAI.TargetSensor._targetCooldownTime = 1.5f;

                    vicUAI.CommanderAI._identifyTargetDurationRange = new Vector2(1.5f, 2.5f);
                    vicUAI.CommanderAI._sweepCommsCheckDuration = 4;


                    vicUAI.combatSpeedLimit = 25;
                    vicUAI.firingSpeedLimit = 20;

                    //m2Ai.AccuracyModifiers.Angle._radius = 2.4f;
                    vicUAI.AccuracyModifiers.Angle.MaxDistance = 1500;
                    vicUAI.AccuracyModifiers.Angle.MaxRadius = 5f;
                    vicUAI.AccuracyModifiers.Angle.MinRadius = 2f;
                    vicUAI.AccuracyModifiers.Angle.IncreaseAccuracyPerShot = false;

                    MelonLogger.Msg("Better AI loaded");
                }


                if (stabilityControl.Value)
                {
                    vicVC.drivingAssists.stability.active = true;
                    vicVC.drivingAssists.stability.enabled = true;
                    vicVC.drivingAssists.stability.intensity = 0.2f;
                }

                ////ERA detection for BUSK designation
                if (vic.UniqueName == "M2BRADLEY")
                {
                    GameObject hullARAT = vic.GetComponent<LateFollowTarget>()._lateFollowers[0].transform.Find("HULL").gameObject;
                    if (hullARAT.transform.Find("M2 Hull ERA Array(Clone)/") != null)
                    {
                        vic._friendlyName += " BUSK";
                    }
                }
            }

            yield break;
        }

        public static void Init()
        {
            if (ammo_M919 == null)
            {
                foreach (AmmoCodexScriptable s in Resources.FindObjectsOfTypeAll(typeof(AmmoCodexScriptable)))
                {
                    if (s.AmmoType.Name == "25mm APDS-T M791") ammo_m791 = s.AmmoType;
                    if (s.AmmoType.Name == "25mm HEI-T M792") ammo_m792 = s.AmmoType;
                    if (s.AmmoType.Name == "BGM-71C I-TOW") ammo_I_TOW = s.AmmoType;
                }

                var era_optimizations_adats = new List<AmmoType.ArmorOptimization>() { };

                string[] era_names = new string[] {
                    "kontakt-1 armour",
                    "kontakt-5 armour",
                    "relikt armour",
                    "ARAT-1 Armor Codex",
                    "BRAT-M3 Armor Codex",
                    "BRAT-M5 Armor Codex",
                };

                foreach (ArmorCodexScriptable s in Resources.FindObjectsOfTypeAll<ArmorCodexScriptable>())
                {
                    if (era_names.Contains(s.name))
                    {
                        AmmoType.ArmorOptimization optimization_adats = new AmmoType.ArmorOptimization();
                        optimization_adats.Armor = s;
                        optimization_adats.RhaRatio = 0.2f;
                        era_optimizations_adats.Add(optimization_adats);
                    }

                    if (era_optimizations_adats.Count == era_names.Length) break;
                }

                int apCapacity_25mm = apCount.Value;
                int heCapacity_25mm = heCount.Value;
                MelonLogger.Msg("Total 25mm AP/HE Count: " + (apCapacity_25mm + heCapacity_25mm));

                if ((apCapacity_25mm + heCapacity_25mm) > 1500)
                {
                    apCapacity_25mm = 300;
                    heCapacity_25mm = 1200;
                    MelonLogger.Msg("Invalid total 25mm AP/HE amount, defaulting to 300 AP/1200 HE");
                }

                int apCapacity_30mm = apCount.Value;
                int heCapacity_30mm = heCount.Value;

                if (gunType.Value == "XM813")
                {
                    MelonLogger.Msg("Total 30mm AP/HE Count: " + (apCapacity_30mm + heCapacity_30mm));

                    if ((apCapacity_30mm + heCapacity_30mm) > 400)
                    {
                        apCapacity_30mm = 200;
                        heCapacity_30mm = 200;
                        MelonLogger.Msg("Invalid total 30mm AP/HE amount, defaulting to 200 AP/200 HE");
                    }
                }

                // M791 APFSDS-T

                ammo_M791m6 = new AmmoType();
                Util.ShallowCopy(ammo_M791m6, ammo_m791);
                ammo_M791m6.Name = "M791 APDS-T";

                ammo_codex_M791m6 = ScriptableObject.CreateInstance<AmmoCodexScriptable>();
                ammo_codex_M791m6.AmmoType = ammo_M791m6;
                ammo_codex_M791m6.name = "ammo_M791m6";

                clip_M791m6 = new AmmoType.AmmoClip();
                clip_M791m6.Capacity = apCapacity_25mm;
                clip_M791m6.Name = "M791 APDS-T";
                clip_M791m6.MinimalPattern = new AmmoCodexScriptable[1];
                clip_M791m6.MinimalPattern[0] = ammo_codex_M791m6;

                clip_codex_M791m6 = ScriptableObject.CreateInstance<AmmoClipCodexScriptable>();
                clip_codex_M791m6.name = "clip_M791m6";
                clip_codex_M791m6.ClipType = clip_M791m6;

                // M919 APFSDS-T

                ammo_M919 = new AmmoType();
                Util.ShallowCopy(ammo_M919, ammo_m791);
                ammo_M919.Name = "M919 APFSDS-T";
                ammo_M919.Caliber = 25;
                ammo_M919.RhaPenetration = 102f;
                ammo_M919.MuzzleVelocity = 1390f;
                ammo_M919.Mass = 0.134f;
                ammo_M919.CertainRicochetAngle = 5;
                ammo_M919.SpallMultiplier = 1.25f;
                ammo_M919.MaxSpallRha = 10;
                ammo_M919.MinSpallRha = 3;

                ammo_codex_M919 = ScriptableObject.CreateInstance<AmmoCodexScriptable>();
                ammo_codex_M919.AmmoType = ammo_M919;
                ammo_codex_M919.name = "ammo_M919";

                clip_M919 = new AmmoType.AmmoClip();
                clip_M919.Capacity = apCapacity_25mm;
                clip_M919.Name = "M919 APFSDS-T";
                clip_M919.MinimalPattern = new AmmoCodexScriptable[1];
                clip_M919.MinimalPattern[0] = ammo_codex_M919;

                clip_codex_M919 = ScriptableObject.CreateInstance<AmmoClipCodexScriptable>();
                clip_codex_M919.name = "clip_M919";
                clip_codex_M919.ClipType = clip_M919;

                // ADATS
                ammo_ADATS = new AmmoType();
                Util.ShallowCopy(ammo_ADATS, ammo_I_TOW);
                ammo_ADATS.Name = "MIM-146 ADATS";
                ammo_ADATS.Caliber = 152;
                ammo_ADATS.RhaPenetration = 1000f;
                ammo_ADATS.MuzzleVelocity = 510f;
                ammo_ADATS.Mass = 51f;
                ammo_ADATS.TntEquivalentKg = 12.5f;
                ammo_ADATS.Tandem = true;
                ammo_ADATS.SpallMultiplier = 1.5f;
                ammo_ADATS.TurnSpeed = 2.5f;
                ammo_ADATS.DetonateSpallCount = 300;
                ammo_ADATS.MaxSpallRha = 50;
                ammo_ADATS.MinSpallRha = 3;
                ammo_ADATS.MaximumRange = 10000;
                ammo_ADATS.ImpactFuseTime = 20; //max flight time is 20 secs
                ammo_ADATS.CertainRicochetAngle = 5;
                if (adatsTandem.Value) ammo_ADATS.ArmorOptimizations = era_optimizations_adats.ToArray<AmmoType.ArmorOptimization>();
                ProxyFuzeADATS.AddFuzeADATS(ammo_ADATS);

                ammo_codex_ADATS = ScriptableObject.CreateInstance<AmmoCodexScriptable>();
                ammo_codex_ADATS.AmmoType = ammo_ADATS;
                ammo_codex_ADATS.name = "ammo_ADATS";

                clip_ADATS = new AmmoType.AmmoClip();
                clip_ADATS.Capacity = 4;
                clip_ADATS.Name = "MIM-146 ADATS";
                clip_ADATS.MinimalPattern = new AmmoCodexScriptable[1];
                clip_ADATS.MinimalPattern[0] = ammo_codex_ADATS;

                clip_codex_ADATS = ScriptableObject.CreateInstance<AmmoClipCodexScriptable>();
                clip_codex_ADATS.name = "clip_ADAT";
                clip_codex_ADATS.ClipType = clip_ADATS;

                // APEX APHE-T
                ammo_APEX = new AmmoType();
                Util.ShallowCopy(ammo_APEX, ammo_m792);
                ammo_APEX.Name = "APEX APHE-T";
                ammo_APEX.Coeff = 0.1f;
                ammo_APEX.Caliber = 25;
                ammo_APEX.RhaPenetration = 35f;
                ammo_APEX.MuzzleVelocity = 1270f;
                ammo_APEX.Mass = 0.222f;
                ammo_APEX.TntEquivalentKg = 0.050f;
                ammo_APEX.SpallMultiplier = 1.25f;
                ammo_APEX.DetonateSpallCount = 30;
                ammo_APEX.MaxSpallRha = 16f;
                ammo_APEX.MinSpallRha = 2f;
                ammo_APEX.CertainRicochetAngle = 5;

                ammo_codex_APEX = ScriptableObject.CreateInstance<AmmoCodexScriptable>();
                ammo_codex_APEX.AmmoType = ammo_APEX;
                ammo_codex_APEX.name = "ammo_APEX";

                clip_APEX = new AmmoType.AmmoClip();
                clip_APEX.Capacity = heCapacity_25mm;
                clip_APEX.Name = "APEX APHE-T";
                clip_APEX.MinimalPattern = new AmmoCodexScriptable[1];
                clip_APEX.MinimalPattern[0] = ammo_codex_APEX;

                clip_codex_APEX = ScriptableObject.CreateInstance<AmmoClipCodexScriptable>();
                clip_codex_APEX.name = "clip_APEX";
                clip_codex_APEX.ClipType = clip_APEX;

                // M920 MPAB-T
                ammo_M920 = new AmmoType();
                Util.ShallowCopy(ammo_M920, ammo_m792);
                ammo_M920.Name = "M920 MPAB-T";
                ammo_M920.Coeff = 0.1f;
                ammo_M920.Caliber = 25;
                ammo_M920.RhaPenetration = 15f;
                ammo_M920.MuzzleVelocity = 1270f;
                ammo_M920.Mass = 0.222f;
                ammo_M920.TntEquivalentKg = 0.050f;
                ammo_M920.SpallMultiplier = 2f;
                ammo_M920.DetonateSpallCount = 60;
                ammo_M920.MaxSpallRha = 32f;
                ammo_M920.MinSpallRha = 2f;
                ammo_M920.CertainRicochetAngle = 5;

                ammo_codex_M920 = ScriptableObject.CreateInstance<AmmoCodexScriptable>();
                ammo_codex_M920.AmmoType = ammo_M920;
                ammo_codex_M920.name = "ammo_M920";

                clip_M920 = new AmmoType.AmmoClip();
                clip_M920.Capacity = heCapacity_25mm;
                clip_M920.Name = "M920 MPAB-T";
                clip_M920.MinimalPattern = new AmmoCodexScriptable[1];
                clip_M920.MinimalPattern[0] = ammo_codex_M920;

                clip_codex_M920 = ScriptableObject.CreateInstance<AmmoClipCodexScriptable>();
                clip_codex_M920.name = "clip_M920";
                clip_codex_M920.ClipType = clip_M920;

                // MK258 
                ammo_mk258 = new AmmoType();
                Util.ShallowCopy(ammo_mk258, ammo_m791);
                ammo_mk258.Name = "MK258 APFSDS-T";
                ammo_mk258.Caliber = 30;
                ammo_mk258.RhaPenetration = 116f;
                ammo_mk258.MuzzleVelocity = 1430f;
                ammo_mk258.Mass = 0.161f;
                ammo_mk258.CertainRicochetAngle = 5;
                ammo_mk258.SpallMultiplier = 1.5f;
                ammo_mk258.MaxSpallRha = 14;
                ammo_mk258.MinSpallRha = 4;


                ammo_codex_mk258 = ScriptableObject.CreateInstance<AmmoCodexScriptable>();
                ammo_codex_mk258.AmmoType = ammo_mk258;
                ammo_codex_mk258.name = "ammo_mk258";

                clip_mk258 = new AmmoType.AmmoClip();
                clip_mk258.Capacity = apCapacity_30mm;
                clip_mk258.Name = "MK258 APFSDS-T";
                clip_mk258.MinimalPattern = new AmmoCodexScriptable[1];
                clip_mk258.MinimalPattern[0] = ammo_codex_mk258;

                clip_codex_mk258 = ScriptableObject.CreateInstance<AmmoClipCodexScriptable>();
                clip_codex_mk258.name = "clip_mk258";
                clip_codex_mk258.CompatibleWeaponSystems = new WeaponSystemCodexScriptable[1];
                clip_codex_mk258.CompatibleWeaponSystems[0] = gun_xm813;
                clip_codex_mk258.ClipType = clip_mk258;

                // MK310
                ammo_mk310 = new AmmoType();
                Util.ShallowCopy(ammo_mk310, ammo_m792);
                ammo_mk310.Name = "MK310 PABM-T";
                ammo_mk310.Coeff = 0.12f;
                ammo_mk310.Caliber = 30;
                ammo_mk310.RhaPenetration = 30f;
                ammo_mk310.MuzzleVelocity = 1170f;
                ammo_mk310.Mass = 0.424f;
                ammo_mk310.TntEquivalentKg = 0.140f;
                ammo_mk310.SpallMultiplier = 2.25f;
                ammo_mk310.DetonateSpallCount = 60;
                ammo_mk310.MaxSpallRha = 32f;
                ammo_mk310.MinSpallRha = 2f;
                ammo_mk310.CertainRicochetAngle = 5;

                ammo_codex_mk310 = ScriptableObject.CreateInstance<AmmoCodexScriptable>();
                ammo_codex_mk310.AmmoType = ammo_mk310;
                ammo_codex_mk310.name = "ammo_mk310";

                clip_mk310 = new AmmoType.AmmoClip();
                clip_mk310.Capacity = heCapacity_30mm;
                clip_mk310.Name = "MK310 PABM-T";
                clip_mk310.MinimalPattern = new AmmoCodexScriptable[1];
                clip_mk310.MinimalPattern[0] = ammo_codex_mk310;

                clip_codex_mk310 = ScriptableObject.CreateInstance<AmmoClipCodexScriptable>();
                clip_codex_mk310.name = "clip_mk310";
                clip_codex_mk310.CompatibleWeaponSystems = new WeaponSystemCodexScriptable[1];
                clip_codex_mk310.CompatibleWeaponSystems[0] = gun_xm813;
                clip_codex_mk310.ClipType = clip_mk310;
            }
            StateController.RunOrDefer(GameState.GameReady, new GameStateEventHandler(Convert), GameStatePriority.Lowest);
        }


        [HarmonyPatch(typeof(GHPC.Weapons.LiveRound), "Start")]
        public static class Airburst
        {
            private static void Postfix(GHPC.Weapons.LiveRound __instance)
            {
                if (__instance.Info.Name == "M920 MPAB-T")
                {

                    FieldInfo rangedFuseTimeField_m920 = typeof(GHPC.Weapons.LiveRound).GetField("_rangedFuseCountdown", BindingFlags.Instance | BindingFlags.NonPublic);
                    FieldInfo rangedFuseTimeActiveField_m920 = typeof(GHPC.Weapons.LiveRound).GetField("_rangedFuseActive", BindingFlags.Instance | BindingFlags.NonPublic);
                    FieldInfo ballisticsComputerField_m920 = typeof(FireControlSystem).GetField("_bc", BindingFlags.Instance | BindingFlags.NonPublic);

                    FireControlSystem FCS_m920 = __instance.Shooter.WeaponsManager.Weapons[0].FCS;
                    BallisticComputerRepository bc_m920 = ballisticsComputerField_m920.GetValue(FCS_m920) as BallisticComputerRepository;

                    float range_m920 = FCS_m920.CurrentRange;
                    float fallOff_m920 = bc_m920.GetFallOfShot(M6A2_Adats.ammo_M920, range_m920);
                    float extra_distance_m920 = range_m920 > 2000 ? 19f + 3.5f : 17f;

                    //funky math 
                    rangedFuseTimeField_m920.SetValue(__instance, bc_m920.GetFlightTime(M6A2_Adats.ammo_M920, range_m920 + range_m920 / M6A2_Adats.ammo_M920.MuzzleVelocity * 2 + (range_m920 + fallOff_m920) / 2000f + extra_distance_m920));
                    rangedFuseTimeActiveField_m920.SetValue(__instance, true);
                }

                if (__instance.Info.Name == "MK310 PABM-T")
                {

                    FieldInfo rangedFuseTimeField_mk310 = typeof(GHPC.Weapons.LiveRound).GetField("_rangedFuseCountdown", BindingFlags.Instance | BindingFlags.NonPublic);
                    FieldInfo rangedFuseTimeActiveField_mk310 = typeof(GHPC.Weapons.LiveRound).GetField("_rangedFuseActive", BindingFlags.Instance | BindingFlags.NonPublic);
                    FieldInfo ballisticsComputerField_mk310 = typeof(FireControlSystem).GetField("_bc", BindingFlags.Instance | BindingFlags.NonPublic);

                    FireControlSystem FCS_mk310 = __instance.Shooter.WeaponsManager.Weapons[0].FCS;
                    BallisticComputerRepository bc_mk310 = ballisticsComputerField_mk310.GetValue(FCS_mk310) as BallisticComputerRepository;

                    float range_mk310 = FCS_mk310.CurrentRange;
                    float fallOff_mk310 = bc_mk310.GetFallOfShot(M6A2_Adats.ammo_mk310, range_mk310);
                    float extra_distance_mk310 = range_mk310 > 2000 ? 19f + 3.5f : 17f;

                    //funky math 
                    rangedFuseTimeField_mk310.SetValue(__instance, bc_mk310.GetFlightTime(M6A2_Adats.ammo_mk310, range_mk310 + range_mk310 / M6A2_Adats.ammo_mk310.MuzzleVelocity * 2 + (range_mk310 + fallOff_mk310) / 2000f + extra_distance_mk310));
                    rangedFuseTimeActiveField_mk310.SetValue(__instance, true);
                }

            }
        }
    }
}