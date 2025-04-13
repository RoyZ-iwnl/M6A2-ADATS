# M6A2 ADATS v1.3

This mod replaces the M2 Bradley with the M6 Linebacker but pretend it has the ADATS and GAU-12 (or others) instead!

## Features:

- Compatibility for GHPC Update 20250401.1
- New XM813 30mm Chain Gun
- Tweaks to super optics setting

## Installation:
1.) Install [MelonLoader v0.6.1](https://github.com/LavaGang/MelonLoader/).

2.) Download the latest version from the [release page](https://github.com/Cyances/M6A2-ADATS/releases).

3.) Place zM6A2Adats.dll file in the mods folder:

4.) Launch the game directly (not from Steam).
   
5.) On first time running this mod, the entries in MelonPreferences.cfg will only appear after launching the game then closing it.


## How to use the M920 MPAB-T, XM310 PABM-T and MIM-146 ADATS:
### MPAB or PABM Point-detonate + Time-delay Fuze

- To use airburst mode, simply laze the target. The round will detonate araound the distance set by LRF/manual elevation.
- To use point-detonate mode, make sure the range setting is at least 10 meters more than the target to ensure it would not be in airburst mode. As long as the round directly hits the target, it will use the point-detonate fuze.


### ADATS or PABM Point-detonate + Proxmity Fuze
- To use proximity mode, press middle mouse button and the round should have [Proximity] suffix to its name in the lower left part of the UI
- To use point-detonate mode, make sure the [Proximity] suffix is not present

  ## Round types list:
| Name  | Penetration (mm) | Fragment/Spalling Penetration (mm)| Muzzle Velocity (m/s) | Note |
| ------------- | ------------- | ------------- | ------------- | ------------- |
| M791 APDS-T | 60 |  | 1345  |  |
| M919 APFSDS-T  | 102 | 10 | 1575 | +25% spalling chance and +66% spalling performance. |
| M792 HEI-T | 8 |  | 1100 |  |
| APEX APHE-T | 35* |  | 1270 | Point-detonate fuze only. |
| M920 MPAB-T | 15* |  | 1270 | Hypothethical round. Point-detonate + airburst fuze.  |
| MK258 APFSDS-T | 116 |  | 1430 | +50% spalling chance and +133% spalling performance. |
| MK310 PABM-T | 30* | 32** | 1170 | Point-detonate + airburst fuze or proximity fuze. |
| MIM-146 ADATS | 1000 | 50 | 510 | Point-detonate + proximity fuze. Optional tandem warhead config where it makes ERA only 25% effective. |

- *These are HE rounds so actual penetration is not the same in the table
- **These are <i>up to</i> values so not every fragment will perform the same
- 
## Mod Configuration (in UserData/MelonPreferences.cfg):

<p>
	<ul> 
		<li>I suggest getting Notepad++ so it would be easier to identify each category</li>
		<li>Use GAU-12 (true by default)</li>
		<li>Use M919 (false by default)</li>
		<li>Use M920 MPAB (false by default)</li>
		<li>AP and HE round count (300/1200 by default). If total round count exceeds 1500, the mod will use the default mix.</li>
		<li>Enable ADATS tandem warhead (false by default)</li>
		<li>ADATS proximity fuze sensitivity (3 by default)</li>
		<li>Reticle horizontal stabilization when leading (false by default)</li>
		<li>Super optics (false by default)</li>
		<li>Better vehicle dynamics (false by default)</li>
		<li>Better AI spotting and gunnery skill (false by default)</li>
		<li>Composite hull (false by default)</li>
		<li>Composite turret (false by default)</li>
	</ul>
</p>

![ADATS MelonPreferences](https://github.com/Cyances/M6A2-ADATS/assets/154455050/cc844e91-b272-4593-99e4-68e2bd2895b4)

Special thanks to Swiss (https://github.com/SovGrenadier) for allowing the forking of this mod and ATLAS (https://github.com/thebeninator) for assisting with some of the code.
