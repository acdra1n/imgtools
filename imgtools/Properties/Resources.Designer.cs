﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace imgtools.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("imgtools.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Usage: imgtools &lt;command&gt; [arguments]
        ///Commands:
        ///  brightness &lt;image&gt; &lt;percentage&gt; - changes the brightness of an image.
        ///  info &lt;image&gt; - retrieves information about an image.
        ///  invert &lt;image&gt; - inverts the colors of an image.
        ///  parsehex &lt;hex_color&gt; - reveals the values of a hex color string.
        ///  removealpha &lt;image&gt; - removes all transparency of an image.
        ///  replacecolor &lt;image&gt; &lt;color&gt; &lt;new_color&gt; [--ignoreAlpha] - replaces all occurences of &lt;color&gt; (hex format) with &lt;new_color&gt; of an image.
        ///  resize &lt; [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string help {
            get {
                return ResourceManager.GetString("help", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to using static imgtools.Plugin.Stdlib;
        ///using System.Drawing;
        ///
        ///public class TestAlgorithm : ProcessingAlgorithm {
        ///	public override bool Execute(Bitmap image, params object[] args) {
        ///		int height = image.Height;
        ///		int width = image.Width;
        ///		
        ///		Color GetPx(int x, int y) {
        ///			return image.GetPixel(x, y);
        ///		}
        ///		
        ///		Color SetPx(int x, int y, Color c) {
        ///			return image.SetPixel(x, y, c);
        ///		}
        ///		
        ///		%ALGORITHM_CODE%
        ///		return true;
        ///	}
        ///	
        ///	public override float GetVersion() {
        ///		return &quot;%VERSION%&quot;;
        ///	}        /// [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string imtalgorithm_template {
            get {
                return ResourceManager.GetString("imtalgorithm_template", resourceCulture);
            }
        }
    }
}
