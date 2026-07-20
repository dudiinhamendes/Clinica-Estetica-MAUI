using System.Threading.Tasks;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;

namespace SistemaClinica.ViewModels;

public class BoasVindasViewModel
{
    public async Task IrParaLogin()
    {
        await Task.Delay(3000);

        Application.Current!.Windows[0].Page = new AppShell();
    }
}