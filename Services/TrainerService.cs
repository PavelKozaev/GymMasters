using Entities;
using Microsoft.EntityFrameworkCore;
using Repository;
using Services.Interfaces;

namespace Services
{
    public class TrainerService : ITrainerService
    {
        private readonly IRepository<Trainer> _repository;

        public TrainerService(IRepository<Trainer> repository)
        {
            _repository = repository;
        }

        public async Task<List<Trainer>> GetTrainers()
        {
            return await _repository.GetAll().ToListAsync();
        }

        public async Task<Trainer> SaveAsync(Trainer trainer)
        {
            trainer.UpdateAt = DateTime.Now;
            trainer.CreateAt = DateTime.Now;

            return await _repository.CreateAsync(trainer);
        }

        public async Task<bool?> UpdateAsync(int id, Trainer trainer)
        {
            var existingTrainer = await _repository.GetSingleAsync(id);
            if (existingTrainer is null)
            {
                return null;
            }
            existingTrainer.UpdateAt = DateTime.Now;
            existingTrainer.Notes = trainer.Notes;
            existingTrainer.Sex = trainer.Sex;
            existingTrainer.Address = trainer.Address;
            existingTrainer.DateOfBirth = trainer.DateOfBirth;
            existingTrainer.CreatedBy = trainer.CreatedBy;
            existingTrainer.FirstName = trainer.FirstName;
            existingTrainer.LastName = trainer.LastName;
            existingTrainer.Email = trainer.Email;
            existingTrainer.Notes = trainer.Notes;
            existingTrainer.Salary = trainer.Salary;

            return await _repository.UpdateAsync(existingTrainer);
        }

        public async Task<bool?> DeleteAsync(int id)
        {
            var existingTrainer = await _repository.GetSingleAsync(id);
            if (existingTrainer is null)
            {
                return null;
            }

            return await _repository.DeleteAsync(existingTrainer);
        }
    }
}