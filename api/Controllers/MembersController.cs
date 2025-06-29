﻿using api.Data;
using api.Entities;
using api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Authorize]
    public class MembersController(AppDbContext dataContext, IMemberRepository memberRepository) : BaseApiController
    {
        [HttpGet]
        public async Task <ActionResult<IReadOnlyList<Member>>> GetMembers()
        {
            return Ok(await memberRepository.GetMembersAsync());
        }
        [HttpPost]
        public async Task<ActionResult<AppUser>> CreateMember(AppUser member)
        {
            dataContext.Users.Add(member);
            await dataContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetMembers), new { id = member.Id }, member);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Member>> GetMemberById(string id)
        {
            var member = await memberRepository.GetMemberByIdAsync(id);
            if (member == null)
            {
                return NotFound();
            }
            return member;
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMember(string id, AppUser member)
        {
            if (id != member.Id)
            {
                return BadRequest();
            }
            dataContext.Entry(member).State = EntityState.Modified;
            try
            {
                await dataContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MemberExists(id))
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
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMember(string id)
        {
            var member = await dataContext.Users.FindAsync(id);
            if (member == null)
            {
                return NotFound();
            }
            dataContext.Users.Remove(member);
            await dataContext.SaveChangesAsync();
            return NoContent();
        }

        private bool MemberExists(string id)
        {
            throw new NotImplementedException();
        }

        [HttpGet("{id}/photos")]
        public async Task<ActionResult<IReadOnlyList<Photo>>> GetMemberPhotos(string id)
        {
            return Ok(await memberRepository.GetPhotosForMemberAsync(id));
        }
    }
}
