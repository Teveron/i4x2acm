using System;
using System.Collections.Generic;
using System.IO;

namespace i4x2acm
{
    class Program
    {
        static void Main(String[] args)
        {
            var filePaths = GetFiles(args[0]);
            SplitFiles(filePaths);
        }

        private static List<String> GetFiles(String path)
        {
            List<String> filePaths = new List<string>();

            if (Directory.Exists(path))
                filePaths.AddRange(Directory.GetFiles(path, "*.i4x"));
            else if (File.Exists(path) && Path.GetExtension(path) == "i4x")
                filePaths.Add(path);
            else
                Console.WriteLine("No i4x files found.");

            return filePaths;
        }

        private static void SplitFiles(List<String> filePaths)
        {
            foreach (String filePath in filePaths)
            {
                String directoryPath = Path.GetDirectoryName(filePath);
                String fileName = Path.GetFileName(filePath);

                // Create a folder named after the file.
                String newDirectoryPath = Path.Combine(directoryPath, Path.GetFileNameWithoutExtension(filePath));
                Directory.CreateDirectory(newDirectoryPath);

                using (FileStream fileStream = File.OpenRead(filePath))
                {
                    Byte[] acmFramesBytes = new Byte[4];
                    fileStream.Seek(12, SeekOrigin.Begin);
                    fileStream.Read(acmFramesBytes, 0, 4);
                    UInt32 acmFrames = BitConverter.ToUInt32(acmFramesBytes, 0);

                    for (Int32 acmFrameIndex = 0; acmFrameIndex < acmFrames; acmFrameIndex++)
                    {
                        Byte[] acmFrameStartBytes = new Byte[4];
                        fileStream.Seek(acmFrameIndex * 16 + 20, SeekOrigin.Begin);
                        fileStream.Read(acmFrameStartBytes, 0, 4);
                        UInt32 acmFrameStart = BitConverter.ToUInt32(acmFrameStartBytes, 0);

                        Byte[] acmFrameLengthBytes = new Byte[4];
                        fileStream.Seek(acmFrameIndex * 16 + 24, SeekOrigin.Begin);
                        fileStream.Read(acmFrameLengthBytes, 0, 4);
                        UInt32 acmFrameLength = BitConverter.ToUInt32(acmFrameLengthBytes, 0);

                        Byte[] acmFrame = new Byte[acmFrameLength];
                        fileStream.Seek(acmFrameStart, SeekOrigin.Begin);
                        fileStream.Read(acmFrame, 0, (Int32)acmFrameLength);
                        File.WriteAllBytes(Path.Combine(newDirectoryPath, acmFrameIndex + ".acm"), acmFrame);
                    }
                }
            }
        }
    }
}
