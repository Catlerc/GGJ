using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public Vector2Int pos;
    public void Destroy() => Destroy(gameObject);
}