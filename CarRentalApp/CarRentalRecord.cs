//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CarRentalApp
{
    using System;
    using System.Collections.Generic;
    
    public partial class CarRentalRecord
    {
        public int id { get; set; }
        public string Costumer { get; set; }
        public Nullable<System.DateTime> DateRented { get; set; }
        public Nullable<System.DateTime> DateReturn { get; set; }
        public Nullable<decimal> Cost { get; set; }
        public Nullable<int> CarTypeid { get; set; }
    
        public virtual CarType CarType { get; set; }
    }
}
