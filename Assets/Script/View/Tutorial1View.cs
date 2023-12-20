using SimpleFileBrowser;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Net.NetworkInformation;

namespace PRJ
{
    public class Tutorial1View : UIView
    {
        string data = "";

        public void Show()
        {
            ShowLayer();
        }

        protected override void OnFirstShow()
        {
            Find<Button>("ButtonGroup/BackBtn").onClick.AddListener(OnClick_BackBtn);
            Find<Button>("LoadArea/LoadFileBtn").onClick.AddListener(OnClick_LoadBtn);
        }

        protected override void OnShow()
        {
            Find<Image>("BG").color = UtilityManager.Instance.HexColor("#FFCECEFF");
        }

        IEnumerator ShowLoadDialogCoroutine()
        {
            yield return FileBrowser.WaitForLoadDialog(FileBrowser.PickMode.FilesAndFolders, true, null, null, "Load Files and Folders", "Load");
            Debug.Log(FileBrowser.Success);

            string fileName = "";
            string output = "";

            if (FileBrowser.Success)
            {
                for (int i = 0; i < FileBrowser.Result.Length; i++)
                {
                    Debug.Log($"{FileBrowser.FoldersFilterText} $$ {FileBrowser.Result[i]}");
                    fileName = FileBrowser.Result[i];
                    output = "D:\\DKP\\ConfigCheck\\@configData.json";
                }

                byte[] bytes = FileBrowserHelpers.ReadBytesFromFile(FileBrowser.Result[0]);

                //string destinationPath = Path.Combine(Application.persistentDataPath, FileBrowserHelpers.GetFilename(FileBrowser.Result[0]));
                //FileBrowserHelpers.CopyFile(FileBrowser.Result[0], destinationPath);

                Debug.Log($"{fileName} {output}");
                DecryptFile(fileName, output);
            }
        }

        public static bool EncryptFile(string inputFile, string outputFile, string keyBase = "07110711")
        {
            byte[] key = CreateKey($"{keyBase}mac1234");

            using (Aes aes = Aes.Create())
            {
                aes.Key = key;

                using (FileStream fsInput = new FileStream(inputFile, FileMode.Open, FileAccess.Read))
                {
                    using (FileStream fsEncrypted = new FileStream(outputFile, FileMode.Create, FileAccess.Write))
                    {
                        fsEncrypted.Write(aes.IV, 0, aes.IV.Length);
                        using (CryptoStream cs = new CryptoStream(fsEncrypted, aes.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            int data;
                            while ((data = fsInput.ReadByte()) != -1)
                            {
                                cs.WriteByte((byte)data);
                            }
                        }
                    }
                }
            }

            // 프로세스 완료시, return true;
            return true;
        }

        public static bool DecryptFile(string inputFile, string outputFile, string keyBase = "07110711")
        {
            byte[] key = CreateKey($"{keyBase}mac1234");

            using (Aes aes = Aes.Create())
            {
                aes.Key = key;

                using (FileStream fsInput = new FileStream(inputFile, FileMode.Open, FileAccess.Read))
                {
                    byte[] iv = new byte[aes.BlockSize / 8];
                    fsInput.Read(iv, 0, iv.Length);
                    aes.IV = iv;

                    using (FileStream fsDecrypted = new FileStream(outputFile, FileMode.Create, FileAccess.Write))
                    {
                        using (CryptoStream cs = new CryptoStream(fsInput, aes.CreateDecryptor(), CryptoStreamMode.Read))
                        {
                            int data;
                            while ((data = cs.ReadByte()) != -1)
                            {
                                fsDecrypted.WriteByte((byte)data);
                            }
                        }
                    }
                }
            }

            // 프로세스 완료시, return true;
            return true;
        }

        private static byte[] CreateKey(string baseString)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                // SHA-256을 사용하여 고정 길이의 키 생성
                return sha256.ComputeHash(Encoding.UTF8.GetBytes(baseString));
            }
        }

        private static string GetMacAddr()
        {
            //Debug.Log($"[Current MacAddr] : {NetworkInterface.GetAllNetworkInterfaces()[0].GetPhysicalAddress().ToString()}");
            return NetworkInterface.GetAllNetworkInterfaces()[0].GetPhysicalAddress().ToString();
        }

        #region Event
        private void OnClick_BackBtn()
        {
            ProjectRootUI.Instance.View<MainView>().Show();
        }

        private void OnClick_LoadBtn()
        {
            FileBrowser.SetFilters(true, new FileBrowser.Filter("Images", ".jpg", ".png"), new FileBrowser.Filter("Text Files", ".txt", ".pdf"));
            FileBrowser.SetDefaultFilter(".jpg");
            FileBrowser.SetExcludedExtensions(".lnk", ".tmp", ".zip", ".rar", ".exe");
            FileBrowser.AddQuickLink("Users", "C:\\Users", null);
            StartCoroutine(ShowLoadDialogCoroutine());
        }

        #endregion
    }
}
