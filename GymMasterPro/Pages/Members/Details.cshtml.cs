using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Entities;
using Services.Interfaces;

namespace GymMasterPro.Pages.Members
{
    public class DetailsModel : PageModel
    {
        private readonly IMemberService _memberService;

        public DetailsModel(IMemberService memberService)
        {
            _memberService = memberService;
        }

      public Member Member { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == 0 || await _memberService.GetMembers() == null)
            {
                return NotFound();
            }

            var member = await _memberService.GetMemberById(id);
            if (member == null)
            {
                return NotFound();
            }
            else 
            {
                Member = member;
            }
            return Page();
        }
    }
}
