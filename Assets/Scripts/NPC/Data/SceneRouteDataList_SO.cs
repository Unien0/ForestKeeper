using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SceneRouteDataList_SO", menuName ="Map/SceneRouteData")]
public class SceneRouteDataList_SO : ScriptableObject
{
    [Header("注意堆栈的先进后出原则，先写终点再写起点，99999表示可随意移动")]
    public List<SceneRoute> sceneRoutesList;
}
