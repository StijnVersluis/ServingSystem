using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using LogicLayer;
using DataLayer;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace ServingSystem.Models
{
    public class CRUDProductViewModel
    {
        private ProductContainer pCont = new ProductContainer(new ProductDAL());
        [HiddenInput]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        [DefaultValue(0)]
        public double Price { get; set; }

        [DisplayName("Product Type")]
        public List<ProductType> ProductTypes { get; set; }

        public CRUDProductViewModel()
        {
            ProductTypes = pCont.GetAllTypes();
        }
        public CRUDProductViewModel(string name, double price)
        {
            ProductTypes = pCont.GetAllTypes();
            Name = name;
            Price = price;
        }
        public CRUDProductViewModel(int id, string name, double price)
        {
            Id = id;
            ProductTypes = pCont.GetAllTypes();
            Name = name;
            Price = price;
        }
    }
}
