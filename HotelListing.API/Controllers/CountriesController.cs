using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelListing.API.Data;
using HotelListing.API.Models.Country;
using AutoMapper;
using HotelListing.API.Contracts;

namespace HotelListing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly ICountriesRepository _countriesRepository;
        private readonly IMapper _mapper;

        public CountriesController(ICountriesRepository countriesRepository, IMapper mapper)
        {
            this._countriesRepository = countriesRepository;
            this._mapper = mapper;
        }

        // GET: api/Countries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetCountryDto>>> GetCountries()
        {
            var countries = await _countriesRepository.GetAllAsync();
            var records = _mapper.Map<List<GetCountryDto>>(countries);
            return Ok(records);
        }

        // GET: api/Countries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CountryDto>> GetCountry(int id)
        {
            //var country = await _countriesRepository.Countries.Include(q => q.Hotels)
            var country = await _countriesRepository.GetDetails(id);
                //.FirstOrDefaultAsync(q => q.id==id);

            if (country == null)    
            {
                return NotFound();
            }

            var countryDto = _mapper.Map<CountryDto>(country);

            return Ok(countryDto);
        }

        // PUT: api/Countries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCountry(int id, UpdateCountryDto updateCountryDto)
        {
            if (id != updateCountryDto.Id)
            {
                return BadRequest();
            }

            // _context.Entry(updateCountryDto).State = EntityState.Modified;
            //var country = await _countriesRepository.Countries.FindAsync(id);
            var country = await _countriesRepository.GetAsync(id);

            if (country == null)
            {
                return NotFound();
            }

            _mapper.Map(updateCountryDto, country);

            try
            {
                await _countriesRepository.UpdateAsync(country);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CountryExists(id))
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

        // POST: api/Countries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Country>> PostCountry(CreateCountryDto createCountryDto)
        {

            //The below code is replaces with AutoMapper Above KB
            //var country = new Country()
            //{
            //    Name = createCountry.Name,
            //    ShortName = createCountry.ShortName
            //};

            //AutoMapper
            var country = _mapper.Map<Country>(createCountryDto);

            //_countriesRepository.Countries.Add(country);
            await _countriesRepository.AddAsync(country);
            //await _countriesRepository.SaveChangesAsync();

            return CreatedAtAction("GetCountry", new { id = country.id }, country);
        }

        // DELETE: api/Countries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            //var country = await _countriesRepository.Countries.FindAsync(id);
            var country = await _countriesRepository.GetAsync(id);
            if (country == null)
            {
                return NotFound();
            }

            //_countriesRepository.Countries.Remove(country);
            await _countriesRepository.DeleteAsync(id);
            //await _countriesRepository.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> CountryExists(int id)
        {
            //return _countriesRepository.Countries.Any(e => e.id == id);
             return await _countriesRepository.Exists(id);
        }
    }
}
