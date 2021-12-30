using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RESTful.Data;
using RESTful.Models;
using RESTful.Commons;

namespace RESTful.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserInfoesController : ControllerBase
    {
        private readonly IuserInfoContext _context;

        public UserInfoesController(IuserInfoContext context)
        {
            _context = context;
        }

        // GET: api/UserInfo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserInfo>>> GetInfo()
        {
            return await _context.UserInfo.ToListAsync();
        }

        // GET: api/UserInfo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserInfo>> GetInfo(int id)
        {
            var info = await _context.UserInfo.FindAsync(id);

            if (info == null)
            {
                return NotFound();
            }

            return info;
        }

        // PUT: api/UserInfo/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInfo(int id, UserInfo info)
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

        // POST: api/UserInfo/signup
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("signup")]
        public async Task<ActionResult<UserInfo>> SignUp(UserInfo request)
        {
            List<string> errors = new List<string>();

            if (Helper.IsNotValidEmail(request.Email))
            {
                errors.Add("Not a valid Email");
                return BadRequest(errors);
            }

            if (string.IsNullOrWhiteSpace(request.Password)){
                errors.Add("Please Enter Password");
                return BadRequest(errors);
            }
            var accounts = from a in _context.UserInfo
                           select a;

            accounts = accounts.Where(s => s.Email.Contains(request.Email));

            if (accounts.Any())
            {
                errors.Add("Email Is Registered");
                return BadRequest(errors);
            }
            else
            {
                _context.UserInfo.Add(request);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetInfo), new { id = request.ID }, request);
            }
        }

        // POST: api/UserInfo/signup
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("login")]
        public ActionResult<UserInfo> Login(UserInfo request)
        {
            List<string> errors = new List<string>();
            if (string.IsNullOrWhiteSpace(request.Email))
            {
                errors.Add("Please Enter Email");
                return BadRequest(errors);
            }

            if (string.IsNullOrWhiteSpace(request.Password))
            {
                errors.Add("Please Enter Password");
                return BadRequest(errors);
            }

            var accounts = from a in _context.UserInfo
                            select a;
            var passwords = from p in _context.UserInfo
                            where p.Email== request.Email
                            select p;
            accounts = accounts.Where(s => s.Email.Contains(request.Email));
            passwords = passwords.Where(p => p.Password.Contains(request.Password));
            if (accounts.Any() && passwords.Any())
            {
                return Ok("Login Successfully");
            }
            else if(accounts.Any() && !passwords.Any())
            {
                errors.Add("Password Is Incorrect");
                return BadRequest(errors);
            }
            else
            {
                errors.Add("Not Found Email");
                return BadRequest(errors);
            }
        }

        // DELETE: api/UserInfo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInfo(int id)
        {
            var info = await _context.UserInfo.FindAsync(id);
            if (info == null)
            {
                return NotFound();
            }

            _context.UserInfo.Remove(info);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        private bool InfoExists(int id)
        {
            return _context.UserInfo.Any(e => e.ID == id);
        }
    }
}
