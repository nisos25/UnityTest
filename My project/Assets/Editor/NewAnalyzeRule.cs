using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;
using UnityEditor.AddressableAssets.Settings.GroupSchemas;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class NewAnalyzeRule : IPreprocessBuildWithReport
{
    public int callbackOrder { get; }
    
    public void OnPreprocessBuild(BuildReport report)
    {
        AddressableAssetSettings settings = AddressableAssetSettingsDefaultObject.Settings;
        if (settings == null)
        {
            Debug.LogError("AddressableAssetSettings not found. Make sure you have the Addressables package installed.");
            return;
        }

        var listTest = settings.groups.ToList();
        
        foreach (AddressableAssetGroup group in listTest)
        {
            
            if (group.entries.Count > 5)
            {
                string newGroupName = group.Name;
                int groupNumber = 1;
                while (settings.groups.Exists(g => g.Name == newGroupName))
                {
                    newGroupName = $"{group.Name}_{groupNumber.ToString("D2")}";
                    groupNumber++;
                }
                
                AddressableAssetGroup newGroup = settings.CreateGroup(newGroupName, false, false, false, null);
                int excessCount = group.entries.Count - 5;
                var entriesToRemove = group.entries.ToList().GetRange(5, excessCount); 
                List<AddressableAssetEntry> entriesToRemoveList = new List<AddressableAssetEntry>(entriesToRemove);
                List<AddressableAssetEntry> newGroupList = new List<AddressableAssetEntry>(newGroup.entries.ToList());
                foreach (var entry in entriesToRemoveList)
                {
                    group.RemoveAssetEntry(entry);
                    newGroupList.Add(entry);
                }

                
                if (newGroupList.Count > 0)
                {
                    settings.MoveEntries(newGroupList, newGroup);
                }

                Debug.Log($"Fixed group '{group.Name}' by creating new group '{newGroup.Name}' with {excessCount} entries.");
            }
        }
    }
}
