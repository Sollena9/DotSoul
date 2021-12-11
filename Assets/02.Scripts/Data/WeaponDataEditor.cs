using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;


public class WeaponDataEditor : OdinMenuEditorWindow
{

    [MenuItem("Tools/WeaponData")]
    private static void OpenWindow()
    {
        GetWindow<WeaponDataEditor>().Show();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        if (createNewWeaponData != null)
            DestroyImmediate(createNewWeaponData.weaponData);
    }

    private CreateNewWeaponData createNewWeaponData;

    protected override OdinMenuTree BuildMenuTree()
    {
        var tree = new OdinMenuTree();

        createNewWeaponData = new CreateNewWeaponData();
        tree.Add("Create New", createNewWeaponData);
        tree.AddAllAssetsAtPath("Weapon Data", "Assets/04_Data/", typeof(WeaponData));

        return tree;
    }

    protected override void OnBeginDrawEditors()
    {
        //gets refernce to curruntly selected item
        OdinMenuTreeSelection seleted = this.MenuTree.Selection;

        SirenixEditorGUI.BeginHorizontalToolbar();
        {
            GUILayout.FlexibleSpace();

            if(SirenixEditorGUI.ToolbarButton("Delete Currunt"))
            {
                WeaponData asset = seleted.SelectedValue as WeaponData;
                string path = AssetDatabase.GetAssetPath(asset);
                AssetDatabase.DeleteAsset(path);
                AssetDatabase.SaveAssets();
            }
        }

        SirenixEditorGUI.EndHorizontalToolbar();

    }

    public class CreateNewWeaponData
    {
        public CreateNewWeaponData()
        {
            weaponData = ScriptableObject.CreateInstance<WeaponData>();
            weaponData.weaponName = "New Weapon Data";
        }

        [InlineEditor(ObjectFieldMode = InlineEditorObjectFieldModes.Hidden)]
        public WeaponData weaponData;


        [Button("Add New Weapon")]
        private void CreateNewData()
        {
            AssetDatabase.CreateAsset(weaponData, "Assets/04_Data/" + weaponData.weaponName + ".asset");
            AssetDatabase.SaveAssets();

                
            //create new instance
            weaponData = ScriptableObject.CreateInstance<WeaponData>();
            weaponData.weaponName = "New Weapon Data";
        }
    }

}
