# overlayFormatter
 compiles (dlc)_overlays.xml & shop_tattoo.meta into a single JSON file

 An example use case would be to update your clothing script with the newest overlays

 ## Basic Instructions
 1. Use OpenIV or Codewalker and extract all `_overlays.xml` files and `shop_tattoo.meta` files
 2. Put them in a folder
 3. Select folder
 4. Format
 5. Export

 ## Example output
 ```json
 [
  {
    "name": "FM_Hip_M_Tat_000",
    "zone": 0,
    "type": 0,
    "faction": 4,
    "gender": 0,
    "label": "TAT_HP_000",
    "collection": "mpHipster_overlays"
  },
  {
    "name": "FM_Hip_M_Tat_001",
    "zone": 3,
    "type": 0,
    "faction": 4,
    "gender": 0,
    "label": "TAT_HP_001",
    "collection": "mpHipster_overlays"
  }
 ]
 ```
## Enums
```c#
    public enum Zone
    {
        ZONE_TORSO = 0,
        ZONE_HEAD = 1,
        ZONE_LEFT_ARM = 2,
        ZONE_RIGHT_ARM = 3,
        ZONE_LEFT_LEG = 4,
        ZONE_RIGHT_LEG = 5,
        ZONE_UNKNOWN = 6,
        ZONE_NONE = 7,
    };
    public enum Faction
    {
        MICHAEL = 0,
        FRANKLIN = 1,
        TREVOR = 2,
        FM = 4,
    }
    public enum Gender
    {
        GENDER_MALE = 0,
        GENDER_FEMALE = 1,
        GENDER_DONTCARE = 2,
    }

    public enum Type
    {
        TYPE_TATTOO = 0,
        TYPE_BADGE = 1,
        TYPE_HAIR = 2,
    }
```
 ## Screenshot
 ![Image of Program](https://i.imgur.com/I2vyQAm.png)
