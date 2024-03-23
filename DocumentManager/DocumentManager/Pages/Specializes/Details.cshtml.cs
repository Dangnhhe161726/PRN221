﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DocumentManager.Models;
using DocumentManager.Services;

namespace DocumentManager.Pages.Specializes
{
    public class DetailsModel : PageModel
    {
        private readonly DocumentManager.Services.ApplicationDbcontext _context;

        public DetailsModel(DocumentManager.Services.ApplicationDbcontext context)
        {
            _context = context;
        }

      public Specialize Specialize { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Specializes == null)
            {
                return NotFound();
            }

            var specialize = await _context.Specializes.FirstOrDefaultAsync(m => m.Id == id);
            if (specialize == null)
            {
                return NotFound();
            }
            else 
            {
                Specialize = specialize;
            }
            return Page();
        }
    }
}