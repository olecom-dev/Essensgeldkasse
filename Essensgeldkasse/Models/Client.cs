using Microsoft.Maui.Graphics.Text;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Essensgeldkasse.Models
{
    [Table("client")]
    public class Client : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        [PrimaryKey, AutoIncrement, Column("ID")]
        public int ID { get; set; }
        public int Counter { get; set; } = 0;
        public String Code { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }

        private Decimal _saldo;
        public Decimal Saldo
        {
            get => _saldo; 
            set
            {
                if (_saldo <= 0m)
                {
                    _saldo = value;
                    ForegroundColor = Microsoft.Maui.Graphics.Color.FromArgb("#ff0000"); 

                }
                if(_saldo >=0m)
                {
                    _saldo = value;
                    ForegroundColor = Microsoft.Maui.Graphics.Color.FromArgb("#000000"); ;

                }
            }
        }

        private Color _foregroundColor;
        [Ignore]
        public Color ForegroundColor { get => _foregroundColor; set { _foregroundColor = value; } }
        private Color _backgroundColor;
        [Ignore]
        public Color BackgroundColor
        {
            get
            {
          if(Counter % 2 == 0)
                {
                    return Color.FromArgb("#FFA07A");
                }
                if (Counter % 2!=0) 
                {

                    return Color.FromArgb("#7FFFD4");
                }
                return Color.FromArgb("#ffffff");
            }
            set
            {
                _backgroundColor = value;
            }
        }
            public void OnPropertyChanged([CallerMemberName] string name = "Saldo") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    }
        public class Clients : IQueryAttributable
        {
       
            public List<Client> List { get; set; }
            public Clients cli { get;  set; }
            public Clients()
            {
                List = new List<Client>();
            }

            public void Add(Client newClient)
            {
            newClient.Counter = List.Count > 0 ? List.Max(x => x.Counter) + 1 : 1; 
            List.Add(newClient);
            }
            public void ApplyQueryAttributes(IDictionary<string, object> query)
            {
                cli = query["Clients"] as Clients;
                OnPropertyChanged("cli");
            }

            private void OnPropertyChanged(string v)
            {
                throw new NotImplementedException();
            }

            public void Remove(Client oldClient)
            {
          
                List.Remove(oldClient);
            List.ForEach((x) => { if (x.Counter > oldClient.Counter) x.Counter = x.Counter - 1; });
        }
        }
    
}
