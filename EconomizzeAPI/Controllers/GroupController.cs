using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Economizze.Library;
using Microsoft.AspNetCore.Http.HttpResults;
using AutoMapper;
using EconomizzeAPI.Model;
using EconomizzeAPI.Services.Repositories.Interfaces;
using EconomizzeAPI.Services.Repositories.Classes;

namespace EconomizzeAPI.Controllers
{
    [ApiController]
    [Route("api/grupo")]
    public class GroupController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IGroupRepository _groupRepository;
        public GroupController(IGroupRepository groupRepository, IMapper mapper)
        {
            _groupRepository = groupRepository ?? throw new ArgumentNullException(nameof(groupRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpPost]
        public async Task<ActionResult<GroupViewModel>> CreateGroup(GroupViewModel group)
        {
            var map = _mapper.Map<Group>(group);
            var GroupViewModel = await _groupRepository.CreateGroupAsync(map);
            if (GroupViewModel.Item2.HasError)
            {
                return BadRequest();
            }

            return CreatedAtRoute("grupo", new { GroupId = GroupViewModel.Item1.GroupId }, _mapper.Map<GroupViewModel>(map));
        }

        [HttpGet("{groupId}", Name = "grupo")]
        public async Task<ActionResult<GroupViewModel>> GetById(short groupId)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GroupViewModel>>> ReadAllGroups()
        {
            var groups = await _groupRepository.ReadAllGroupsAsync();
            if (groups.Count() < 1)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<GroupViewModel>>(groups));
        }
    }
}
