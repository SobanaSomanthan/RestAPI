using AutoMapper;
using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.Dto;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_VillaAPI.Controllers
{
    [Route("api/VillaAPI")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        public VillaAPIController(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<VillaDto>>> GetVillas()
        {
            IEnumerable<Villa> villaList = await _db.Villas.ToListAsync();
            return Ok(_mapper.Map<List<VillaDto>>(villaList));   //Mapping<Destination>(Object)
        }
        [HttpGet("{id:int}",Name ="GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(200, Type =typeof(VillaDto))]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(404)]
        public async Task <ActionResult<VillaDto>> GetVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var villa = await _db.Villas.FirstOrDefaultAsync(u => u.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<VillaDto>(villa));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<VillaDto>> CreateVilla([FromBody] VillaCreateDto CreateDto)
        {
            if (await _db.Villas.FirstOrDefaultAsync(u=>u.Name.ToLower()==CreateDto.Name.ToLower())!=null)
            {
                ModelState.AddModelError("CustomError", "Villa already exists!");
                return BadRequest(ModelState);
            }
            if(CreateDto == null)
            {
                return BadRequest(CreateDto);
            }
            
            Villa model = _mapper.Map<Villa>(CreateDto);

            
            await _db.Villas.AddAsync(model);
            await _db.SaveChangesAsync();
            
            return CreatedAtRoute("GetVilla", new {id=model.Id }, model);
        }
        [HttpDelete("{id:int}", Name = "DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteVilla(int id)
        {
            if(id==0)
            {
                return BadRequest();
            }
            var villa= await _db.Villas.FirstOrDefaultAsync(u => u.Id == id);
            if (villa==null)
            {
                return NotFound();
            }
            _db.Villas.Remove(villa);
            await _db.SaveChangesAsync();
            return NoContent();
        }
        [HttpPut("{id:int}", Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateVilla(int id, [FromBody] VillaUpdateDto UpdateDto)
        {
            if(UpdateDto == null || id!=UpdateDto.Id)
            {
                return BadRequest();    
            }
            
            Villa model = _mapper.Map<Villa>(UpdateDto);

            
            _db.Villas.Update(model);
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("{id:int}", Name = "PatchVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PatchVilla(int id, JsonPatchDocument<VillaUpdateDto> PatchDTO)
        {
            if (PatchDTO == null || id == 0)
            {
                return BadRequest();
            }
            var ST = await _db.Villas.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);

            
            VillaUpdateDto villaDto = _mapper.Map<VillaUpdateDto>(ST);

           
            if (ST == null)
            {
                return BadRequest();
            }
            PatchDTO.ApplyTo(villaDto, ModelState);

            Villa model = _mapper.Map<Villa>(villaDto);

            
            _db.Villas.Update(model);
            await _db.SaveChangesAsync();

            if(!ModelState.IsValid) 
            {
                return BadRequest(ModelState);    
            }
            return NoContent();
        }
    }
}
