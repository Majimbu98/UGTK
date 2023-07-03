using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class VFXinfo
{
   [SerializeField] public string VFXName;
   [SerializeField] public string VFXInfo;
   [SerializeField] public Category category;

   public VFXinfo(string _VFXName, string _VFXinfo, string _category)
   {
      VFXName = _VFXName;
      VFXInfo = _VFXinfo;

      switch (_category)
      {
         case "EnemyBossRelated":
            
            category = Category.EnemyBossRelated;
            
            break;
         case "OtherEnvironmental":
            
            category = Category.OtherEnvironmental;
            
            break;
         case "OrinnRelated":
            
            category = Category.OrinnRelated;
            
            break;
      }
   }

}
