using System.Collections.Generic;
using System.Threading.Tasks;

namespace Volo.Abp.TextTemplating.VirtualFiles
{
    public class NullLocalizedTemplateContentReader : ILocalizedTemplateContentReader
    {
        public static NullLocalizedTemplateContentReader Instance { get; } = new NullLocalizedTemplateContentReader();

        private NullLocalizedTemplateContentReader()
        {
            
        }

        public string GetContentOrNull(string culture)
        {
            return null;
        }

        public List<TemplateContentInfo> GetAllContentInfos()
        {
            return new List<TemplateContentInfo>();
        }
    }
}