using Iress_API_bookshelf.DTOs;
using Iress_API_bookshelf.EF.Models;
using Iress_API_bookshelf.Models;
using Iress_API_bookshelf.Services.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Iress_API_bookshelf.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBusinessLogicService _BusinessLogicService;
        private readonly IWebHostEnvironment _hostingEnv;
        public BookController(IBusinessLogicService BusinessLogicService, IWebHostEnvironment hostingEnv)
        {
            _BusinessLogicService = BusinessLogicService;
            _hostingEnv = hostingEnv;
        }


        [HttpPost]
        [Route("AddBook")]
        public async Task<ResponseData> AddBook(BookDetailsDTO bookDetailsDTO)
        {
            var responseData = new ResponseData();

            var book = new Book
            {
                Title = bookDetailsDTO.Title,
                Description = bookDetailsDTO.Description,
                Author = bookDetailsDTO.Author,
                ImageName = "",
                Year = bookDetailsDTO.Year,
                Genre= bookDetailsDTO.Genre
            };

            var savedFlag = await _BusinessLogicService.AddBook(book);

            if (savedFlag != 1)
            {
                responseData.Message = "Could Not Save Updated Changes";
                responseData.Data = null;
                responseData.IsSuccess = false;

                return responseData;
            }

            responseData.Message = "Successfully Added Image";
            responseData.Data = book;
            responseData.IsSuccess = true;

            return responseData;

        }

        [HttpPost]
        [Route("UploadImage")]
        public async Task<ResponseData> UploadImage([FromForm] BookImageContent bookImageContent)
        {

            var responseData = new ResponseData();

            var savedImageName = await SaveFileImageToImageFolder(bookImageContent.BookImage);

            if (savedImageName == null)
            {
                responseData.Message = "Could not upload image";
                responseData.Data = null;
                responseData.IsSuccess = false;

                return responseData;
            }

            var bookId = int.Parse(bookImageContent.BookId);
            var book = await _BusinessLogicService.GetBookById(bookId);

            if (book == null)
            {
                responseData.Message = "Could not get book";
                responseData.Data = null;
                responseData.IsSuccess = false;

                return responseData;
            }

            book.ImageName = savedImageName;

            var saved = await _BusinessLogicService.UpdateBook(book);

            if (saved != 1)
            {
                responseData.Message = "Could Not Save Updated Changes";
                responseData.Data = null;
                responseData.IsSuccess = false;

                return responseData;
            }

            var bookImagePath = GetImageFilePath(book.ImageName);

            var bookResponseDTO = new BookResponseDTO
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                Author = book.Author,
                Year = book.Year,
                Image = bookImagePath
            };

            responseData.Message = "Successfully Uploaded Image";
            responseData.Data = bookResponseDTO;
            responseData.IsSuccess = true;

            return responseData;
        }

        [HttpPost]
        [Route("UpdateOldImage")]
        public async Task<ResponseData> UpdateOldImage([FromForm] BookImageContent bookImageContent)
        {

            var responseData = new ResponseData();
            var bookId = int.Parse(bookImageContent.BookId);
            var book = await _BusinessLogicService.GetBookById(bookId);
            var bookImagePath = GetImageFilePath(book.ImageName);

            var bookResponseDTO = new BookResponseDTO
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                Author = book.Author,
                Year = book.Year,
                Image = bookImagePath
            };


            if (bookImageContent.BookImage == null) {
                responseData.Message = "No new image to upload";
                responseData.Data = bookResponseDTO;
                responseData.IsSuccess = false;

                return responseData;
            }

            var savedImageName = await SaveFileImageToImageFolder(bookImageContent.BookImage);

            if (savedImageName == null)
            {
                responseData.Message = "Could not upload image";
                responseData.Data = null;
                responseData.IsSuccess = false;

                return responseData;
            }

           

            if (book == null)
            {
                responseData.Message = "Could not get book";
                responseData.Data = null;
                responseData.IsSuccess = false;

                return responseData;
            }

            if (book.ImageName != null && book.ImageName != "")
            {
                DeleteImage(book.ImageName);
            }

            book.ImageName = savedImageName;

            var saved = await _BusinessLogicService.UpdateBook(book);

            if (saved != 1)
            {
                responseData.Message = "Could Not Save Updated Changes";
                responseData.Data = null;
                responseData.IsSuccess = false;

                return responseData;
            }

            var bookImagePathTwo= GetImageFilePath(book.ImageName);

            var bookResponseDTOTwo = new BookResponseDTO
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                Author = book.Author,
                Year = book.Year,
                Image = bookImagePathTwo
            };

            responseData.Message = "Successfully Updated Image";
            responseData.Data = bookResponseDTOTwo;
            responseData.IsSuccess = true;

            return responseData;
        }


        [HttpGet]
        [Route("GetAllBooks")]
        public async Task<ResponseData> GetAllBooks()
        {
            var responseData = new ResponseData();

            var allBooks = await _BusinessLogicService.GetAllBooks();

            if (allBooks == null)
            {
                responseData.Message = "Something went wrong trying to get all books";
                responseData.Data = null;
                responseData.IsSuccess = false;
                return responseData;
            }

            var bookResponseDTOs = new List<BookResponseDTO>();

            foreach (var book in allBooks)
            {
                var bookImagePath = book.ImageName == "" ? "" : GetImageFilePath(book.ImageName);

                var bookResponseDTO = new BookResponseDTO
                {
                    Id = book.Id,
                    Title = book.Title,
                    Description = book.Description,
                    Author = book.Author,
                    Year = book.Year,
                    Image = bookImagePath
                };

                bookResponseDTOs.Add(bookResponseDTO);
            }

            responseData.Message = "Got all books.";
            responseData.Data = bookResponseDTOs;
            responseData.IsSuccess = true;
            return responseData;
        }

        [HttpGet]
        [Route("GetBookById/{id}")]
        public async Task<ResponseData> GetBookById([FromRoute] int id)
        {
            var responseData = new ResponseData();

            var book = await _BusinessLogicService.GetBookById(id);

            if (book == null)
            {
                responseData.Message = "Could not get book";
                responseData.Data = null;
                responseData.IsSuccess = false;

                return responseData;
            }

            var bookImagePath = GetImageFilePath(book.ImageName);

            var bookResponseDTO = new BookResponseDTO
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                Author = book.Author,
                Year = book.Year,
                Image = bookImagePath,
                Genre = book.Genre
            };


            responseData.Message = "Could not get book";
            responseData.Data = bookResponseDTO;
            responseData.IsSuccess = true;

            return responseData;
        }

        [HttpPatch]
        [Route("UpdateBook")]
        public async Task<ResponseData> UpdateBook(UpdateBookDTO updateBookDTO)
        {
            var responseData = new ResponseData();

            var afterSlash = GetContentAfterSlash(updateBookDTO.ImageName);
            var fullPath = GetImageFilePath(afterSlash);

            var book = new Book
            {
                Id = updateBookDTO.Id,
                Title = updateBookDTO.Title,
                Description = updateBookDTO.Description,
                Author = updateBookDTO.Author,
                ImageName = afterSlash,
                Year = updateBookDTO.Year,
                Genre = updateBookDTO.Genre
            };


            var bookResponseDTO = new BookResponseDTO
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                Author = book.Author,
                Year = book.Year,
                Image = fullPath,
                Genre = book.Genre
            };

            var savedFlag = await _BusinessLogicService.UpdateBook(book);

            if (savedFlag != 1)
            {
                responseData.Message = "Could Not Save Updated Changes";
                responseData.Data = null;
                responseData.IsSuccess = false;

                return responseData;
            }

            responseData.Message = "Successfully Updated Image";
            responseData.Data = bookResponseDTO;
            responseData.IsSuccess = true;

            return responseData;
        }

        [HttpPost]
        [Route("DeleteBook/{id}")]
        public async Task<ResponseData> DeleteBook([FromRoute] int id)
        {
            var responseData = new ResponseData();

            var book = await _BusinessLogicService.GetBookById(id);

            if (book == null)
            {
                responseData.Message = "Could not get book";
                responseData.Data = null;
                responseData.IsSuccess = false;

                return responseData;
            }

            if (book.ImageName != null && book.ImageName != "")
            {
                DeleteImage(book.ImageName);
            }

            var seletedFlag = await _BusinessLogicService.DeleteBook(id);

            if (seletedFlag != 1)
            {
                responseData.Message = "Could Not Save Updated Changes";
                responseData.Data = 0;
                responseData.IsSuccess = false;

                return responseData;
            }

       

            responseData.Message = "Successfully Updated Image";
            responseData.Data = 1;
            responseData.IsSuccess = true;

            return responseData;
        }

        private async Task<string> SaveFileImageToImageFolder(IFormFile imageFile)
        {
            string imageName = new String(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ', '-');
            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(imageFile.FileName);

            var imagePath = Path.Combine(_hostingEnv.ContentRootPath, "images", imageName);

            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }
            return imageName;
        }

        private string GetImageFilePath(string imageName)
        {
            var filePath = String.Format("{0}://{1}{2}/images/{3}", Request.Scheme, Request.Host, Request.PathBase, imageName);
            return filePath;
        }

        private void DeleteImage(string imageName)
        {
            var imagePath = Path.Combine(_hostingEnv.ContentRootPath, "images", imageName);
            if (System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);
        }

        private string GetContentAfterSlash(string imagePath) {
            string path = imagePath;
            int pos = path.LastIndexOf("/") + 1;
            return path.Substring(pos, path.Length - pos);
        }
    }
}
