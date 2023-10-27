using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essensgeldkasse.Models
{
    [Table("product")]
    public class Product
    {
        [PrimaryKey, AutoIncrement, Column("ID")]
        public int ID { get; set; } 
        public string ProductName { get; set; }      
        public string ProductDescription { get; set; }
        public Decimal ProductPrice { get; set; }
        public String BackgroundColor {  get; set; }
    }
}
