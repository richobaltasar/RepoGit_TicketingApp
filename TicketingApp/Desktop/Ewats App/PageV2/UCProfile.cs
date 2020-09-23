using SharedCode;
using SharedCode.Models;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Ewats_App.PageV2
{
    public partial class UCProfile : UserControl
    {
        public UCProfile()
        {
            InitializeComponent();
        }

        private void UCProfile_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.White;
            Function.GlobalFunc ff = new Function.GlobalFunc();
            var profile = ff.GetProfile(General.IDUser);
            //id,username,NamaLengkap,Photo,Gender,Alamat,Email,NoHp,password
            if (profile.id != "")
            {
                if (profile.ImgLink != null)
                {
                    var request = System.Net.WebRequest.Create(ConfigurationFileStatic.PathImgWeb + "" + profile.ImgLink.Replace("//", "/"));
                    try
                    {
                        using (var response = request.GetResponse())
                        using (var stream = response.GetResponseStream())
                        {
                            Img.Image = Bitmap.FromStream(stream);
                            Img.SizeMode = PictureBoxSizeMode.StretchImage;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                txtNamaLengkap.Text = profile.NamaLengkap;
                txtUsername.Text = profile.username;
                txtAlamat.Text = profile.Alamat;
                txtContact.Text = profile.NoHp;
                txtEmail.Text = profile.Email;
                txtNIK.Text = "NIK-" + profile.id;
                txtPassword.Text = profile.password;
                var listGender = ff.GetListGender();
                foreach (var d in listGender)
                {
                    cmbGender.Items.Add(d.Text);
                }
                cmbGender.SelectedIndex = cmbGender.FindStringExact(profile.Gender);
            }

        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            Form fc = Application.OpenForms["ChangePassword"];
            if (fc != null)
            {
                fc.Show();
                fc.BringToFront();
            }
            else
            {
                PageV2.ChangePassword frm = new PageV2.ChangePassword();
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.BringToFront();
                frm.MaximizeBox = false;
                frm.MinimizeBox = false;
                frm.Show();
            }
        }
    }
}
