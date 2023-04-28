using Entities;
using Microsoft.EntityFrameworkCore;
using Repository;
using Services.Interfaces;

namespace Services
{
    public class MemberService : IMemberService
    {
        private readonly IRepository<Member> _repository;

        public MemberService(IRepository<Member> repository)
        {
            _repository = repository;
        }

        public async Task<List<Member>> GetMembers()
        {
            return await _repository.GetAll().Include(m => m.Trainer).ToListAsync();
        }

        public async Task<Member?> GetMemberById(int id)
        {
            return await _repository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Member> SaveAsync(Member member)
        {
            member.UpdateAt = DateTime.Now;
            member.CreateAt = DateTime.Now;

            return await _repository.CreateAsync(member);
        }

        public async Task<bool?> UpdateAsync(int id, Member member)
        {
            var existingMember = await _repository.GetSingleAsync(id);
            if (existingMember is null)
            {
                return null;
            }
            existingMember.UpdateAt = DateTime.Now;
            existingMember.Notes = member.Notes;
            existingMember.Notes = member.Notes;
            existingMember.Sex = member.Sex;
            existingMember.Address = member.Address;
            existingMember.TrainerId = member.TrainerId;


            return await _repository.UpdateAsync(existingMember);
        }

        public async Task<bool?> DeleteAsync(int id)
        {
            var existingMember = await _repository.GetSingleAsync(id);
            if (existingMember is null)
            {
                return null;
            }

            return await _repository.DeleteAsync(existingMember);
        }
    }
}