using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.U2D.Sprites;
using UnityEditor.UIElements;
using UnityEngine;
using Object = UnityEngine.Object;

public class ImporterSpriteFix : EditorWindow
{
    private GameObject holder;
    private SerializedObject serializedObject;
    private SerializedProperty spriteProperty;
    private Vector2 pivot;
    private int pixelDensity;

    [MenuItem("Tools/Import Sprite Fix")]

    public static void ShowWindow()
    {
        GetWindow(typeof(ImporterSpriteFix));
    }

    public void OnEnable()
    {
        holder = new GameObject("Holder");
        serializedObject = new SerializedObject(holder.AddComponent(typeof(ContainerHolder)));
        spriteProperty = serializedObject.FindProperty("container");
    }

    public void OnDisable()
    {
        if (holder) DestroyImmediate(serializedObject.targetObject);
    }

    public void OnDestroy()
    {
        if (holder) DestroyImmediate(holder);
    }

    private void OnGUI()
    {
        serializedObject.Update();
        EditorGUILayout.LabelField("Sprite to Fix");
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(spriteProperty);
        pivot = EditorGUILayout.Vector2Field("Pivot", pivot);
        EditorGUILayout.Space();
        pixelDensity = EditorGUILayout.IntField("Pixel Density",pixelDensity);
        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Fix Pivot"))
        {
            FixSprites();
        }
        if (GUILayout.Button("Fix Pixel Density"))
        {
            FixSprites(true);
        }
        EditorGUILayout.EndHorizontal();
        serializedObject.ApplyModifiedProperties();
    }
    
    private void FixSprites(bool fixDensity = false)
    {
        var hoderComp = holder.GetComponent<ContainerHolder>();
        foreach (var texture in hoderComp.container)
        {
            string path = AssetDatabase.GetAssetPath(texture);

            TextureImporter importer = AssetImporter.GetAtPath(path) as TextureImporter;
            if (importer && importer.spriteImportMode == SpriteImportMode.Multiple)
            {
                ISpriteEditorDataProvider dataProvider = AssetImporter.GetAtPath(path) as ISpriteEditorDataProvider;
                if (dataProvider == null)
                {
                    Debug.LogWarning(texture.name + " is not a valid sprite importer");
                    continue;
                }

                SpriteRect[] spriteRects = dataProvider.GetSpriteRects();

                for (int i = 0; i < spriteRects.Length; i++)
                {
                    spriteRects[i].pivot = pivot;
                }

                dataProvider.SetSpriteRects(spriteRects);
                EditorUtility.SetDirty(importer);
                importer.SaveAndReimport();
            }
            else if (importer && importer.spriteImportMode == SpriteImportMode.Single)
            {
                importer.spritePivot = pivot;
                importer.SaveAndReimport();

                Debug.Log($"Pivot du sprite '{texture.name}' modifié avec succès.");
            }
            else
            {
                Debug.LogWarning("L'importateur est introuvable ou le sprite n'est pas en mode Single/Multiple.");
            }
        }
    }

}

