using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Volo.Abp.TextTemplating.VirtualFiles
{
    public class VirtualFileTemplateContentContributor : ITemplateContentContributor, ITransientDependency
    {
        public const string VirtualPathPropertyName = "VirtualPath";

        private readonly ILocalizedTemplateContentReaderFactory _localizedTemplateContentReaderFactory;

        public VirtualFileTemplateContentContributor(
            ILocalizedTemplateContentReaderFactory localizedTemplateContentReaderFactory)
        {
            _localizedTemplateContentReaderFactory = localizedTemplateContentReaderFactory;
        }

        public virtual async Task<string> GetOrNullAsync(TemplateContentContributorContext context)
        {
            var localizedReader = await _localizedTemplateContentReaderFactory
                .CreateAsync(context.TemplateDefinition);

            return localizedReader.GetContentOrNull(
                context.Culture
            );
        }

        public virtual async Task<List<TemplateContentInfo>> GetAllContentInfosAsync(TemplateDefinition templateDefinition)
        {
            var localizedReader = await _localizedTemplateContentReaderFactory
                .CreateAsync(templateDefinition);

            return localizedReader.GetAllContentInfos();
        }
    }
}