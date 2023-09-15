using System;
using System.Collections.Generic;
using System.Text;

namespace Sxe.Engine.UI
{
    public interface ISchemeManager
    {
        IScheme DefaultScheme { get; }

        /// <summary>
        /// Load a scheme from a file
        /// </summary>
        /// <param name="schemeFileName"></param>
        /// <returns></returns>
        IScheme LoadSchemeFromFile(string schemeFileName);
        IScheme GetScheme(string schemeName);

        /// <summary>
        /// Unload all schemes
        /// </summary>
        void UnloadAll();
    }
}
