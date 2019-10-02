﻿using System.Linq;
using CRUDOperation.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using CRUDOperation.Abstractions.BLL;
using CRUDOperation.Models.RazorViewModels.Product;
using System;
using AutoMapper;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using CRUDOperation.Models.APIViewModels;

namespace CRUDOperation.WebApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductManager _productManager;
        private readonly ICategoryManager _categoryManager;

        private readonly IMapper _mapper;


        //private readonly CRUDOperation.DatabaseContext.CRUDOperationDbContext _db; //Search Facilities

        public ProductController(IProductManager productManager, ICategoryManager categoryManager, IMapper mapper)
        {
            _productManager = productManager;
            _categoryManager = categoryManager; //Dropdown List

            _mapper = mapper;

            //_db = new CRUDOperation.DatabaseContext.CRUDOperationDbContext(); //Search Facilities
        }

        [AllowAnonymous]
        public IActionResult Index(string searchBy, string search,double price) //Search Facilities
        {
            //return View(_db.Products.Where(x => x.Name.StartsWith(search) || search == null).ToList());
            if (searchBy == "Price")
            {
                return View(_productManager.GetByPrice(price));
            }
            else if (searchBy == "Name")
            {
                return View(_productManager.GetByName(search));
            }
            else if (searchBy == "CategoryName")
            {
                return View(_productManager.GetByCategory(search));
            }


            var model = _productManager.GetAll();

            return View(model);
        }
        [Authorize]
        public IActionResult ProductListIndex()
        {
            var products = _productManager.GetAll();
            return View(products);
        }


        public IActionResult Create()
        {
            var products = _productManager.GetAll();
            var model = new ProductCreateViewModel();
            model.ProductList = products.ToList();


            PopulateDropdownList(); /*Dropdown List Binding*/
            return View(model);
        }

        [HttpPost]

        public async Task<IActionResult> Create(ProductCreateViewModel model,List<IFormFile> ImageUrl)
        {
            if (ModelState.IsValid)
            {
                var product = _mapper.Map<Product>(model); //AutoMapper

               //Save Images start
                foreach (var item in ImageUrl)
                {
                    if(item.Length>0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            await item.CopyToAsync(ms);
                            product.ImageUrl = ms.ToArray();

                            bool isAdded = _productManager.Add(product);
                            if (isAdded)
                            {
                                ViewBag.SuccessMessage = "Saved Successfully!";
                            }
                        }
                    }
                    
                }
                //Image save End
            }
            else
            {
                ViewBag.ErrorMessage = "Operation Failed!";
            }

            model.ProductList = _productManager.GetAll().ToList();
            PopulateDropdownList(model.CategoryId); /*Dropdown List Binding*/
            return View(model);
        }


        private void PopulateDropdownList(object selectList=null) /*Dropdown List Binding*/
        {
            var category = _categoryManager.GetAll();
            ViewBag.SelectList = new SelectList(category, "Id", "Name", selectList);
        }
        public IActionResult Edit(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var product = _productManager.GetById((Int64)id);
            PopulateDropdownList(product.CategoryId);
            ProductCreateViewModel productCreateViewModel = _mapper.Map<ProductCreateViewModel>(product);
            if (product == null)
            {
                return NotFound();
            }



            //var productCreateViewModel = new ProductCreateViewModel();

            productCreateViewModel.ProductList = _productManager.GetAll().ToList();

            //productCreateViewModel.Name = product.Name;
            //productCreateViewModel.Price = product.Price;
            //productCreateViewModel.ExpireDate = product.ExpireDate;
            //productCreateViewModel.IsActive = product.IsActive;
            //productCreateViewModel.Category = product.Category;
            //productCreateViewModel.CategoryId = Convert.ToInt32(product.CategoryId);
            //productCreateViewModel.ImageUrl = product.ImageUrl;
            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,Price,ExpireDate,IsActive,CategoryId,CategoryName")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            PopulateDropdownList(product.CategoryId);

            if (ModelState.IsValid)
            {
                bool isUpdated = _productManager.Update(product);
                if (isUpdated)
                {
                    var products = _productManager.GetAll();
                    ViewBag.SuccessMessage = "Updated Successfully!";
                    return View("Index", products);
                }
            }
            return View(product);
        }


        public IActionResult Delete(long id)
        {
            var product = _productManager.GetById(id);
            if (ModelState.IsValid)
            {
                bool isDeleted = _productManager.Delete(product);
                if (isDeleted)
                {
                    var products = _productManager.GetAll();
                    ViewBag.SuccessMessage = "Data Deleted Successfully.!";
                    return View("Index", products);  
                }

            }
            return RedirectToAction(nameof(Index));
        }

        //private bool ProductExists(int id)
        //{
        //    return _productManager.ProductExists(id);
        //}

        public IActionResult Details(long id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            var product = _productManager.GetById(id);

            //if (product == null)
            //{
            //    return NotFound();
            //}
            //return View(product);

            //var product = _productManager.GetAll();
            return View(product);

        }


        public IActionResult Show()
        {
            return View();
        }
        public IActionResult GetProductPartial(long id)
        {
            var product = _productManager.GetById(id);
            if (product == null)
            {
                return null;
            }
            var productDto = _mapper.Map<ProductDto>(product);
            return PartialView("Product/_ProductDetails", productDto);

        }


        public IActionResult Variants()
        {
            return View();
        }
        
    }
}


