using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SceneRouteDataList_SO", menuName ="Map/SceneRouteData")]
public class SceneRouteDataList_SO : ScriptableObject
{
    [Header("ע���ջ���Ƚ����ԭ����д�յ���д��㣬99999��ʾ�������ƶ�")]
    public List<SceneRoute> sceneRoutesList;
}
