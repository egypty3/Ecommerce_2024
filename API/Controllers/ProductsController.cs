using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Infrastructure.Data;
using Core.Interfaces;
using Infrastructure.Data.Specifications;
using Core.Specifications;
using API.DTOs;
using AutoMapper;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
  
        private readonly IMapper _mapper;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<ProductType> _productTypeRepository;
        private readonly IRepository<ProductBrand> _productBrandRepository;
  
        public ProductsController(
            IRepository<Product> productRepository,
            IRepository<ProductType> productTypeRepository,
            IRepository<ProductBrand> productBrandRepository,
            IMapper mapper)
        {
            _productRepository = productRepository;
            _productTypeRepository = productTypeRepository;
            _productBrandRepository = productBrandRepository;
            _mapper = mapper;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts()
        {
            var spec = new ProductsWithTypesAndBrandsSpecification("pricedesc");   

            var products = await _productRepository.ListAsync(spec);


            // without automapper

            //var productsDTO = products.Select(product => new ProductDTO
            //{
            //    Id = product.Id,
            //    Name = product.Name,
            //    Price = product.Price,
            //    PictureUrl = product.PictureUrl,
            //    ProductBrand = product.ProductBrand.Name,
            //    ProductType = product.ProductType.Name
            //}
            //);

            var productDTOs = _mapper.Map<List<ProductDTO>>(products);

            return Ok(productDTOs);
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProduct(int id)
        {
           var spec = new ProductsWithTypesAndBrandsSpecification(id);
            var product = await _productRepository.GetByIdAsync(spec);


            // without automapper

            //var productDTO = new ProductDTO
            //{
            //    Id = product.Id,
            //    Name = product.Name,               
            //    Price = product.Price,
            //    PictureUrl = product.PictureUrl,
            //    ProductBrand = product.ProductBrand.Name,
            //    ProductType = product.ProductType.Name
            //};
            var productDTO = _mapper.Map<Product, ProductDTO>(product);
            return Ok(productDTO);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<ProductBrand>>> GetProductBrands()
        {
            var spec = new BaseSpecification<ProductBrand>();
            return
                Ok(await _productBrandRepository.ListAsync( spec));
        }

        [HttpGet("types")]
        public async Task<ActionResult<IEnumerable<ProductType>>> GetProductTypes()
        {
            var spec = new BaseSpecification<ProductType>();
            return
                Ok(await _productTypeRepository.ListAsync(spec));
        }
        //    // PUT: api/Products/5
        //    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //    [HttpPut("{id}")]
        //    public async Task<IActionResult> PutProduct(int id, Product product)
        //    {
        //        if (id != product.Id)
        //        {
        //            return BadRequest();
        //        }

        //        _context.Entry(product).State = EntityState.Modified;

        //        try
        //        {
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!ProductExists(id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }

        //        return NoContent();
        //    }

        //    // POST: api/Products
        //    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //    [HttpPost]
        //    public async Task<ActionResult<Product>> PostProduct(Product product)
        //    {
        //        _context.Products.Add(product);
        //        await _context.SaveChangesAsync();

        //        return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        //    }

        //    // DELETE: api/Products/5
        //    [HttpDelete("{id}")]
        //    public async Task<IActionResult> DeleteProduct(int id)
        //    {
        //        var product = await _context.Products.FindAsync(id);
        //        if (product == null)
        //        {
        //            return NotFound();
        //        }

        //        _context.Products.Remove(product);
        //        await _context.SaveChangesAsync();

        //        return NoContent();
        //    }

        //    private bool ProductExists(int id)
        //    {
        //        return _context.Products.Any(e => e.Id == id);
        //    }
    }
}
