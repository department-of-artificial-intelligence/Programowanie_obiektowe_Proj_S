using Project.Common;
using Project.Model;

namespace Project.View.Abstractions
{
    internal interface IAddDriverWindow : IWindow
    {
        public Driver Driver { get; set; }
    }
}
