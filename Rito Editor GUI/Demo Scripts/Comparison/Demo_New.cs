#if UNITY_EDITOR

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// 날짜 : 2021-06-01 AM 2:18:13
// 작성자 : Rito

namespace Rito.EditorUtilities.Demo
{
    public class Demo_New : MonoBehaviour
    {
        public string stringValue;
        public bool foldoutValue, toggleButtonValue;

        public float[] floatArray = { 0.1f, 0.2f, 0.3f, 0.4f };
        public int floatSelected;

        [CustomEditor(typeof(Demo_New))]
        private class CE : RitoEditor
        {
            private Demo_New m;
            private void OnEnable() => m = target as Demo_New;

            protected override void OnSetup(RitoEditorGUI.Setting setting)
            {
                setting
                    //.KeepSameViewWidth()
                    //.SetEditorBackgroundColor(Color.white.SetA(0.2f))
                    .SetLayoutControlXPositions(0.01f, 0.985f)
                    .ActivateRectDebugger()
                    .ActivateTooltipDebugger();
            }

            protected override void OnDrawInspector()
            {
                const float boxOutlineWidth = 2f;

                FoldoutHeaderBox.Blue
                    .SetData("Header Box", m.foldoutValue, boxOutlineWidth, 4f)
                    .DrawLayout(3, 2f)
                    //.Draw(20f, 64f).Layout()
                    .GetValue(out m.foldoutValue);

                if (m.foldoutValue)
                {
                    StringField.Violet
                        .SetData("String Field", m.stringValue, "Placeholder")
                        .DrawLayout().GetValue(out m.stringValue);

                    Dropdown<float>.Purple
                        .SetData("Float Dropdown", m.floatArray, m.floatSelected)
                        .DrawLayout().GetValue(out m.floatSelected);

                    // Button & ToggleButton
                    const float buttonRatio = 0.7f;

                    Button.Blue
                        .SetData("Button")
                        .Draw(0.01f, buttonRatio, 20f);

                    ToggleButton.Blue
                        .SetData("Toggle Button", m.toggleButtonValue)
                        .Draw(buttonRatio + 0.01f, 0.985f, 20f).Layout()
                        .GetValue(out m.toggleButtonValue);
                }
            }
        }
    }
}

#endif