using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public int toScore = 0;
    public Vector2Int pos;
    public void Destroy() => Destroy(gameObject);
    public bool isStatic = true;
}