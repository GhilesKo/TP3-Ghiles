using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VoyageAPI.Data;
using VoyageAPI.Models;
using VoyageAPI.Models.DTOs.Request;
using VoyageAPI.Models.DTOs.Response;

namespace VoyageAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class VoyagesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        public VoyagesController(ApplicationDbContext context, IMapper mapper, UserManager<User> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }

        // GET: api/Voyages
        [AllowAnonymous]
        [HttpGet("Public")]
        public async Task<ActionResult<IEnumerable<VoyageResponse>>> GetPublicVoyages()
        {

            var voyageDomain = await _context.Voyages.Include("Users").Where(x => x.Public == true).ToListAsync();
            var voyagesMapped = _mapper.Map<List<VoyageResponse>>(voyageDomain);
            return Ok(voyagesMapped);
        }

        [HttpGet("Custom")]
        public async Task<ActionResult<IEnumerable<Voyage>>> GetVoyages()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = await _userManager.FindByIdAsync(userId);
            var voyages = await _context.Voyages.Where(x => x.Users.Any(u => u.Id == user.Id)).ToListAsync();
            return Ok(voyages);

        }


        // GET: api/Voyages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Voyage>> GetVoyage(int id)
        {
            var voyage = await _context.Voyages.FindAsync(id);

            if (voyage == null)
            {
                return NotFound();
            }

            return voyage;
        }

        // PUT: api/Voyages/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVoyage(int id, Voyage voyage)
        {
            if (id != voyage.Id)
            {
                return BadRequest();
            }

            _context.Entry(voyage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VoyageExists(id))
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

        // POST: api/Voyages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        [HttpPost]
        public async Task<ActionResult<Voyage>> PostVoyage([FromBody] VoyageCreateRequest voyageRequest)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = await _userManager.FindByIdAsync(userId);

            var voyage = _mapper.Map<Voyage>(voyageRequest);

            voyage.Users.Add(user);
            voyage.UsersCount = voyage.Users.Count;


            await _context.Voyages.AddAsync(voyage);
            await _context.SaveChangesAsync();

            var voyageResponse = _mapper.Map<VoyageResponse>(voyage);

            return CreatedAtAction("GetVoyage", new { id = voyage.Id }, voyageResponse);

        }

        [HttpPost("InviteUser")]
        public async Task<ActionResult> InviteUser([FromBody] VoyageInviteUserRequest voyageInviteUserRequest)
        {

            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = await _userManager.FindByIdAsync(userId);

            var voyage = await _context.Voyages.Include("Users").SingleOrDefaultAsync(v => v.Id == voyageInviteUserRequest.VoyageId
            && v.Users.Any(u => u.Id == user.Id));

            if (voyage is null)
            {
                return NotFound($"Voyage '{voyageInviteUserRequest.VoyageId}' not found");
            }

            var invitedUser = await _userManager.FindByEmailAsync(voyageInviteUserRequest.UserEmail);
            if (invitedUser is null)
            {
                return NotFound($"User '{voyageInviteUserRequest.UserEmail}' not found");

            }
            if (voyage.Users.Contains(invitedUser))
            {
                return BadRequest($"User '{voyageInviteUserRequest.UserEmail}' is already part of the voyage");
            }
            voyage.Users.Add(invitedUser);
            voyage.UsersCount = voyage.Users.Count;
            await _context.SaveChangesAsync();
            return Ok();
        }
        // DELETE: api/Voyages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVoyage(int id)
        {
            var voyage = await _context.Voyages.FindAsync(id);
            if (voyage == null)
            {
                return NotFound();
            }

            _context.Voyages.Remove(voyage);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VoyageExists(int id)
        {
            return _context.Voyages.Any(e => e.Id == id);
        }
    }
}
