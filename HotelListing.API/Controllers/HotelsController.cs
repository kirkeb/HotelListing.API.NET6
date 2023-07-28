using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelListing.API.Data;
using HotelListing.API.Contracts;
using AutoMapper;
using HotelListing.API.Repository;
using HotelListing.API.Models.Country;
using HotelListing.API.Models.Hotel;
using System.Diagnostics.Metrics;

namespace HotelListing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        //private readonly HotelListingDbContext _context;
        private readonly IHotelsRepository _hotelsRepository;
        private readonly IMapper _mapper;


        public HotelsController(IHotelsRepository hotelsRepository, IMapper mapper)
        {
            
            this._hotelsRepository = hotelsRepository;
            this._mapper = mapper;
        }

        // GET: api/Hotels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HotelDto>>> GetHotels()
        {
            //return await _hotelsRepository.Hotels.ToListAsync();
            var hotels = await _hotelsRepository.GetAllAsync();
            var records = _mapper.Map<List<GetHotelDto>>(hotels);
            return Ok(records);
        }

        // GET: api/Hotels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HotelDto>> GetHotel(int id)
        {
            var hotel = await _hotelsRepository.GetAsync(id);

            if (hotel == null)
            {
                return NotFound();
            }

            //var hotelDto = _mapper.Map<HotelDto>(hotel);

            return Ok(_mapper.Map<HotelDto>(hotel));

            
        }

        // PUT: api/Hotels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHotel(int id, HotelDto HotelDto)
        {
            if (id != HotelDto.Id)
            {
                return BadRequest();
            }

            //_hotelsRepository.Entry(hotel).State = EntityState.Modified;

            var hotel = await _hotelsRepository.GetAsync(id);

            if (hotel == null)
            {
                return NotFound();
            }

            _mapper.Map(HotelDto, hotel);


            try
            {
                await _hotelsRepository.UpdateAsync(hotel);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await HotelExists(id))
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

        // POST: api/Hotels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Hotel>> PostHotel(CreateHotelDto createHotelDto)
        {
            //The Below code is replaced with AutoMapper
            //_hotelsRepository.Hotels.Add(hotel);
            //await _hotelsRepository.SaveChangesAsync();

            //return CreatedAtAction("GetHotel", new { id = hotel.Id }, hotel);

            var hotel = _mapper.Map<Hotel>(createHotelDto);

            await _hotelsRepository.AddAsync(hotel);
            return CreatedAtAction("GetHotel", new { id = hotel.Id }, hotel);


        }

        // DELETE: api/Hotels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            var hotel = await _hotelsRepository.GetAsync(id);
            if (hotel == null)
            {
                return NotFound();
            }

            await _hotelsRepository.DeleteAsync(id);

           // _hotelsRepository.Hotels.Remove(hotel);
            //await _hotelsRepository.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> HotelExists(int id)
        {
            return await _hotelsRepository.Exists(id);
        }
    }
}
