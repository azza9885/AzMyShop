﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AzMyShop.Core.Models;  //build the project so that these models are available in the dropdown when the view is being created 
using AzMyShop.DataAccess.InMemory;

namespace AzMyShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        // GET: ProductManager
        Productrepository context;
        public ProductManagerController() // creating a context for product repository | this is a constructor , 
                                          // whenever this is called a new context of productrepository is created
        {
            context = new Productrepository();
        }
        public ActionResult Index() //Making the Index page return a list of products by pulling in the list from collections list on repository page

        {
            List<Product> products = context.Collection().ToList();
            return View(products);
        }

        public ActionResult Create()  // this method is to display a page to fill in the product details
        {
            Product product = new Product();
            return View(product);
        }

        [HttpPost]
        public ActionResult Create(Product product) // this method is to post the product details of the newly created product
        { 
            if(!ModelState.IsValid)  //this is neccessary to check to make sure any validation set on the page is correct else return to product list 
                                     //with the neccessary validation errors 
            {
                return View(product);
            }

            else
            {
                context.Insert(product);
                context.Commit();

                return RedirectToAction("Index");  // returning to the index after successfully adding a product
            }
        }

        public ActionResult Edit(string Id)
        {
            Product product = context.Find(Id);  //to load the product to be edited from the database

            if(product == null)
            {
                return HttpNotFound();
            }

            else
            {
                return View(product); //else return the view with the product that we have found
            }
        }
        
        [HttpPost]
        public ActionResult Edit(Product product,String Id)  //default template requires to send through the updateed product along with the original product ID
                                                             //just in case you want to update the original product ID
        {
            Product producttoupdate = context.Find(Id);

            if(producttoupdate == null)
            {
                return HttpNotFound();
            }

            else
            {
                if (!ModelState.IsValid)  //this is neccessary to check to make sure any validation set on the page is correct else return to product list 
                                          //with the neccessary validation errors 
                {
                    return View(product);
                }

                producttoupdate.Category = product.Category;
                producttoupdate.Description = product.Description;
                producttoupdate.Image = product.Image;
                producttoupdate.Name = product.Name;
                producttoupdate.Price = product.Price;

                context.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(string Id) // the first action is to load the product from the DB that needs to be deleted
        {
            Product producttodelete = context.Find(Id);

            if(producttodelete == null)
            {
                return HttpNotFound();
            }

            else
            {
                return View(producttodelete);
            }
        }

        [HttpPost]
        [ActionName("Delete")] //Alternative action name of Delete
        public ActionResult ConfirmDelete(string Id)  // the second action is so that the user can confirm before they can actually delete the product
        {
            Product producttodelete = context.Find(Id);
            if(producttodelete == null)
            {
                return HttpNotFound();
            }

            else
            {
                context.Delete(Id);
                context.Commit();
                return RedirectToAction("Index");

            }
        }
    }
}