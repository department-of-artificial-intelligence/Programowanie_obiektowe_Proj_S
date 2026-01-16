using WypozyczalniaSamochodow.Common;
using WypozyczalniaSamochodow.Model;

namespace WypozyczalniaSamochodow.View.Abstractions;

public interface IAddBranchWindow: IWindow
{
    public Branch Branch { get; set; }
}

