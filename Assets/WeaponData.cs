using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;


public class EnemyDataEditor : OdinMenuEditorWindow
{

    [MenuItem("Tools/EnemyData")]
    private static void OpenWindow()
    {
        GetWindow<EnemyDataEditor>().Show();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        if (createNewEnemyData != null)
            DestroyImmediate(createNewEnemyData.enemyData);
    }

    private CreateNewEnemyData createNewEnemyData;

    protected override OdinMenuTree BuildMenuTree()
    {
        var tree = new OdinMenuTree();

        createNewEnemyData = new CreateNewEnemyData();
        tree.Add("Create New", createNewEnemyData);
        tree.AddAllAssetsAtPath("Enemy Data", "Assets/04_Data/", typeof(EnemyData));

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
                EnemyData asset = seleted.SelectedValue as EnemyData;
                string path = AssetDatabase.GetAssetPath(asset);
                AssetDatabase.DeleteAsset(path);
                AssetDatabase.SaveAssets();
            }
        }

        SirenixEditorGUI.EndHorizontalToolbar();

    }

    public class CreateNewEnemyData
    {
        public CreateNewEnemyData()
        {
            enemyData = ScriptableObject.CreateInstance<EnemyData>();
            enemyData.enemyName = "New Enemy Data";
        }

        [InlineEditor(ObjectFieldMode = InlineEditorObjectFieldModes.Hidden)]
        public EnemyData enemyData;


        [Button("Add New Enemy")]
        private void CreateNewData()
        {
            AssetDatabase.CreateAsset(enemyData, "Assets/04_Data/" + enemyData.enemyName + ".asset");
            AssetDatabase.SaveAssets();

                
            //create new instance
            enemyData = ScriptableObject.CreateInstance<EnemyData>();
            enemyData.enemyName = "New Enemy Data";
        }
    }

}
