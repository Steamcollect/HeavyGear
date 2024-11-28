using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(SSO_Crate))]
public class SSO_CrateEditor : OdinEditor
{
    private SSO_Crate targetObject;

    protected override void OnEnable()
    {
        base.OnEnable();
        targetObject = (SSO_Crate)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        UpdatePieChart();
    }
    
    private void UpdatePieChart()
    {
        if (targetObject.contentCrate.Count == 0) return;
        float totalWeight = 0;
        List<Tuple<float,string,Color>> percentageData = new List<Tuple<float,string,Color>>();
        for (int i = 0; i < targetObject.contentCrate.Count; i++)
        {
            totalWeight += targetObject.contentCrate[i].weight;
        }
        for (int i = 0; i < targetObject.contentCrate.Count; i++)
        {
            percentageData.Add( new Tuple<float, string, Color>(targetObject.contentCrate[i].weight/totalWeight,
                targetObject.contentCrate[i].data ?  targetObject.contentCrate[i].data.name :"Default",
                Color.HSVToRGB(i / (float)targetObject.contentCrate.Count, 0.7f, 1f)));
        }

        DrawPieChart(percentageData);
    }

    private void DrawPieChart(List<Tuple<float,string,Color>> pieChartData)
    {
        // Récupérer la taille de la zone de dessin
        
        Rect rect = EditorGUILayout.GetControlRect(GUILayout.Height(200));
        
        float startAngle = 0f;

        // Dessiner chaque secteur du camembert
        for (int i = 0; i < pieChartData.Count; i++)
        {
            ;
            float angle = pieChartData[i].Item1 * 360f;

            // Dessiner le secteur du camembert
            DrawPieSlice(rect, startAngle, angle, pieChartData[i].Item3);

            // Mettre à jour l'angle de départ pour le prochain secteur
            startAngle += angle;
        }
        DrawLegend(pieChartData,rect);
    }

    // Fonction qui dessine un secteur du camembert
    private void DrawPieSlice(Rect rect, float startAngle, float angle, Color color)
    {
        float radius = Mathf.Min(rect.width, rect.height) / 2;

        // Utiliser Handles pour dessiner
        Handles.color = color;
        Handles.DrawSolidArc(rect.center,  Vector3.forward,  Quaternion.Euler(0,0,startAngle) * Vector3.up, angle, radius);
    }
    
    private void DrawLegend(List<Tuple<float,string,Color>> pieChartData, Rect rect)
    {
        float startY =- (pieChartData.Count * 20f /2f);
        float offsetX = 180f;  // Décalage pour que la légende soit à côté du camembert

        // Dessiner les items de la légende
        foreach (var item in pieChartData)
        {
            GUI.color = item.Item3;
            EditorGUI.LabelField(new Rect( rect.center.x + offsetX, rect.center.y + startY, 100, 20), $"{item.Item2} : {item.Item1*100}%");
            startY += 25f; // Espacement entre chaque ligne de légende
        }

        // Réinitialiser la couleur de GUI après avoir dessiné la légende
        GUI.color = Color.white;
    }
}