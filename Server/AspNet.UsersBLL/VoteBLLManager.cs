using AspNet.DTO.Model;
using AspNetdbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspNet.UsersBLL
{
    public class VoteBLLManager : IVoteBLLManager
    {
        private readonly IUnitOfWork _unitOfWork;

        public VoteBLLManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
        public Vote AddVote(Vote entity)
        {
            try
            {
                Vote vote = GetUnitOfWork().Add(entity);
                _unitOfWork.Save();
                return vote;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public int DeleteVote(Vote vote)
        {
            int result = 0;
            try
            {
                GetUnitOfWork().Remove(vote);
                _unitOfWork.Save();
                return result = 1;
            }
            catch (Exception)
            {

                return result = 0;
            }

        }

        public List<Vote> GetAllVote()
        {
            try
            {
                return GetUnitOfWork().GetAll().ToList();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public Vote UpdateVote(Vote entity)
        {
            try
            {
                Vote vote = GetUnitOfWork().Edit(entity);
                _unitOfWork.Save();
                return vote;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        private IGenericRepository<Vote> GetUnitOfWork()
        {
            return _unitOfWork.Repository<Vote>();
        }
    }
    public interface IVoteBLLManager
    {
        Vote AddVote(Vote  vote);
        Vote UpdateVote(Vote vote);
        List<Vote> GetAllVote();
        int DeleteVote(Vote vote);

    }
}
