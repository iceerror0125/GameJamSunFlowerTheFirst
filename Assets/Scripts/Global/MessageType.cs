using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MessageType
{
   ShowItemDetail,
   ShowRiddleItem,
   
   // Key control
   InteractPressed,
   ShowInventory,
   
   // riddle
   PlaceCorrectItem,
   ChooseCorrectNumber,
   ClaimReward,
   
   // ui
   ShowPartical,
   ChangeToPart2,
}
