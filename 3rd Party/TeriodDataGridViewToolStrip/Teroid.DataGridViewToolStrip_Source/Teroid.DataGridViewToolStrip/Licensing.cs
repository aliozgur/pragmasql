namespace Teroid.DataGridViewToolStrip
{
    using Microsoft.Win32;
    using System;
    using System.Windows.Forms;

    internal class Licensing
    {
        private bool bDesignMode;
        private bool bLicensed;
        private string DecryptedLicenceKey = "";
        private string EncryptedLicenceKey = "";
        private int ProductID;
        private string ProductName;

        internal Licensing(int productid, string productname, bool designmode)
        {
            this.ProductID = productid;
            this.ProductName = productname;
            this.bDesignMode = designmode;
            this.ReadLicenceKeyFromRegistry();
        }

        private string Decrypt(string source)
        {
            if (source.Length != 0x10)
            {
                this.bLicensed = false;
                this.ShowInvalidMessage();
                return "";
            }
            for (int i = 0; i <= 15; i++)
            {
                if ((source[i] < '0') || (source[i] > '9'))
                {
                    this.bLicensed = false;
                    this.ShowInvalidMessage();
                    return "";
                }
            }
            string str = source[14].ToString() + source[15].ToString();
            int num2 = Convert.ToInt32(str);
            string str3 = "";
            for (int j = 0; j <= 13; j++)
            {
                int num5;
                string str2 = (num2 + (j * 3)).ToString();
                int num7 = Convert.ToInt32(str2[str2.Length - 1].ToString());
                int num6 = Convert.ToInt32(source[j]) - 0x30;
                int num4 = num6 + num7;
                if (num4 > 9)
                {
                    num5 = num4 - 10;
                }
                else
                {
                    num5 = num4;
                }
                str3 = str3 + num5.ToString();
            }
            return (str3 + str);
        }

        private void ReadLicenceKeyFromRegistry()
        {
            if (!this.bLicensed)
            {
                try
                {
                    RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Teroid");
                    this.EncryptedLicenceKey = (string) key.GetValue(this.ProductName);
                    if (this.EncryptedLicenceKey == null)
                    {
                        this.EncryptedLicenceKey = "";
                    }
                    if (this.EncryptedLicenceKey != "")
                    {
                        this.DecryptedLicenceKey = this.Decrypt(this.EncryptedLicenceKey);
                        if (this.DecryptedLicenceKey != "")
                        {
                            this.bLicensed = this.ValidateLicenceKey(this.DecryptedLicenceKey);
                        }
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        private void ShowInvalidMessage()
        {
            if (this.bDesignMode && (this.EncryptedLicenceKey != ""))
            {
                MessageBox.Show("LicenceKey is invalid. Please enter a valid key", "Teroid Data Grid View Print Control 1.1");
            }
        }

        private void ShowValidatedMessage()
        {
            bool bDesignMode = this.bDesignMode;
        }

        private bool ValidateLicenceKey(string key)
        {
            bool flag = true;
            char ch = key[0];
            char ch2 = key[1];
            char ch3 = key[2];
            char ch4 = key[3];
            if (Convert.ToInt32(ch.ToString() + ch2.ToString() + ch3.ToString() + ch4.ToString()) != this.ProductID)
            {
                flag = false;
            }
            if (flag)
            {
                int num2 = 0;
                int num3 = Convert.ToInt32(key[12].ToString() + key[13].ToString());
                for (int i = 4; i <= 11; i++)
                {
                    num2 += Convert.ToInt32((int) (key[i] - '0'));
                }
                if (num2 != num3)
                {
                    flag = false;
                }
            }
            if (flag)
            {
                this.ShowValidatedMessage();
                return flag;
            }
            this.ShowInvalidMessage();
            return flag;
        }

        private void WriteLicenceKeyToRegistry()
        {
            if (this.bDesignMode)
            {
                try
                {
                    Registry.CurrentUser.CreateSubKey(@"Software\Teroid").SetValue(this.ProductName, this.EncryptedLicenceKey);
                }
                catch (Exception)
                {
                }
            }
        }

        internal bool DesignMode
        {
            get
            {
                return this.bDesignMode;
            }
            set
            {
                this.bDesignMode = value;
            }
        }

        internal string LicenceKey
        {
            get
            {
                return this.EncryptedLicenceKey;
            }
            set
            {
                this.EncryptedLicenceKey = value;
                this.DecryptedLicenceKey = this.Decrypt(this.EncryptedLicenceKey);
                if (this.DecryptedLicenceKey != "")
                {
                    this.bLicensed = this.ValidateLicenceKey(this.DecryptedLicenceKey);
                }
                else
                {
                    this.bLicensed = false;
                }
                if (this.bLicensed)
                {
                    this.WriteLicenceKeyToRegistry();
                }
            }
        }

        internal bool Licensed
        {
            get
            {
                return this.bLicensed;
            }
        }
    }
}

