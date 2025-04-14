# M6A2 ADATS v1.3

This mod replaces the M2 Bradley with the M6 Linebacker but pretend it has the GAU-12 (and others) and ADATS instead!

## Features:

- Converts M2 Bradley to a hypothetical M6A1/A2/A3 ADATS variant
- Replaces the vanilla 25mm M242 Bushmaster autocannon with an improved M242 or 25mm GAU-12/U Equalizer rotary cannon or 30mm XM813 chain gun
- GAU-12/U Equalizer: 3600 RPM and 1500 rounds (300 AP/1200 APHE)
- Designated as M6A1 ADATS when using improved M242, M6A2 ADATS when using GAU-12 and M6A3 when using XM813
- Automatic gun lead calculation (like the Abrams) and optional reticle horizontal stabilization
- Replaces the BGM-71C I-TOW with the MIM-146 ADATS
- Expanded ammunition list (see table below)
- MIM-146 ADATS: 4 ready to launch (imagine two tubes on each side of the turret) and 12 stowed
- "BUSK" postfix when ERA is detected (NATO ERA v1.2.2+ required)
- Different config options (see more detailed info below)

## Installation:
1.) Install [MelonLoader v0.6.1](https://github.com/LavaGang/MelonLoader/).

2.) Download the latest version from the [release page](https://github.com/Cyances/M6A2-ADATS/releases).

3.) Place zM6A2Adats.dll file in the mods folder:

4.) Launch the game directly (not from Steam).
   
5.) On first time running this mod, the entries in MelonPreferences.cfg will only appear after launching the game then closing it.

  ## Round types list:
| Name  | Penetration (mm) | Fragment/Spalling Penetration (mm)| Muzzle Velocity (m/s) | Note |
| ------------- | ------------- | ------------- | ------------- | ------------- |
| M791 APDS-T | 60 |  | 1345  |  |
| M919 APFSDS-T  | 102 |  | 1575 | +25% spalling chance and +66% spalling performance. |
| M792 HEI-T | 8* |  | 1100 |  |
| APEX APHE-T | 35* | 16** | 1270 | Point-detonate fuze only. |
| M920 MPAB-T | 15* | 32** | 1270 | Hypothethical round. Point-detonate + airburst fuze.  |
| MK258 APFSDS-T | 116 |  | 1430 | +50% spalling chance and +133% spalling performance. |
| MK310 PABM-T | 30* | 32** | 1170 | Point-detonate + airburst fuze or proximity fuze. |
| MIM-146 ADATS | 1000 | 50** | 510 | Point-detonate + proximity fuze. Optional tandem warhead config where it makes ERA only 25% effective. |

- *These are HE rounds so actual penetration may not be the same as the table
- **These are <i>up to</i> values so not every fragment will perform the same


## How to use the M920 MPAB-T, XM310 PABM-T and MIM-146 ADATS:
### MPAB Point-detonate + Time-delay Fuze

- To use airburst mode, simply laze the target. The round will detonate araound the distance set by LRF/manual elevation.
- To use point-detonate mode, make sure the range setting is at least 10 meters more than the target to ensure it would not be in airburst mode. As long as the round directly hits the target, it will use the point-detonate fuze.


### ADATS Point-detonate + Proxmity Fuze
- To use proximity mode, press middle mouse button and the round should have [Proximity] suffix to its name in the lower left part of the UI
- To use point-detonate mode, make sure the [Proximity] suffix is not present


## Mod Configuration (in UserData/MelonPreferences.cfg):
- I suggest getting Notepad++ so it would be easier to identify each category
- Rebuild the config file when upgrading to v1.3
- Gun type (M242 by default)
- Use M919 (false by default)
- Use M920 MPAB (false by default)
- AP and HE round count (300/1200 by default). If total round count exceeds 1500 for 25mm or 600 for 30mm, the mod will use the default mix.
- Enable ADATS tandem warhead (false by default)
- Gun proximity fuze (false by default, switches PABM to proxmity instead of time-day)
- ADATS proximity fuze sensitivity (3 by default)
- Gun proximity fuze sensitivity (2.5 by default, for PABM only)
- Reticle horizontal stabilization when leading (false by default)
- Super optics (false by default)
- Better vehicle dynamics (false by default)
- Better AI spotting and gunnery skill (false by default)
- Composite hull (false by default)
- Composite turret (false by default)

![image](https://github.com/user-attachments/assets/7219ef5c-ee15-4692-9219-ae3471095991)

Special thanks to Swiss (https://github.com/SovGrenadier) for allowing the forking of this mod and ATLAS (https://github.com/thebeninator) for assisting with some of the code.
