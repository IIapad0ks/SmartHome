using SmartHome.Core.Models;

namespace SmartHome.Core.Models
{
    public interface INameModel : IModel
    {
        string Name { get; set; }
    }
}
