﻿using FinalP.Web.Models;
using FinalP.Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace FinalP.Web.Controllers
{
    public class EtudiantController : Controller
    {
        public readonly IClasseService _classeService;
        public EtudiantController(IClasseService classeService)
        {
            _classeService = classeService;
        }

        public async Task<IActionResult> EtudiantIndex()
        {
            List<EtudiantDto> list = new List<EtudiantDto>();
            var response = await _classeService.GetAllEtudiantsAsync<ResponseDto>();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<EtudiantDto>>(Convert.ToString(response.Result));
            }
            return View(list);
        }

        public async Task<IActionResult> EtudiantCreate()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EtudiantCreate(EtudiantDto model)
        {
            if (ModelState.IsValid)
            { 
                var response = await _classeService.CreateEtudiantAsync<ResponseDto>(model);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(EtudiantIndex));
                }
            }
            return View(model);
            
        }
    }
}
