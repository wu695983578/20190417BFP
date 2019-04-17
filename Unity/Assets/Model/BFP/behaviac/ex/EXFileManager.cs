
using UnityEngine;
/**
* 拓展的文件管理类
* **/
namespace ETModel
{
    public class EXFileManager : behaviac.FileManager
    {
        public EXFileManager()
        {
        }

        public override void FileClose(string filePath, string ext, byte[] pBuffer)
        {
          
            base.FileClose(filePath, ext, pBuffer);
        }

        public override byte[] FileOpen(string filePath, string ext = null)
        {
            GameObject kv = (GameObject)ResourcesHelper.Load("KV");
            return  kv.Get<TextAsset>(filePath).bytes;
        }
    }//class_end
}
