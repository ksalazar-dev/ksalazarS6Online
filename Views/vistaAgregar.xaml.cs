using System.Net;

namespace ksalazarS6Online.Views;

public partial class vistaAgregar : ContentPage
{
    public vistaAgregar()
    {
        InitializeComponent();
    }

    private void btnGuardar_Clicked(object sender, EventArgs e)
    {
        try
        {
            WebClient cliente = new WebClient();
            var parametros = new System.Collections.Specialized.NameValueCollection();
            parametros.Add("nombre", TxtNombre.Text);
            parametros.Add("apellido", TxtApellido.Text);
            parametros.Add("edad", TxtEdad.Text);
            cliente.UploadValues("http://192.168.68.108/wsestudiante/restEstudiantes.php", "POST", parametros);
            DisplayAlert("Alerta", "Ingreso correcto", "OK");
            Navigation.PushAsync(new vistaEstudiante());

        }
        catch (Exception ex)
        {

            Console.WriteLine("Error: " +ex);
        }
    }
}