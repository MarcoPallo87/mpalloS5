using mpalloS5.Models;

namespace mpalloS5.Views;

public partial class vPersona : ContentPage
{
    int idEditar = 0;

    public vPersona()
    {
        InitializeComponent();
    }

    private async void btnMostrar_Clicked(object sender, EventArgs e)
    {
        try
        {
            List<Persona> people = App._personRepo.GetAllPerson();
            ListaPersonas.ItemsSource = people;

            if (people.Count == 0)
            {
                await DisplayAlertAsync("Aviso", "La lista está vacía. No hay personas guardadas.", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Error al mostrar", ex.Message, "OK");
        }
    }

    private async void btnAgregar_Clicked(object sender, EventArgs e)
    {
        try
        {
            lblStatusMessage.Text = "";

            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                await DisplayAlertAsync("Error", "Por favor, ingrese un nombre.", "OK");
                return;
            }

            if (idEditar > 0)
            {
                App._personRepo.UpdatePerson(idEditar, txtName.Text);
                lblStatusMessage.Text = App._personRepo.StatusMessage;

                if (App._personRepo.StatusMessage.Contains("Error"))
                {
                    await DisplayAlertAsync("Error al actualizar", App._personRepo.StatusMessage, "OK");
                }
                else
                {
                    txtName.Text = "";
                    idEditar = 0;
                    btnAgregar.Text = "Agregar Persona";
                    btnMostrar_Clicked(sender, e);
                }
            }
            else
            {
                App._personRepo.AddNewPerson(txtName.Text);
                lblStatusMessage.Text = App._personRepo.StatusMessage;

                if (App._personRepo.StatusMessage.Contains("Error"))
                {
                    await DisplayAlertAsync("Error al guardar", App._personRepo.StatusMessage, "OK");
                }
                else
                {
                    txtName.Text = "";
                    await DisplayAlertAsync("Éxito", "Persona guardada correctamente. Presione 'Mostrar Personas' para actualizar la lista.", "OK");
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Error inesperado", ex.Message, "OK");
        }
    }

    private void btnEditar_Clicked(object sender, EventArgs e)
    {
        try
        {
            var button = (Button)sender;
            idEditar = (int)button.CommandParameter;

            var persona = App._personRepo.GetAllPerson().FirstOrDefault(p => p.Id == idEditar);

            if (persona != null)
            {
                txtName.Text = persona.Name;
                lblStatusMessage.Text = "Editando persona con ID: " + idEditar;
                btnAgregar.Text = "Guardar Cambios";
            }
        }
        catch (Exception ex)
        {
            lblStatusMessage.Text = "Error: " + ex.Message;
        }
    }

    private async void btnEliminar_Clicked(object sender, EventArgs e)
    {
        try
        {
            lblStatusMessage.Text = "";
            var button = (Button)sender;
            int idPersona = (int)button.CommandParameter;

            bool confirmar = await DisplayAlertAsync("Confirmar", "¿Está seguro de eliminar esta persona?", "Sí", "No");

            if (confirmar)
            {
                App._personRepo.DeletePerson(idPersona);
                lblStatusMessage.Text = App._personRepo.StatusMessage;
                btnMostrar_Clicked(sender, e);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Error al eliminar", ex.Message, "OK");
        }
    }
}