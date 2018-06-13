using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AskME.Core.Interfaces
{
    public interface IUploadImg
    {
        string UploadSingleFile(IFormFile file);
    }
}
