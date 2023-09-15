using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework.Content;

namespace Sxe.Engine.UI
{
    public class SchemeManager : ISchemeManager
    {
        public IScheme DefaultScheme
        {
            get { return defaultScheme; }
        }

        Dictionary<string, IScheme> schemeDictionary = new Dictionary<string,IScheme>();
        IScheme defaultScheme;
        ContentManager content;

        public SchemeManager(IServiceProvider services)
        {
            content = new ContentManager(services);
        }

        public IScheme LoadSchemeFromFile(string schemeFileName)
        {
            IScheme outScheme = GetScheme(schemeFileName);
            if (outScheme != null)
                return outScheme;

            outScheme = new Scheme(schemeFileName, content);

            if (defaultScheme == null)
                defaultScheme = outScheme;
            else
                outScheme.DefaultScheme = defaultScheme;

            return outScheme;
        }

        public IScheme GetScheme(string schemeFileName)
        {
            if (schemeDictionary.ContainsKey(schemeFileName))
            {
                return schemeDictionary[schemeFileName];
            }
            return null;
        }

        public void UnloadAll()
        {
        }
    }
}
