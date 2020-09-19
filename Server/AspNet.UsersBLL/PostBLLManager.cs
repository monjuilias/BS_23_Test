using AspNet.DTO.Model;
using AspNetdbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspNet.UsersBLL
{
    public class PostBLLManager : IPostBLLManager
    {

        private readonly IUnitOfWork _unitOfWork;

        public PostBLLManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
        public Post AddPost(Post entity)
        {
            try
            {
                Post post = GetUnitOfWork().Add(entity);
                _unitOfWork.Save();
                return post;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }

        public int DeletePost(Post entity)
        {
            int result = 0;
            try
            {
                GetUnitOfWork().Remove(entity);
                _unitOfWork.Save();
                return result = 1;
            }
            catch (Exception)
            {

                return result = 0;
            }

        }

        public List<Post> GetAllPost()
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

        public Post UpdatePost(Post entity)
        {
            try
            {
                Post post = GetUnitOfWork().Edit(entity);
                _unitOfWork.Save();
                return post;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        private IGenericRepository<Post> GetUnitOfWork()
        {
            return _unitOfWork.Repository<Post>();
        }

    }
    public interface IPostBLLManager
    {
        Post AddPost(Post post);
        Post UpdatePost(Post post);
        List<Post> GetAllPost();
        int DeletePost(Post post);

    }
}
