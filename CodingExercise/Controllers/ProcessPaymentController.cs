using System;
using System.Collections.Generic;
using AutoMapper;
using CodingExercise.AppLayer;
using CodingExercise.Domain;
using CodingExercise.RestModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CodingExercise.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProcessPaymentController : ControllerBase
    {
        private readonly ILogger<ProcessPaymentController> _logger;
        private IMapper _mapper;
        private IPmtService _pmtServ;

        public ProcessPaymentController(
            ILogger<ProcessPaymentController> logger,
            IMapper mapper,
            IPmtService pmtServ
            )
        {
            _logger = logger;
            _mapper = mapper;
            _pmtServ = pmtServ;
        }

        [HttpPost]
        public IActionResult Post([FromBody]ProcessPaymentModel pmtModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var p = _mapper.Map<ProcessPayment>(pmtModel);
                _pmtServ.Create(p);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
                return StatusCode(500);
            }
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            yield return "Merge !";
        }
    }
}
