using Project.Common;
using Project.Model;

namespace Project.View.Abstractions
{
    public interface IAddDriverWindow : IWindow
    {
        public Driver Driver { get; set; }
    }
}
