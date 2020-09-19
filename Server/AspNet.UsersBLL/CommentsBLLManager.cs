using AspNet.DTO.Model;
using AspNetdbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspNet.UsersBLL
{
    public class CommentsBLLManager : ICommentsBLLManager
    {

        private readonly IUnitOfWork _unitOfWork;

        public CommentsBLLManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
        public Comments AddComments(Comments entity)
        {
            try
            {
                Comments comments = GetUnitOfWork().Add(entity);
                _unitOfWork.Save();
                return comments;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int DeleteComments(Comments comments)
        {
            int result = 0;
            try
            {
                GetUnitOfWork().Remove(comments);
                _unitOfWork.Save();
                return result = 1;
            }
            catch (Exception)
            {

                return result = 0;
            }
        }

        public List<Comments> GetAll()
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

        public Comments UpdateComments(Comments entity)
        {
            try
            {
                Comments comments = GetUnitOfWork().Edit(entity);
                _unitOfWork.Save();
                return comments;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        private IGenericRepository<Comments> GetUnitOfWork()
        {
            return _unitOfWork.Repository<Comments>();
        }
    }
    public interface ICommentsBLLManager
    {
        Comments AddComments(Comments comments);
        Comments UpdateComments(Comments comments);
        List<Comments> GetAll();
        int DeleteComments(Comments comments);
     
    }
}
