using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication5.Entities;
using WebApplication5.Migrations;
using WebApplication5.Models.Users;
using WebApplication5.Services;

namespace WebApplication5.Controllers
{
    [Route("users/{id}/contacts/{username}")]
    [ApiController]
    public class ConversationsController : ControllerBase
    {
        private IConversationService _service;
        private IMapper _mapper;
        public ConversationsController(IConversationService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult getConversation(int id, string username)
        {
            _service.getMessages(id, username);
            var messages = _service.getMessages(id, username);
            var model = _mapper.Map<IList<MessageModel>>(messages);

            return Ok(model);
        }
    }
}



