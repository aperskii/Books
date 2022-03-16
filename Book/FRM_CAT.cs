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

namespace Book
{
    public partial class FRM_CAT : Form
    {
        //var for sql conn
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        public FRM_CAT()
        {
            InitializeComponent();
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            if (TXT_CAT.Text != "")
            { 
            con.ConnectionString = (@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\DBBOOK.mdf;Integrated Security=True;Connect Timeout=30");
                con.Open();
                cmd.Connection = con;
            cmd.CommandText = "INSERT INTO TBCAT (CAT) VALUES (@CAT) ";
            cmd.Parameters.AddWithValue("@CAT", TXT_CAT.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                Form FRM_ADD = new FRM_DIADD();
                FRM_ADD.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Write name of categorie");
            }
        }
    }
}
