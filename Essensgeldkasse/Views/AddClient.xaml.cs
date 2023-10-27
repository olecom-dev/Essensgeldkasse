using Essensgeldkasse.Models;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text.Json;


namespace Essensgeldkasse.Views;
[QueryProperty(nameof(Client), "Client")]
public partial class AddClient : ContentPage, IQueryAttributable, INotifyPropertyChanged
{
	public Clients clients = new Clients();

		public Client Client { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void ApplyQueryAttributes(IDictionary<string, object> query)
		{
			Client = query["Client"] as Client;

       //     OnPropertyChanged("Client");
		}
	
   
    Database.ClientsDatabase database = new Database.ClientsDatabase();
	public AddClient()
	{
        InitializeComponent();





    }


    public async void OnBtnAddClient_Clicked(object sender, EventArgs e)
	{


		var client = new Client
		{
			ID=Int32.Parse(TbID.Text),
			Code = TbCode.Text,
			FirstName = TbFirstName.Text,
			LastName = TbLastName.Text,
			Saldo = Decimal.Parse(TbSaldo.Text)
		};
		try
		{
			clients.Add(client);
		await	database.SaveClientAsync(client);
		}
		catch (Exception ex) { Debug.WriteLine(ex); }


		await Shell.Current.GoToAsync("///home");
    }
	public async void OnBtnDeleteClient_Clicked(object sender, EventArgs args)
	{
		var client = new Client
		{
			ID = Int32.Parse(TbID.Text),
			Code = TbCode.Text,
			FirstName = TbFirstName.Text,
			LastName = TbLastName.Text,
			Saldo = Decimal.Parse(TbSaldo.Text)
		};
		clients.Remove(client);
		await database.DeleteClientAsync(client);
        await Shell.Current.GoToAsync("///home");
    }
    protected override void OnNavigatedTo(NavigatedToEventArgs args)
	{
		base.OnNavigatedTo(args);
		if(Client!=null)
		{
			TbID.Text = Client.ID.ToString();
            TbCode.Text = Client.Code;
            TbFirstName.Text = Client.FirstName;
            TbLastName.Text = Client.LastName;
            TbSaldo.Text = Client.Saldo.ToString();
        }
		else
		{
			TbID.Text = 0.ToString();
			TbCode.Text = String.Empty;
			TbFirstName.Text = String.Empty;
			TbLastName.Text = String.Empty;
			TbSaldo.Text = String.Empty;
		}
   
    }
}