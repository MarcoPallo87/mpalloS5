using mpalloS5.Models;

namespace mpalloS5.Views;

public partial class vPersona : ContentPage
{
    public vPersona()
    {
        InitializeComponent();
    }

    private void btnMostrar_Clicked(object sender, EventArgs e)
    {
        List<Persona> people = App._personRepo.GetAllPerson();
        ListaPersonas.ItemsSource = people;
    }

    private void btnAgregar_Clicked(object sender, EventArgs e)
    {
        lblStatusMessage.Text = "";
        App._personRepo.AddNewPerson(txtName.Text);
        lblStatusMessage.Text = App._personRepo.StatusMessaage;
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
                lblStatusMessage.Text = App._personRepo.StatusMessaage;
                btnMostrar_Clicked(sender, e);
            }
        }
        catch (Exception ex)
        {
            lblStatusMessage.Text = "Error: " + ex.Message;
        }
    }
}