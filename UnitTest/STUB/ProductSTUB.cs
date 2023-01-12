using DataLayer;
using InterfaceLayer;
using InterfaceLayer.DTO;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.STUB
{
    public class ProductSTUB : IProductContainer, IProduct
    {
        /// <summary>A List of products.</summary>
        public List<ProductDTO> productList;
        /// <summary>A List of product types.</summary>
        public List<ProductTypeDTO> typeList = new List<ProductTypeDTO>()
            {
                new ProductTypeDTO(1, "Food"),
                new ProductTypeDTO(2, "Drink"),
                new ProductTypeDTO(3, "Extra"),
            };

        public ProductSTUB()
        {
            productList = new List<ProductDTO>();
            ProductDTO[] list = {
                new ProductDTO(1, "Pizza", 5.99, 1),
                new ProductDTO(2, "Pasta", 6.99, 1),
                new ProductDTO(3, "Sushi", 5.99, 1),
                new ProductDTO(4, "Cola", 2.99, 2),
                new ProductDTO(5, "Ice Tea", 3.49, 2),
                new ProductDTO(6, "Fanta", 2.99, 2),
                new ProductDTO(7, "Fries", 5.99, 3),
                new ProductDTO(8, "Salad", 5.99, 3),
            };
            productList.AddRange(list);
        }

        public bool CreateProduct(ProductDTO product)
        {
            int pAmount = productList.Count;
            productList.Add(new(productList.Count, product.Name, product.Price, product.Type));
            return pAmount < productList.Count;
        }

        public bool DeleteProduct(int id)
        {
            int pAmount = productList.Count;
            productList.Remove(productList.First(prod => prod.Id == id));
            return pAmount > productList.Count;
        }

        public bool Edit(ProductDTO productDTO)
        {
            productList.Where(prod => prod.Id == productDTO.Id).ToList().ForEach(prod =>
            {
                prod.Name = productDTO.Name;
                prod.Price = productDTO.Price;
                prod.Type = productDTO.Type;
            });
            return productList.Any(prod => prod.Id == productDTO.Id);
        }

        public List<ProductDTO> GetAll()
        {
            return productList;
        }

        public List<ProductDTO> GetAllOfType(int type)
        {
            return productList.FindAll(prod => prod.Type == type);
        }

        public List<ProductTypeDTO> GetAllTypes()
        {
            return typeList;
        }

        public ProductDTO GetProduct(int Id)
        {
            return productList.Find(prod => prod.Id == Id);
        }

        public string GetProductTypeName(int typeId)
        {
            return typeList.Find(type => type.Id == typeId).Name;
        }
    }
}
