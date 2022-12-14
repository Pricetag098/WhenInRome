using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicator : MonoBehaviour
{
    public Texture Icon = null;
    public Texture Arrow = null;
    public float arrowSize = 1f, iconSize = 1f;
    public Color Color = Color.white;
    public Health health;
    void Start()
    {
    health = GetComponent<Health>();
        var instance = OffscreenMarkers.Instance();
        if (instance)
        {
            instance.Register(this);
        }
    }
       
}

class OffscreenMarkers : MonoBehaviour
{
        
    public static OffscreenMarkers Instance()
    {
        Camera mc = Camera.main;
        if (!mc)
        {
            Debug.LogWarning("Couldn't find main camera.");
            return null;
        }
        var instance = mc.GetComponent<OffscreenMarkers>();
        if (!instance)
        {
            instance = mc.gameObject.AddComponent<OffscreenMarkers>();
        }
        return instance;
    }

    private Camera _camera => gameObject.GetComponent<Camera>();
    private List<Indicator> _trackedObjects = new List<Indicator>();

    public void Register(Indicator om)
    {
        if (!_trackedObjects.Contains(om))
        {
            _trackedObjects.Add(om);
        }
        else
        {
            Debug.LogWarning("Tracked objects list already contains " + om, om);
        }
    }

    private bool IsVisible(GameObject go)
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(_camera);
        Renderer[] rends = go.GetComponentsInChildren<Renderer>();
        foreach (var r in rends)
        {
            if (GeometryUtility.TestPlanesAABB(planes, r.bounds))
            {
                return true;
            }
        }
        return false;
    }

    private Vector3 FixBehindCamera(Vector3 p)
    {
        Vector3 d = p - _camera.transform.position;
        Vector3 n = _camera.transform.forward;
        float ds = Vector3.Dot(d, n);

        if (ds > 0)
        {
            return p;
        }

        // make sure that we are never on the camera plane
        if (ds == 0)
        {
            return p + n * 0.00001f;
        }

        return p - 2 * ds * n;
    }

    void OnGUI()
    {
        if (!Event.current.type.Equals(EventType.Repaint))
        {
            return;
        }
        Rect camRect = _camera.pixelRect;
        Vector2 iconSize = new Vector2(camRect.height / 14, camRect.height / 14);
        Vector2 iconExt = iconSize / 2;
        Vector2 arrowSize = new Vector2(iconSize.x / 4, iconSize.y / 2);
        Vector2 arrowExt = arrowSize / 2;
        Matrix4x4 mt = new Matrix4x4();
        mt.SetColumn(0, new Vector3(1, 0, 0));
        mt.SetColumn(1, new Vector3(0, 1, 0));
        mt.SetColumn(2, new Vector3(0, 0, 1));
        mt.SetColumn(3, new Vector4(-arrowExt.x, -arrowExt.y, 0, 1));
        float margin = arrowSize.y;
        for (int i = _trackedObjects.Count - 1; i >= 0; i--)
        {
        Indicator marker = _trackedObjects[i];
            if (marker && marker.gameObject && marker.health.health > 0)
            {
                if (!IsVisible(marker.gameObject) && marker.gameObject.activeSelf && Time.timeScale !=0)
                {
                    Vector3 wp = FixBehindCamera(marker.transform.position);
                    Vector2 mrkScrPos = _camera.WorldToScreenPoint(wp);
                    mrkScrPos.y = camRect.height - mrkScrPos.y;
                    Vector2 iconPos = new Vector2(
                        Mathf.Clamp(mrkScrPos.x, iconExt.x + margin, camRect.width - iconExt.x - margin),
                        Mathf.Clamp(mrkScrPos.y, iconExt.y + margin, camRect.height - iconExt.y - margin)
                    );
                    Vector2 tempIconSize = iconSize * marker.iconSize;
                    Rect ri = new Rect(iconPos.x - iconExt.x, iconPos.y - iconExt.y, tempIconSize.x, tempIconSize.y);
                    GUI.DrawTexture(ri, marker.Icon);
                    Vector2 towardMrk = mrkScrPos - iconPos;
                    if (towardMrk.sqrMagnitude > 0.001f)
                    {
                        Vector2 twn = towardMrk.normalized;
                        Vector2 arrowPos = iconPos + twn * (iconExt.x + arrowExt.y);
                        Matrix4x4 oldm = GUI.matrix;
                        Matrix4x4 mr = new Matrix4x4();
                        mr.SetColumn(0, new Vector3(twn.y, -twn.x, 0));
                        mr.SetColumn(1, new Vector3(-twn.x, -twn.y, 0));
                        mr.SetColumn(2, new Vector3(0, 0, 1));
                        mr.SetColumn(3, new Vector4(arrowPos.x, arrowPos.y, 0, 1));
                        GUI.matrix = mr * mt;
                        Vector2 tempArrowSize = arrowSize * marker.arrowSize;
                        Rect ra = new Rect(0, 0, tempArrowSize.x, tempArrowSize.y);
                        GUI.DrawTexture(ra, marker.Arrow, ScaleMode.StretchToFill, alphaBlend: true, imageAspect: 0,
                                            color: marker.Color, borderWidth: 0, borderRadius: 0);
                        GUI.matrix = oldm;
                    }
                }
            }
            else
            {
                _trackedObjects.RemoveAt(i);
            }
        }
    }

}