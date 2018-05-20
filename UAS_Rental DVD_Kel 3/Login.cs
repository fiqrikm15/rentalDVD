using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UAS_Rental_DVD_Kel_3.Entities;
using UAS_Rental_DVD_Kel_3.User;

namespace UAS_Rental_DVD_Kel_3
{
    public partial class Login : Form
    {
        static MongoClient client = new MongoClient();
        static IMongoDatabase db = client.GetDatabase("db_rental");
        static IMongoCollection<Admin> adm = db.GetCollection<Admin>("admin");

        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            txt_username.Focus();
            this.TopMost = true;
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            if (txt_username.Text == "")
                MessageBox.Show("Username tidak boleh kosong!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (txt_password.Text == "")
                MessageBox.Show("Password tidak boleh kosong!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                try
                {

                    MD5 md5 = new MD5CryptoServiceProvider();
                    md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(txt_password.Text));

                    byte[] result = md5.Hash;

                    StringBuilder strBuillder = new StringBuilder();

                    for (int i = 0; i < result.Length; i++)
                    {
                        strBuillder.Append(result[i].ToString("x2"));
                    }

                    /**
                    * check username and password from database 
                    * and count data from database
                    */
                    List<Admin> staffList = adm.AsQueryable().Where(p => p.Username == txt_username.Text && p.Password == strBuillder.ToString()).ToList();

                    var staffCount = staffList.Count();

                    if (staffCount == 1)
                    {
                        LoginInfo.UserID = staffList[0].Id.ToString();

                        this.Hide();

                        MainForm mf = new MainForm();
                        mf.Show();
                    }
                    else
                    {
                        MessageBox.Show("Login Failed");
                    }
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message.ToString());
                }
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
