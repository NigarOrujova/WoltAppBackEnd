﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WoltEntity.Utilities.File
{
    public static class Helper
    {
        public static void RemoveFile(string root, string folder, string image)
        {
            string path = Path.Combine(root, folder, image);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
        }
        public enum UserRoles
        {
            Admin,
            Member,
            Moderator
        }
    }
}
