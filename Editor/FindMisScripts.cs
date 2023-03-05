#region Copyright
// **********************************************************************
// Copyright (C) #COPYRIGHTYEAR# #COMPANYNAME#
//
// Script Name :		FindMisScripts.cs
// Author Name :		#AuthorName#
// Create Time :		#CreateTime#
// Description :
// **********************************************************************
#endregion

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

public class FindMisScripts 
{
   //预制体路径
   public const string PREFABS_UI_PATH_TEMP = "Assets/Resources/Prefabs/";
   private static int _checkIndex = 0;
   [MenuItem("Tools/查找丢失脚本")]
  static void CheckLoseComponentEx()
   {
      _checkIndex = 0;
      List<string> prefasbList = new List<string>();

      var selectObjects = EditorHelpers.GetSelectedObject();
      if (selectObjects.Count > 0)
      {
         prefasbList.AddRange(selectObjects);
      }
      else
      {
         prefasbList.AddRange(BuildHelper.GetAssetsFromPath(PREFABS_UI_PATH_TEMP, false));
      }

      for (var i = 0; i < prefasbList.Count; ++i)
      {
         var prefabPath = prefasbList[i];
         //var basePrefabName = Path.GetFileNameWithoutExtension(prefabPath);
         var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
         if (prefab != null)
         {
            FindInGO(prefab, prefabPath);
         }

         var pngName = Path.GetFileNameWithoutExtension(prefabPath);
         var showName = string.Format("{0}-{1}/{2}", pngName, i, prefasbList.Count);
         EditorUtility.DisplayProgressBar("CheckLoseComponentEx", showName, i / (float)prefasbList.Count);
      }

      EditorUtility.ClearProgressBar();
   }

   private static void FindInGO(GameObject g, string path)
   {
      Component[] components = g.GetComponents<Component>();
      for (int i = 0; i < components.Length; i++)
      {
         var component = components[i];
         if (component == null)
         {
            Debug.LogWarningFormat("GameObject:{0} has null component:{1}", GetFullPrefabHierarchy(g.transform), path);
         }
           
           
         _checkIndex++;
      }

      // Now recurse through each child GO (if there are any):
      foreach (Transform childT in g.transform)
      {
         //Debug.Log("Searching " + childT.name  + " " );
         FindInGO(childT.gameObject, path);
      }
   }
   
   public static string GetFullPrefabHierarchy(Transform refab)
   {
      string fullHierarchy = string.Empty;
      if (refab.parent != null)
      {
         fullHierarchy += GetFullPrefabHierarchy(refab.parent) + "-";
      }

      fullHierarchy += refab.name;

      return fullHierarchy;
   }
}
