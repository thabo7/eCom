using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyShop.Core.Models;

namespace MyShop.DataAccess.InMemory
{// this class are no longer needed since we have a class InMemory that does the job
    public class ProductRepository
    {

        ObjectCache cache = MemoryCache.Default;
        List<Product> products ;


        public ProductRepository() //this contract will do a standard initialization 
        {
            products = cache["products"] as List<Product>;
            if(products == null)
            {
                products = new List<Product>();
            }


        }
        public void Commit() //when people adds product we don't want to add the product straight away
        {
            cache["products"] = products; 
        }
        public void Insert(Product p)
        {
            products.Add(p);
        }
        public void Update(Product product)
        {
            Product productToUpdate = products.Find(p => p.Id == product.Id); // searching the product that needs to be updated
            if(productToUpdate !=null)
            {
                productToUpdate = product;
            }
            else
            {
                throw new Exception("Product not found");
            }

        }

        public Product Find(string Id)
        {
            Product product = products.Find(p => p.Id == Id); 
            if (product != null)
            {
                return product;
            }
            else
            {
                throw new Exception("Product not found");
            }
        }
        public IQueryable<Product> Collection()
        {
            return products.AsQueryable();
        }
        public void Delete(string Id)
        {
            Product productToDelete = products.Find(p => p.Id == Id); // searching the product that needs to be delated
            if (productToDelete != null)
            {
               products.Remove(productToDelete);
            }
            else
            {
                throw new Exception("Product not found");
            }

        }
    }
}
