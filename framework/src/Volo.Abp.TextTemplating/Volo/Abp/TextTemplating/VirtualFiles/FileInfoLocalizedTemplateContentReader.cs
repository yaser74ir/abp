using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.FileProviders;

namespace Volo.Abp.TextTemplating.VirtualFiles
{
    public class FileInfoLocalizedTemplateContentReader : ILocalizedTemplateContentReader
    {
        private string _content;

        public async Task ReadContentsAsync(IFileInfo fileInfo)
        {
            _content = await fileInfo.ReadAsStringAsync();
        }

        public string GetContentOrNull(string culture)
        {
            if (culture == null)
            {
                return _content;
            }

            return null;
        }

        public List<TemplateContentInfo> GetAllContentInfos()
        {
            var info = new List<TemplateContentInfo>()
            {
                new TemplateContentInfo()
                {
                    Content = _content
                }
            };

            return info;
        }
    }
}