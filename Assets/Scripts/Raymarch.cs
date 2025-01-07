using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Camera))]
[ExecuteInEditMode]
public class raymarch : MonoBehaviour
{
    [SerializeField]
    private Shader _shader;

    public Material _raymarchMaterial
    {
        get 
        { 
            if (!_raymarchMat && _shader)
            {
                _raymarchMat = new Material(_shader);
                _raymarchMat.hideFlags = HideFlags.HideAndDontSave;
            }
            return _raymarchMat;        
        }
    }

    private Material _raymarchMat;

    public Camera _camera
    {
        get
        {
            if (!_cam)
            {
                _cam = GetComponent<Camera>();
            }
            return _cam;
        }
    }
    private Camera _cam;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (!_raymarchMat)
        {
            Graphics.Blit(source, destination);
            return;
        }
    }
}
