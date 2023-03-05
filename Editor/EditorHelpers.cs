#region 模块信息

// **********************************************************************
// Copyright (C) 2017 The company name
// 创作者 ：蔡振立
// 文件名(File Name): EditorHelpers.cs
// 创建时间(CreateTime):  2023年01月30日 星期一 08:56       
// 模块描述(Module description):
// **********************************************************************

#endregion

using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class EditorHelpers
{
    public static List<string> GetSelectedObject()
    {
        var paths = new List<string>();
        var objs = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);
        foreach (Object obj in objs)
        {
            var fullpath = AssetDatabase.GetAssetPath(obj);

            if (File.Exists(fullpath))
            {
                if (!paths.Contains(fullpath))
                {
                    paths.Add(fullpath);
                }
            }
        }

        return paths;
    }
}
