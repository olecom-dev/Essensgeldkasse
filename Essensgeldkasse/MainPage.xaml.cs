using Essensgeldkasse.Models;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace Essensgeldkasse
{
    public partial class MainPage : ContentPage
    {
        Database.ClientsDatabase database;

        List<Product> products = new List<Product>();
        List<Product> productsList = new List<Product>();   
        Clients clientsList = new Clients();
       Clients clients = new Clients();
        Decimal s = 0m;

        public  MainPage()
        {
            database = new Database.ClientsDatabase();
          
            InitializeComponent();
            BindingContext = this;
            /*        products.Add(new Product { ProductName = "Essen 1", ProductDescription = "(2,00 €)", ProductPrice = 2.0m, BackgroundColor = "#0066ff" });
                     products.Add(new Product { ProductName = "Essen 2", ProductDescription = "(2,50 €)", ProductPrice = 2.5m, BackgroundColor = "#0066ff" });
                     products.Add(new Product { ProductName = "Essen 3", ProductDescription = "(3,00 €)", ProductPrice = 3.0m, BackgroundColor = "#0066ff" });
                     products.Add(new Product { ProductName = "Aufgetautes", ProductDescription = "(1,50 €)", ProductPrice = 1.5m, BackgroundColor = "#0066ff" });
                     products.Add(new Product { ProductName = "Süsses/Obst", ProductDescription = "(0,70 €)", ProductPrice = 0.7m, BackgroundColor = "#0066ff" });
                     products.Add(new Product { ProductName = "Müsli/Kuchen", ProductDescription = "(0,80 €)", ProductPrice = 0.8m, BackgroundColor = "#0066ff" });
                     products.Add(new Product { ProductName = "Apfelschorle", ProductDescription = "(0,80 €)", ProductPrice = 0.8m, BackgroundColor = "#9999ff" });
                     products.Add(new Product { ProductName = "Mineralwasser", ProductDescription = "(0,40 €)", ProductPrice = 0.4m, BackgroundColor = "#9999ff" });
                     products.Add(new Product { ProductName = "Kaffee/Tee", ProductDescription = "(1,00 €)", ProductPrice = 1.0m, BackgroundColor = "#9999ff" });
                     products.Add(new Product { ProductName = "Kaffee/Tee *5", ProductDescription = "(5,00 €)", ProductPrice = 5.0m, BackgroundColor = "#9999ff" });


                     clients.Add(new Client { Code = "MA", FirstName = "Andreas", LastName = "Mertens", Saldo = -2.5m });
                     clients.Add(new Client { Code = "57", FirstName = "Oliver", LastName = "Hüsten", Saldo = 0m });  */






        }


        public async void MainPage_Loaded(object sender, EventArgs e)
        {
      /*     foreach (Client client in clients.List)
            {
                var result = await database.SaveClientAsync(client);
            }
            foreach (Product product in products)
            {
                var res = await database.SaveProductAsync(product);
            }  */
                productsList = await database.GetProductsAsync();
            clientsList.List = await database.GetClientsAsync("LastName", "asc");
          
            
            listClients.ItemsSource = clientsList.List;

            var i = 0;
            var z = 0;
            var counter = 0;
            foreach (var product in productsList) {

                Button btn = new Button
                {
                    Text = product.ProductName + " " + product.ProductDescription,
                    BackgroundColor = Color.FromArgb(product.BackgroundColor),
                    LineBreakMode = LineBreakMode.WordWrap
                };
                btn.Clicked += async (sender, args) =>
                {
                    if (listClients.SelectedItem != null)
                    {
                        s -= product.ProductPrice; var i = clientsList.List.IndexOf((Client)listClients.SelectedItem);
                        clientsList.List[i].Saldo = s; await database.SaveClientAsync(clientsList.List[i]); listClients.ItemsSource = null; listClients.ItemsSource = clientsList.List;
                    }
                };
                if (counter % 3 == 0)
                {
                    z++;
                    i = 0;
                   
                }

                gridButtonsDyn.Add(btn, i++, z);
                counter++;
            }
        }
        protected async override void OnNavigatedTo(NavigatedToEventArgs args)
        {
            clientsList.List = await database.GetClientsAsync("LastName", "asc");
            listClients.ItemsSource = null; 
            listClients.ItemsSource = clientsList.List;

            base.OnNavigatedTo(args);
        }
        public async void CmdAddClient(object sender, EventArgs e)
        {

           
            await Shell.Current.GoToAsync($"addClient");
        }
        public async void Edit_Command(object sender, EventArgs args)
        {
            if (listClients.SelectedItem != null)
            {
                var cl = listClients.SelectedItem as Client;
                var navigationParameter = new Dictionary<String, Object> { { "Client", cl } };
                await Shell.Current.GoToAsync("addClient", navigationParameter);
            }
        }
        public void BtnMeal1Add_Click(object sender, EventArgs e)
        {
           
            if (listClients.SelectedItem !=null)
            {
                s -= productsList[0].ProductPrice;
                var i = clientsList.List.IndexOf((Client)listClients.SelectedItem);
                clientsList.List[i].Saldo = s;
                listClients.ItemsSource = null;
                listClients.ItemsSource = clientsList.List;
            }

        }
        private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.Count > 0)

            {

                foreach (var g in clientsList.List)

                {

                    if (e.CurrentSelection.Contains(g))
                    {
                        s = g.Saldo;
                        listClients.SelectedItem = g;
                        listClients.Focus();

                    }

                }
            }
        }

        private async void BtnCash_Click(object sender, EventArgs e)
        {
            if (listClients.SelectedItem != null)
            {
                if(EntCash.Text != null){
                    s = s + Decimal.Parse(EntCash.Text);
                    var i = clientsList.List.IndexOf((Client)listClients.SelectedItem);
                    clientsList.List[i].Saldo = s;
                   await database.SaveClientAsync(clientsList.List[i]);
                    listClients.ItemsSource = null;
                    listClients.ItemsSource = clientsList.List;
                }
                EntCash.Text = String.Empty;
            }
        }
        private void BtnMeal2_Click(object sender, EventArgs e)
        {
            if (listClients.SelectedItem != null)
            {
                s -= 2.5m;
                var i = clientsList.List.IndexOf((Client)listClients.SelectedItem);
                clientsList.List[i].Saldo = s;
                listClients.ItemsSource = null;
                listClients.ItemsSource = clientsList.List;
            }
        }
        private void BtnMeal3_Click(object sender, EventArgs e)
        {
            if (listClients.SelectedItem != null)
            {
                s -= 3.0m;
                var i = clientsList.List.IndexOf((Client)listClients.SelectedItem);
                clientsList.List[i].Saldo = s;
                listClients.ItemsSource = null;
                listClients.ItemsSource = clientsList.List;
            }
        }
        private void BtnSweets_Click(object sender, EventArgs e)
        {
            if (listClients.SelectedItem != null)
            {
                s -= 0.7m;
                var i = clientsList.List.IndexOf((Client)listClients.SelectedItem);
                clientsList.List[i].Saldo = s;
                listClients.ItemsSource = null;
                listClients.ItemsSource = clientsList.List;
            }
        }
        private void BtnCereals_Click(object sender, EventArgs e)
        {
            if (listClients.SelectedItem != null)
            {
                s -= 0.8m;
                var i = clientsList.List.IndexOf((Client)listClients.SelectedItem);
                clientsList.List[i].Saldo = s;
                listClients.ItemsSource = null;
                listClients.ItemsSource = clientsList.List;
            }
        }
        private void BtnFrozen_Click(object sender, EventArgs e)
        {
            if (listClients.SelectedItem != null)
            {
                s -= 1.5m;
                var i = clientsList.List.IndexOf((Client)listClients.SelectedItem);
                clientsList.List[i].Saldo = s;
                listClients.ItemsSource = null;
                listClients.ItemsSource = clientsList.List;
            }
        }
        private void BtnApplejuice_Click(object sender, EventArgs e)
        {
            if (listClients.SelectedItem != null)
            {
                s -= 0.8m;
                var i = clientsList.List.IndexOf((Client)listClients.SelectedItem);
                clientsList.List[i].Saldo = s;
                listClients.ItemsSource = null;
                listClients.ItemsSource = clientsList.List;
            }

        }
        private void BtnMineralwater_Click(object sender, EventArgs e)
        {
            if (listClients.SelectedItem != null)
            {
                s -= 0.4m;
                var i = clientsList.List.IndexOf((Client)listClients.SelectedItem);
                clientsList.List[i].Saldo = s;
                listClients.ItemsSource = null;
                listClients.ItemsSource = clientsList.List;
            }

        }
        private async void BtnSortCode_Click(object sender, EventArgs e)
        {
            clientsList.List = await database.GetClientsAsync("Code", "asc");
            listClients.ItemsSource = clientsList.List;
        }
        private async void BtnSortFirstName_Click(object sender, EventArgs e)
        {
            clientsList.List = await database.GetClientsAsync("FirstName", "asc");
            listClients.ItemsSource = clientsList.List;
        }
        bool doubleTapped;
        bool ignoreNextTap;
        void BtnCodeOnTapGestureRecognizerDoubleTapped(object sender, TappedEventArgs args)
        {
            doubleTapped = true;
        }
        async void BtnCodeOnTapGestureRecognizerSingleTapped(object sender, TappedEventArgs args)
        {
            var delay = Task.Delay(555);
            await delay;
            if (ignoreNextTap)
            {
                ignoreNextTap = false;
                return;
            }
            if (doubleTapped)
            {
                doubleTapped = false;
                ignoreNextTap = true;
                clientsList.List = await database.GetClientsAsync("Code", "dsc");
                listClients.ItemsSource = clientsList.List;
            }
            else
            {
                clientsList.List = await database.GetClientsAsync("Code", "asc");
                listClients.ItemsSource = clientsList.List;
            }
        }
        void BtnFirstNameOnTapGestureRecognizerDoubleTapped(object sender, TappedEventArgs args)
        {
            doubleTapped = true;
        }
        async void BtnFirstNameOnTapGestureRecognizerSingleTapped(object sender, TappedEventArgs args)
        {
            var delay = Task.Delay(555);
            await delay;
            if (ignoreNextTap)
            {
                ignoreNextTap = false;
                return;
            }
            if (doubleTapped)
            {
                doubleTapped = false;
                ignoreNextTap = true;
                clientsList.List = await database.GetClientsAsync("FirstName", "dsc");
                listClients.ItemsSource = clientsList.List;
            }
            else
            {
                clientsList.List = await database.GetClientsAsync("FirstName", "asc");
                listClients.ItemsSource = clientsList.List;
            }
        }
        void BtnLastNameOnTapGestureRecognizerDoubleTapped(object sender, TappedEventArgs args)
        {
            doubleTapped = true;
        }
        async void BtnLastNameOnTapGestureRecognizerSingleTapped(object sender, TappedEventArgs args)
        {
            var delay = Task.Delay(555);
            await delay;
            if (ignoreNextTap)
            {
                ignoreNextTap = false;
                return;
            }
            if (doubleTapped)
            {
                doubleTapped = false;
                ignoreNextTap = true;
                clientsList.List = await database.GetClientsAsync("LastName", "dsc");
                listClients.ItemsSource = clientsList.List;
            }
            else
            {
                clientsList.List = await database.GetClientsAsync("LastName", "asc");
                listClients.ItemsSource = clientsList.List;
            }
        }
        public void OnItemSelectedChanged(object sender, SelectionChangedEventArgs e)
        {
           
            if (e.CurrentSelection != null)
            {
                var g = (Client)e.CurrentSelection[0];
                s = g.Saldo;
                g.Saldo = 0;
                listClients.SelectedItem = g;
            }
        }
       
    }
}