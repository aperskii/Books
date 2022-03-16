using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace Book
{
    public partial class FRM_ADD : Form
    {
        //var for sql connec
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        List<string> List = new List<string>();
        public int state;
        public FRM_ADD()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form FRM_CAT = new FRM_CAT();
            bunifuTransition1.ShowSync(FRM_CAT);
        }

        private void FRM_ADD_Load(object sender, EventArgs e)
        {
            try
            {
                con.ConnectionString = (@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\DBBOOK.mdf;Integrated Security=True;Connect Timeout=30");
                cmd.Connection = con;
                con.Open();
                cmd.CommandText = ("SELECT CAT FROM TBCAT");
                var rd = cmd.ExecuteReader();
                while(rd.Read())
                {
                    List.Add(Convert.ToString(rd[0]));
                }
                int i = 0;
                while(i<List.LongCount())
                {
                    txt_cat.Items.Add(List[i]);
                    i++;
                }
            }
            catch (Exception EX)
            {
                MessageBox.Show(EX.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            if (txt_auther.Text == "" || txt_name.Text == "" || txt_cat.Text=="" || txt_price.Text=="" )
            {
                MessageBox.Show(" Complete first informations book ");
            }
        else
            {
                if(state == 0)
                {
                    //insert 
                    //For covert image to binary
                    MemoryStream ma = new MemoryStream();
                    cover.Image.Save(ma, System.Drawing.Imaging.ImageFormat.Jpeg);
                    var _cover = ma.ToArray();
                    //Sql Command
                    con.ConnectionString = (@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\DBBOOK.mdf;Integrated Security=True;Connect Timeout=30");
                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = "INSERT INTO BOOKS (TITLE,AUTHER,PRICE,CAT,DATE,RATE,COVER) VALUES (@TITLE,@AUTHER,@PRICE,@CAT,@DATE,@RATE,@COVER) ";
                    cmd.Parameters.AddWithValue("@TITLE", txt_name.Text);
                    cmd.Parameters.AddWithValue("@AUTHER", txt_auther.Text);
                    cmd.Parameters.AddWithValue("@PRICE", txt_price.Text);
                    cmd.Parameters.AddWithValue("@CAT", txt_cat.Text);
                    cmd.Parameters.AddWithValue("@DATE", txt_date.Value);
                    cmd.Parameters.AddWithValue("@RATE", txt_rate.Value);
                    cmd.Parameters.AddWithValue("@COVER", _cover);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Form FRM_ADD = new FRM_DIADD();
                    FRM_ADD.Show();
                    this.Close();
                }
                else
                {
                    // edit
                    MemoryStream ma = new MemoryStream();
                    cover.Image.Save(ma, System.Drawing.Imaging.ImageFormat.Jpeg);
                    var _cover = ma.ToArray();
                    //Sql Command
                    con.ConnectionString = (@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\DBBOOK.mdf;Integrated Security=True;Connect Timeout=30");
                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = "UPDATE BOOKS SET TITLE=@TITLE,AUTHER=@AUTHER,PRICE=@PRICE,CAT=@CAT,DATE=@DATE,RATE=@RATE,COVER ";
                    cmd.Parameters.AddWithValue("@TITLE", txt_name.Text);
                    cmd.Parameters.AddWithValue("@AUTHER", txt_auther.Text);
                    cmd.Parameters.AddWithValue("@PRICE", txt_price.Text);
                    cmd.Parameters.AddWithValue("@CAT", txt_cat.Text);
                    cmd.Parameters.AddWithValue("@DATE", txt_date.Value);
                    cmd.Parameters.AddWithValue("@RATE", txt_rate.Value);
                    cmd.Parameters.AddWithValue("@COVER", _cover);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Form FRM_DIEDIT = new FRM_DIADD();
                    FRM_DIEDIT.Show();
                    this.Close();
                }
                
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var dia = new OpenFileDialog();
            dia.Filter = "png|*.png";
            var result = dia.ShowDialog();
            if(result==DialogResult.OK)
            {
                cover.Image = Image.FromFile(dia.FileName);
            }

        }
    }
}
