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

 ## Screenshot
 ![Image of Program](https://i.imgur.com/I2vyQAm.png)
