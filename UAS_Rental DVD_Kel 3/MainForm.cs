using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UAS_Rental_DVD_Kel_3.Entities;
using UAS_Rental_DVD_Kel_3.User;

namespace UAS_Rental_DVD_Kel_3
{
    public partial class MainForm : Form
    {
        static MongoClient client = new MongoClient();
        static IMongoDatabase db = client.GetDatabase("db_rental");
        static IMongoCollection<Anggota> anggota = db.GetCollection<Anggota>("anggota");
        static IMongoCollection<Film> film = db.GetCollection<Film>("film");
        static IMongoCollection<Pinjam> pinjam = db.GetCollection<Pinjam>("pinjam");

        public MainForm()
        {
            InitializeComponent();
            ReadDataAnggota();
            ReadDataFilm();
            ReadDataPinjam();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (LoginInfo.UserID != "")
            {
                loginToolStripMenuItem.Visible = false;
                logoutToolStripMenuItem.Visible = true;
                //tb_mainControl.Visible = true;
            }
            else
            {
                logoutToolStripMenuItem.Visible = true;
                loginToolStripMenuItem.Visible = false;
                //tb_mainControl.Visible = true;
            }
        }

        private void ReadDataPinjam()
        {
            var pinjamList = pinjam.AsQueryable().Where(p => p.Status == "Pinjam").ToList();
            dtg_peminjaman.DataSource = pinjamList;
        }

        private void ReadDataFilm()
        {
            var filmList = film.AsQueryable().ToList<Film>();
            dtg_listFilm.DataSource = filmList;

            foreach(var data in filmList)
            {
                cb_filmtitle.Items.Add(data.Title);
            }
        }

        private void ReadDataAnggota()
        {
            List<Anggota> anggotaList = anggota.AsQueryable().ToList();
            dtg_anggota.DataSource = anggotaList;

            foreach (var data in anggotaList)
            {
                cb_membername.Items.Add(data.FirstName + " " + data.LastName);
            }
        }

        private void reset()
        {
            txt_idanggota.Text = "";
            txt_firstName.Text = "";
            txt_lastName.Text = "";
            txt_email.Text = "";
            txt_address1.Text = "";
            txt_address2.Text = "";
            txt_city.Text = "";
            txt_zip.Text = "";
            txt_idfilm.Text = "";
            txt_title.Text = "";
            rtb_description.Text = "";
            txt_realeasYear.Text = "";
            cb_language.SelectedItem = "";
            txt_duration.Text = "";
            txt_rating.Text = "";
            cb_filmtitle.SelectedItem = "";
            cb_membername.SelectedItem = "";
            dt_rentalDate.Value = System.DateTime.Now;
            dt_returnDate.Value = System.DateTime.Now;
            cb_status.SelectedItem = "";
        }

        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Login lg = new Login();
            lg.Show();
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoginInfo.UserID = "";
            Login lg = new Login();
            lg.Show();
            lg.TopMost = true;
            this.Hide();
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
            this.Close();
        }

        private void btn_simpan_Click(object sender, EventArgs e)
        {
            var first_name = txt_firstName.Text;
            var last_name = txt_lastName.Text;
            var email = txt_email.Text;
            var address1 = txt_address1.Text;
            var address2 = txt_address2.Text;
            var city = txt_city.Text;
            var postal_zip = txt_zip.Text;

            Anggota a = new Anggota
            {
                FirstName = first_name,
                LastName = last_name,
                Email = email,
                Address = address1,
                Address2 = address2,
                City = city,
                PostalZip = postal_zip
            };

            anggota.InsertOne(a);
            ReadDataAnggota();
            reset();
        }

        private void btn_ubah_Click(object sender, EventArgs e)
        {
            var updateDef = Builders<Anggota>.Update.
                Set("first_name", txt_firstName.Text).
                Set("last_name", txt_lastName.Text).
                Set("email", txt_email.Text).
                Set("address", txt_address1.Text).
                Set("address2", txt_address2.Text).
                Set("city", txt_city.Text).
                Set("postal_zip", txt_zip.Text);

            anggota.UpdateOne(p => p.Id == ObjectId.Parse(txt_idanggota.Text), updateDef);
            ReadDataAnggota();
            reset();
        }

        private void dtg_anggota_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txt_idanggota.Text = dtg_anggota.Rows[e.RowIndex].Cells[0].Value.ToString();
            txt_firstName.Text = dtg_anggota.Rows[e.RowIndex].Cells[1].Value.ToString();
            txt_lastName.Text = dtg_anggota.Rows[e.RowIndex].Cells[2].Value.ToString();
            txt_email.Text = dtg_anggota.Rows[e.RowIndex].Cells[3].Value.ToString();
            txt_address1.Text = dtg_anggota.Rows[e.RowIndex].Cells[4].Value.ToString();
            txt_address2.Text = dtg_anggota.Rows[e.RowIndex].Cells[5].Value.ToString();
            txt_city.Text = dtg_anggota.Rows[e.RowIndex].Cells[6].Value.ToString();
            txt_zip.Text = dtg_anggota.Rows[e.RowIndex].Cells[7].Value.ToString();
        }

        private void btn_hapus_Click(object sender, EventArgs e)
        {
            anggota.DeleteOne(s => s.Id == ObjectId.Parse(txt_idanggota.Text));
            ReadDataAnggota();
            reset();
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        //simpan film
        private void button3_Click(object sender, EventArgs e)
        {
            var id_film = txt_idfilm.Text;
            var title = txt_title.Text;
            var description = rtb_description.Text;
            var releaseY = txt_realeasYear.Text;
            var lang = cb_language.SelectedItem.ToString();
            var duration = txt_duration.Text;
            var rating = txt_rating.Text;

            Film flm = new Film
            {
                Title = title,
                Description = description,
                ReleaseYear = Double.Parse(releaseY),
                Language = lang,
                Length = Double.Parse(duration),
                Rating = rating
            };

            film.InsertOne(flm);
            ReadDataFilm();
            reset();
        }

        private void dtg_listFilm_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txt_idfilm.Text = dtg_listFilm.Rows[e.RowIndex].Cells[0].Value.ToString();
            txt_title.Text = dtg_listFilm.Rows[e.RowIndex].Cells[1].Value.ToString();
            rtb_description.Text = dtg_listFilm.Rows[e.RowIndex].Cells[2].Value.ToString();
            txt_realeasYear.Text = dtg_listFilm.Rows[e.RowIndex].Cells[3].Value.ToString();
            cb_language.SelectedItem = dtg_listFilm.Rows[e.RowIndex].Cells[4].Value.ToString();
            txt_duration.Text = dtg_listFilm.Rows[e.RowIndex].Cells[5].Value.ToString();
            txt_rating.Text = dtg_listFilm.Rows[e.RowIndex].Cells[6].Value.ToString();
        }

        private void btn_ubahFilm_Click(object sender, EventArgs e)
        {
            var id_film = txt_idfilm.Text;
            var title = txt_title.Text;
            var description = rtb_description.Text;
            var releaseY = Double.Parse(txt_realeasYear.Text);
            var lang = cb_language.SelectedItem.ToString();
            var duration = Double.Parse(txt_duration.Text);
            var rating = txt_rating.Text;

            var updateDef = Builders<Film>.Update.
                Set("title", title).
                Set("description", description).
                Set("release_year", releaseY).
                Set("language", lang).
                Set("length", duration).
                Set("rating", rating);

            film.UpdateOne(p => p.Id == ObjectId.Parse(txt_idfilm.Text), updateDef);
            ReadDataFilm();
            reset();
        }

        private void btn_hapusFilm_Click(object sender, EventArgs e)
        {
            film.DeleteOne(p => p.Id == ObjectId.Parse(txt_idfilm.Text));
            ReadDataFilm();
            reset();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var film_title = cb_filmtitle.SelectedItem.ToString();
            var member_name = cb_membername.SelectedItem.ToString();
            var rental_date = dt_rentalDate.Value.ToShortDateString();
            var return_date = dt_returnDate.Value.ToShortDateString();
            var status = cb_status.SelectedItem.ToString();

            Pinjam pj = new Pinjam
            {
                Title = film_title,
                MemberName = member_name,
                RentalDate = DateTime.Parse(rental_date),
                ReturnDate = DateTime.Parse(return_date),
                Status = status
            };

            pinjam.InsertOne(pj);
            ReadDataPinjam();
            reset();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var film_title = cb_filmtitle.SelectedItem.ToString();
            var member_name = cb_membername.SelectedItem.ToString();
            var rental_date = dt_rentalDate.Value.ToShortDateString();
            var return_date = dt_returnDate.Value.ToShortDateString();
            var status = cb_status.SelectedItem.ToString();

            var updateDef = Builders<Pinjam>.Update.
                Set("title", film_title).
                Set("member_name", member_name).
                Set("rental_date", DateTime.Parse(rental_date)).
                Set("return_date", DateTime.Parse(return_date)).
                Set("status", status);

            pinjam.UpdateOne(p => p.id == ObjectId.Parse(textBox1.Text), updateDef);
            ReadDataPinjam();
            reset();
        }

        private void dtg_peminjaman_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dtg_peminjaman.Rows[e.RowIndex].Cells[0].Value.ToString();
            cb_filmtitle.Text = dtg_peminjaman.Rows[e.RowIndex].Cells[1].Value.ToString();
            cb_membername.Text = dtg_peminjaman.Rows[e.RowIndex].Cells[2].Value.ToString();
            dt_rentalDate.Text = dtg_peminjaman.Rows[e.RowIndex].Cells[3].Value.ToString();
            dt_returnDate.Text = dtg_peminjaman.Rows[e.RowIndex].Cells[4].Value.ToString();
            cb_status.Text = dtg_peminjaman.Rows[e.RowIndex].Cells[5].Value.ToString();
        }
    }
}
