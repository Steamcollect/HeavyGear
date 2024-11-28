using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.UIElements;

public class PieChart : VisualElement
{
    private float raduis = 100.0f;
    private List<float> value = new (){100f};

    public float Radius
    {
        get => raduis;
        set
        {
            raduis = value;
        }
    }

    public float Diameter => raduis * 2.0f;

    public List<float> Value {
        get => value;
        set { this.value = value; MarkDirtyRepaint(); }
    }

    public PieChart()
    {
        generateVisualContent += DrawCanvas;
    }

    void DrawCanvas(MeshGenerationContext ctx)
    {
        var painter = ctx.painter2D;
        painter.strokeColor = Color.white;
        painter.fillColor = Color.white;

        var percentages = value;
        
        float angle = 0.0f;
        float anglePct = 0.0f;
        for (var index = 0; index < percentages.Count; index++)
        {
            var pct = percentages[index];
            anglePct += 360.0f * (100/pct);
            painter.fillColor = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f);
            painter.BeginPath();
            painter.MoveTo(new Vector2(raduis, raduis));
            painter.Arc(new Vector2(raduis, raduis), raduis, angle, anglePct);
            painter.Fill();
            angle = anglePct;
        }
    }
}