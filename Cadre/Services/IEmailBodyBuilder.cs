using Cadre.ViewModels;
using System.Collections.Generic;

namespace Cadre
{
    public interface IEmailBodyBuilder
    {
        string Build(List<PostViewModel> viewModels);
    }
}
