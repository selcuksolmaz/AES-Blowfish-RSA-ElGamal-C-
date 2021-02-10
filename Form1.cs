using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Encryption_Algorithms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        ElGamalAlgorithm elGamal;
        RSA rsa;
        public void Form1_Load(object sender, EventArgs e)
        {
            elGamal = new ElGamalAlgorithm(keySize: 8);
            rsa = new RSA(keySize: 8);
            RSA_groupBox.Visible = false;
            Blowfish_groupBox.Visible = false;
            Gamal_groupBox.Visible = false;
            AES_groupBox.Visible = false;
            lbl_AES_Compare_time.Visible = false;
            lbl_AES_Blowfish_time.Visible = false;
            lbl_AES_time.Visible = false;
            lbl_Blowfish_Aes_time.Visible = false;
            lbl_Blowfish_Compare_time.Visible = false;
            lbl_Blowfish_time.Visible = false;
            lbl_RSA_Compare_time.Visible = false;
            lbl_RSA_Gamal_time.Visible = false;
            lbl_RSA_time.Visible=false;
            lbl_Gamal_Compare_Time.Visible = false;
            lbl_Gamal_RSA_Time.Visible = false;
            lbl_Gamal_Time.Visible = false;
            lbl_Note.Visible = false;
        }

        private void lbl_AES_Output_Click(object sender, EventArgs e)
        {

        }

        private void AES_Button_Click(object sender, EventArgs e)
        {
            lbl_Note.Visible = false;
            lblErrorMessage.Visible = false;
            AES_groupBox.Visible = true;
            RSA_groupBox.Visible = false;
            Blowfish_groupBox.Visible = false;
            Gamal_groupBox.Visible = false;
        }

        private void btn_Gamal_Encrypt_Click(object sender, EventArgs e)
        {
            Stopwatch watch = new Stopwatch();
            Stopwatch watch1 = new Stopwatch();
            if (txt_Gamal_Input.Text == "")
            {
                lblErrorMessage.Visible = true;
                lblErrorMessage.Text =
                    "You must enter Input Value! ";
            }
            /*else if (txt_Input_Blowfish.TextLength < 16)
            {
                lblErrorMessage.Visible = true;
                lblErrorMessage.Text =
                    "You must enter a value larger than 15 size! ";
            }*/
            else
            {
                string plaintext = txt_Gamal_Input.Text;
                watch.Start();
                string ciphertext = elGamal.Encryption(plaintext);
                watch.Stop();
                lbl_Gamal_Time.Text = "El-Gamal Time Encrypt = " + watch.Elapsed.TotalMilliseconds + " ms";


                watch1.Start();
                string ciphertext_to_RSA = rsa.Encryption(plaintext);
                watch1.Stop();
                lbl_Gamal_RSA_Time.Text = "RSA Time Encrypt = " + watch1.Elapsed.TotalMilliseconds + " ms";

                if (watch.Elapsed.TotalMilliseconds< watch1.Elapsed.TotalMilliseconds)
                {
                    lbl_Gamal_Compare_Time.Text = "El-Gamal Encrypt is faster than RSA Encrypt";
                }
                else
                {
                    lbl_Gamal_Compare_Time.Text = "RSA Encrypt is faster than El-Gamal Encrypt";
                }


                lbl_Gamal_Compare_Time.Visible = true;
                lbl_Gamal_RSA_Time.Visible = true;
                lbl_Gamal_Time.Visible = true;
                txt_Gamal_Output.Text = ciphertext;
                byte[] bytes = Encoding.UTF8.GetBytes(ciphertext);
                txt_Gamal_Hex.Text = BitConverter.ToString(bytes);
            }
        }

        private void btn_Gamal_Decrypt_Click(object sender, EventArgs e)
        {
            Stopwatch watch = new Stopwatch();
            Stopwatch watch1 = new Stopwatch();
            if (txt_Gamal_Input.Text == "")
            {
                lblErrorMessage.Visible = true;
                lblErrorMessage.Text =
                    "You must enter Input Value! ";
            }
            /*else if (txt_Input_Blowfish.TextLength < 16)
            {
                lblErrorMessage.Visible = true;
                lblErrorMessage.Text =
                    "You must enter a value larger than 15 size! ";
            }*/
            else
            {
                string ciphertext = txt_Gamal_Input.Text;
                watch.Start();
                string plaintext = elGamal.Decryption(ciphertext);
                watch.Stop();
                lbl_Gamal_Time.Text = "El-Gamal Time Decrypt = " + watch.Elapsed.TotalMilliseconds + " ms";

                string ciphertext_Gamal = rsa.Encryption(plaintext);
                watch1.Start();
                string plaintext_Gamal = rsa.Decryption(ciphertext_Gamal);
                watch1.Stop();
                lbl_Gamal_RSA_Time.Text = "RSA Time Decrypt = " + watch1.Elapsed.TotalMilliseconds + " ms";

                if (watch.Elapsed.TotalMilliseconds< watch1.Elapsed.TotalMilliseconds)
                {
                    lbl_Gamal_Compare_Time.Text = "El-Gamal Decrypt is faster than RSA Decrypt";
                }
                else
                {
                    lbl_Gamal_Compare_Time.Text = "RSA Decrypt is faster than El-Gamal Decrypt";
                }

                txt_Gamal_Output.Text = plaintext;
                byte[] bytes = Encoding.UTF8.GetBytes(plaintext);
                txt_Gamal_Hex.Text = BitConverter.ToString(bytes);
            }
        }

        private void RSA_Button_Click(object sender, EventArgs e)
        {
            lbl_Note.Visible = false;
            lblErrorMessage.Visible = false;
            Gamal_groupBox.Visible = false;
            RSA_groupBox.Visible = true;
            Blowfish_groupBox.Visible = false;
            AES_groupBox.Visible = false;
        }

        private void Gamal_Button_Click(object sender, EventArgs e)
        {
            lbl_Note.Visible = false;
            lblErrorMessage.Visible = false;
            Gamal_groupBox.Visible = true;
            RSA_groupBox.Visible = false;
            Blowfish_groupBox.Visible = false;
            AES_groupBox.Visible = false;
        }

        private void btn_RSA_Encrypt_Click(object sender, EventArgs e)
        {
            Stopwatch watch = new Stopwatch();
            Stopwatch watch1 = new Stopwatch();
            if (txt_RSA_Input.Text == "")
            {
                lblErrorMessage.Visible = true;
                lblErrorMessage.Text =
                    "You must enter Input Value! ";
            }
            /*else if (txt_Input_Blowfish.TextLength < 16)
            {
                lblErrorMessage.Visible = true;
                lblErrorMessage.Text =
                    "You must enter a value larger than 15 size! ";
            }*/
            else
            {
                string plaintext = txt_RSA_Input.Text;

                watch.Start();
                string ciphertext = rsa.Encryption(plaintext);
                watch.Stop();
                lbl_RSA_time.Text = "RSA Time Encrypt = " + watch.Elapsed.TotalMilliseconds + " ms";

                watch1.Start();
                string ciphertext_to_Gamal = elGamal.Encryption(plaintext);
                watch1.Stop();
                lbl_RSA_Gamal_time.Text = "El-Gamal Time Encrypt = " + watch1.Elapsed.TotalMilliseconds + " ms";
                if (watch.Elapsed.TotalMilliseconds< watch1.Elapsed.TotalMilliseconds)
                {
                    lbl_RSA_Compare_time.Text = "RSA Encrypt is faster than El-Gamal Encrypt";
                }
                else
                {
                    lbl_RSA_Compare_time.Text = "El-Gamal Encrypt is faster than RSA Encrypt";
                }

                lbl_RSA_Compare_time.Visible = true;
                lbl_RSA_Gamal_time.Visible = true;
                lbl_RSA_time.Visible = true;
                txt_RSA_output.Text = ciphertext;
                byte[] bytes = Encoding.UTF8.GetBytes(ciphertext);
                txt_RSA_hex.Text = BitConverter.ToString(bytes);
            }
        }

        private void btn_RSA_Decrypt_Click(object sender, EventArgs e)
        {
            Stopwatch watch = new Stopwatch();
            Stopwatch watch1 = new Stopwatch();
            if (txt_RSA_Input.Text == "")
            {
                lblErrorMessage.Visible = true;
                lblErrorMessage.Text =
                    "You must enter Input Value! ";
            }
            /*else if (txt_Input_Blowfish.TextLength < 16)
            {
                lblErrorMessage.Visible = true;
                lblErrorMessage.Text =
                    "You must enter a value larger than 15 size! ";
            }*/
            else
            {
                string ciphertext = txt_RSA_Input.Text;

                watch.Start();
                string plaintext = rsa.Decryption(ciphertext);
                watch.Stop();
                lbl_RSA_time.Text = "RSA Time Decrypt = " + watch.Elapsed.TotalMilliseconds + " ms";

                string ciphertext_to_Gamal = elGamal.Encryption(plaintext);
                watch1.Start();
                string plaintext_Gamal = elGamal.Decryption(ciphertext_to_Gamal);
                watch.Stop();
                lbl_RSA_Gamal_time.Text = "El Gamal Decrypt = " + watch1.Elapsed.TotalMilliseconds + " ms";

                if (watch.Elapsed.TotalMilliseconds < watch1.Elapsed.TotalMilliseconds)
                {
                    lbl_RSA_Compare_time.Text = "RSA Decrypt is faster than El-Gamal Decrypt";
                }
                else
                {
                    lbl_RSA_Compare_time.Text = "El-Gamal Decrypt is faster than RSA Decrypt";
                }
                      

                txt_RSA_output.Text = plaintext;
                byte[] bytes = Encoding.UTF8.GetBytes(plaintext);
                txt_RSA_hex.Text = BitConverter.ToString(bytes);
            }
        }

        private void Blowfish_Button_Click(object sender, EventArgs e)
        {
            lblErrorMessage.Visible = false;
            Blowfish_groupBox.Visible = true;
            RSA_groupBox.Visible = false;
            Gamal_groupBox.Visible = false;
            AES_groupBox.Visible = false;
            lbl_Note.Visible = true;
            lbl_Note.Text = "If you want to compare Blowfish and AES, you must enter the key size 16, 24 or 32.";
        }

        private void btn_Blowfish_Encrypt_Click(object sender, EventArgs e)
        {
            Stopwatch watch = new Stopwatch();
            Stopwatch watch1 = new Stopwatch();
            if (txt_Blowfish_Input.Text == "")
            {
                lblErrorMessage.Visible = true;
                lblErrorMessage.Text =
                    "You must enter Input Value! ";
            }
            else if (txt_Blowfish_key.Text == "")
            {
                lblErrorMessage.Visible = true;
                lblErrorMessage.Text =
                    "You must enter Key Value! ";
            }
            else
            {
                string plaintext = txt_Blowfish_Input.Text;
                // Get the key
                string key = txt_Blowfish_key.Text;

                var clearText_to_AES = Encoding.UTF8.GetBytes(txt_Blowfish_Input.Text);
                var key_to_AES = Encoding.UTF8.GetBytes(txt_Blowfish_key.Text);
                if (key.Length==16 || key.Length == 24 || key.Length == 32)
                {
                    watch.Start();
                    BlowFish blowFish = new BlowFish(key);
                    string cipherText = blowFish.Encrypt_CBC(plaintext);
                    watch.Stop();
                    lbl_Blowfish_time.Text = "Blowfish Time Encrypt = " + watch.Elapsed.TotalMilliseconds + " ms";

                    watch1.Start();
                    byte[] cipherText_to_AES = AesImplementation.Encrypt(clearText_to_AES, key_to_AES);
                    watch1.Stop();
                    lbl_Blowfish_Aes_time.Text = "AES Time Encrypt = " + watch1.Elapsed.TotalMilliseconds + " ms";

                    if (watch.Elapsed.TotalMilliseconds< watch1.Elapsed.TotalMilliseconds)
                    {
                        lbl_Blowfish_Compare_time.Text = "Blowfish Decrypt is faster than AES Decrypt";
                    }
                    else
                    {
                        lbl_Blowfish_Compare_time.Text = "AES Encrypt is faster than Blowfish Encrypt";
                    }

                    lbl_Blowfish_Aes_time.Visible = true;
                    lbl_Blowfish_Compare_time.Visible = true;
                    lbl_Blowfish_time.Visible = true;
                    txt_Blowfish_output.Text = cipherText;
                    byte[] bytes = Encoding.UTF8.GetBytes(cipherText);
                    txt_Blowfish_hex.Text = BitConverter.ToString(bytes);
                }
                else
                {
                    watch.Start();
                    BlowFish blowFish = new BlowFish(key);
                    string cipherText = blowFish.Encrypt_CBC(plaintext);
                    watch.Stop();
                    lbl_Blowfish_time.Text = "Blowfish Time Encrypt = " + watch.Elapsed.TotalMilliseconds + " ms";

                    lbl_Blowfish_time.Visible = true;
                    txt_Blowfish_output.Text = cipherText;
                    byte[] bytes = Encoding.UTF8.GetBytes(cipherText);
                    txt_Blowfish_hex.Text = BitConverter.ToString(bytes);
                }
                
            }
        }

        private void btn_Blowfish_Decrypt_Click(object sender, EventArgs e)
        {
           
            Stopwatch watch = new Stopwatch();
            Stopwatch watch1 = new Stopwatch();
            if (txt_Blowfish_Input.Text == "")
            {
                lblErrorMessage.Visible = true;
                lblErrorMessage.Text =
                    "You must enter Input Value! ";
            }
            else if (txt_Blowfish_key.Text == "")
            {
                lblErrorMessage.Visible = true;
                lblErrorMessage.Text =
                    "You must enter Key Value! ";
            }
            else if (txt_Blowfish_Input.TextLength < 16)
            {
                lblErrorMessage.Visible = true;
                lblErrorMessage.Text =
                    "You must enter a value larger than 15 size! ";
            }
            else
            {
                string ciphertext = txt_Blowfish_Input.Text;
                // Get the key
                string key = txt_Blowfish_key.Text;
                var clearText_to_AES = Encoding.UTF8.GetBytes(txt_Blowfish_Input.Text);
                var key_to_AES = Encoding.UTF8.GetBytes(txt_Blowfish_key.Text);

               
                watch.Start();
                BlowFish blowFish = new BlowFish(key);
                string plaintext = blowFish.Decrypt_CBC(ciphertext);
                watch.Stop();
                lbl_Blowfish_time.Text = "Blowfish Time Decrypt = " + watch.Elapsed.TotalMilliseconds + " ms";
                byte[] cipherText_to_AES = AesImplementation.Encrypt(clearText_to_AES, key_to_AES);
                watch1.Start();
                byte[] plainText = AesImplementation.Decrypt(cipherText_to_AES, key_to_AES);
                watch1.Stop();
                lbl_Blowfish_Aes_time.Text = "AES Time Decrypt = " + watch1.Elapsed.TotalMilliseconds + " ms";
                if (watch.Elapsed.TotalMilliseconds< watch1.Elapsed.TotalMilliseconds)
                {
                    lbl_Blowfish_Compare_time.Text = "Blowfish Decrypt is faster than AES Decrypt";
                }
                else
                {
                    lbl_Blowfish_Compare_time.Text = "AES Decrypt is faster than Blowfish Decrypt";
                }

                    
                txt_Blowfish_output.Text = plaintext;
                byte[] bytes = Encoding.UTF8.GetBytes(plaintext);
                txt_Blowfish_hex.Text = BitConverter.ToString(bytes);
               
                
            }
        }

        private void btn_AES_Encrypt_Click(object sender, EventArgs e)
        {
            
            if (ValidateFields())
            {
                try
                {
                    Stopwatch watch = new Stopwatch();
                    Stopwatch watch1 = new Stopwatch();
                    lblErrorMessage.Visible = false;

                    var clearText = Encoding.UTF8.GetBytes(txt_AES_Input.Text);
                    string plaintext_to_Blow = txt_AES_Input.Text;
                    string key_to_Blow=txt_AES_key.Text;
                    // Get the key
                    var key = Encoding.UTF8.GetBytes(txt_AES_key.Text); 

                    watch.Start();
                    byte[] cipherText = AesImplementation.Encrypt(clearText, key);
                    watch.Stop();
                    lbl_AES_time.Text = "AES Time Encrypt = " + watch.Elapsed.TotalMilliseconds + " ms";

                    watch1.Start();
                    BlowFish blowFish = new BlowFish(key);
                    string cipherText_to_Blow = blowFish.Encrypt_CBC(plaintext_to_Blow);
                    watch1.Stop();
                    lbl_AES_Blowfish_time.Text = "Blowfish Time Encrypt = " + watch1.Elapsed.TotalMilliseconds + " ms";

                    if (watch.Elapsed.TotalMilliseconds<watch1.Elapsed.TotalMilliseconds)
                    {
                        lbl_AES_Compare_time.Text = "AES Encrypt is faster than Blowfish Encrypt";
                    }
                    else
                    {
                        lbl_AES_Compare_time.Text = "Blowfish Encrypt is faster than AES Encrypt";
                    }

                    lbl_AES_time.Visible = true;
                    lbl_AES_Blowfish_time.Visible = true;
                    lbl_AES_Compare_time.Visible = true;
                    txt_AES_output.Text = Convert.ToBase64String(cipherText);

                    txt_AES_hex.Text = BitConverter.ToString(cipherText);
                    txt_AES_output.Enabled = true;
                }
                catch (Exception exception)
                {
                    MessageBox.Show($@"An error occured: {exception}");
                }
            }
        }




        private bool ValidateFields()
        {
            if(txt_AES_Input.Text=="")
            {
                lblErrorMessage.Visible = true;
                lblErrorMessage.Text =
                    "Please enter Input!"; return false;
            }
            switch (cmbx_AES.SelectedItem.ToString())
            {

                case "128":
                    if (txt_AES_key.TextLength != 16)
                    {
                        txt_AES_key.Focus();
                        lblErrorMessage.Visible = true;
                        lblErrorMessage.Text =
                            "You must enter 16 characters for the 128-bit key!"; return false;
                    }

                    break;
                case "192":
                    if (txt_AES_key.TextLength != 24)
                    {
                        txt_AES_key.Focus();
                        lblErrorMessage.Visible = true;
                        lblErrorMessage.Text =
                           "You must enter 24 characters for the 192-bit key!"; return false;
                    }

                    break;
                case "256":
                    if (txt_AES_key.TextLength != 32)
                    {
                        txt_AES_key.Focus();
                        lblErrorMessage.Visible = true;
                        lblErrorMessage.Text =
                            "You must enter 32 characters for the 256-bit key!"; return false;
                    }

                    break;
            }


            return true;
        }

        private void btn_AES_Decrypt_Click(object sender, EventArgs e)
        {
            if (ValidateFields())
            {
                try
                {
                    Stopwatch watch = new Stopwatch();
                    Stopwatch watch1 = new Stopwatch();
                    lblErrorMessage.Visible = false;
                    string plaintext_to_Blow = txt_AES_Input.Text;
                    string key_to_Blow = txt_AES_key.Text;

                    byte[] cipherText = Convert.FromBase64String(txt_AES_Input.Text);

                    // Get the key
                    var key = Encoding.UTF8.GetBytes(txt_AES_key.Text);

                    // Decrypt the cipher text
                    watch.Start();
                    byte[] plainText = AesImplementation.Decrypt(cipherText, key);
                    watch.Stop();
                    lbl_AES_time.Text = "AES Decrypt = " + watch.Elapsed.TotalMilliseconds + " ms";

                    BlowFish blowFish = new BlowFish(key_to_Blow);
                    string ciphertext_to_Blow = blowFish.Encrypt_CBC(plaintext_to_Blow);
                    watch1.Start();
                    string plaintext_Blow = blowFish.Decrypt_CBC(ciphertext_to_Blow);
                    watch1.Stop();
                    lbl_AES_Blowfish_time.Text = "Blowfish Decrypt = " + watch1.Elapsed.TotalMilliseconds + " ms";

                    if (watch.Elapsed.TotalMilliseconds < watch1.Elapsed.TotalMilliseconds)
                    {
                        lbl_AES_Compare_time.Text = "AES Decrypt is faster than Blowfish Decrypt";
                    }
                    else
                    {
                        lbl_AES_Compare_time.Text = "Blowfish Decrypt is faster than AES Decrypt";
                    }

                    lbl_AES_time.Visible = true;
                    lbl_AES_Blowfish_time.Visible = true;
                    lbl_AES_Compare_time.Visible = true;
                    txt_AES_output.Text = Convert.ToBase64String(cipherText);
                    // Set the right output
                    txt_AES_output.Text = Encoding.UTF8.GetString(plainText);
                    txt_AES_hex.Text = BitConverter.ToString(plainText);
                }
                catch (Exception exception)
                {
                    MessageBox.Show($@"An error occured: {exception}");
                }
            }
        }
    }
}
