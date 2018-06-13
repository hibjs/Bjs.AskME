using AskME.Core.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AskME.Infrastructure.FileService
{
    public class UploadImg : IUploadImg
    {
        IHostingEnvironment _hostingEnvironment;
        public UploadImg(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        /// <summary>
        /// 单文件上传
        /// </summary>
        /// <param name="file">图片路径</param>
        /// <returns></returns>
        public  string UploadSingleFile(IFormFile file)
        {
            string fullDir = "";
            if (file != null && file.Length > 0)
            {
                string fileName = Path.GetFileName(file.FileName);
                string fileExt = Path.GetExtension(fileName);
                if (fileExt == ".jpg" || fileExt == ".jpeg" || fileExt == ".png" || fileExt == ".gif")
                {
                    //文件存储路径（相对路径）
                    string dirName = "/UploadImgs/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day + "/";
                    //文件存储物理路径
                    string physicalDirName = _hostingEnvironment.WebRootPath + dirName;
                    //文件不存在创建文件
                    if (!Directory.Exists(physicalDirName))
                    {
                        Directory.CreateDirectory(physicalDirName);
                    }
                    //新的文件名
                    string newFileName = Guid.NewGuid().ToString() + fileExt;
                    //获得的相对文件路径（用于存储在数据库）
                    fullDir = dirName + newFileName;
                    //物理路径
                    string physicalPath = physicalDirName + newFileName;
                    using (var steam = new FileStream(physicalPath, FileMode.Create))
                    {
                         file.CopyTo(steam);
                    }
                }
            }
            return fullDir;
        }
    }
}
