using System;
using UnityEditor;
using UnityEngine;

public class FractalMaterialEditor : MaterialEditor {

    //Variables.
    static int previewSize = 3;

    //Override the inspector draw 
    public override void OnInspectorGUI() {

        //Only draw anything if the inspector is visible.
        if (isVisible) {

            //Get the current material.
            Material material = target as Material;

            //Display the inspector fields.
            GUILayoutOption[] emptyGUILayoutOption = new GUILayoutOption[] { };
            EditorGUILayout.LabelField(new GUIContent("Fractal Settings"), EditorStyles.boldLabel, emptyGUILayoutOption);
            int fractalType = EditorGUILayout.IntPopup(new GUIContent("Fractal Type", "The type of fractal to display."), material.GetInt("fractalType"),
                    new GUIContent[] { new GUIContent("Mandelbrot"), new GUIContent("Julia") }, new int[] { 0, 1 }, emptyGUILayoutOption);
            material.SetInt("fractalType", fractalType);
            material.SetInt("iterations", Math.Min(Math.Max(EditorGUILayout.IntField(new GUIContent("Iterations", "The number of iterations for each pixel " +
                    "in the fractal image. Larger values will allow for more distinct colours in the fractal, but will take longer to render."),
                    material.GetInt("iterations"), emptyGUILayoutOption), 2), 9999));
            material.SetFloat("convergenceThreshold", Math.Min(Math.Max(EditorGUILayout.FloatField(new GUIContent("Convergence Threshold",
                    "The value at which each pixel in the fractal algorithm is deemed to have \"converged\". Keep this as low as possible to ensure " +
                    "rendering is quick, but the image quality is not compromised."), material.GetFloat("convergenceThreshold"), emptyGUILayoutOption), 1),
                    9999));
            material.SetInt("smoothing", EditorGUILayout.Toggle(new GUIContent("Smoothing", "Smooths the fractal colours. This does not work for all " +
                    "colouring methods, so ensure it is turned off if it doesn't make any difference. If this option is selected and the fractal still does " +
                    "not appear to be as smooth as it could be, try increasing the convergence threshold, above."), material.GetInt("smoothing") == 1,
                    emptyGUILayoutOption) ? 1 : 0);
            material.SetInt("multibrot", Math.Min(Math.Max(EditorGUILayout.IntField(new GUIContent("Multibrot", "A \"Multibrot\" is an extension of the " +
                    "Mandelbrot/Julia set which is rendered by raising each point in the complex plane to a value other than the default 2. Experiment to " +
                    "see what Multibrots from 2 to 5 look like!"), material.GetInt("multibrot"), emptyGUILayoutOption), 2), 5));
            Vector2 centre = EditorGUILayout.Vector2Field(new GUIContent("Centre", "The centre point of the fractal to render. This can be set manually, or " +
                    "using the preview image, below."), new Vector2(material.GetFloat("centreX"), material.GetFloat("centreY")), emptyGUILayoutOption);
            float scale = EditorGUILayout.FloatField(new GUIContent("Scale", "The scale of the fractal to render. This can be set manually, or using the " +
                    "preview image, below."), material.GetFloat("scale"), emptyGUILayoutOption);
            if (fractalType == 1) {
                EditorGUILayout.Separator();
                EditorGUILayout.LabelField(new GUIContent("Julia Fractal Type Settings"), EditorStyles.boldLabel, emptyGUILayoutOption);                
                Rect juliaFractalConstantRectangle = EditorGUILayout.GetControlRect(true, emptyGUILayoutOption);
                EditorGUI.LabelField(new Rect(juliaFractalConstantRectangle.xMin, juliaFractalConstantRectangle.yMin, EditorGUIUtility.labelWidth,
                        juliaFractalConstantRectangle.height), new GUIContent("Constant", "The complex constant to use when generating a Julia fractal."));
                material.SetFloat("juliaConstantReal", EditorGUI.FloatField(new Rect(juliaFractalConstantRectangle.xMin + EditorGUIUtility.labelWidth,
                        juliaFractalConstantRectangle.yMin, (juliaFractalConstantRectangle.width - EditorGUIUtility.labelWidth) * 0.4f,
                        juliaFractalConstantRectangle.height), material.GetFloat("juliaConstantReal"), EditorStyles.numberField));
                EditorGUI.LabelField(new Rect(juliaFractalConstantRectangle.xMin + (juliaFractalConstantRectangle.width * 0.4f) +
                        (EditorGUIUtility.labelWidth * 0.6f), juliaFractalConstantRectangle.yMin, (juliaFractalConstantRectangle.width -
                        EditorGUIUtility.labelWidth) * 0.2f, juliaFractalConstantRectangle.height), "+ i", EditorStyles.label);
                material.SetFloat("juliaConstantImaginary", EditorGUI.FloatField(new Rect(juliaFractalConstantRectangle.xMin +
                        (juliaFractalConstantRectangle.width * 0.6f) + (EditorGUIUtility.labelWidth * 0.4f), juliaFractalConstantRectangle.yMin,
                        (juliaFractalConstantRectangle.width - EditorGUIUtility.labelWidth) * 0.4f, juliaFractalConstantRectangle.height),
                        material.GetFloat("juliaConstantImaginary"), EditorStyles.numberField));
            }
            EditorGUILayout.Separator();
            EditorGUILayout.LabelField(new GUIContent("Colour Settings"), EditorStyles.boldLabel, emptyGUILayoutOption);
            EditorGUILayout.Separator();
            material.SetColor("backgroundColour", EditorGUILayout.ColorField(new GUIContent("Background Colour", "The background colour to merge with the " +
                    "fractal."), material.GetColor("backgroundColour"), emptyGUILayoutOption));
            material.SetFloat("backgroundColourAmount", EditorGUILayout.Slider(new GUIContent("Background Amount", "The amount that the background colour " +
                    "is to be merged with the fractal. A value of 0 ignores the background colour completely, while a value of 1 shows only the background " +
                    "colour."), material.GetFloat("backgroundColourAmount"), 0, 1, emptyGUILayoutOption));

            for (int i = 0; i < 3; i++) {
                string colourName = i == 0 ? "Red" : (i == 1 ? "Green" : "Blue");
                EditorGUILayout.Separator();
                EditorGUILayout.LabelField(new GUIContent(colourName), EditorStyles.boldLabel, emptyGUILayoutOption);
                int formula = EditorGUILayout.IntPopup(new GUIContent("Formula", "The formula for calculating the " + colourName.ToLower() +
                        " component of the fractal colour."), material.GetInt(colourName.ToLower() + "Formula"),
                        new GUIContent[] { new GUIContent("Fixed Value"), new GUIContent("Iterations"), new GUIContent("Sine (Iterations * Multiplier)"),
                        new GUIContent("Cosine (Iterations * Multiplier)"), new GUIContent("Tangent (Iterations * Multiplier)"),
                        new GUIContent("Real (Zn) * Multiplier"), new GUIContent("Imaginary (Zn) * Multiplier"),
                        new GUIContent("Sine (Real (Zn) * Multiplier)"), new GUIContent("Cosine (Real (Zn) * Multiplier)"),
                        new GUIContent("Tangent (Real (Zn) * Multiplier)"), new GUIContent("Sine (Imaginary (Zn) * Multiplier)"),
                        new GUIContent("Cosine (Imaginary (Zn) * Multiplier)"), new GUIContent("Tangent (Imaginary (Zn) * Multiplier)") },
                        new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 }, emptyGUILayoutOption);
                material.SetInt(colourName.ToLower() + "Formula", formula);
                if (formula == 0)
                    material.SetFloat(colourName.ToLower() + "FixedValue", Math.Min(Math.Max(EditorGUILayout.FloatField(new GUIContent("Value",
                            "The fixed " + colourName.ToLower() + " value to set."), material.GetFloat(colourName.ToLower() + "FixedValue"),
                            emptyGUILayoutOption), 0), 1));
                else if (formula >= 2)
                    material.SetFloat(colourName.ToLower() + "Multiplier", EditorGUILayout.FloatField(new GUIContent("Multiplier",
                            "The multiplier to apply to the selected formula for calculating the " + colourName.ToLower() +
                            " component of the fractal colour."), material.GetFloat(colourName.ToLower() + "Multiplier"), emptyGUILayoutOption));
            }
            EditorGUILayout.HelpBox("Use the left mouse button to drag the preview fractal below, and the mouse wheel to zoom in or out. When using the " +
                    "mouse wheel, hold shift to zoom at a faster rate or control to zoom at a slower rate. These operations will affect the \"center\" and " +
                    "\"scale\" properties, above.", MessageType.Info);
            previewSize = EditorGUILayout.IntPopup(new GUIContent("Preview Size", "Whether to draw a preview image, below, and if so what size to draw it at."),
                    previewSize, new GUIContent[] { new GUIContent("Off"), new GUIContent("Small"), new GUIContent("Medium"), new GUIContent("Large") },
                    new int[] { 0, 1, 2, 3 }, emptyGUILayoutOption);

            //Draw the preview image, if required.
            if (previewSize > 0) {
                int targetWidth = (Screen.width * 9) / (previewSize == 3 ? 10 : previewSize == 2 ? 20 : 40);
                Rect previewRectangle = EditorGUILayout.GetControlRect(false, targetWidth, GUIStyle.none, emptyGUILayoutOption);
                previewRectangle.width = Math.Min(previewRectangle.width, targetWidth);
                previewRectangle.height = previewRectangle.width;
                float centreHorizontally = ((float) ((Screen.width * 9) / 10) - previewRectangle.width) / 2;
                previewRectangle.xMin += centreHorizontally;
                previewRectangle.xMax += centreHorizontally;
                float width = previewRectangle.width;
                for (int i = 0; i < 3; i++) {
                    EditorGUI.DrawRect(previewRectangle, i % 2 == 0 ? Color.black : Color.white);
                    previewRectangle.xMin += width * 0.01f;
                    previewRectangle.xMax -= width * 0.01f;
                    previewRectangle.yMin += width * 0.01f;
                    previewRectangle.yMax -= width * 0.01f;
                }

                //Draw the preview texture, creating it if necessary.
                Texture2D previewTexture = new Texture2D(32, 32);
                GUI.SetNextControlName("Preview Texture");
                EditorGUI.DrawPreviewTexture(previewRectangle, previewTexture, material);
                DestroyImmediate(previewTexture);

                //The mouse wheel zooms in and out of the fractal.
                if (Event.current.type == EventType.ScrollWheel) {
                    Vector2 mousePosition = Event.current.mousePosition - new Vector2(previewRectangle.xMin, previewRectangle.yMin);
                    if (Math.Abs(Event.current.delta.y) > 0 && mousePosition.x > 0 && mousePosition.x < previewRectangle.xMax && mousePosition.y > 0 &&
                            mousePosition.y < previewRectangle.yMax) {
                        GUI.FocusControl("Preview Texture");
                        scale *= (Event.current.delta.y / (Event.current.shift ? 10 : (Event.current.control ? 1000 : 100))) + 1;
                        Event.current.Use();
                    }
                }

                //The left mouse button drags the preview image.
                else if (Event.current.type == EventType.MouseDrag && Event.current.button == 0) {
                    Vector2 mousePosition = Event.current.mousePosition - new Vector2(previewRectangle.xMin, previewRectangle.yMin);
                    if (mousePosition.x > 0 && mousePosition.x < previewRectangle.xMax && mousePosition.y > 0 && mousePosition.y < previewRectangle.yMax) {
                        GUI.FocusControl("Preview Texture");
                        Vector2 mouseDelta = new Vector2(-Event.current.delta.x, Event.current.delta.y);
                        centre += (mouseDelta * scale) / width;
                        Event.current.Use();
                    }
                }
            }

            //Set the centre and scale properties, now that they have potentially been modified by the dragging/scaling of the preview image.
            material.SetFloat("centreX", centre.x);
            material.SetFloat("centreY", centre.y);
            material.SetFloat("scale", scale);
        }
    }
}
