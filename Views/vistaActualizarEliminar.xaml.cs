using ksalazarS6Online.Models;

namespace ksalazarS6Online.Views;

public partial class vistaActualizarEliminar : ContentPage
{
    public vistaActualizarEliminar(Estudiante datos)
    {
        InitializeComponent();
        txtCodigo.Text = datos.codigo.ToString();
        txtNombre.Text = datos.nombre;
        txtApellido.Text = datos.apellido;
        txtEdad.Text = datos.edad.ToString();
    }
    // ?? BOTÓN ACTUALIZAR (PUT)
    private async void btnActualizar_Clicked(object sender, EventArgs e)
    {
        try
        {
            string url = $"http://192.168.68.108/wsestudiante/restEstudiantes.php" +
                         $"?codigo={txtCodigo.Text}" +
                         $"&nombre={txtNombre.Text}" +
                         $"&apellido={txtApellido.Text}" +
                         $"&edad={txtEdad.Text}";

            HttpClient cliente = new HttpClient();
            var contenido = new StringContent(""); // PUT requiere contenido vacío

            var respuesta = await cliente.PutAsync(url, contenido);

            if (respuesta.IsSuccessStatusCode)
            {
                await DisplayAlert("Actualizado", "El estudiante fue actualizado correctamente", "OK");
                await Navigation.PopAsync(); // Regresar a la lista
            }
            else
            {
                await DisplayAlert("Error", "No se pudo actualizar el registro", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }

    // ??? BOTÓN ELIMINAR (DELETE)
    private async void btnEliminar_Clicked(object sender, EventArgs e)
    {
        bool confirmar = await DisplayAlert("Confirmar", "¿Seguro que deseas eliminar este registro?", "Sí", "No");
        if (!confirmar) return;

        try
        {
            string url = $"http://192.168.68.108/wsestudiante/restEstudiantes.php?codigo={txtCodigo.Text}";

            HttpClient cliente = new HttpClient();
            var respuesta = await cliente.DeleteAsync(url);

            if (respuesta.IsSuccessStatusCode)
            {
                await DisplayAlert("Eliminado", "El estudiante fue eliminado correctamente", "OK");
                await Navigation.PopAsync(); // Regresar a la lista
            }
            else
            {
                await DisplayAlert("Error", "No se pudo eliminar el registro", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }
}
