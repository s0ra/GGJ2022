using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[RequireComponent(typeof(PolygonCollider2D))]
public class PixelPerfectCollider2D : MonoBehaviour
{
    private static PixelPerfectCollider2D _instance;
    public static PixelPerfectCollider2D Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<PixelPerfectCollider2D>();
            }
            return _instance;
        }
    }

    [Tooltip("All pixels with an alpha value greater than or equal to the AlphaThreshhold are considered solid.")]
    [Range(0, 1)]
    public float AlphaThreshhold = 0.5f;

    public int SegmentSize = 1;
    public RenderTexture renderTexture;
    public Camera orthoCamera;
    public void Regenerate()
    {
        Debug.Log($"PixelPerfectCollider2D Regenerate");
        //Test that all references are not null.
        if (GetComponent<PolygonCollider2D>() == null)
        {
            gameObject.AddComponent<PolygonCollider2D>();
            Debug.LogWarning("No polygonCollider2D component was found on " + gameObject.name + " so a new one was added.");
        }
        PolygonCollider2D polygoncollider = GetComponent<PolygonCollider2D>();
        Texture2D texture = ForceReadable(toTexture2D(renderTexture));
        
        List<ColliderSegment> segments;
        //Get all the one pixel long segments that will makeup our collider.
        segments = GetSegments(texture, SegmentSize);
        List<List<Vector2>> paths;
        //Finally we trace paths that connect all the segments.
        paths = FindPaths(segments);
        //Convert to localspace.
        Bounds bounds = orthoCamera.OrthographicBounds();
        //Debug.Log($"orthoCamera.OrthographicBounds = {bounds}");
        paths = ConvertToLocal(paths, bounds, renderTexture.width, renderTexture.height);
        //Move relative to the pivot.
        paths = CalculatePivot(paths, bounds, renderTexture.width, renderTexture.height);
        //And last we tell the polygon collider to start using our new data.
        polygoncollider.pathCount = paths.Count;
        for (int p = 0; p < paths.Count; p++)
        {
            polygoncollider.SetPath(p, paths[p].ToArray());
        }
        Debug.Log($"polygoncollider SetPath");

    }

    //this struct will store data about sections of collider.
    private struct ColliderSegment
    {
        public Vector2 Point1;
        public Vector2 Point2;
        public ColliderSegment(Vector2 Point1, Vector2 Point2)
        {
            this.Point1 = Point1;
            this.Point2 = Point2;
        }
    }

    private List<List<Vector2>> ConvertToLocal(List<List<Vector2>> original,
         Bounds bounds, float width, float height)
    {
        float distance = Mathf.Abs(bounds.max.x - bounds.min.x);
        float distance2 = Mathf.Abs(bounds.max.y - bounds.min.y);
        /*Debug.Log($"ConvertToLocal,distance = {distance},distance2 = {distance2}" +
        	$",width = {width}" +
        	$",height = {height}, x*{distance/width} y*{distance/height}");*/

        for (int p = 0; p < original.Count; p++)
        {
            for (int o = 0; o < original[p].Count; o++)
            {
                Vector2 currentpoint = original[p][o];
                //Debug.Log($"ConvertToLocal,currentpoint = {currentpoint}");
                currentpoint.x *= distance;
                currentpoint.x /= width;
                currentpoint.y *= distance2;
                currentpoint.y /= height;
                original[p][o] = currentpoint;
                //Debug.Log($"ConvertToLocal original[{p}][{o}] = {currentpoint}");
            }
        }
        return original;
    }

    private List<List<Vector2>> CalculatePivot(List<List<Vector2>> original,
         Bounds bounds, float width, float height)
    {
        float distance = Mathf.Abs(bounds.max.x - bounds.min.x);
        float distance2 = Mathf.Abs(bounds.max.y - bounds.min.y);

        Vector2 pivot = (Vector2)orthoCamera.transform.position;
        //Debug.Log($"CalculatePivot, orthoCamera position = {pivot}");
        pivot.x = pivot.x * distance / width;
        //Debug.Log($"pivot.x * {distance} / {width} = {pivot.x}");

        //pivot.x = (pivot.x - 0.5f) * distance / width;
        pivot.y = pivot.y * distance2 / height;
        //Debug.Log($"pivot.y * {distance2} / {height} = {pivot.y}");

        //Debug.Log($"CalculatePivot, orthoCamera position = {pivot}");

        //pivot.y = (pivot.y - 0.5f) * distance2 / height;

        /*Debug.Log($"CalculatePivot, pivot = {pivot}" +
        	$",distance = {distance},distance2 = {distance2}" +
        	$",width = {width}" +
            $",height = {height}, x*{distance / width} y*{distance / height}");*/
        Vector2 offset = new Vector2(-distance / 2f, -distance2 / 2f);
        //Debug.Log($"CalculatePivot, offset = {offset}");
        for (int p = 0; p < original.Count; p++)
        {
            for (int o = 0; o < original[p].Count; o++)
            {
                original[p][o] += offset;
            }
        }
        return original;
    }

    //This function traces along the segments and connects them together into paths.
    List<List<Vector2>> FindPaths(List<ColliderSegment> segments)
    {
        List<List<Vector2>> output = new List<List<Vector2>>();
        while (segments.Count > 0)
        {
            Vector2 currentpoint = segments[0].Point2;
            List<Vector2> currentpath = new List<Vector2> { segments[0].Point1, segments[0].Point2 };
            segments.Remove(segments[0]);

            bool currentpathcomplete = false;
            while (currentpathcomplete == false)
            {
                currentpathcomplete = true;
                for (int s = 0; s < segments.Count; s++)
                {
                    if (segments[s].Point1 == currentpoint)
                    {
                        currentpathcomplete = false;
                        currentpath.Add(segments[s].Point2);
                        currentpoint = segments[s].Point2;
                        segments.Remove(segments[s]);
                    }
                    else if (segments[s].Point2 == currentpoint)
                    {
                        currentpathcomplete = false;
                        currentpath.Add(segments[s].Point1);
                        currentpoint = segments[s].Point1;
                        segments.Remove(segments[s]);
                    }
                }
            }
            output.Add(currentpath);
        }
        return output;
    }

    bool HasPixel(Texture2D texture, int x, int y)
    {
        Color color = texture.GetPixel(x, y);
        bool higherAlpha = color.a >= AlphaThreshhold;
        float value = 0f;
        bool lowerr = color.r <= value;
        bool lowerg = color.g <= value;
        bool lowerb = color.b <= value;
        return higherAlpha && lowerr && lowerg && lowerb;
    }

    //This function finds the one pixel long segments that make up the collider.
    List<ColliderSegment> GetSegments(Texture2D texture, int size = 1)
    {
        List<ColliderSegment> output = new List<ColliderSegment>();
        //Loop over each pixel.
        for (int y = 0; y < texture.height; y += size)
        {
            for (int x = 0; x < texture.width; x += size)
            {
                //First check that the current pixel is solid.
                if (HasPixel(texture, x, y))
                {
                    //if it is check the pixels above, bellow, to the left, and to the right to see if they are edges.
                    if (y + size >= texture.height 
                        || !HasPixel(texture, x, y + size))// check up
                    {
                        output.Add(new ColliderSegment(new Vector2(x, y + size),
                            new Vector2(x + size, y + size)));
                    }
                    if (y - size < 0 
                        || !HasPixel(texture, x, y - size))// check down
                    {
                        output.Add(new ColliderSegment(new Vector2(x, y),
                            new Vector2(x + size, y)));
                    }
                    if (x + size >= texture.width 
                        || !HasPixel(texture, x + size, y))// check right
                    {
                        output.Add(new ColliderSegment(new Vector2(x + size, y),
                            new Vector2(x + size, y + size)));
                    }
                    if (x - size < 0 
                        || !HasPixel(texture, x - size, y))// check left
                    {
                        output.Add(new ColliderSegment(new Vector2(x, y),
                            new Vector2(x, y + size)));
                    }
                }
            }
        }
        return output;
    }

    //This function forces unity to allow us to read a texture.
    private Texture2D ForceReadable(Texture2D Original)
    {
        byte[] TextureData = Original.EncodeToPNG();
        Texture2D Copy = new Texture2D(Original.width, Original.height);
        ImageConversion.LoadImage(Copy, TextureData, false);
        return Copy;
    }

    Texture2D toTexture2D(RenderTexture rTex)
    {
        Texture2D tex = new Texture2D(rTex.width, rTex.height, TextureFormat.RGB24, false);
        // ReadPixels looks at the active RenderTexture.
        RenderTexture.active = rTex;
        tex.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
        tex.Apply();
        return tex;
    }
}

#if UNITY_EDITOR
//This class gives unity custom instructions for how to show our component and adds the regenerate button to the inspector.
[CustomEditor(typeof(PixelPerfectCollider2D))]
public class PixelColider2DEditor : Editor
{
    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Regenerate Collider!"))
        {
            PixelPerfectCollider2D collider = (PixelPerfectCollider2D)target;
            collider.Regenerate();
        }
        base.OnInspectorGUI();
    }
}
#endif
