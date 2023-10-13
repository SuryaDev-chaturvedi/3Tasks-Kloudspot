using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;
using Random = UnityEngine.Random;
using System.Linq;

public class WindowGraph3 : MonoBehaviour
{
    [SerializeField] private Sprite circleSpritewhite;   // Yesterday
    [SerializeField] private Sprite circleSpriteRed;     // Today
    private RectTransform graphContainer;
    private RectTransform labelTemplateX;
    private RectTransform labelTemplateY;
    // private RectTransform dashTemplateX;
    // private RectTransform dashTemplateY;


    // Updating the graph, when even is true than add 1,2,-3... else ....
    bool even = true;
    List<int> valueList;
    List<int> valueList2;
    private void Awake()
    {
        graphContainer = transform.Find("graphContainer").GetComponent<RectTransform>();
        labelTemplateX = graphContainer.Find("labelTemplateX").GetComponent<RectTransform>();
        labelTemplateY = graphContainer.Find("labelTemplateY").GetComponent<RectTransform>();
        // dashTemplateX = graphContainer.Find("dashTemplateX").GetComponent<RectTransform>(); 
        // dashTemplateY = graphContainer.Find("dashTemplateY").GetComponent<RectTransform>();

        valueList = new List<int>() { 149, 264, 142, 57, 258, 352, 168, 134, 150, 246, 142, 352 };
        ShowGraph(valueList, (int _i) => (_i + 1) + "AM", (float _f) => Mathf.RoundToInt(_f) + "-" + Mathf.RoundToInt(_f + 50));

        valueList2 = new List<int>() { 259, 164, 142, 157, 158, 252, 168, 134, 250, 46, 42, 52 };
        ShowGraph2(valueList2, (int _i) => (_i + 1) + "AM", (float _f) => Mathf.RoundToInt(_f) + "-" + Mathf.RoundToInt(_f + 50));

    }

    IEnumerator changegraph()
    {
        yield return new WaitForSeconds(0.5f);
        int[] arr = { -1, -2, -3, -4, -5, 1, 2, 3, 4, 5, -6, 7, -7, 8, -8, 9, -9, 10, 10, 11, -11, 12, -12, 13, -13 };

        // first delete the values of graph then draw them---
        int Delete_from_index = 4;

        Transform graph_container_parent = graphContainer.transform;

        // Check if the parent has children.
        if (graph_container_parent.childCount > Delete_from_index)
        {
            // Loop through the children starting from the 5th position.
            for (int i = Delete_from_index; i < graph_container_parent.childCount; i++)
            {
                // Destroy the child GameObject.
                Destroy(graph_container_parent.GetChild(i).gameObject);
            }
        }


        for (int i = 0; i < valueList.Count; i = i + 2)
        {
            int randomIndex = Random.Range(0, arr.Length);
            if (even)
            {
                if (valueList[i] + arr[randomIndex] <= valueList.Max() && valueList[i] + arr[randomIndex] >= valueList.Min())
                    valueList[i] += arr[randomIndex];

                if (i + 1 < valueList.Count)
                {
                    if (valueList[i] + arr[randomIndex] <= valueList.Max() && valueList[i] + arr[randomIndex] >= valueList.Min())
                        valueList[i + 1] += arr[randomIndex];
                }
                even = false;
            }
            else
            {
                if (valueList[i] + arr[randomIndex] <= arr.Max() && valueList[i] + arr[randomIndex] >= valueList.Min())
                    valueList[i] += arr[randomIndex];
                if (i + 1 < valueList.Count)
                {
                    if (valueList[i] + arr[randomIndex] <= arr.Max() && valueList[i] + arr[randomIndex] >= valueList.Min())
                        valueList[i + 1] += arr[randomIndex];
                }
                even = true;
            }
        }

        ShowGraph(valueList, (int _i) => (_i + 1) + "AM", (float _f) => Mathf.RoundToInt(_f) + "-" + Mathf.RoundToInt(_f + 50));

        for (int i = 0; i < valueList2.Count; i = i + 2)
        {
            int randomIndex = Random.Range(0, arr.Length);
            if (even)
            {
                if (valueList2[i] + arr[randomIndex] <= valueList2.Max() && valueList2[i] + arr[randomIndex] >= valueList2.Min())
                    valueList2[i] += arr[randomIndex];
                if (i + 1 < valueList2.Count)
                {
                    if (valueList2[i] + arr[randomIndex] <= valueList2.Max() && valueList2[i] + arr[randomIndex] >= valueList2.Min())
                        valueList2[i + 1] += arr[randomIndex];
                }
                even = false;
            }
            else
            {
                if (valueList2[i] + arr[randomIndex] <= valueList2.Max() && valueList2[i] + arr[randomIndex] >= valueList2.Min())
                    valueList2[i] += arr[randomIndex];
                if (i + 1 < valueList2.Count)
                {
                    if (valueList2[i] + arr[randomIndex] <= valueList2.Max() && valueList2[i] + arr[randomIndex] >= valueList2.Min())
                        valueList2[i + 1] += arr[randomIndex];
                }
                even = true;
            }
        }

        ShowGraph2(valueList2, (int _i) => (_i + 1) + "AM", (float _f) => Mathf.RoundToInt(_f) + "-" + Mathf.RoundToInt(_f + 50));

    }

    private GameObject CreateCircle(Vector2 anchoredPosition)
    {
        GameObject gameObject = new GameObject("circle", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().sprite = circleSpriteRed;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.sizeDelta = new Vector2(11, 11);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        return gameObject;
    }

    private GameObject CreateCircle2(Vector2 anchoredPosition)
    {
        GameObject gameObject = new GameObject("circle", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().sprite = circleSpritewhite;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.sizeDelta = new Vector2(11, 11);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        return gameObject;
    }

    private void ShowGraph(List<int> valueList, Func<int, string> getAxisLabelX = null, Func<float, string> getAxisLabelY = null)
    {
        if (getAxisLabelX == null)
        {
            getAxisLabelX = delegate (int _i) { return _i.ToString(); };
        }
        if (getAxisLabelY == null)
        {
            getAxisLabelY = delegate (float _f) { return Mathf.RoundToInt(_f).ToString(); };
        }

        float graphHeight = graphContainer.sizeDelta.y;
        float yMaximum = 400f;
        float xSize = 50f;   // initialy is=t is 50, I made it 5

        GameObject lastCircleGameObject = null;
        for (int i = 0; i < valueList.Count; i++)
        {
            float xPosition = xSize + i * xSize;
            float yPosition = (valueList[i] / yMaximum) * graphHeight;

            GameObject circleGameObject = CreateCircle(new Vector2(xPosition, yPosition));
            if (lastCircleGameObject != null)
            {
                CreateDotConnection(lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition, circleGameObject.GetComponent<RectTransform>().anchoredPosition);
            }
            lastCircleGameObject = circleGameObject;

            RectTransform labelX = Instantiate(labelTemplateX);
            labelX.SetParent(graphContainer, false);
            labelX.gameObject.SetActive(true);
            labelX.anchoredPosition = new Vector2(xPosition, -7f);
            labelX.GetComponent<Text>().text = getAxisLabelX(i);

            //   RectTransform dashX = Instantiate(dashTemplateX);
            /*   dashX.SetParent(graphContainer, false);
                 dashX.gameObject.SetActive(true);
                 dashX.anchoredPosition = new Vector2(xPosition, -3f);
            */

        }

        int separatorCount = 8;
        for (int i = 0; i <= separatorCount; i++)
        {
            RectTransform labelY = Instantiate(labelTemplateY);
            labelY.SetParent(graphContainer, false);
            labelY.gameObject.SetActive(true);
            float normalizedValue = i * 1f / separatorCount;
            labelY.anchoredPosition = new Vector2(-7f, normalizedValue * graphHeight);
            labelY.GetComponent<Text>().text = getAxisLabelY(normalizedValue * yMaximum);

            /*  RectTransform dashY = Instantiate(dashTemplateY);
                dashY.SetParent(graphContainer, false);
                dashY.gameObject.SetActive(true);
                dashY.anchoredPosition = new Vector2(-4f, normalizedValue * graphHeight);
            */
        }
    }

    private void ShowGraph2(List<int> valueList, Func<int, string> getAxisLabelX = null, Func<float, string> getAxisLabelY = null)
    {
        if (getAxisLabelX == null)
        {
            getAxisLabelX = delegate (int _i) { return _i.ToString(); };
        }
        if (getAxisLabelY == null)
        {
            getAxisLabelY = delegate (float _f) { return Mathf.RoundToInt(_f).ToString(); };
        }

        float graphHeight = graphContainer.sizeDelta.y;
        float yMaximum = 400f;
        float xSize = 50f;   // initialy is=t is 50, I made it 5

        GameObject lastCircleGameObject = null;
        for (int i = 0; i < valueList.Count; i++)
        {
            float xPosition = xSize + i * xSize;
            float yPosition = (valueList[i] / yMaximum) * graphHeight;
            GameObject circleGameObject = CreateCircle2(new Vector2(xPosition, yPosition));
            if (lastCircleGameObject != null)
            {
                CreateDotConnection2(lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition, circleGameObject.GetComponent<RectTransform>().anchoredPosition);
            }
            lastCircleGameObject = circleGameObject;

            RectTransform labelX = Instantiate(labelTemplateX);
            labelX.SetParent(graphContainer, false);
            labelX.gameObject.SetActive(true);
            labelX.anchoredPosition = new Vector2(xPosition, -7f);
            labelX.GetComponent<Text>().text = getAxisLabelX(i);

            //      RectTransform dashX = Instantiate(dashTemplateX);
            /*     dashX.SetParent(graphContainer, false);
                 dashX.gameObject.SetActive(true);
                 dashX.anchoredPosition = new Vector2(xPosition, -3f);
            */

        }

        int separatorCount = 8;
        for (int i = 0; i <= separatorCount; i++)
        {
            RectTransform labelY = Instantiate(labelTemplateY);
            labelY.SetParent(graphContainer, false);
            labelY.gameObject.SetActive(true);
            float normalizedValue = i * 1f / separatorCount;
            labelY.anchoredPosition = new Vector2(-7f, normalizedValue * graphHeight);
            labelY.GetComponent<Text>().text = getAxisLabelY(normalizedValue * yMaximum);

            /*  RectTransform dashY = Instantiate(dashTemplateY);
                dashY.SetParent(graphContainer, false);
                dashY.gameObject.SetActive(true);
                dashY.anchoredPosition = new Vector2(-4f, normalizedValue * graphHeight);
            */
        }
    }

    private void CreateDotConnection(Vector2 dotPositionA, Vector2 dotPositionB)
    {
        GameObject gameObject = new GameObject("dotConnection", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().color = Color.red; /*   new Color(1,1,1, .5f);  */
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        Vector2 dir = (dotPositionB - dotPositionA).normalized;
        float distance = Vector2.Distance(dotPositionA, dotPositionB);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.sizeDelta = new Vector2(distance, 3f);
        rectTransform.anchoredPosition = dotPositionA + dir * distance * .5f;
        rectTransform.localEulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVectorFloat(dir));
    }

    private void CreateDotConnection2(Vector2 dotPositionA, Vector2 dotPositionB)
    {
        GameObject gameObject = new GameObject("dotConnection", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().color = Color.white; /*   new Color(1,1,1, .5f);  */
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        Vector2 dir = (dotPositionB - dotPositionA).normalized;
        float distance = Vector2.Distance(dotPositionA, dotPositionB);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.sizeDelta = new Vector2(distance, 3f);
        rectTransform.anchoredPosition = dotPositionA + dir * distance * .5f;
        rectTransform.localEulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVectorFloat(dir));
    }

    public void Updatebtn()
    {
        StartCoroutine(changegraph());
    }
}