using Entities;
using GymMasterPro.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Converters;

namespace GymMasterPro.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ApplicationDbContext _context;

        public IndexModel(ILogger<IndexModel> logger,
            ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        
        public int TotalMember { get; set; } = default!;
        public int TotalTrainers { get; set; } = default!;
        public int ActiveMembers { get; set; } = default!;
        public int InActiveMembers { get; set; } = default!;
        public int ExpiredMembersShip { get; set; } = default!;

        public async Task OnGet()
        {
            if (_context.Members != null)
            {
                TotalMember = await _context.Members.CountAsync();
            }

            if (_context.Trainers != null)
            {
                TotalTrainers = await _context.Trainers.CountAsync();
            }

            if (_context.Members != null)
            {
                ActiveMembers = await _context.Members.Include(m => m.Memberships)
                                                      .CountAsync(m => m.Memberships!.Any(ms => ms.EndDate > DateTime.Now));  
            }

            if (_context.Members != null)
            {
                ExpiredMembersShip = await _context.Members.Include(m => m.Memberships)
                                                      .CountAsync(m => m.Memberships!.Any(ms => ms.EndDate < DateTime.Now));
            }

            if (_context.Members != null)
            {
                InActiveMembers = await _context.Members.CountAsync(m => m.CreateAt < DateTime.Now.AddMonths(-1));
            }
        }
    }
}