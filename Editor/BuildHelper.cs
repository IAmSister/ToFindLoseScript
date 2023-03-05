#region 模块信息

// **********************************************************************
// Copyright (C) 2017 The company name
// 创作者 ：蔡振立
// 文件名(File Name): BuildHelper.cs
// 创建时间(CreateTime):  2023年01月30日 星期一 08:59       
// 模块描述(Module description):
// **********************************************************************

#endregion

using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BuildHelper
{
    // Get scene or plain assets from path
    internal static string[] GetAssetsFromPath(string path, bool onlySceneFiles)
    {
        if (!File.Exists(path) && !Directory.Exists(path))
            return new string[] { };

        bool isDir = (File.GetAttributes(path) & FileAttributes.Directory) == FileAttributes.Directory;
        bool isSceneFile = Path.GetExtension(path) == ".unity";
        if (!isDir)
        {
            if (onlySceneFiles && !isSceneFile)
                // If onlySceneFiles is true, we can't add file without "unity" extension
                return new string[] { };

            return new string[] { path };
        }
        else
        {
            string[] subFiles = null;
            if (onlySceneFiles)
                subFiles = FindSceneFileInDir(path, SearchOption.AllDirectories);
            else
                subFiles = FindAssetsInDir(path, SearchOption.AllDirectories);

            return subFiles;
        }
    }

    private static string[] FindSceneFileInDir(string dir, SearchOption option)
    {
        return Directory.GetFiles(dir, "*.unity", option);
    }

    private static string[] FindAssetsInDir(string dir, SearchOption option)
    {
        List<string> files = new List<string>(Directory.GetFiles(dir, "*.*", option));
        files.RemoveAll(x => x.EndsWith(".meta", System.StringComparison.OrdinalIgnoreCase)
                             || x.EndsWith(".unity", System.StringComparison.OrdinalIgnoreCase)
                             || x.EndsWith(".tpsheet", System.StringComparison.OrdinalIgnoreCase));
        return files.ToArray();
    }

}
