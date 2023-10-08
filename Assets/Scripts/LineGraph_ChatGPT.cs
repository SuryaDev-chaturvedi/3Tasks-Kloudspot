using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineGraph_ChatGPT : MonoBehaviour
{
    public RectTransform graphContainer; // Reference to the container where the graph will be drawn.
    public Text dataPointTextPrefab; // Prefab for displaying data points on the graph.

    // Data for the graph. Each entry represents peak hours used at a specific time duration.
    private List<float> dataPoints = new List<float>
    {
        2.5f, 3.0f, 4.2f, 3.8f, 5.1f, 6.0f, 5.5f
    };

    private float graphWidth;
    private float graphHeight;

    void Start()
    {
        graphWidth = graphContainer.rect.width;
        graphHeight = graphContainer.rect.height;

        // Draw the graph initially.
        DrawGraph();
    }

    void DrawGraph()
    {
        float xIncrement = graphWidth / (dataPoints.Count - 1);
        float maxDataPoint = Mathf.Max(dataPoints.ToArray());

        for (int i = 0; i < dataPoints.Count; i++)
        {
            float xPosition = i * xIncrement;
            float yPosition = (dataPoints[i] / maxDataPoint) * graphHeight;

            // Create a data point display text.
            Text dataPointText = Instantiate(dataPointTextPrefab);
            dataPointText.transform.SetParent(graphContainer, false);
            dataPointText.rectTransform.anchoredPosition = new Vector2(xPosition, yPosition);
            dataPointText.text = dataPoints[i].ToString("F1"); // Format the value with one decimal place.

            // Draw lines connecting data points (except for the first point).
            if (i > 0)
            {
                DrawLine(
                    new Vector2((i - 1) * xIncrement, (dataPoints[i - 1] / maxDataPoint) * graphHeight),
                    new Vector2(xPosition, yPosition)
                );
            }
        }
    }

    void DrawLine(Vector2 startPoint, Vector2 endPoint)
    {
        GameObject lineObj = new GameObject("Line");
        lineObj.transform.SetParent(graphContainer, false);
        RectTransform lineRect = lineObj.AddComponent<RectTransform>();
        Image lineImage = lineObj.AddComponent<Image>();
        lineImage.color = Color.blue;

        Vector2 dir = endPoint - startPoint;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        float length = dir.magnitude;

        lineRect.sizeDelta = new Vector2(length, 2f); // Adjust the thickness of the line here.
        lineRect.anchoredPosition = startPoint + dir / 2f;
        lineRect.localEulerAngles = new Vector3(0, 0, angle);
    }
}