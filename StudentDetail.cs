using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//STUDENTDETAIL
namespace Student_Detail
{
    public partial class Form1 : Form
    {
        student Student = new student();
        studentEntities studentDetails = new studentEntities();
        public Form1()
        {
            InitializeComponent();

        }

        private void Bt_clear_Click(object sender, EventArgs e)
        {
            txtage.Text = txtid.Text = txtname.Text = "";//TextBoxCondition
        }

        private void Bt_insert_Click(object sender, EventArgs e)
        {
            try
            {
                student Student1 = new student();
                if (!string.IsNullOrEmpty(txtid.Text) && !string.IsNullOrEmpty(txtname.Text) && !string.IsNullOrEmpty(txtage.Text))
                {
                    var comp = Convert.ToInt32(txtid.Text);
                    int count = studentDetails.students.ToList().Where(a => a.st_id == comp).Count();
                    if (count > 0)
                    {
                        MessageBox.Show("already exists Id..try new one..");//IDShouldNotRepested
                        return;
                    }
                    else
                    {
                        Student1.st_id = Convert.ToInt32(txtid.Text);
                    }
                    Student1.st_name = txtname.Text;//GettingUserName

                    if (Rd_Btn_Male.Checked)//GenderButtonDetail
                        Student1.gender = "male";
                    else if (Rd_Btn_female.Checked)
                        Student1.gender = "female";

                    var data = Convert.ToInt32(txtage.Text);
                    if (data <= 6 || data > 100)//AgeConstrints
                    {
                        warning.Visible = true;
                        MessageBox.Show("enter valid data");
                        return;
                    }
                    else
                    {
                        Student1.age = data;
                    }
                }
                else
                {
                    MessageBox.Show("* Please fill all the fields");
                    return;
                }
                studentDetails.students.Add(Student1);//ADDDetailsToTheDatabase
                //studentDetails.students.Attach(Student);
                //studentDetails.Entry(Student).State = EntityState.Modified;
                studentDetails.SaveChanges();
                dataGridView1.DataSource = studentDetails.students.ToList().Where(a => a.st_id == Student1.st_id).ToList();
                txtage.Text = txtid.Text = txtname.Text = "";
              
            }
            catch (Exception excp)
            {

                MessageBox.Show(excp.Message, "error");
            }

        }


        private void Bt_delete_Click(object sender, EventArgs e)
        {
//DELETEButton
            try
            {
                if (!string.IsNullOrEmpty(txtid.Text))
                {
                    if (txtid.Text != null)
                    {
                        if (MessageBox.Show("confirm to delete this record", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            var comp = Convert.ToInt32(txtid.Text);
                            var list = studentDetails.students.ToList().Where(a => a.st_id == comp).First();
                            studentDetails.students.Remove(list);//DeleteParticularDetail
                            studentDetails.SaveChanges();
                            dataGridView1.DataSource = studentDetails.students.ToList().Where(a => a.st_id == list.st_id).ToList();
                            MessageBox.Show("deleted");
                        }

                    }
                    else
                    {
                        MessageBox.Show("there is no data found");
                        return;
                    }

                }
                else
                {
                    MessageBox.Show("mention Id of the record be deleted ");
                    return;
                }
            }
            catch (Exception EX)
            {
                MessageBox.Show("there is no data found");
                return;
            }
        }

        private void txtid_TextChanged(object sender, EventArgs e)
        {
//Empty
        }

        private void Bt_update_Click(object sender, EventArgs e)
        {
            try//UpdateButtonSameAsRegisterButtonButUpdateDetailsOfExstingDetail
            {
                if (!string.IsNullOrEmpty(txtid.Text))
                {
                    var comp = Convert.ToInt32(txtid.Text);
                    var list = studentDetails.students.ToList().Where(a => a.st_id == comp).FirstOrDefault();
                    if (list == null)
                    {
                        MessageBox.Show("first insert the data");
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(txtage.Text))
                        {
                            var data = Convert.ToInt32(txtage.Text);
                            if (data <= 6 || data > 100)
                            {
                                warning.Visible = true;
                                MessageBox.Show("enter valid data");
                                return;
                            }
                            else
                                list.age = Convert.ToInt32(txtage.Text);
                        }

                        if (!string.IsNullOrEmpty(txtname.Text))
                        {
                            list.st_name = txtname.Text;
                        }
                        if (Rd_Btn_Male.Checked)
                        {
                            list.gender = "male";
                        }
                        else if (Rd_Btn_female.Checked)
                        {
                            list.gender = "female";
                        }


                        studentDetails.students.Attach(list);
                        studentDetails.Entry(list).State = EntityState.Modified;
                        studentDetails.SaveChanges();
                        dataGridView1.DataSource = studentDetails.students.ToList().Where(a => a.st_id == list.st_id).ToList();
                    }
                }
                else
                {
                    MessageBox.Show("please give Id..");
                    return;
                }
            }
            catch (Exception excp)
            {

                MessageBox.Show(excp.Message, "error");
            }

        }

        private void txtid_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)//TextBoxConstraints
            {
                e.Handled = true;
            }
        }

        private void txtname_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsLetter(ch) && ch != 8 && ch != 46 && ch!=32)//TextBoxConstraints
            {
                e.Handled = true;
            }
        }

        private void Btn_dlt_all_Click(object sender, EventArgs e)
        {
            try
            {
                if (studentDetails.students == null)
                {
                    MessageBox.Show("there is no data found");
                    return;
                }

                else if(MessageBox.Show("confirm to delete this record", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    studentDetails.Database.ExecuteSqlCommand("truncate table [student]");//DEleteAllDetailOfTable
                    MessageBox.Show("table deleted");
                    return;
                }

            }
            catch (Exception excp)
            {

                MessageBox.Show(excp.Message, "error");
            }
        }

        private void btn_sh_rec_Click(object sender, EventArgs e)
        {
            try//ShowButton
            {
                if (string.IsNullOrEmpty(txtid.Text) && string.IsNullOrEmpty(txtname.Text) && string.IsNullOrEmpty(txtage.Text))
                {
                    MessageBox.Show("enter the Id first");
                }
                else if (!string.IsNullOrEmpty(txtid.Text))
                {
                    var comp = Convert.ToInt32(txtid.Text);
                    var list = studentDetails.students.ToList().Where(a => a.st_id == comp).FirstOrDefault();
                    dataGridView1.DataSource = studentDetails.students.ToList().Where(a => a.st_id == list.st_id).ToList();
                }
                else
                {
                    MessageBox.Show("no data found");
                }

            }
            catch (Exception exp)
            {
                MessageBox.Show("there is no data");
            }
        }
        private void btn_sh_all_Click(object sender, EventArgs e)
        {
            try//ShowAllDetailButton
            {
                if (studentDetails.students == null)
                {
                    MessageBox.Show("there is no data found");
                }
                else
                {
                    dataGridView1.DataSource = studentDetails.students.ToList();
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "error");
            }
        }
    }
}
        
    

        
    

