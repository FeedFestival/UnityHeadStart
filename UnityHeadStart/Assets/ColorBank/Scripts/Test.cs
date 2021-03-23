using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class Test : MonoBehaviour
{
    public GameObject ChildPrefab;
    public Vector3? StartPoint;
    public Vector3 Direction;
    public Vector3? End;
    public float Distance = 1.5f;
    public List<GameObject> Children;
    private int _childIndex;
    private int _tryPositioning;
    public int MaxTryPositioning;
    private Dictionary<int, List<GameObject>> _quadrant;

    // Start is called before the first frame update
    void Start()
    {
        StartPoint = transform.position;
        Direction = new Vector3(1, 1, 0);

        Children = new List<GameObject>();
        _childIndex = 0;

        AddChild();
    }

    private void AddChild()
    {
        ClearLog();
        _tryPositioning = 0;
        Vector3? newPos = CastRayToWorld();
        if (newPos.HasValue == false)
        {
            return;
        }
        var go = CreateFromPrefab(ChildPrefab);
        go.transform.position = newPos.Value;
        go.gameObject.name = "child[" + _childIndex + "]";
        Children.Add(go);
        _childIndex++;

        _quadrant = new Dictionary<int, List<GameObject>>();
        foreach (var child in Children)
        {
            if (child.transform.position.x >= 0 && child.transform.position.y >= 0)
            {
                AddToQuadrant(1, child);
            }
            else if (child.transform.position.x >= 0 && child.transform.position.y < 0)
            {
                AddToQuadrant(2, child);
            }
            else if (child.transform.position.x < 0 && child.transform.position.y < 0)
            {
                AddToQuadrant(3, child);
            }
            else
            {
                AddToQuadrant(4, child);
            }
        }
        var debug = "";
        foreach (var quad in _quadrant)
        {
            var debugGos = "";
            foreach (var gos in quad.Value)
            {
                debugGos += gos.name + ", ";
            }
            debug += "q" + quad.Key + ": (" + debugGos + "), ";
        }
        Debug.Log(debug);
    }

    private void AddToQuadrant(int quadKey, GameObject go)
    {
        if (_quadrant.ContainsKey(quadKey))
        {
            _quadrant[quadKey].Add(go);
            return;
        }
        _quadrant.Add(quadKey, new List<GameObject>());
        _quadrant[quadKey].Add(go);
    }

    private Vector3? CastRayToWorld()
    {
        float x = Random.Range(-10, 10);
        float y = Random.Range(-10, 9);
        Vector3 dir = new Vector3(x, y, 0);
        dir.Normalize();
        Ray ray = new Ray(StartPoint.Value, dir);
        End = ray.origin + (ray.direction * Distance);

        Debug.Log("World point " + End);

        if (IsTooCloseToSiblings(End.Value))
        {
            _tryPositioning++;
            if (_tryPositioning > MaxTryPositioning)
            {
                End = FindSpaceBetweenChildren();
                if (End.HasValue == false)
                {
                    Debug.Log("Tried positioning " + _tryPositioning + " times. Can't do it.");
                    return null;
                }
                return null;
                // return End.Value;
            }
            return CastRayToWorld();
        }

        return End.Value;
    }

    private Vector3? FindSpaceBetweenChildren()
    {
        for (var i = 0; i < Children.Count; i++)
        {
            int l = i - 1;
            if (l < 0)
            {
                l = Children.Count - 1;
            }
            bool isSpaceBetween = IsSpaceBetween(Children[i], Children[l]);
            if (isSpaceBetween == false)
            {
                int r = i + 1;
                if (r > Children.Count - 1)
                {
                    r = 0;
                }
                isSpaceBetween = IsSpaceBetween(Children[i], Children[r]);
            }
            if (isSpaceBetween)
            {
                Debug.Log("isSpaceBetween: " + isSpaceBetween);
            }
        }
        return null;
    }

    private bool IsSpaceBetween(GameObject current, GameObject sibling)
    {
        float distance = Vector3.Distance(current.transform.position, sibling.transform.position);
        Debug.Log(current.name + " with " + sibling.name + " distance: " + distance);
        if (distance > 0.87f)
        {
            Debug.Log("Found space between " + current.name + " and " + sibling.name + " distance: " + distance);
            return true;
        }
        return false;
    }

    private bool IsTooCloseToSiblings(Vector3 pos)
    {
        foreach (var child in Children)
        {
            float distance = Vector3.Distance(pos, child.transform.position);
            Debug.Log("pos: " + pos + " with child[" + child.name + "] distance: " + distance);
            if (distance < Distance)
            {
                return true;
            }
        }
        return false;
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.A))
        {
            AddChild();
        }

        if (StartPoint.HasValue && End.HasValue)
        {
            Debug.DrawLine(StartPoint.Value, End.Value, Color.red);
        }
    }

    public GameObject CreateFromPrefab(string resourcePath, Vector3? originPosition = null)
    {
        var targetPrefab = Resources.Load(resourcePath) as GameObject;
        if (originPosition == null)
        {
            originPosition = new Vector3(0, 0, 0);
        }
        return GameObject.Instantiate(targetPrefab, originPosition.Value, Quaternion.identity) as GameObject;
    }

    public GameObject CreateFromPrefab(GameObject targetPrefab, Vector3? originPosition = null)
    {
        if (originPosition == null)
        {
            originPosition = new Vector3(0, 0, 0);
        }
        return GameObject.Instantiate(targetPrefab, originPosition.Value, Quaternion.identity) as GameObject;
    }

    public void ClearLog()
    {
        // var assembly = Assembly.GetAssembly(typeof(UnityEditor.Editor));
        // var type = assembly.GetType("UnityEditor.LogEntries");
        // var method = type.GetMethod("Clear");
        // method.Invoke(new object(), null);
    }

    private string DebugList<T>(List<T> array, string name)
    {
        string debug = string.Empty;
        foreach (T bId in array)
        {
            debug += bId + ",";
        }
        return name + " (" + debug + ")[" + array.Count + "]";
    }
}
