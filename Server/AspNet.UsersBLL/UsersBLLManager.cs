using AspNet.DTO;
using AspNet.DTO.Model;
using AspNet.DTO.VM;
using AspNetdbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspNet.UsersBLL
{
    public class UsersBLLManager : IUserBLLManager
    {
        private readonly IUnitOfWork _unitOfWork;

        public UsersBLLManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
        //method fro add user
        public Users AddUsers(Users users)
        {
            DTO.Users user = GetUnitOfWork().Add(users);
            _unitOfWork.Save();
            return user;
        }
        /// <summary>
        /// methos for get all user
        /// </summary>
        /// <returns></returns>
        public List<Users> GetAll()
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
        /// <summary>
        /// method for pull post details data.
        /// </summary>
        /// <param name="postFilter"></param>
        /// <returns></returns>
        public PaginationResponse GetAllPostDetail(PostFilterPagination postFilter)
        {
            PaginationResponse paginationResponse = new PaginationResponse();
            try
            {             
                var res = GetUnitOfWorkPost().GetAll().Select(p => new Post()
                {
                    Comments = p.Comments,
                    NumberOfComments = p.Comments.Count,
                    CreatedBy = p.CreatedBy,
                    CreatedTime = p.CreatedTime,
                    PostContent = p.PostContent,
                    PostID = p.PostID,

                }).ToList();

                foreach (var item in res)
                {
                    foreach (Comments comment in item.Comments)
                    {
                        Comments comments = GetUnitOfWorkComments().Find(p => comment.CommentsID == p.CommentsID).Select(c => new Comments()
                        {
                            Vote = c.Vote,
                        }).FirstOrDefault();

                        comment.NumberOfLike = comments.Vote.Count(p => p.CommentsID == comment.CommentsID && p.LikeORDislike == true);
                        comment.NumberOfLike = comments.Vote.Count(p => p.CommentsID == comment.CommentsID && p.LikeORDislike == false);
                    }
                }

                postFilter.Pagination.totalItems = res.Count;
                paginationResponse.Data = res.Skip((postFilter.Pagination.currentPage - 1) * postFilter.Pagination.itemsPerPage)
                            .Take(postFilter.Pagination.itemsPerPage).GroupBy(p => p.PostID).ToList();
                paginationResponse.Pagination = postFilter.Pagination;
                return paginationResponse;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
       

        public Users UpdateUsers(Users users)
        {
            try
            {
                DTO.Users user = GetUnitOfWork().Edit(users);
                _unitOfWork.Save();
                return user;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }
        private IGenericRepository<DTO.Users> GetUnitOfWork()
        {
            return _unitOfWork.Repository<DTO.Users>();
        }
        private IGenericRepository<Post> GetUnitOfWorkPost()
        {
            return _unitOfWork.Repository<Post>();
        }
        private IGenericRepository<Comments> GetUnitOfWorkComments()
        {
            return _unitOfWork.Repository<Comments>();
        }
    }

    public interface IUserBLLManager
    {
        Users AddUsers(Users users);
        Users UpdateUsers(Users users);
        List<Users> GetAll();
        PaginationResponse GetAllPostDetail(PostFilterPagination postFilter);

    }
}
