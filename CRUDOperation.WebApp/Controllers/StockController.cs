using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using CRUDOperation.Models;
using CRUDOperation.Abstractions.BLL;
using CRUDOperation.Repositories;
using AutoMapper;
using CRUDOperation.Models.RazorViewModels.Stock;
using CRUDOperation.Abstractions.Repositories;

namespace CRUDOperation.WebApp.Controllers
{
    public class StockController : Controller
    {
        private IStockManager _stockManager;
        private IProductManager _productManager;


        private IMapper _mapper;

        private CRUDOperation.DatabaseContext.CRUDOperationDbContext _db; //Search Facilities

        public StockController(IStockManager stockManager, IMapper mapper, IProductManager productManager)
        {
            _mapper = mapper;
            _stockManager = stockManager;

            _productManager = productManager; //Dropdown List

            _db = new CRUDOperation.DatabaseContext.CRUDOperationDbContext(); //Search Facilities
        }

        // GET: Stock
        public IActionResult Index()
        {
            var stocks = _stockManager.GetAll();
            var model = new StockCreateViewModel();
            model.StockList = stocks.ToList();
            PopulateDropdownList(); /*Dropdown List Binding*/
            return View(stocks);
        }

        // GET: Stock/Details/5
        public IActionResult Create()
        {
            var stocks = _stockManager.GetAll();
            var model = new StockCreateViewModel();
            model.StockList = stocks.ToList();
            PopulateDropdownList(); /*Dropdown List Binding*/
            return View(model);
        }

        [HttpPost]

        public IActionResult Create(StockCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var stock = _mapper.Map<Stock>(model); //AutoMapper

                bool isAdded = _stockManager.Add(stock);
                if (isAdded)
                {
                    ViewBag.SuccessMessage = "Saved Successfully!";
                }


            }
            else
            {
                ViewBag.ErrorMessage = "Operation Failed!";
            }

            model.StockList = _stockManager.GetAll().ToList();
            PopulateDropdownList(model.ProductId); /*Dropdown List Binding*/
            return View(model);
        }


        private void PopulateDropdownList(object selectList = null) /*Dropdown List Binding*/
        {
            var product = _productManager.GetAll();
            ViewBag.SelectList = new SelectList(product, "Id", "Name", selectList);
        }
        //public IActionResult Edit(int? id)
        //{

        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var stock = _stockManager.GetById((Int64)id);
        //    if (stock == null)
        //    {
        //        return NotFound();
        //    }

        //    PopulateDropdownList(stock.ProductId);

        //    var stockCreateViewModel = new StockCreateViewModel();

        //    stockCreateViewModel.StockList = _stockManager.GetAll().ToList();

        //    stockCreateViewModel.Name = stock.Name;
        //    stockCreateViewModel.Quantity = stock.Quantity;


        //    stockCreateViewModel.Product = stock.Product;
        //    stockCreateViewModel.ProductId = Convert.ToInt32(stock.ProductId);
        //    return View(/*"Create",productCreateViewModel*/ stock);
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Edit(int id, [Bind("Id,Name,Price,ExpireDate,IsActive,CategoryId,CategoryName")] Product product)
        //{
        //    if (id != product.Id)
        //    {
        //        return NotFound();
        //    }

        //    PopulateDropdownList(product.CategoryId);

        //    if (ModelState.IsValid)
        //    {
        //        bool isUpdated = _productManager.Update(product);
        //        if (isUpdated)
        //        {
        //            var products = _productManager.GetAll();
        //            ViewBag.SuccessMessage = "Updated Successfully!";
        //            return View("Index", products);
        //        }
        //    }
        //    return View(product);
        //}

        ////public IActionResult Delete(int? id)
        ////{
        ////    if (id == null)
        ////    {
        ////        return NotFound();
        ////    }

        ////    var product = _productManager.GetById((Int64)id);

        ////    if (product == null)
        ////    {
        ////        return NotFound();
        ////    }
        ////    return View(product);
        ////}

        ////[HttpPost, ActionName("Delete")]
        ////[ValidateAntiForgeryToken]

        //public IActionResult Delete(int id)
        //{
        //    var product = _productManager.GetById(id);
        //    if (ModelState.IsValid)
        //    {
        //        bool isDeleted = _productManager.Delete(product);
        //        if (isDeleted)
        //        {
        //            var products = _productManager.GetAll();
        //            ViewBag.SuccessMessage = "Data Deleted Successfully.!";
        //            return View("Index", products);
        //        }

        //    }
        //    return RedirectToAction(nameof(Index));
        //}

        ////private bool ProductExists(int id)
        ////{
        ////    return _productManager.ProductExists(id);
        ////}

        //public IActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var product = _productManager.GetById((Int64)id);

        //    if (product == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(product);
        //}
    }
}
