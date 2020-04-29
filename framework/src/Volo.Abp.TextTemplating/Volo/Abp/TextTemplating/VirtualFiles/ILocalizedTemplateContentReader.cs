using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Volo.Abp.TextTemplating.VirtualFiles
{
    public interface ILocalizedTemplateContentReader
    {
        string GetContentOrNull([CanBeNull] string culture);

        List<TemplateContentInfo> GetAllContentInfos();
    }
}