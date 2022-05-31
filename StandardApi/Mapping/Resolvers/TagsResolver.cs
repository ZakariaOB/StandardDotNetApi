using AutoMapper;
using StandardApi.Contracts.V1.Responses;
using StandardApi.Domain;
using StandardApi.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace StandardApi.Mapping.Resolvers
{
    public class TagsResolver : IValueResolver<Message, MessageResponse, string>
    {
        public string Resolve(Message source, MessageResponse destination, string destMember, ResolutionContext context)
        {
            if (source is null || source.Tags.IsNullOrEmpty())
            {
                return string.Empty;
            }
            IEnumerable<Tag> tags = source.Tags;
            IEnumerable<Tag> orderedTags = tags.OrderBy(t => t.Description);
            IEnumerable<string> numberedTags = orderedTags.Select((ta, index) => $"{index + 1}) {ta.Description}");
            return string.Join("#", numberedTags);
        }
    }
}
