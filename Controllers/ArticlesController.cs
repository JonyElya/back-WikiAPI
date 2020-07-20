using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Back_Wiki;
using Microsoft.AspNetCore.Razor.Language;

namespace Back_Wiki.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly DbInteractor _context;

        public ArticlesController(DbInteractor context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Article>>> GetArticles( string title)
        {
            var result = await _context.Articles.Where(article => title == null || article.title.ToLower().Contains(title.Trim().ToLower())).ToListAsync();
            return result;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Article>> GetArticles(long id)
        {
            var articles = await _context.Articles.FindAsync(id);

            if (articles == null)
            {
                return NotFound();
            }

            return articles;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutArticles(long id, Article articles)
        {
            if (id != articles.Id)
            {
                return BadRequest();
            }

            _context.Entry(articles).State = EntityState.Modified;

            try
            {   
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticlesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
   
        [HttpPost]
        public async Task<ActionResult<Article>> PostArticles(Article articles)
        {
            var art = new Article
            {
               title = articles.title,
               snippet =  articles.snippet,
               pageId = articles.pageId
            };
            _context.Articles.Add(art);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetArticles", new { id = art.Id }, art);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Article>> DeleteArticles(long id)
        {
            var articles = await _context.Articles.FindAsync(id);
            if (articles == null)
            {
                return NotFound();
            }

            _context.Articles.Remove(articles);
            await _context.SaveChangesAsync();

            return articles;
        }

        private bool ArticlesExists(long id)
        {
            return _context.Articles.Any(e => e.Id == id);
        }
    }
}
