using ksalazarS6Online.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace ksalazarS6Online.Views;

public partial class vistaEstudiante : ContentPage
{
	private const string Url = "http://192.168.68.108/wsestudiante/restEstudiantes.php";
	private readonly HttpClient cliente= new HttpClient();
	private ObservableCollection<Estudiante> _estudiante;

	public async void Mostrar()
	{
		var content = await cliente.GetStringAsync(Url);
		List<Estudiante> mostrarEst = JsonConvert.DeserializeObject<List<Estudiante>>(content);
		_estudiante = new ObservableCollection<Estudiante>(mostrarEst);
		listaEstudiantes.ItemsSource = _estudiante;
	}
    public vistaEstudiante()
	{
		InitializeComponent();
		Mostrar();
	}
}