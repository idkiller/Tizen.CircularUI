using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Tizen.Wearable.CircularUI.Forms.Renderer.Interop
{
    static class Eext
    {
        internal const string Lib = "libefl-extension.so.0";

        internal const string RotarySelectorBackgroundImagePartName = "selector,bg_image";
        internal const string RotarySelectorIconPartName = "selector,icon";

        internal enum RotarySelectorState
        {
            Normal,
            Pressed
        }

        [DllImport(Lib)]
        internal static extern void eext_rotary_selector_item_part_content_set(IntPtr item, string part_name, int state, IntPtr content);

        [DllImport(Lib)]
        internal static extern void eext_rotary_selector_part_color_set(IntPtr obj, string part_name, int state, int r, int g, int b, int a);

        [DllImport(Lib)]
        internal static extern void eext_rotary_selector_item_part_color_set(IntPtr item, string part_name, int state, int r, int g, int b, int a);
    }
}
