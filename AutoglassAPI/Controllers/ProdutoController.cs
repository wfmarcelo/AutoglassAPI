using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoglassAPI.Models;
using AutoglassAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace AutoglassAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoController : ControllerBase
    {
        private ProdutoService _produtoService;

        public ProdutoController(ProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        [HttpPost]
        public async Task<ActionResult<ProdutoController>> Post(Produto model)
        {
            try
            {
                await _produtoService.CreateAsync(model);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return CreatedAtAction(nameof(Get), new { id = model.Id}, model);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Produto>> Get(int id)
        {
            var model = await _produtoService.GetAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            return model;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> GetAll([FromQuery] int pageNumber = 1, int pageSize = 10)
        {
            var produtos = await _produtoService.GetAllAsync(pageNumber, pageSize);

            return Ok(produtos);
        }

    }
}