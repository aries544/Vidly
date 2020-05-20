using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using System.Data.Entity;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomerController : Controller
    {
        private ApplicationDbContext _context;

        public CustomerController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        public ActionResult New()
        {
            List<MembershipType> membershipTypes = _context.MembershipTypes.ToList();
            CustomerFormViewModel newCustomerViewModel = new CustomerFormViewModel 
            {                
                Customer=new Customer(),
                MembershipTypes = membershipTypes 
            };
            return View("CustomerForm",newCustomerViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                CustomerFormViewModel newCustomerViewModel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipTypes = _context.MembershipTypes.ToList()
                };
                return View("CustomerForm", newCustomerViewModel);
            }

            if (customer.Id == 0)
                _context.Customers.Add(customer);
            else 
            {
                Customer customerOld = _context.Customers.Single(c => c.Id == customer.Id);
                customerOld.Name = customer.Name;
                customerOld.BirthDate = customer.BirthDate;
                customerOld.MembershipTypeId = customer.MembershipTypeId;
                customerOld.IsSuscribedToNewLestter = customer.IsSuscribedToNewLestter;
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Customer");
        }

        // GET: Customer
        public ActionResult Index(int? pageIndex, string sortBy)
        {
            if (!pageIndex.HasValue)
                pageIndex = 1;

            if (String.IsNullOrWhiteSpace(sortBy))
                sortBy = "name";

            List<Customer> customers = _context.Customers.Include(c => c.MembershipType).ToList();
            return View(customers);
        }

        public ActionResult Detail(int id)
        {
            Customer customer = _context.Customers.Include(c=>c.MembershipType).SingleOrDefault(c => c.Id == id);

            return View(customer);
        }

        public ActionResult Edit(int id)
        {
            Customer customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();

            CustomerFormViewModel newCustomerViewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes=_context.MembershipTypes.ToList()
            };
            return View("CustomerForm",newCustomerViewModel);
        }
    }
}