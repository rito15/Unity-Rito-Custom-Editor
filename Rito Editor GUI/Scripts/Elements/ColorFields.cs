#if UNITY_EDITOR

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// 날짜 : 2021-06-02 PM 4:17:12
// 작성자 : Rito

namespace Rito.EditorUtilities
{
    public partial class ColorField : DrawingElement<Color, ColorField>
    {
        public static ColorField Default { get; } = new ColorField();
        protected GUIStyle labelStyle;

        // Data
        protected GUIContent labelContent;
        protected float widthThreshold = 0.4f;

        // Styles - Label
        public Color labelColor = Color.white;
        public int labelFontSize = 12;
        public FontStyle labelFontStyle = FontStyle.Normal;
        public TextAnchor labelAlignment = TextAnchor.MiddleLeft;

        // Styles - Color Picker
        public Color colorPickerColor = Color.white;

        public override ColorField Clone()
        {
            return new ColorField
            {
                labelColor = labelColor,
                labelFontSize = labelFontSize,
                labelFontStyle = labelFontStyle,
                labelAlignment = labelAlignment,
                colorPickerColor = colorPickerColor,
            };
        }

        /***********************************************************************
        *                               Style Setters
        ***********************************************************************/
        #region .

        public ColorField SetLabelColor(Color color)
        {
            this.labelColor = color;
            return this;
        }
        public ColorField SetLabelFontSize(int fontSize)
        {
            this.labelFontSize = fontSize;
            return this;
        }
        public ColorField SetLabelFontStyle(FontStyle fontStyle)
        {
            this.labelFontStyle = fontStyle;
            return this;
        }
        public ColorField SetLabelTextAlignment(TextAnchor allignment)
        {
            this.labelAlignment = allignment;
            return this;
        }

        public ColorField SetColorPickerColor(Color color)
        {
            this.colorPickerColor = color;
            return this;
        }

        #endregion

        public ColorField SetData(string label, Color value, float widthThreshold = 0.4f)
        {
            this.labelContent = new GUIContent(label);
            this.value = value;
            this.widthThreshold = widthThreshold;

            return this;
        }

        public override ColorField Draw(in float xLeft, in float xRight, float yOffset, in float height,
            in float xLeftOffset = 0f, in float xRightOffset = 0f)
        {
            if (CheckDrawErrors()) return this;
            SetRect(xLeft, xRight, yOffset, height, xLeftOffset, xRightOffset);

            // Styles
            if (labelStyle == null)
                labelStyle = new GUIStyle(GUI.skin.label);

            labelStyle.normal.textColor = labelColor;
            labelStyle.hover.textColor = labelColor.AddRGB(0.25f);
            labelStyle.focused.textColor = labelColor.AddRGB(0.25f);
            labelStyle.fontSize = labelFontSize;
            labelStyle.fontStyle = labelFontStyle;
            labelStyle.alignment = labelAlignment;

            // Rects
            ref float t = ref widthThreshold;
            float omt = 1f - t;
            Rect labelRect = new Rect(rect.x, rect.y, rect.width * t, rect.height);
            Rect inputRect = new Rect(rect.x + rect.width * t, rect.y, rect.width * omt, rect.height);

            // 1. Label
            EditorGUI.PrefixLabel(labelRect, labelContent, labelStyle);

            // 2. Field
            var oldColor = GUI.color;
            GUI.color = colorPickerColor;

            value = EditorGUI.ColorField(inputRect, "", value);

            GUI.color = oldColor;

            EndDraw();
            return this;
        }
    }
    public partial class ColorPicker : DrawingElement<Color, ColorPicker>
    {
        public static ColorPicker Default { get; } = new ColorPicker();

        // Styles - Color Picker
        public Color colorPickerColor = Color.white;

        public override ColorPicker Clone()
        {
            return new ColorPicker
            {
                colorPickerColor = colorPickerColor
            };
        }

        public ColorPicker SetData(Color value)
        {
            this.value = value;

            return this;
        }

        public override ColorPicker Draw(in float xLeft, in float xRight, float yOffset, in float height,
            in float xLeftOffset = 0f, in float xRightOffset = 0f)
        {
            if (CheckDrawErrors()) return this;
            SetRect(xLeft, xRight, yOffset, height, xLeftOffset, xRightOffset);

            var oldColor = GUI.color;
            GUI.color = colorPickerColor;

            value = EditorGUI.ColorField(rect, "", value);

            GUI.color = oldColor;

            EndDraw();
            return this;
        }
    }
}

#endif