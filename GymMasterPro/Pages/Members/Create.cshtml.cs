using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Entities;
using Microsoft.AspNetCore.Identity;
using Services.Interfaces;

namespace GymMasterPro.Pages.Members
{
    public class CreateModel : PageModel
    {
        private readonly IMemberService _memberService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITrainerService _trainerService;

        public CreateModel(IMemberService memberService, 
                           UserManager<IdentityUser> userManager,
                           ITrainerService trainerService)
        {
            _memberService = memberService;
            _userManager = userManager;
            _trainerService = trainerService;
        }

        public async Task<IActionResult> OnGet()
        {
            ViewData["TrainerId"] = new SelectList(await _trainerService.GetTrainers(), "Id", "FirstName");
            return Page();
        }

        [BindProperty]
        public Member Member { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || Member == null)
            {
                return Page();
            }

            var loggedInUser = await _userManager.GetUserAsync(User);
            if (loggedInUser == null)
            {
                return Page();
            }
            Member.UpdateAt = DateTime.Now;
            Member.CreateAt = DateTime.Now;
            Member.CreatedBy = loggedInUser?.UserName;

            await _memberService.SaveAsync(Member);

            return RedirectToPage("./Index");
        }
    }
}
