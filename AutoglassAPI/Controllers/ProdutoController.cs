using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoglassAPI.Models;
using AutoglassAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<ActionResult<ProdutoController>> Post(ProdutoDTO modelDTO)
        {
            var model = modelDTO.GetProdutoModel();
            try
            {

                await _produtoService.CreateAsync(model);
            }
            catch (DbUpdateException ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return CreatedAtAction(nameof(Get), new { id = model.Id }, ProdutoDTO.GetProdutoToProdutoDTO(model));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ProdutoDTO modelDTO)
        {
            if (id != modelDTO.Id)
            {
                return BadRequest();
            }

            var modelToUpdate = await _produtoService.GetAsync(id);
            if (modelToUpdate == null)
            {
                return NotFound();
            }

            var model = modelDTO.GetProdutoModel();

            try
            {
                await _produtoService.UpdateAsync(model);
            }
            catch (System.Exception ex)
            {
                
                return BadRequest(ex.Message);
            }

            return NoContent();

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _produtoService.GetAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            await _produtoService.DeleteAsync(id);

            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProdutoDTO>> Get(int id)
        {
            var model = await _produtoService.GetAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            return model;
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedList<ProdutoDTO>>> GetAll(
            [FromQuery] string? descricao, int pageNumber = 1, int pageSize = 10)
        {
            var produtos = await _produtoService.GetAllAsync(descricao, pageNumber, pageSize);

            var TotalPages = produtos.TotalPages;

            return produtos;
        }

    }
}