using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IuserInfo.Data;
using IuserInfo.Models;
using IuserInfo.Pages.Infos;
using IuserInfo.Commons;

namespace IuserInfo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InfoesController : ControllerBase
    {
        private readonly IuserInfoContext _context;

        public InfoesController(IuserInfoContext context)
        {
            _context = context;
        }

        // GET: api/Infoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Info>>> GetInfo()
        {
            return await _context.Info.ToListAsync();
        }

        // GET: api/Infoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Info>> GetInfo(int id)
        {
            var info = await _context.Info.FindAsync(id);

            if (info == null)
            {
                return NotFound();
            }

            return info;
        }

        // PUT: api/Infoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInfo(int id, Info info)
        {
            if (id != info.ID)
            {
                return BadRequest();
            }

            _context.Entry(info).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InfoExists(id))
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

        // POST: api/Infoes/signup
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("signup")]
        public async Task<ActionResult<Info>> SignUp(Info request)
        {
            List<string> errors = new List<string>();

            if (Helper.IsNotValidEmail(request.IuserAccount))
            {
                errors.Add("Not a valid Email");
                return BadRequest(errors);
            }
            var accounts = from a in _context.Info
                           where a.IuserAccount == request.IuserAccount
                           select a;

            accounts = accounts.Where(s => s.IuserAccount.Contains(request.IuserAccount));

            if (accounts.Any())
            {
                errors.Add("Email Is Registered");
                return BadRequest(errors);
            }
            else
            {
                _context.Info.Add(request);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetInfo), new { id = request.ID }, request);
            }
        }

        // POST: api/Infoes/signup
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("login")]
        public ActionResult<Info> Login(Info request)
        {
            List<string> errors = new List<string>();
            if (string.IsNullOrWhiteSpace(request.IuserAccount))
            {
                errors.Add("Email is required ");
            }

            if (string.IsNullOrWhiteSpace(request.IuserPassword))
            {
                errors.Add("Password is required ");
            }

            if(errors.Count > 0)
            {
                return BadRequest(errors);
            }
            else
            {
                var accounts = from a in _context.Info
                               where a.IuserAccount == request.IuserAccount
                               select a;
                var passwords = from p in _context.Info
                               where p.IuserPassword == request.IuserPassword
                               select p;
                accounts = accounts.Where(s => s.IuserAccount.Contains(request.IuserAccount));
                passwords = passwords.Where(p => p.IuserPassword.Contains(request.IuserPassword));
                if (accounts.Any() && passwords.Any())
                {
                    return Ok(accounts);
                }
                else
                {
                    errors.Add("Not Found Email Or Password Is Incorrect");
                    return BadRequest(errors);
                }
            }

        }

        // DELETE: api/Infoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInfo(int id)
        {
            var info = await _context.Info.FindAsync(id);
            if (info == null)
            {
                return NotFound();
            }

            _context.Info.Remove(info);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        private bool InfoExists(int id)
        {
            return _context.Info.Any(e => e.ID == id);
        }
    }
}
