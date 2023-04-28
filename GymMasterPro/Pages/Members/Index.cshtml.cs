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
    public class IndexModel : PageModel
    {
        private readonly IMemberService _memberService;

        public IndexModel(IMemberService memberService)
        {
            _memberService = memberService;
        }

        public IList<Member> Member { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Member = await _memberService.GetMembers();
        }
    }
}
