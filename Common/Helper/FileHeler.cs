using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace Common.Helper
{
    public class FileHeler
    {
        /// <summary>
        /// 目录分隔符
        /// windows "\" Mac OS and Linux "/"
        /// </summary>
        private static string DirectorySeparatorChar = Path.DirectorySeparatorChar.ToString();
        /// <summary>
        /// 包含应用程序的目录的绝对路径
        /// </summary>
        private static string _ContentRootPath = DI.ServiceProvider.GetRequiredService<IHostingEnvironment>().ContentRootPath;
        /// <summary>
        /// 获取文件绝对路径
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string MapPath(string path)
        {
            return IsAbsolute(path) ? path : Path.Combine(_ContentRootPath, path.TrimStart('~', '/').Replace("/", DirectorySeparatorChar));
        }

        public static bool IsAbsolute(string path)
        {
            return Path.VolumeSeparatorChar == ';' ? path.IndexOf(Path.VolumeSeparatorChar) > 0 : path.IndexOf('\\') > 0;
        }
        /// <summary>
        /// 检测指定路径是否存在
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="isDirectory">是否是目录</param>
        /// <returns></returns>
        public static bool Isexit(string path,bool isDirectory)
        {
            return isDirectory ? Directory.Exists(MapPath(path)) : File.Exists(MapPath(path));
        }
        /// <summary>
        /// 检测目录是否为空
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool IsEmptyDirectory(string path)
        {
            return Directory.GetFiles(MapPath(path)).Length <= 0 && Directory.GetDirectories(MapPath(path)).Length <= 0;
        }
        /// <summary>
        /// 创建目录或文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="isDirectory"></param>
        public static void CreatFiles(string path,bool isDirectory)
        {
            try
            {
                if (!Isexit(path, isDirectory))
                {
                    if (isDirectory)
                    {
                        Directory.CreateDirectory(MapPath(path));
                    }
                    else
                    {
                        FileInfo file = new FileInfo(MapPath(path));
                        FileStream fs = file.Create();
                        fs.Dispose();
                    }
                }
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        /// <summary>
        /// 删除文件或目录
        /// </summary>
        /// <param name="path"></param>
        /// <param name="isDirectory"></param>
        public static void DeleteFiles(string path,bool isDirectory)
        {
            try
            {
                if (!Isexit(path,isDirectory))
                {
                    if (isDirectory)
                    {
                        Directory.Delete(MapPath(path));
                    }
                    else
                    {
                        File.Delete(MapPath(path));
                    }
                }
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        /// <summary>
        /// 清空目录下所有文件及子目录，依然保留该目录
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns></returns>
        public static void ClearDirectory(string path)
        {
            if (!Isexit(path,true))
            {
                //目录下所有文件
                string[] files = Directory.GetFiles(MapPath(path));
                foreach (var file in files)
                {
                    DeleteFiles(file, false);
                }
                //目录下所有子目录
                string[] directories = Directory.GetDirectories(MapPath(path));
                foreach (var directory in directories)
                {
                    DeleteFiles(directory, true);
                }
            }
        }
        /// <summary>
        /// 复制文件内容到目标文件夹
        /// </summary>
        /// <param name="sourcePath">源文件</param>
        /// <param name="targetPath">目标文件夹</param>
        /// <param name="isOverWrite">是否可以覆盖</param>
        public static void Copy(string sourcePath,string targetPath,bool isOverWrite = true)
        {
            File.Copy(MapPath(sourcePath), MapPath(targetPath), isOverWrite);
        }
        /// <summary>
        /// 移动文件
        /// </summary>
        /// <param name="sourcePath">源文件</param>
        /// <param name="targetPath">目标文件夹</param>
        public static void Move(string sourcePath, string targetPath)
        {
            string sourceFileName = GetFileName(sourcePath);
            //如果目标文件夹不存在则创建
            if (!Isexit(targetPath, true))
            {
                CreatFiles(targetPath, true);
            }
            else
            {
                //如果目标文件夹存在同名文件则删除
                if (Isexit(Path.Combine(MapPath(targetPath),sourceFileName),false))
                {
                    DeleteFiles(Path.Combine(MapPath(targetPath), sourceFileName), true);
                }
            }
            File.Move(MapPath(sourcePath), Path.Combine(MapPath(targetPath), sourceFileName));
        }
        /// <summary>
        /// 获取文件名和扩展名
        /// </summary>
        /// <param name="sourcePath"></param>
        /// <returns></returns>
        private static string GetFileName(string path)
        {
            return Path.GetFileName(MapPath(path));
        }
    }
}
