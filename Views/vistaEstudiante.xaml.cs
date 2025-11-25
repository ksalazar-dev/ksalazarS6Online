using ksalazarS6Online.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace ksalazarS6Online.Views;

public partial class vistaEstudiante : ContentPage
{
    private const string Url = "http://192.168.68.108/wsestudiante/restEstudiantes.php";
    private readonly HttpClient cliente = new HttpClient();
    private ObservableCollection<Estudiante> _estudiante;

    public vistaEstudiante()
    {
        InitializeComponent();
        Mostrar();  // Primera carga
    }

    // ?? Recarga la lista cada vez que la página aparece en pantalla
    protected override void OnAppearing()
    {
        base.OnAppearing();
        Mostrar();
    }

    public async void Mostrar()
    {
        try
        {
            var content = await cliente.GetStringAsync(Url);
            List<Estudiante> mostrarEst = JsonConvert.DeserializeObject<List<Estudiante>>(content);
            _estudiante = new ObservableCollection<Estudiante>(mostrarEst);
            listaEstudiantes.ItemsSource = _estudiante;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }

    private void btnAgregar_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new vistaAgregar());
    }

    private void listaEstudiantes_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem == null) return;

        var objetoEstudiante = (Estudiante)e.SelectedItem;
        Navigation.PushAsync(new vistaActualizarEliminar(objetoEstudiante));

        listaEstudiantes.SelectedItem = null; // Evitar que quede seleccionado
    }
}
