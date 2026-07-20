using Microsoft.Extensions.DependencyInjection;

namespace SistemaClinica;

public partial class App : Application
{

	public static bool ValidadorAdm {get; set;}

	public static bool ValidadorUser {get; set; }
	
	public App()
	{
		InitializeComponent();
	}

	protected override Window CreateWindow(IActivationState? activationState)
	{
		return new Window(new Views.BoasVindasPage());
	}
}