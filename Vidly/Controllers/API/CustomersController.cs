using AutoMapper;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.API
{
    public class CustomersController : ApiController
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        /// <summary>
        /// Devuelve una lista de Customers
        /// GET api/customers
        /// </summary>
        /// <returns>Listado de Customers</returns>
        public IEnumerable<CustomerDto> GetCustomers()
        {
            return _context.Customers.ToList().Select(Mapper.Map<Customer,CustomerDto>);
        }
        /// <summary>
        /// Devuelve un solo cliente
        /// GET api/customers/1
        /// </summary>
        /// <param name="id">Id del cliente a buscar</param>
        /// <returns>Información del Cliente encontrado</returns>
        public IHttpActionResult GetCustomer(int id)
        {
            Customer customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return NotFound();

            return Ok(Mapper.Map<Customer, CustomerDto>(customer));
        }
        /// <summary>
        /// Crea un nuevo Customer
        /// POST /api/customer
        /// </summary>
        /// <param name="customerDto">Información de customer a ser ingresada en la BDD</param>
        /// <returns>Objeto Customer almacenado en la Bdd</returns>
        [HttpPost]
        public IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            Customer customer = Mapper.Map<CustomerDto, Customer>(customerDto);
            _context.Customers.Add(customer);
            _context.SaveChanges();

            customerDto.Id = customer.Id;

            return Created(new Uri(Request.RequestUri+"/"+customer.Id),customerDto);
        }

        /// <summary>
        /// Permite actualizar la información del cliente
        /// PUT /api/customers/1
        /// </summary>
        /// <param name="id">id del Customer que se va actualizar</param>
        /// <param name="customerDto">Información del cliente que se va a actualizar</param>
        [HttpPut]
        public void UpdateCustomer(int id,CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            Customer customerInDB = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customerInDB == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Mapper.Map(customerDto, customerInDB);
                        
            _context.SaveChanges();
        }

        /// <summary>
        /// Elimina un Cliente de la Bdd
        /// DELETE /api/customers/1
        /// </summary>
        /// <param name="id">Id del cliente a eliminar</param>
        [HttpDelete]
        public void DeleteCustomer(int id)
        {
            Customer customerInDB = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customerInDB == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Customers.Remove(customerInDB);
            _context.SaveChanges();
        }
    }
}
