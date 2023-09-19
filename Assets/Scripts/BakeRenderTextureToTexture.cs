#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using System.IO;
using UnityEngine;

namespace UtilitiesCustomPackage.EditorExtensions.Windows
{
    public class BakeRenderTextureToTexture : EditorWindow
    {
        private string _textureName;

        private RenderTexture _renderTexture;

        private Texture2D _outputTexture;

        [MenuItem("Custom Editor/Bake RT to Texture")]
        public static void ShowWindow()
        {
            BakeRenderTextureToTexture bakeRenderTextureToTexture = GetWindow<BakeRenderTextureToTexture>("Bake RT to Texture");
            bakeRenderTextureToTexture.minSize = bakeRenderTextureToTexture.maxSize = new Vector2(230f, 140f);
            
        }

        private void OnGUI()
        {
            _renderTexture = (RenderTexture)EditorGUI.ObjectField(new Rect(10f, 5f, 200f, 200f), "Render Texture", _renderTexture, typeof(RenderTexture), false);
            EditorGUI.LabelField(new Rect(10f, 55f, 150f, 20f), "Texture Name:");
            _textureName = EditorGUI.TextField(new Rect(10f, 77f, 200f, 20f), _textureName);

            EditorGUI.BeginDisabledGroup(string.IsNullOrEmpty(_textureName) || _renderTexture == null);
            GUILayout.BeginArea(new Rect(40f, 120f, 150f, 30f));
            if(GUILayout.Button("Bake Texture"))
            {
                _outputTexture = new Texture2D(_renderTexture.width, _renderTexture.height, TextureFormat.RGB24, false);
                PackTexture();
            }
            GUILayout.EndArea();
            EditorGUI.EndDisabledGroup();
        }

        private void PackTexture()
        {
            //WRITE RT TO TEXTURE
            Rect rectReadTexture = new Rect(0, 0, _outputTexture.width, _outputTexture.height);
            RenderTexture.active = _renderTexture;
            _outputTexture.ReadPixels(rectReadTexture, 0, 0);
            _outputTexture.Apply();
            RenderTexture.active = null;


            //EXPORT TEXTURE
            string finalPath = _textureName + ".png";
            if (finalPath.Length != 0)
            {
                byte[] pngData = _outputTexture.EncodeToPNG();
                if(pngData != null)
                {
                    File.WriteAllBytes(finalPath, pngData);
                }
            }
            AssetDatabase.ImportAsset(finalPath);
            Object obj = AssetDatabase.LoadAssetAtPath(finalPath, typeof(Object));
            Selection.activeObject = obj;
            EditorGUIUtility.PingObject(obj);
            Repaint();
        }
    }
}
#endif
