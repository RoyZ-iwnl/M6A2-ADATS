# M6A2 ADATS v1.3.2

## Branch release notes:

- Compatibility for GHPC Update ~~20250401.1~~ ~~20256030~~ 20250902
- New XM813 30mm Chain Gun (includes MK258 APFSDS-T and MK310 PABM-T)
- Tweaks to super optics setting (gun reticle remains when using thermals on lower zoom level and better reticle stability, arbitrary 3x improvement in FLIR resolution)
- "BUSK" postfix when ERA is detected (NATO ERA v1.2.2+ required)


## How to use the XM310 PABM-T:
### PABM Point-detonate + Time-delay Fuze

- To use airburst mode, simply laze the target. The round will detonate araound the distance set by LRF/manual elevation.
- To use point-detonate mode, make sure the range setting is at least 10 meters more than the target to ensure it would not be in airburst mode. As long as the round directly hits the target, it will use the point-detonate fuze.


### PABM Point-detonate + Proximity Fuze
- To use proximity mode, press middle mouse button and the round should have [Proximity] suffix to its name in the lower left part of the UI
- To use point-detonate mode, make sure the [Proximity] suffix is not present

  ## Ammunition list:
| Name  | Penetration (mm) | Fragment/Spalling Penetration (mm)| Muzzle Velocity (m/s) | Note |
| ------------- | ------------- | ------------- | ------------- | ------------- |
| M791 APDS-T | 60 |  | 1345  |  |
| M919 APFSDS-T  | 102 |  | 1575 | +25% spalling chance and +66% spalling performance. |
| M792 HEI-T | 8* |  | 1100 |  |
| APEX APHE-T | 35* |  | 1270 | Point-detonate fuze only. |
| M920 MPAB-T | 15* |  | 1270 | Hypothethical round. Point-detonate + airburst fuze.  |
| MK258 APFSDS-T | 116 |  | 1430 | +50% spalling chance and +133% spalling performance. |
| MK310 PABM-T | 30* | 32** | 1170 | Point-detonate + airburst fuze or proximity fuze. |
| MIM-146 ADATS | 1000 | 50** | 510 | Point-detonate + proximity fuze. Optional tandem warhead config where it makes ERA only 25% effective. |

- *These are HE rounds so actual penetration may not be the same as the table
- **These are <i>up to</i> values so not every fragment will perform the same
- 
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
